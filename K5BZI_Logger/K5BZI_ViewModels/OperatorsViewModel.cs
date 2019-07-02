using K5BZI_Models;
using K5BZI_Models.ViewModelModels;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;
using System.Linq;
using System.Windows;

namespace K5BZI_ViewModels
{
    public class OperatorsViewModel : IOperatorsViewModel
    {
        public OperatorsModel Model { get; private set; }
        private readonly IOperatorService _operatorService;

        public OperatorsViewModel(IOperatorService operatorService)
        {
            _operatorService = operatorService;

            Initialize();
        }

        public void PopulateOperators(Event eventModel)
        {
            Model.Operators.Clear();

            var operators = _operatorService.GetOperatorsByEvent(eventModel);

            if (operators.Any())
            {
                operators.ForEach(_ => Model.Operators.Add(_));

                Model.CurrentOperator = operators.First();
            }
        }

        private void Initialize()
        {
            Model = new OperatorsModel
            {
                EditOperatorsAction = () => EditOperators()
            };
        }

        private void EditOperators()
        {
            MessageBox.Show("Not Implemented.");
        }
    }
}
