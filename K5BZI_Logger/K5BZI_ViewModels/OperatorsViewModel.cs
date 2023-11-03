using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using K5BZI_Models;
using K5BZI_Models.ViewModelModels;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;
using MessageBox = System.Windows.Forms.MessageBox;

namespace K5BZI_ViewModels
{
    public class OperatorsViewModel : IOperatorViewModel
    {
        #region Properties

        public OperatorModel OperatorModel { get; private set; }

        private readonly IOperatorService _operatorService;
        private readonly IEventService _eventService;
        private readonly ISubmitViewModel _submitViewModel;

        #endregion

        #region Constructors

        public OperatorsViewModel(
            IOperatorService operatorService,
            IEventService eventService,
            ISubmitViewModel submitViewModel)
        {
            _operatorService = operatorService;
            _eventService = eventService;
            _submitViewModel = submitViewModel;

            Initialize();
        }

        #endregion

        #region Private Methods

        private async Task Initialize()
        {
            OperatorModel = new OperatorModel
            {
                EditOperatorsAction = async (_) => await UpdateOperatorAsync(OperatorModel.CurrentEvent.ActiveOperator, false),
                EditEventOperatorAction = async (_) => await UpdateOperatorAsync(OperatorModel.CurrentEvent.ActiveOperator, true),
                CurrentOperatorAction = async (_) => await SetCurrentOperatorAsync(),
                DeleteOperatorAction = async (_) => await DeleteOperatorAsync(),
                CurrentEventOperatorAction = async (_) => await SetCurrentOperatorAsync(),
                DeleteEventOperatorAction = async (_) => await DeleteOperatorAsync(),
                AddOperatorAction = (_) => AddOperator(),
                AddClubAction = (_) => AddOperator(true),
                EditOperatorAction = (_) => EditOperators(),
                ChangeOperatorAction = (_) => EditOperators(true),
                UpdateOperatorAction = async (_) => await UpdateOperatorAsync(OperatorModel.Operator, false),
                UpdateEventOperatorAction = async (_) => await UpdateOperatorAsync(OperatorModel.Operator, true)
            };

            var operators = await _operatorService.GetOperatorsAsync();

            if (operators.Any())
                operators.ForEach(_ => OperatorModel.Operators.Add(_));
        }

        private async Task UpdateOperatorAsync(Operator operatorObj, bool isEvent)
        {
            if (OperatorModel.CurrentEvent == null) return;

            OperatorModel.Operators.Add(operatorObj);

            await _operatorService.SaveOperatorsAsync(OperatorModel.Operators.ToList());

            if (!OperatorModel.CurrentEvent.Operators.Any())
                operatorObj.IsActive = true;

            if (!OperatorModel.CurrentEvent.Operators.Any(_ => _.CallSign?.ToUpper() == operatorObj.CallSign?.ToUpper()))
                OperatorModel.CurrentEvent.Operators.Add(operatorObj);

            OperatorModel.EditOperatorIsOpen = false;
            OperatorModel.IsOpen = false;

            await _eventService.SaveEventsAsync(OperatorModel.Events);
        }

        private async Task DeleteOperatorAsync()
        {
            var operatorObj = OperatorModel.CurrentEvent.ActiveOperator;

            var deleteConfirmName = OperatorModel.ShowEventOperators ?
                String.Format("Removing {0} from the event.", operatorObj.CallSign) :
                String.Format("Deleting {0}.", operatorObj.CallSign);

            var confirmResult = MessageBox.Show(deleteConfirmName, "Are you sure?", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                if (!OperatorModel.ShowEventOperators)
                {
                    OperatorModel.Operators.Remove(operatorObj);
                }

                OperatorModel.CurrentEvent.Operators.Remove(operatorObj);

                await _eventService.SaveEventsAsync(OperatorModel.Events);
            }
        }

        private async Task SetCurrentOperatorAsync()
        {
            var operatorObj = OperatorModel.CurrentEvent.ActiveOperator;

            if (operatorObj != null && operatorObj.IsClub)
            {
                MessageBox.Show("Only individuals can be set as active operator", "You can't do that", MessageBoxButtons.OK);

                return;
            }

            await UpdateOperatorAsync(operatorObj, true);

            _submitViewModel.SubmitModel.SelectedSubmitOperator = OperatorModel.CurrentEvent.ActiveOperator;

            OperatorModel.EditOperatorIsOpen = false;
            OperatorModel.IsOpen = false;
        }

        private void EditOperators(bool eventOnly = false)
        {
            OperatorModel.ShowEventOperators = eventOnly;
            OperatorModel.ShowCloseButton = true;
            OperatorModel.IsOpen = true;
        }

        public void AddOperator(bool isClub = false)
        {
            OperatorModel.Operator.Clear();

            OperatorModel.ShowCloseButton = true;
            OperatorModel.EditOperatorIsOpen = true;

            OperatorModel.Operator.IsClub = isClub;
        }

        #endregion
    }
}
