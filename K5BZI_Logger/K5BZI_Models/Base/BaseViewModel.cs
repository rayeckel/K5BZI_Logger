using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using PropertyChanged;

namespace K5BZI_Models.Base
{
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : IBaseViewModel
    {
        public BaseViewModel()
        {
            IsOpen = false;
            ShowCloseButton = false;
        }

        public bool IsOpen { get; set; }

        public bool ShowCloseButton { get; set; }


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