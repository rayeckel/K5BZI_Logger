using K5BZI_Models;
using K5BZI_Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace K5BZI_Services
{
    public class FileStoreService : IFileStoreService
    {
        private const string loggerDirectoryName = "K5BZI_Logger";

        public List<LogListing> GetLogListing()
        {
            return new List<LogListing>();
        }

        public async void WriteToFile(ICollection<LogEntry> LogEntries, string logFileName)
        {
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), loggerDirectoryName);
            var fileName = Path.Combine(filePath, String.Concat(logFileName, ".json"));

            Directory.CreateDirectory(Path.GetDirectoryName(fileName));

            var serializer = new JsonSerializer();

            using (var sw = new StreamWriter(File.Open(fileName, FileMode.OpenOrCreate)))
            using (var writer = new JsonTextWriter(sw))
            {
                await Task.Run(() => serializer.Serialize(writer, LogEntries));
            }
        }
    }
}
