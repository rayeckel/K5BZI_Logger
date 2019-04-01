using K5BZI_Models;
using K5BZI_Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace K5BZI_Services
{
    internal class FileStoreService : IFileStoreService
    {
        private string _loggerDirectoryName = "K5BZI_Logger";
        private string _jsonExtension = ".json";

        #region Public Methods

        public FileInfo[] GetLogListing()
        {
            var filePath = CreateFilePath(String.Empty);

            return new DirectoryInfo(filePath).GetFiles();
        }

        public void OpenLogDirectory()
        {
            Process.Start(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), _loggerDirectoryName));
        }

        public List<T> ReadLog<T>(string logFileName, bool isLogFile = true)
            where T : class
        {
            var fileName = CreateFilePath(logFileName, isLogFile);
            var serializer = new JsonSerializer();

            using (var sr = new StreamReader(File.Open(fileName, FileMode.OpenOrCreate)))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                return serializer.Deserialize<List<T>>(jsonTextReader);
            }
        }

        public async void WriteToFile<T>(ICollection<T> LogEntries, string logFileName, bool isLogFile = true)
            where T : class
        {
            var fileName = CreateFilePath(logFileName, isLogFile);
            var serializer = new JsonSerializer();

            using (var sw = new StreamWriter(File.Open(fileName, FileMode.OpenOrCreate)))
            using (var writer = new JsonTextWriter(sw))
            {
                await Task.Run(() => serializer.Serialize(writer, LogEntries));
            }
        }

        #endregion

        #region Private Methods

        private string CreateFilePath(string logFileName, bool isLogFile = true)
        {
            var filePath = String.Empty;

            if (!isLogFile)
                filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _loggerDirectoryName);
            else
                filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), _loggerDirectoryName);

            Directory.CreateDirectory(filePath);

            if (String.IsNullOrEmpty(logFileName))
                return filePath;

            var fileName = Path.Combine(filePath, String.Concat(logFileName, _jsonExtension));

            Directory.CreateDirectory(Path.GetDirectoryName(fileName));

            return fileName;
        }

        #endregion
    }
}
