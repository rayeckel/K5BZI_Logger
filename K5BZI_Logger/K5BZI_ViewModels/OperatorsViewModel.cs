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
        public EditOperatorModel EditOperator { get; private set; }

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

        #region Public Methods
        public async Task UpdateOperatorAsync(Operator operatorObj, bool isEvent)
        {
            if (operatorObj == null) return;

            var existingOperator = OperatorModel.Operators
                .FirstOrDefault(_ => _.CallSign?.ToUpper() == operatorObj.CallSign?.ToUpper());

            if (existingOperator == null)
                OperatorModel.Operators.Add(operatorObj);
            else
            {
                existingOperator.CallSign = operatorObj.CallSign;
                existingOperator.City = operatorObj.City;
                existingOperator.Country = operatorObj.Country;
                existingOperator.FirstName = operatorObj.FirstName;
                existingOperator.LastName = operatorObj.LastName;
                existingOperator.State = operatorObj.State;
                existingOperator.ZipCode = operatorObj.ZipCode;
                existingOperator.IsClub = operatorObj.IsClub;
                existingOperator.ClubCall = operatorObj.ClubCall;
                existingOperator.ClubName = operatorObj.ClubName;
                existingOperator.IsActive = operatorObj.IsActive;
            }

            await _operatorService.SaveOperators(OperatorModel.Operators.ToList());

            //The first time the app is run 'currentEvent' will still be null
            if (OperatorModel.CurrentEvent == null)
                OperatorModel.CurrentEvent = _eventService.GetAllEvents().First();

            if (!OperatorModel.CurrentEvent.Operators.Any(_ => _.CallSign?.ToUpper() == operatorObj.CallSign?.ToUpper()))
                OperatorModel.CurrentEvent.Operators.Add(operatorObj);

            OperatorModel.IsOpen = false;

            _eventService.UpdateEvent(OperatorModel.CurrentEvent, OperatorModel.CurrentEvent.Operators.ToList());
        }

        public void AddOperator()
        {
            EditOperator.Operator.Clear();

            EditOperator.ShowCloseButton = true;
            EditOperator.IsOpen = true;

            EditOperator.Operator.IsClub = false;
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            OperatorModel = new OperatorModel
            {
                EditOperatorsAction = async (_) => await UpdateOperatorAsync(OperatorModel.CurrentEvent.ActiveOperator, false),
                EditEventOperatorAction = async (_) => await UpdateOperatorAsync(OperatorModel.CurrentEvent.ActiveOperator, true),
                CurrentOperatorAction = async (_) => await SetCurrentOperatorAsync(),
                DeleteOperatorAction = async (_) => await DeleteOperatorAsync(),
                CurrentEventOperatorAction = async (_) => await SetCurrentOperatorAsync(),
                DeleteEventOperatorAction = async (_) => await DeleteOperatorAsync(),
                AddOperatorToEventAction = (_) => AddOperatorToEvent(),
                AddClubToEventAction = (_) => AddClubToEvent(),
                AddOperatorAction = (_) => AddOperator(),
                AddClubAction = (_) => AddClub(),
                EditOperatorAction = (_) => EditOperators(),
                ChangeOperatorAction = (_) => EditOperators(true)
            };

            EditOperator = new EditOperatorModel
            {
                Operator = new Operator(),
                UpdateOperatorAction = async (_) => await UpdateOperatorAsync(EditOperator.Operator, false),
                UpdateEventOperatorAction = async (_) => await UpdateOperatorAsync(EditOperator.Operator, true)
            };

            var operators = _operatorService.GetFullOperatorListing();

            if (operators.Any())
                operators.ForEach(_ => OperatorModel.Operators.Add(_));
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

            EditOperator.IsOpen = false;
            OperatorModel.IsOpen = false;
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
                OperatorModel.CurrentEvent.Operators.Remove(operatorObj);

                if (!OperatorModel.ShowEventOperators)
                {
                    await _operatorService.DeleteOperatorAsync(operatorObj);
                    OperatorModel.Operators.Remove(operatorObj);
                }

                _eventService.UpdateEvent(OperatorModel.CurrentEvent, OperatorModel.CurrentEvent.Operators.ToList());
            }
        }

        private void AddOperatorToEvent()
        {
            AddOperator();
        }

        private void AddClubToEvent()
        {
            AddClub();
        }

        private void EditOperators(bool eventOnly = false)
        {
            OperatorModel.ShowEventOperators = eventOnly;
            OperatorModel.ShowCloseButton = true;
            OperatorModel.IsOpen = true;
        }

        private void AddClub()
        {
            EditOperator.Operator.Clear();

            EditOperator.ShowCloseButton = true;
            EditOperator.IsOpen = true;

            EditOperator.Operator.IsClub = true;
        }

        #endregion
    }
}
