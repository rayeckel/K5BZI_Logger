using K5BZI_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K5BZI_Services.Interfaces
{
    public interface IExportService
    {
        void ExportLog(Event eventLog, ICollection<LogEntry> logEntries, LogType logType);
    }
}
