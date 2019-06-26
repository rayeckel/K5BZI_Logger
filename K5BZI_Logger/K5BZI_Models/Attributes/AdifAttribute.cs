using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K5BZI_Models.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class AdifAttribute : Attribute
    {
        public AdifAttribute(string fieldName)
        {
            FieldName = fieldName;
        }

        public string FieldName { get; set; }
    }
}
