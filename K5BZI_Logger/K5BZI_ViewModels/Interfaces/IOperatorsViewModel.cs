using K5BZI_Models;
using K5BZI_Models.ViewModelModels;

namespace K5BZI_ViewModels.Interfaces
{
    public interface IOperatorsViewModel
    {
        OperatorsModel Model { get; }

        void PopulateOperators(Event eventModel);
    }
}
