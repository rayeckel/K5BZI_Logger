﻿using K5BZI_Models.Extensions;
using K5BZI_Models.Main;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;
using PropertyChanged;
using System;

namespace K5BZI_ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainLoggerViewModel : IMainLoggerViewModel
    {
        public MainModel Model { get; private set; }

        private readonly IFileStoreService _fileStoreService;

        public MainLoggerViewModel(IFileStoreService fileStoreService)
        {
            _fileStoreService = fileStoreService;

            Model = new MainModel();
            Model.CreateNewEntryAction = () => CreateMockLogEntry();
            Model.LogItAction = () => SaveLogEntry();

            CreateMockLogEntry();
        }

        private void SaveLogEntry()
        {
            Model.LogEntries.Add(Model.LogEntry.Clone());

            _fileStoreService.WriteToFile(Model.LogEntries);

            CreateNewLogEntry();
        }

        private void CreateMockLogEntry()
        {
            CreateNewLogEntry();
            Model.LogEntry.CallSign = "KC5IHO";
            Model.LogEntry.ContactTime = DateTime.Now;
            Model.LogEntry.Event.EventName = "Parks On The Air";
            Model.LogEntry.Signal.Band = "40m";
            Model.LogEntry.Signal.Frequency = 7.225;
            Model.LogEntry.SignalReport.Sent = 599;
            Model.LogEntry.SignalReport.Received = 479;
        }

        private void CreateNewLogEntry()
        {
            Model.LogEntry.ClearProperties();
        }
    }
}
