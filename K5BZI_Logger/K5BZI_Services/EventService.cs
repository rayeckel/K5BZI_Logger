using K5BZI_Models;
using K5BZI_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace K5BZI_Services
{
    public class EventService : IEventService
    {
        #region Properties

        private readonly IFileStoreService _fileStoreService;
        private List<Event> _eventList;
        private string _eventLogFileName = "Events";

        #endregion

        #region Constructors

        public EventService(IFileStoreService fileStoreService)
        {
            _fileStoreService = fileStoreService;
            _eventList = new List<Event>();
        }

        #endregion

        #region Public Methods

        public List<Event> GetAllEvents()
        {
            _eventList.Clear();

            var results = _fileStoreService.ReadLog<Event>(_eventLogFileName, false);

            if (results != null)
                _eventList.AddRange(results);

            return _eventList;
        }

        public Event CreateNewEvent(string eventName)
        {
            var newEventName = eventName.Replace(" ", "_");

            return UpdateEvent(new Event
            {
                Id = 0,
                EventName = eventName,
                LogFileName = String.Format("{0}_{1}", newEventName, DateTime.UtcNow.ToString("yyyy'-'MM'-'dd")),
                CreatedDate = DateTime.Now
            });
        }

        public Event UpdateEvent(Event editEvent)
        {
            var existing = _eventList.FirstOrDefault(_ => _.Id == editEvent.Id);

            if (editEvent.Id <= 0 || existing == null)
            {
                editEvent.Id = 1;

                if (_eventList.Any())
                    editEvent.Id = _eventList.Select(_ => _.Id).Max() + 1;

                _eventList.Add(editEvent);
            }
            else
            {
                existing.ARRL_Sect = editEvent.ARRL_Sect;
                existing.Class = editEvent.Class;
                existing.Club = editEvent.Club;
                existing.Comments = editEvent.Comments;
                existing.CqZone = editEvent.CqZone;
                existing.DXCC = editEvent.DXCC;
                existing.EventName = editEvent.EventName;
                existing.IsActive = editEvent.IsActive;
                existing.ItuZone = editEvent.ItuZone;
                existing.LogFileName = editEvent.LogFileName;
                existing.Overlay = editEvent.Overlay;
                existing.Score = editEvent.Score;
                existing.State = editEvent.State;
                existing.TransmitterCount = editEvent.TransmitterCount;
            }

            _fileStoreService.WriteToFile(_eventList, _eventLogFileName, false);

            return editEvent;
        }

        #endregion
    }
}
