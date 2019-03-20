using K5BZI_Models.Base;
using System;

namespace K5BZI_Models
{
    public class LogListing : BaseModel
    {
        public string FileName { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
