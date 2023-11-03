using System.Collections.Generic;
using System.Threading.Tasks;
using K5BZI_Models;
using K5BZI_Services.Interfaces;

namespace K5BZI_Services.Services
{
    public class LogService : ILogService
    {
        #region Properties

        private readonly IFileStoreService _fileStoreService;

        #endregion

        #region Constructors

        public LogService(IFileStoreService fileStoreService)
        {
            _fileStoreService = fileStoreService;
        }

        #endregion

        #region Public Methods

        public List<LogEntry> ReadLog(string logFileName)
        {
            return _fileStoreService.ReadLog<LogEntry>(logFileName);
        }

        public async Task SaveLogAsync(List<LogEntry> logEntries, string logFileName)
        {
            await _fileStoreService.WriteToFileAsync(logEntries, logFileName);
        }

        #endregion
    }
}
