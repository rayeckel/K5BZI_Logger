using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K5BZI_Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AdifAttribute : Attribute
    {
        public AdifAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; set; }
    }
}
