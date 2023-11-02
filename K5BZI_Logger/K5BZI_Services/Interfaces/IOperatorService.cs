using System.Collections.Generic;
using System.Threading.Tasks;
using K5BZI_Models;

namespace K5BZI_Services.Interfaces
{
    public interface IOperatorService
    {
        Task SaveOperators(List<Operator> operators);

        Task DeleteOperatorAsync(Operator newOperator);

        List<Operator> GetFullOperatorListing();
    }
}
