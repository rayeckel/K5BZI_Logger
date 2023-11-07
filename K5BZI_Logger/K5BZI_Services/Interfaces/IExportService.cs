using System.Collections.Generic;
using K5BZI_Models;
using K5BZI_Models.Enums;

namespace K5BZI_Services.Interfaces
{
    public interface IExportService
    {
        void ExportLog(
            Event eventLog,
            IEnumerable<LogEntry> logEntries,
            LogType logType);
    }
}
