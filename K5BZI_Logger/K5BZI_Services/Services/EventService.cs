using System.Collections.Generic;
using System.Threading.Tasks;
using K5BZI_Models;
using K5BZI_Services.Interfaces;

namespace K5BZI_Services.Services
{
    public class EventService : IEventService
    {
        #region Properties

        private readonly IFileStoreService _fileStoreService;
        private string _eventLogFileName = "Events";

        #endregion

        #region Constructors

        public EventService(IFileStoreService fileStoreService)
        {
            _fileStoreService = fileStoreService;
        }

        #endregion

        #region Public Methods

        public void OpenEventDirectory()
        {
            _fileStoreService.OpenEventDirectory();
        }

        public List<Event> GetEvents()
        {
            return _fileStoreService.ReadLog<Event>(_eventLogFileName, false);
        }

        public async Task SaveEventsAsync(List<Event> eventList)
        {
            await _fileStoreService.WriteToFileAsync(eventList, _eventLogFileName, false);
        }

        #endregion
    }
}
