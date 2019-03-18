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
        public async void WriteToFile(ICollection<LogEntry> LogEntries)
        {
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "K5BZI_Logger");
            var fileName = Path.Combine(filePath, "K5BZI_Log.json");

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
