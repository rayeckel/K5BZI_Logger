using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace K5BZI_Models.Base
{
    public class BaseModel : INotifyPropertyChanged
    {
        public void Clear()
        {
            var type = this.GetType();
            var properties = type.GetProperties();

            for (int i = 0; i < properties.Length; ++i)
            {
                try
                {
                    properties[i].SetValue(this, null);
                }
                catch (Exception ex)
                {
                    ex.GetType();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void FirePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
                throw new ArgumentNullException("propertyName");
            try
            {
                this.OnPropertyChanged(propertyName);
            }
            catch (Exception exception)
            {
                Trace.TraceError("{0}.OnPropertyChanged threw {1}: {2}", this.GetType().FullName, exception.GetType().FullName, exception);
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
