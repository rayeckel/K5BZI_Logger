using K5BZI_Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace K5BZI_Services.Interfaces
{
    public interface IFileStoreService
    {
        FileInfo[] GetLogListing();

        List<T> ReadLog<T>(string logFileName, bool isLogFile = true)
            where T : class;

        void WriteToFile<T>(ICollection<T> LogEntries, string logFileName, bool isLogFile = true)
            where T : class;
    }
}
