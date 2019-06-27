using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K5BZI_Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CabrilloAttribute : Attribute
    {
        public CabrilloAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; set; }
    }
}
