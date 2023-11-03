using System.Collections.Generic;
using System.Threading.Tasks;
using K5BZI_Models;
using K5BZI_Services.Interfaces;

namespace K5BZI_Services.Services
{
    public class OperatorService : IOperatorService
    {
        #region Properties

        private readonly IFileStoreService _fileStoreService;
        private const string _operatorsFileName = "Operators";

        #endregion

        #region Constructors

        public OperatorService(IFileStoreService fileStoreService)
        {
            _fileStoreService = fileStoreService;
        }

        #endregion

        #region Public Methods

        public async Task SaveOperatorsAsync(List<Operator> operators)
        {
            await _fileStoreService.WriteToFileAsync(operators, _operatorsFileName, false);
        }

        public async Task<List<Operator>> GetOperatorsAsync()
        {
            return await _fileStoreService.ReadLogAsync<Operator>(_operatorsFileName, false);
        }

        #endregion
    }
}
