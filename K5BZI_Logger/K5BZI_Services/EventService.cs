using K5BZI_Models;
using K5BZI_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace K5BZI_Services
{
    public class EventService : IEventService
    {
        private readonly IFileStoreService _fileStoreService;
        private List<Event> _eventList;
        private string _eventLogFileName = "Events";

        public EventService(IFileStoreService fileStoreService)
        {
            _fileStoreService = fileStoreService;
            _eventList = new List<Event>();
        }

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
            var eventId = 1;

            if (_eventList.Any())
                eventId = _eventList.Select(_ => _.Id).Max() + 1;

            var newEvent = new Event
            {
                Id = eventId,
                EventName = eventName,
                LogFileName = String.Format("{0}_{1}", newEventName, DateTime.UtcNow.ToString("yyyy'-'MM'-'dd")),
                CreatedDate = DateTime.Now
            };

            _eventList.Add(newEvent);

            _fileStoreService.WriteToFile(_eventList, _eventLogFileName, false);

            return newEvent;
        }
    }
}
