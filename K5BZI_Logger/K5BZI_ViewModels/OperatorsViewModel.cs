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
        private readonly IEventViewModel _eventViewModel;

        #endregion

        #region Constructors

        public OperatorsViewModel(
            IOperatorService operatorService,
            IEventService eventService,
            ISubmitViewModel submitViewModel,
            IEventViewModel eventViewModel)
        {
            _operatorService = operatorService;
            _eventService = eventService;
            _submitViewModel = submitViewModel;
            _eventViewModel = eventViewModel;

            Initialize();
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            OperatorModel = new OperatorModel
            {
                Events = _eventViewModel.EventModel.Events,
                EditOperatorsAction = (_) => EditOperators(),
                EditEventOperatorAction = (_) => EditOperators(true),
                CurrentOperatorAction = async (_) => await SetCurrentOperatorAsync(),
                DeleteOperatorAction = async (_) => await DeleteOperatorAsync(),
                CurrentEventOperatorAction = async (_) => await SetCurrentOperatorAsync(),
                DeleteEventOperatorAction = async (_) => await DeleteOperatorAsync(),
                AddOperatorAction = (_) => AddOperator(),
                AddClubAction = (_) => AddOperator(true),
                EditOperatorAction = (_) => EditOperators(),
                ChangeOperatorAction = (_) => EditOperators(true),
                UpdateOperatorAction = async (_) => await UpdateOperatorAsync(OperatorModel.ViewSelectedOperator, false),
                UpdateEventOperatorAction = async (_) => await UpdateOperatorAsync(OperatorModel.ViewSelectedOperator, true)
            };

            var operators = _operatorService.GetOperators();

            if (operators != null && operators.Any())
                operators.ForEach(_ => OperatorModel.Operators.Add(_));

            _eventViewModel.EventModel.CheckOperatorsAction = (_) => CheckOperators();
        }

        private async Task CheckOperators()
        {
            if (!OperatorModel.Operators.Any())
                AddOperator();

            while (OperatorModel.EditOperatorIsOpen) { await Task.Delay(25); }
        }

        private async Task UpdateOperatorAsync(Operator operatorObj, bool isEvent)
        {
            if (OperatorModel.ActiveEvent == null) return;

            if (!OperatorModel.ActiveEvent.Operators.Any())
                operatorObj.IsActive = true;

            if (!OperatorModel.Operators.Any(_ => _.CallSign == operatorObj.CallSign))
                OperatorModel.Operators.Add(operatorObj);

            if (!OperatorModel.ActiveEvent.Operators.Any(_ => _.CallSign?.ToUpper() == operatorObj.CallSign?.ToUpper()))
                OperatorModel.ActiveEvent.Operators.Add(operatorObj);

            OperatorModel.ActiveOperator = operatorObj; //Trigger update
            OperatorModel.EditOperatorIsOpen = false;
            OperatorModel.IsOpen = false;

            await _operatorService.SaveOperatorsAsync(OperatorModel.Operators.ToList());
            await _eventService.SaveEventsAsync(OperatorModel.Events.ToList());
        }

        private async Task DeleteOperatorAsync()
        {
            var deleteConfirmName = OperatorModel.ShowEventOperators ?
                String.Format("Removing {0} from the event.", OperatorModel.ViewSelectedOperator.CallSign) :
                String.Format("Deleting {0}.", OperatorModel.ViewSelectedOperator.CallSign);

            var confirmResult = MessageBox.Show(deleteConfirmName, "Are you sure?", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                if (!OperatorModel.ShowEventOperators)
                {
                    OperatorModel.Operators.Remove(OperatorModel.ViewSelectedOperator);
                }

                OperatorModel.ActiveEvent.Operators.Remove(OperatorModel.ViewSelectedOperator);

                await _eventService.SaveEventsAsync(OperatorModel.Events.ToList());
            }
        }

        private async Task SetCurrentOperatorAsync()
        {
            var operatorObj = OperatorModel.ActiveOperator;

            if (operatorObj != null && operatorObj.IsClub)
            {
                MessageBox.Show("Only individuals can be set as active operator", "You can't do that", MessageBoxButtons.OK);

                return;
            }

            await UpdateOperatorAsync(operatorObj, true);

            _submitViewModel.SubmitModel.SelectedSubmitOperator = OperatorModel.ActiveOperator;

            OperatorModel.ActiveOperator = operatorObj; //Trigger update

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
            OperatorModel.ViewSelectedOperators.Clear();

            OperatorModel.ShowCloseButton = true;
            OperatorModel.EditOperatorIsOpen = true;

            OperatorModel.ViewSelectedOperator.IsClub = isClub;
        }

        #endregion
    }
}
