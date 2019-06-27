using K5BZI_Models;
using K5BZI_Models.Enums;
using System.Collections.Generic;

namespace K5BZI_Services.Interfaces
{
    public interface IExportService
    {
        void ExportLog(Event eventLog, ICollection<LogEntry> logEntries, LogType logType);
    }
}
