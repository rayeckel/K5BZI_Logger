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

        public async Task<bool> WriteToFile<T>(ICollection<T> LogEntries, string logFileName, bool isLogFile = true)
            where T : class
        {
            var fileName = CreateFilePath(logFileName, isLogFile);
            var serializer = new JsonSerializer();

            using (var sw = new StreamWriter(File.Open(fileName, FileMode.OpenOrCreate)))
            using (var writer = new JsonTextWriter(sw))
            {
                await Task.Run(() => serializer.Serialize(writer, LogEntries));
            }

            return true;
        }

        public async Task<bool> WriteToFile(string fileData, string logFileName, string extension = "")
        {
            extension = String.IsNullOrEmpty(extension) ? FileExtensions.Json : extension;

            var fileName = CreateFilePath(logFileName, true, extension);

            using (var sw = new StreamWriter(File.Open(fileName, FileMode.Create)))
            using (var writer = new JsonTextWriter(sw))
            {
                await sw.WriteAsync(fileData);
            }

            return true;
        }

        #endregion

        #region Private Methods

        private string CreateFilePath(string logFileName, bool isLogFile = true, string extension = "")
        {
            var filePath = String.Empty;

            if (!isLogFile)
                filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _loggerDirectoryName);
            else
                filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), _loggerDirectoryName);

            Directory.CreateDirectory(filePath);

            if (String.IsNullOrEmpty(logFileName))
                return filePath;

            extension = String.IsNullOrEmpty(extension) ? FileExtensions.Json : extension;

            var fileName = Path.Combine(filePath, String.Concat(logFileName, extension));

            Directory.CreateDirectory(Path.GetDirectoryName(fileName));

            return fileName;
        }

        #endregion
    }
}
