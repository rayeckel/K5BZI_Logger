using K5BZI_Models;
using K5BZI_Models.Extensions;
using K5BZI_Services.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace K5BZI_Services
{
    public class LogListingService : ILogListingService
    {
        private readonly IEventService _eventService;
        private readonly IFileStoreService _fileStoreService;
        private List<LogEntry> _logEntries;

        public LogListingService(
            IEventService eventService,
            IFileStoreService fileStoreService)
        {
            _eventService = eventService;
            _fileStoreService = fileStoreService;

            _logEntries = new List<LogEntry>();
        }

        public List<LogEntry> ReadLog(string logFileName)
        {
            _logEntries.Clear();

            var results = _fileStoreService.ReadLog<LogEntry>(logFileName);

            if (results != null)
                _logEntries.AddRange(results);

            return _logEntries;
        }

        public LogListing CreateNewLogListing(FileInfo fileInfo)
        {
            var fileName = System.IO.Path.ChangeExtension(fileInfo.Name, null);

            return new LogListing
            {
                FileName = fileName,
                CreatedDate = fileInfo.CreationTimeUtc
            };
        }

        public List<LogListing> GetLogListing()
        {
            var files = _fileStoreService.GetLogListing();
            var listings = new List<LogListing>();

            foreach (var file in files)
            {
                listings.Add(CreateNewLogListing(file));
            }

            return listings;
        }

        public void SaveLogEntry(LogEntry logEntry, Event eventEntry)
        {
            _logEntries.Add(logEntry.Clone());

            _fileStoreService.WriteToFile(_logEntries, eventEntry.LogFileName);
        }
    }
}
