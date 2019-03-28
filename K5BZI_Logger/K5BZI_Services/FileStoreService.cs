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
        #region Properties

        private const string _loggerDirectoryName = "K5BZI_Logger";
        private const string _jsonExtension = ".json";
        private readonly string _filePath;
        private readonly ILogListingService _logListingService;

        #endregion

        #region Constructors

        public FileStoreService(ILogListingService logListingService)
        {
            _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), _loggerDirectoryName);
            _logListingService = logListingService;
        }

        #endregion

        #region Public Methods

        public List<LogListing> GetLogListing()
        {
            Directory.CreateDirectory(_filePath);

            var files = new DirectoryInfo(_filePath).GetFiles();
            var listings = new List<LogListing>();

            foreach (var file in files)
            {
                listings.Add(_logListingService.CreateNewLogListing(file));
            }

            return listings;
        }

        public async Task<List<LogEntry>> ReadLog(string logFileName)
        {
            var fileName = Path.Combine(_filePath, String.Concat(logFileName, _jsonExtension));
            var serializer = new JsonSerializer();

            using (var sr = new StreamReader(fileName))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                return await Task.Run(() => serializer.Deserialize<List<LogEntry>>(jsonTextReader));
            }
        }

        public async void WriteToFile(ICollection<LogEntry> LogEntries, string logFileName)
        {
            var fileName = Path.Combine(_filePath, String.Concat(logFileName, _jsonExtension));

            Directory.CreateDirectory(Path.GetDirectoryName(fileName));

            var serializer = new JsonSerializer();

            using (var sw = new StreamWriter(File.Open(fileName, FileMode.OpenOrCreate)))
            using (var writer = new JsonTextWriter(sw))
            {
                await Task.Run(() => serializer.Serialize(writer, LogEntries));
            }
        }

        #endregion
    }
}
