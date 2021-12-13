﻿using K5BZI_Models.Attributes;
using PropertyChanged;

namespace K5BZI_Models
{
    [AddINotifyPropertyChangedInterface]
    public class SignalReport
    {
        public SignalReport()
        {
            Sent = "599";
            Received = "599";
        }

        [Adif("RST_SENT")]
        public string Sent { get; set; }

        [Adif("RST_RCVD")]
        public string Received { get; set; }
    }
}