using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace K5BZI_Services.Interfaces
{
    public interface IFileStoreService
    {
        FileInfo[] GetLogListing();

        void OpenEventDirectory();

        IEnumerable<DataRow> ReadResourceFile(string resourceFileName, bool usesHearerRow = true);

        Task<List<T>> ReadLogAsync<T>(string logFileName, bool isLogFile = true)
            where T : class;

        Task<bool> WriteToFileAsync<T>(ICollection<T> LogEntries, string logFileName, bool isLogFile = true)
            where T : class;

        Task<bool> WriteToFile(string fileData, string logFileName, string extension = "");
    }
}
