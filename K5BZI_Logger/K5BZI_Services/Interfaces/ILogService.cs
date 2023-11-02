using System.Collections.Generic;
using K5BZI_Models;

namespace K5BZI_Services.Interfaces
{
    public interface ILogService
    {
        List<LogEntry> ReadLog(string logFileName);

        void SaveLogEntry(LogEntry logEntry, string logFileName);

        void UpdateLogEntry(LogEntry logEntry, string logFileName);

        void DeleteLogEntry(LogEntry logEntry, string logFileName);
    }
}
