using K5BZI_Models;
using System.Collections.Generic;
using System.IO;

namespace K5BZI_Services.Interfaces
{
    public interface ILogListingService
    {
        LogListing CreateNewLogListing(FileInfo fileInfo);

        List<LogEntry> ReadLog(string logFileName);

        void OpenLogListing();

        void SaveLogEntry(LogEntry logEntry, Event eventEntry);

        void UpdateLogEntry(LogEntry logEntry, Event eventEntry);

        void DeleteLogEntry(LogEntry logEntry, Event eventEntry);
    }
}
