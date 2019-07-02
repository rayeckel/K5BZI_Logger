using K5BZI_Models;
using System.Collections.Generic;

namespace K5BZI_Services.Interfaces
{
    public interface IOperatorService
    {
        Operator UpdateOperator(Operator newOperator);

        List<Operator> GetFullOperatorListing();
    }
}
