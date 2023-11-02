using System.Threading.Tasks;
using K5BZI_Models;
using K5BZI_Models.ViewModelModels;

namespace K5BZI_ViewModels.Interfaces
{
    public interface IOperatorViewModel
    {
        OperatorModel OperatorModel { get; }

        EditOperatorModel EditOperator { get; }

        void AddOperator();

        Task UpdateOperatorAsync(Operator operatorObj, bool isEvent);
    }
}
