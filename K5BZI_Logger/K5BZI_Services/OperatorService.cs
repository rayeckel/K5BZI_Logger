using K5BZI_Models;
using K5BZI_Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace K5BZI_Services
{
    public class OperatorService : IOperatorService
    {
        private readonly IFileStoreService _fileStoreService;
        private List<Operator> _operators;
        private const string _operatorsFileName = "k5bziLogger_operators";

        public OperatorService(IFileStoreService fileStoreService)
        {
            _fileStoreService = fileStoreService;

            _operators = new List<Operator>();
        }

        public void CreateOperator(
            string callSign,
            string name,
            string city,
            string state,
            string zipcode,
            string country,
            string clubCall,
            bool isClub = false)
        {
            UpdateOperator(new Operator
            {
                CallSign = callSign,
                Name = name,
                City = city,
                State = state,
                ZipCode = zipcode,
                Country = country,
                ClubCall = clubCall,
                IsClub = isClub
            });
        }

        public Operator UpdateOperator(Operator editOperator)
        {
            var existingOperator = _operators.FirstOrDefault(_ => _.CallSign == editOperator.CallSign);

            if (existingOperator == null)
            {
                _operators.Add(editOperator);
                existingOperator = editOperator;
            }
            else
            {
                existingOperator.CallSign = editOperator.CallSign;
                existingOperator.City = editOperator.City;
                existingOperator.Country = editOperator.Country;
                existingOperator.Name = editOperator.Name;
                existingOperator.State = existingOperator.State;
                existingOperator.ZipCode = editOperator.ZipCode;
            }

            _fileStoreService.WriteToFile(_operators, _operatorsFileName, false);

            return existingOperator;
        }

        public List<Operator> GetFullOperatorListing()
        {
            _operators.Clear();

            var results = _fileStoreService.ReadLog<Operator>(_operatorsFileName, false);

            if (results != null)
                _operators.AddRange(results);

            return _operators;
        }
    }
}
