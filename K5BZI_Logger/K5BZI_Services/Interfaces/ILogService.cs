using System.Collections.Generic;
using K5BZI_Models;

namespace K5BZI_Services.Interfaces
{
    public interface ILogService
    {
        List<LogEntry> ReadLog(string logFileName);

        void SaveLogEntry(LogEntry logEntry, Event eventEntry);

        void UpdateLogEntry(LogEntry logEntry, Event eventEntry);

        void DeleteLogEntry(LogEntry logEntry, Event eventEntry);
    }
}
