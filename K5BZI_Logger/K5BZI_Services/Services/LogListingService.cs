using K5BZI_Models;
using K5BZI_Models.Extensions;
using K5BZI_Services.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace K5BZI_Services
{
    public class LogListingService : ILogListingService
    {
        private readonly IFileStoreService _fileStoreService;
        private List<LogEntry> _logEntries;

        public LogListingService(IFileStoreService fileStoreService)
        {
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
            var fileName = Path.ChangeExtension(fileInfo.Name, null);

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

        public void OpenLogListing()
        {
            _fileStoreService.OpenLogDirectory();
        }


        public void SaveLogEntry(LogEntry logEntry, Event eventEntry)
        {
            _logEntries.Add(logEntry.Clone());

            _fileStoreService.WriteToFile(_logEntries, eventEntry.LogFileName);
        }

        public void UpdateLogEntry(LogEntry logEntry, Event eventEntry)
        {
            var updatedLogEntry = _logEntries.First(_ => _.Id == logEntry.Id);

            updatedLogEntry.CallSign = logEntry.CallSign;
            updatedLogEntry.Signal.Frequency = logEntry.Signal.Frequency;
            updatedLogEntry.Signal.Band = logEntry.Signal.Band;
            updatedLogEntry.SignalReport.Sent = logEntry.SignalReport.Sent;
            updatedLogEntry.SignalReport.Received = logEntry.SignalReport.Received;
            updatedLogEntry.Country = logEntry.Country;
            updatedLogEntry.CQZone = logEntry.CQZone;

            updatedLogEntry.Operator = eventEntry.Operators
                .FirstOrDefault(_ => _.CallSign?.ToLowerInvariant() == logEntry.Operator.CallSign.ToLowerInvariant());

            _fileStoreService.WriteToFile(_logEntries, eventEntry.LogFileName);
        }

        public void DeleteLogEntry(LogEntry logEntry, Event eventEntry)
        {
            var existingLogEntry = _logEntries.FirstOrDefault(_ => _.CallSign == logEntry.CallSign);

            if (existingLogEntry != null)
            {
                _logEntries.Remove(existingLogEntry);

                _fileStoreService.WriteToFile(_logEntries, eventEntry.LogFileName);
            }
        }
    }
}
