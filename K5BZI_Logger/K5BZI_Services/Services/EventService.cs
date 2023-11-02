using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using K5BZI_Models;
using K5BZI_Services.Interfaces;

namespace K5BZI_Services.Services
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

        public void OpenEventList()
        {
            _fileStoreService.OpenLogDirectory();
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            _eventList.Clear();

            var results = _fileStoreService.ReadLog<Event>(_eventLogFileName, false);

            if (results != null)
                _eventList.AddRange(results);

            return _eventList;
        }

        public async Task<Event> CreateNewEventAsync(string eventName)
        {
            var newEventName = eventName.Replace(" ", "_");

            return await UpdateEventAsync(new Event
            {
                Id = Guid.NewGuid(),
                EventName = eventName,
                LogFileName = String.Format("{0}_{1}", newEventName, DateTime.UtcNow.ToString("yyyy'-'MM'-'dd")),
                CreatedDate = DateTime.Now
            }, new List<Operator>());
        }

        public async Task<Event> UpdateEventAsync(Event editEvent, List<Operator> operators)
        {
            var existing = _eventList.FirstOrDefault(_ => _.Id == editEvent.Id);

            if (editEvent.Id == Guid.Empty || existing == null)
            {
                editEvent.Id = Guid.NewGuid();

                operators.ForEach(_ => editEvent.Operators.Add(_));

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
                existing.IsDeleted = editEvent.IsDeleted;
                existing.ItuZone = editEvent.ItuZone;
                existing.LogFileName = editEvent.LogFileName;
                existing.Overlay = editEvent.Overlay;
                existing.Score = editEvent.Score;
                existing.State = editEvent.State;
                existing.TransmitterCount = editEvent.TransmitterCount;

                existing.Operators.Clear();
                operators.ForEach(_ => editEvent.Operators.Add(_));
            }

            await _fileStoreService.WriteToFileAsync(_eventList, _eventLogFileName, false);

            return editEvent;
        }

        #endregion
    }
}
