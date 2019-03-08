﻿using PropertyChanged;

namespace K5BZI_Models
{
    [AddINotifyPropertyChangedInterface]
    public class Signal
    {
        public string Band { get; set; }
        public double Frequency { get; set; }
    }
}
