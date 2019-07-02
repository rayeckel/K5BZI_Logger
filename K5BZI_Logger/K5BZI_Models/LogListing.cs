using K5BZI_Models.Base;
using PropertyChanged;
using System;

namespace K5BZI_Models
{
    public class LogListing : BaseViewModel
    {
        public string FileName { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
