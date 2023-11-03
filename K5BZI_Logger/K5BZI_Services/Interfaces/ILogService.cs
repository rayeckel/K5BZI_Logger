using System.Collections.Generic;
using System.Threading.Tasks;
using K5BZI_Models;

namespace K5BZI_Services.Interfaces
{
    public interface ILogService
    {
        List<LogEntry> ReadLog(string logFileName);

        Task SaveLogAsync(List<LogEntry> logEntries, string logFileName);
    }
}
