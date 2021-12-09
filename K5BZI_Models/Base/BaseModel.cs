using System;

namespace K5BZI_Models.Base
{
    public class BaseModel
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
                catch(Exception ex)
                {
                    ex.GetType();
                }
            }
        }
    }
}
