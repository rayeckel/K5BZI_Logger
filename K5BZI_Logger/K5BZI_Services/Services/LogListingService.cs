using System.Collections.Generic;
using System.IO;
using System.Linq;
using K5BZI_Models;
using K5BZI_Models.Extensions;
using K5BZI_Services.Interfaces;

namespace K5BZI_Services.Services
{
    public class LogListingService : ILogService
    {
        #region Properties

        private readonly IFileStoreService _fileStoreService;
        private List<LogEntry> _logEntries;

        #endregion

        #region Constructors

        public LogListingService(IFileStoreService fileStoreService)
        {
            _fileStoreService = fileStoreService;

            _logEntries = new List<LogEntry>();
        }

        #endregion

        #region Public Methods

        public List<LogEntry> ReadLog(string logFileName)
        {
            _logEntries.Clear();

            var results = _fileStoreService.ReadLog<LogEntry>(logFileName);

            if (results != null)
                _logEntries.AddRange(results);

            return _logEntries;
        }

        public List<LogListing> GetLogListing()
        {
            var files = _fileStoreService.GetLogListing();
            var listings = new List<LogListing>();

            foreach (var file in files)
            {
                listings.Add(CreateNewLogList(file));
            }

            return listings;
        }

        public void SaveLogEntry(LogEntry logEntry, string logFileName)
        {
            _logEntries.Add(logEntry.Clone());

            _fileStoreService.WriteToFileAsync(_logEntries, logFileName);
        }

        public void UpdateLogEntry(LogEntry logEntry, string logFileName)
        {
            var updatedLogEntry = _logEntries.First(_ => _.Id == logEntry.Id);

            updatedLogEntry.CallSign = logEntry.CallSign;
            updatedLogEntry.Signal.Frequency = logEntry.Signal.Frequency;
            updatedLogEntry.Signal.Band = logEntry.Signal.Band;
            updatedLogEntry.SignalReport.Sent = logEntry.SignalReport.Sent;
            updatedLogEntry.SignalReport.Received = logEntry.SignalReport.Received;
            updatedLogEntry.Country = logEntry.Country;
            updatedLogEntry.CQZone = logEntry.CQZone;
            updatedLogEntry.Operator = logEntry.Operator;

            _fileStoreService.WriteToFileAsync(_logEntries, logFileName);
        }

        public void DeleteLogEntry(LogEntry logEntry, string logFileName)
        {
            var existingLogEntry = _logEntries.FirstOrDefault(_ => _.CallSign == logEntry.CallSign);

            if (existingLogEntry != null)
            {
                _logEntries.Remove(existingLogEntry);

                _fileStoreService.WriteToFileAsync(_logEntries, logFileName);
            }
        }

        #endregion

        #region Private Methods

        private LogListing CreateNewLogList(FileInfo fileInfo)
        {
            var fileName = Path.ChangeExtension(fileInfo.Name, null);

            return new LogListing
            {
                FileName = fileName,
                CreatedDate = fileInfo.CreationTimeUtc
            };
        }

        #endregion
    }
}
