using K5BZI_Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace K5BZI_Services.Interfaces
{
    public interface IFileStoreService
    {
        List<LogListing> GetLogListing();

        Task<List<LogEntry>> ReadLog(string logFileName);

        void WriteToFile(ICollection<LogEntry> LogEntries, string logFileName);
    }
}
