using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using K5BZI_Models;
using K5BZI_Models.ViewModelModels;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;

namespace K5BZI_ViewModels
{
    public class OperatorsViewModel : IOperatorsViewModel
    {
        #region Properties

        public OperatorModel OperatorModel { get; private set; }
        public EditOperatorModel EditOperator { get; private set; }
        private readonly IOperatorService _operatorService;
        private readonly IEventService _eventService;

        #endregion

        #region Constructors

        public OperatorsViewModel(
            IOperatorService operatorService,
            IEventService eventService)
        {
            _operatorService = operatorService;
            _eventService = eventService;

            Initialize();
        }

        #endregion

        #region Public Methods

        public async void PopulateEventOperators(Event eventModel)
        {
            if (!OperatorModel.Operators.Any())
            {
                OperatorModel.IsOpen = true;
                AddOperator();
            }

            while (OperatorModel.IsOpen) { await Task.Delay(25); }

            OperatorModel.CurrentEvent = eventModel;
            OperatorModel.EventOperators.Clear();

            var currentEventOperators = OperatorModel.CurrentEvent.Operators
                .Select(x => x.CallSign);

            var eventOperators = OperatorModel.Operators
                .Where(_ => currentEventOperators.Contains(_.CallSign))
                .ToList();

            if (eventOperators.Any())
                eventOperators.ForEach(_ => OperatorModel.EventOperators.Add(_));

            UpdateOperator(eventModel.ActiveOperator, true);
        }

        public void UpdateOperator(Operator operatorObj, bool isEvent)
        {
            if (operatorObj == null) return;

            var newOperator = _operatorService.UpdateOperator(operatorObj);

            if (!OperatorModel.EventOperators.Any(_ => _.CallSign?.ToUpper() == newOperator.CallSign?.ToUpper()))
            {
                OperatorModel.EventOperators.Add(newOperator);
            }

            if (!OperatorModel.Operators.Any(_ => _.CallSign?.ToUpper() == newOperator.CallSign?.ToUpper()))
            {
                OperatorModel.Operators.Add(newOperator);
            }

            //The first time the app is run 'currentEvent' will still be null
            if (OperatorModel.CurrentEvent == null)
                OperatorModel.CurrentEvent = _eventService.GetAllEvents().First();

            if (!OperatorModel.CurrentEvent.Operators.Any(_ => _.CallSign?.ToUpper() == newOperator.CallSign?.ToUpper()))
                OperatorModel.CurrentEvent.Operators.Add(operatorObj);

            OperatorModel.CurrentOperator = OperatorModel.CurrentEvent.ActiveOperator = operatorObj;

            EditOperator.IsOpen = false;
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
                EditOperatorAction = (_) => UpdateOperator(OperatorModel.SelectedEventOperator, false),
                EditEventOperatorAction = (_) => UpdateOperator(OperatorModel.SelectedEventOperator, true),
                CurrentOperatorAction = (_) => SetCurrentOperator(OperatorModel.SelectedEventOperator),
                DeleteOperatorAction = (_) => DeleteOperator(OperatorModel.SelectedEventOperator),
                CurrentEventOperatorAction = (_) => SetCurrentOperator(OperatorModel.SelectedEventOperator),
                DeleteEventOperatorAction = (_) => DeleteOperator(OperatorModel.SelectedEventOperator),
                AddOperatorToEventAction = (_) => AddOperatorToEvent(),
                AddClubToEventAction = (_) => AddClubToEvent(),
                AddOperatorAction = (_) => AddOperator(),
                AddClubAction = (_) => AddClub(),
                EditOperatorsAction = (_) => EditOperators(),
                ChangeOperatorAction = (_) => ChangeOperator()
            };

            EditOperator = new EditOperatorModel
            {
                Operator = new Operator(),
                UpdateOperatorAction = (_) => UpdateOperator(EditOperator.Operator, false),
                UpdateEventOperatorAction = (_) => UpdateOperator(EditOperator.Operator, true)
            };

            var operators = _operatorService.GetFullOperatorListing();

            if (operators.Any())
            {
                operators.ForEach(_ => OperatorModel.Operators.Add(_));

                if (OperatorModel.CurrentOperator == null) OperatorModel.CurrentOperator = operators.First();
            }
        }

        private void ChangeOperator()
        {
            EditOperators(true);
        }

        private void SetCurrentOperator(Operator operatorObj)
        {
            if (operatorObj != null && operatorObj.IsClub)
            {
                MessageBox.Show("Only individuals can be set as active operator", "You can't do that", MessageBoxButtons.OK);

                return;
            }

            UpdateOperator(operatorObj, true);

            EditOperator.IsOpen = false;
            OperatorModel.IsOpen = false;
        }

        private void DeleteOperator(Operator operatorObj)
        {
            var deleteConfirmName = OperatorModel.ShowEventOperators ?
                String.Format("Removing {0} from the event.", operatorObj.CallSign) :
                String.Format("Deleting {0}.", operatorObj.CallSign);

            var confirmResult = MessageBox.Show(deleteConfirmName, "Are you sure?", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                OperatorModel.EventOperators.Remove(operatorObj);

                if (!OperatorModel.ShowEventOperators)
                {
                    _operatorService.DeleteOperator(operatorObj);
                    OperatorModel.Operators.Remove(operatorObj);
                }

                _eventService.UpdateEvent(OperatorModel.CurrentEvent, OperatorModel.EventOperators.ToList());
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
