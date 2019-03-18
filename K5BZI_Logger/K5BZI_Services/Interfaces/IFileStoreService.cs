using K5BZI_Models;
using System.Collections.Generic;

namespace K5BZI_Services.Interfaces
{
    public interface IFileStoreService
    {
        void WriteToFile(ICollection<LogEntry> LogEntries, string logFileName);
    }
}
