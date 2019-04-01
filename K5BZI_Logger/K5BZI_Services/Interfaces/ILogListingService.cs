using K5BZI_Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace K5BZI_Services.Interfaces
{
    public interface ILogListingService
    {
        LogListing CreateNewLogListing(FileInfo fileInfo);

        List<LogEntry> ReadLog(string logFileName);

        void OpenLogListing();

        void SaveLogEntry(LogEntry logEntry, Event eventEntry);
    }
}
