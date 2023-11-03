using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using K5BZI_Models;
using K5BZI_Models.EntityModels;
using K5BZI_Services.Interfaces;

namespace K5BZI_Services.Services
{
    public class DefaultsService : IDefaultsService
    {
        #region Properties

        private readonly IFileStoreService _fileStoreService;
        private string _defaultsFileName = "Defaults";
        private Defaults _storedDefaults;

        #endregion

        #region Constructors

        public DefaultsService(IFileStoreService fileStoreService)
        {
            _fileStoreService = fileStoreService;

            GetDefaultsAsync();
        }

        #endregion

        #region Public Methods

        public async Task<Defaults> GetDefaultsAsync()
        {
            var defaults = await _fileStoreService.ReadLogAsync<Defaults>(_defaultsFileName, false);

            if (defaults == null)
            {
                UpdateDefaults(new Defaults());
            }
            else
            {
                _storedDefaults = defaults.FirstOrDefault();
            }

            return _storedDefaults;
        }

        public void UpdateDefaults(Defaults editDefaults)
        {
            if (_storedDefaults == null)
            {
                _storedDefaults = editDefaults;
            }
            else
            {
                _storedDefaults.Assisted = editDefaults.Assisted;
                _storedDefaults.Continent = editDefaults.Continent;
                _storedDefaults.Country = editDefaults.Country;
                _storedDefaults.Power = editDefaults.Power;
                _storedDefaults.QslReceived = editDefaults.QslReceived;
                _storedDefaults.QslSent = editDefaults.QslSent;
            }

            _fileStoreService.WriteToFileAsync(new List<Defaults> { _storedDefaults }, _defaultsFileName, false);
        }

        public void SetDefaults(LogEntry logEntry)
        {
            logEntry.Assisted = _storedDefaults.Assisted;
            logEntry.Continent = _storedDefaults.Continent;
            logEntry.Country = _storedDefaults.Country;
            logEntry.Power = _storedDefaults.Power;
            logEntry.QslReceived = _storedDefaults.QslReceived;
            logEntry.QslSent = _storedDefaults.QslSent;
            logEntry.Signal.Mode = _storedDefaults.Mode;
        }

        #endregion
    }
}
