using K5BZI_Models;
using K5BZI_Models.ViewModelModels;

namespace K5BZI_ViewModels.Interfaces
{
    public interface IOperatorsViewModel
    {
        OperatorModel Model { get; }

        EditOperatorModel EditOperator { get; }

        void PopulateOperators(Event eventModel);

        void UpdateOperator(Operator operatorObj, bool isEvent);
    }
}
