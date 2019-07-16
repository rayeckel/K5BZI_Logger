using K5BZI_Models;
using K5BZI_Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace K5BZI_Services
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

        public void CreateOperator(
            string callSign,
            string firstname,
            string lastname,
            string clubname,
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
                FirstName = firstname,
                LastName = lastname,
                ClubName = clubname,
                City = city,
                State = state,
                ZipCode = zipcode,
                Country = country,
                ClubCall = clubCall,
                IsClub = isClub
            });
        }

        public void DeleteOperator(Operator editOperator)
        {
            var existingOperator = _operators.FirstOrDefault(_ => _.CallSign == editOperator.CallSign);

            if (existingOperator != null)
            {
                _operators.Remove(editOperator);

                _fileStoreService.WriteToFile(_operators, _operatorsFileName, false);
            }
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
                existingOperator.FirstName = editOperator.FirstName;
                existingOperator.LastName = editOperator.LastName;
                existingOperator.State = existingOperator.State;
                existingOperator.ZipCode = editOperator.ZipCode;
                existingOperator.IsClub = editOperator.IsClub;
                existingOperator.ClubCall = editOperator.ClubCall;
                existingOperator.ClubName = editOperator.ClubName;
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

        #endregion
    }
}
