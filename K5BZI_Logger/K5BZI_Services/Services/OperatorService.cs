using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using K5BZI_Models;
using K5BZI_Services.Interfaces;

namespace K5BZI_Services.Services
{
    public class OperatorService : IOperatorService
    {
        #region Properties

        private readonly IFileStoreService _fileStoreService;
        private List<Operator> _operators;
        private const string _operatorsFileName = "Operators";

        #endregion

        #region Constructors

        public OperatorService(IFileStoreService fileStoreService)
        {
            _fileStoreService = fileStoreService;

            _operators = new List<Operator>();
        }

        #endregion

        #region Public Methods

        public async Task SaveOperators(List<Operator> operators)
        {
            await _fileStoreService.WriteToFileAsync(operators, _operatorsFileName, false);
        }

        public async Task DeleteOperatorAsync(Operator editOperator)
        {
            var existingOperator = _operators.FirstOrDefault(_ => _.CallSign == editOperator.CallSign);

            if (existingOperator != null)
            {
                _operators.Remove(editOperator);

                await _fileStoreService.WriteToFileAsync(_operators, _operatorsFileName, false);
            }
        }

        public List<Operator> GetFullOperatorListing()
        {
            _operators.Clear();

            var results = _fileStoreService.ReadLog<Operator>(_operatorsFileName, false);

            if (results != null)
                _operators.AddRange(results);

            return _operators;
        }

        #endregion
    }
}
