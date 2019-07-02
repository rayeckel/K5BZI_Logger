﻿using K5BZI_Models;
using K5BZI_Models.Extensions;
using K5BZI_Models.Main;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;
using System;
using System.Linq;
using System.Windows.Forms;

namespace K5BZI_ViewModels
{
    public class MainLoggerViewModel : IMainLoggerViewModel
    {
        #region Properties

        public MainModel Model { get; private set; }
        private readonly ILogListingService _logListingService;
        private readonly IEventService _eventService;

        #endregion

        #region Constructors

        public MainLoggerViewModel(
            ILogListingService logListingService,
            IEventService eventService)
        {
            _logListingService = logListingService;
            _eventService = eventService;

            Initialize();
        }

        #endregion

        #region Public Methods

        public void SelectEvent(Event selectedEvent)
        {
            Model.Event = selectedEvent;
            Model.LogEntries.Clear();

            var logEntries = _logListingService.ReadLog(Model.Event.LogFileName);

            if (logEntries.Any())
            {
                logEntries.ForEach(_ => Model.LogEntries.Add(_));

                Model.LogEntry.Signal.Band = logEntries.Last().Signal.Band;
                Model.LogEntry.Signal.Frequency = logEntries.Last().Signal.Frequency;
            }
        }

        public void CreateNewLog(Event newEvent)
        {
            Model.Event = newEvent;
            Model.LogEntries.Clear();
            Model.LogEntry.ClearProperties();
            Model.LogEntry.EventId = newEvent.Id;
        }

        #endregion

        #region Private Methods

        private void CheckForDuplicates()
        {
            Model.DuplicateEntries.Clear();

            if (String.IsNullOrEmpty(Model.LogEntry.CallSign))
                return;

            Model.LogEntries
                .Where(_ => _.CallSign.StartsWith(Model.LogEntry.CallSign))
                .ToList()
                .ForEach(_ => Model.DuplicateEntries.Add(_));
        }

        private void Initialize()
        {
            Model = new MainModel
            {
                CreateNewEntryAction = () => Model.LogEntry.ClearProperties(),
                ViewFileStoreAction = () => _logListingService.OpenLogListing(),
                LogItAction = () => SaveLogEntry(),
                EditLogEntryAction = () => EditLogEntry()
            };

            Model.LogEntry.CheckDuplicateEntriesAction = () => CheckForDuplicates();
        }

        private void SaveLogEntry()
        {
            if (String.IsNullOrEmpty(Model.LogEntry.CallSign))
            {
                MessageBox.Show("You must provide a valid Call Sign.", "Oops!");
                return;
            }

            _logListingService.SaveLogEntry(Model.LogEntry, Model.Event);

            Model.LogEntries.Add(Model.LogEntry.Clone());
            Model.LogEntry.ClearProperties();
        }

        private void EditLogEntry()
        {
            _logListingService.UpdateLogEntry(Model.SelectedEntry, Model.Event);
        }

        #endregion
    }
}
