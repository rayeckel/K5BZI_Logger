using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ExcelDataReader;
using K5BZI_Models;
using K5BZI_Services.Interfaces;
using Newtonsoft.Json;

namespace K5BZI_Services.Services
{
    public class FileStoreService : IFileStoreService
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
            var directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), _loggerDirectoryName);

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            Process.Start(directoryPath);
        }

        public IEnumerable<DataRow> ReadResourceFile(string resourceFileName, bool usesHeaderRow = true)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = assembly.GetName().Name;
            var resourceName = String.Format("{0}.Resources.{1}", assemblyName, resourceFileName);

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var excelReader = ExcelReaderFactory.CreateReader(stream))
            {
                var dataSet = excelReader.AsDataSet(
                    new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable =
                            _ => new ExcelDataTableConfiguration { UseHeaderRow = usesHeaderRow }
                    });

                var dataTable = dataSet.Tables[0];

                return (from DataRow row in dataTable.Rows select row);
            }
        }

        public List<T> ReadLog<T>(string logFileName, bool isLogFile = true)
            where T : class
        {
            var fileName = CreateFilePath(logFileName, isLogFile);
            var serializer = new JsonSerializer();

            try
            {
                using (var sr = new StreamReader(File.Open(fileName, FileMode.OpenOrCreate)))
                using (var jsonTextReader = new JsonTextReader(sr))
                {
                    return serializer.Deserialize<List<T>>(jsonTextReader);
                }
            }
            catch (Exception ex)
            {
                ex.GetType();

                return new List<T>();
            }
        }

        public async Task<bool> WriteToFile<T>(ICollection<T> LogEntries, string logFileName, bool isLogFile = true)
            where T : class
        {
            var fileName = CreateFilePath(logFileName, isLogFile);

            using (var sw = new StreamWriter(File.Open(fileName, FileMode.OpenOrCreate)))
            using (var writer = new JsonTextWriter(sw))
            {
                new JsonSerializer().Serialize(writer, LogEntries);
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
