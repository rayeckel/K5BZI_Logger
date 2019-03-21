﻿using K5BZI_Models;
using K5BZI_Models.Main;

namespace K5BZI_ViewModels.Interfaces
{
    public interface IMainLoggerViewModel
    {
        MainModel Model { get; }

        void SelectEvent(LogListing selectedLog);

        void CreateNewLog(Event newEvent);
    }
}
