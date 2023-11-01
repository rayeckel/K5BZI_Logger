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

        public OperatorModel OperatorsModel { get; private set; }
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
            if (!OperatorsModel.Operators.Any())
            {
                OperatorsModel.IsOpen = true;
                AddOperator();
            }

            while (OperatorsModel.IsOpen) { await Task.Delay(25); }

            OperatorsModel.CurrentEvent = eventModel;
            OperatorsModel.EventOperators.Clear();

            var currentEventOperators = OperatorsModel.CurrentEvent.Operators
                .Select(x => x.CallSign);

            var eventOperators = OperatorsModel.Operators
                .Where(_ => currentEventOperators.Contains(_.CallSign))
                .ToList();

            if (eventOperators.Any())
                eventOperators.ForEach(_ => OperatorsModel.EventOperators.Add(_));

            UpdateOperator(eventModel.ActiveOperator, true);
        }

        public void UpdateOperator(Operator operatorObj, bool isEvent)
        {
            if (operatorObj == null) return;

            var newOperator = _operatorService.UpdateOperator(operatorObj);

            if (!OperatorsModel.EventOperators.Any(_ => _.CallSign?.ToUpper() == newOperator.CallSign?.ToUpper()))
            {
                OperatorsModel.EventOperators.Add(newOperator);
            }

            if (!OperatorsModel.Operators.Any(_ => _.CallSign?.ToUpper() == newOperator.CallSign?.ToUpper()))
            {
                OperatorsModel.Operators.Add(newOperator);
            }

            //The first time the app is run 'currentEvent' will still be null
            if (OperatorsModel.CurrentEvent == null)
                OperatorsModel.CurrentEvent = _eventService.GetAllEvents().First();

            if (!OperatorsModel.CurrentEvent.Operators.Any(_ => _.CallSign?.ToUpper() == newOperator.CallSign?.ToUpper()))
                OperatorsModel.CurrentEvent.Operators.Add(operatorObj);

            OperatorsModel.CurrentOperator = OperatorsModel.CurrentEvent.ActiveOperator = operatorObj;

            EditOperator.IsOpen = false;
            OperatorsModel.IsOpen = false;

            _eventService.UpdateEvent(OperatorsModel.CurrentEvent, OperatorsModel.CurrentEvent.Operators.ToList());
        }

        public void AddOperator()
        {
            EditOperator.Model.Clear();

            EditOperator.ShowCloseButton = true;
            EditOperator.IsOpen = true;

            EditOperator.Model.IsClub = false;
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            OperatorsModel = new OperatorModel
            {
                EditOperatorAction = (_) => UpdateOperator(OperatorsModel.SelectedEventOperator, false),
                EditEventOperatorAction = (_) => UpdateOperator(OperatorsModel.SelectedEventOperator, true),
                CurrentOperatorAction = (_) => SetCurrentOperator(OperatorsModel.SelectedEventOperator),
                DeleteOperatorAction = (_) => DeleteOperator(OperatorsModel.SelectedEventOperator),
                CurrentEventOperatorAction = (_) => SetCurrentOperator(OperatorsModel.SelectedEventOperator),
                DeleteEventOperatorAction = (_) => DeleteOperator(OperatorsModel.SelectedEventOperator),
                AddOperatorToEventAction = (_) => AddOperatorToEvent(),
                AddClubToEventAction = (_) => AddClubToEvent(),
                AddOperatorAction = (_) => AddOperator(),
                AddClubAction = (_) => AddClub(),
                EditOperatorsAction = (_) => EditOperators(),
                ChangeOperatorAction = (_) => ChangeOperator()
            };

            EditOperator = new EditOperatorModel
            {
                Model = new Operator(),
                UpdateOperatorAction = (_) => UpdateOperator(EditOperator.Model, false),
                UpdateEventOperatorAction = (_) => UpdateOperator(EditOperator.Model, true)
            };

            var operators = _operatorService.GetFullOperatorListing();

            if (operators.Any())
            {
                operators.ForEach(_ => OperatorsModel.Operators.Add(_));

                if (OperatorsModel.CurrentOperator == null) OperatorsModel.CurrentOperator = operators.First();
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
            OperatorsModel.IsOpen = false;
        }

        private void DeleteOperator(Operator operatorObj)
        {
            var deleteConfirmName = OperatorsModel.ShowEventOperators ?
                String.Format("Removing {0} from the event.", operatorObj.CallSign) :
                String.Format("Deleting {0}.", operatorObj.CallSign);

            var confirmResult = MessageBox.Show(deleteConfirmName, "Are you sure?", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                OperatorsModel.EventOperators.Remove(operatorObj);

                if (!OperatorsModel.ShowEventOperators)
                {
                    _operatorService.DeleteOperator(operatorObj);
                    OperatorsModel.Operators.Remove(operatorObj);
                }

                _eventService.UpdateEvent(OperatorsModel.CurrentEvent, OperatorsModel.EventOperators.ToList());
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
            OperatorsModel.ShowEventOperators = eventOnly;
            OperatorsModel.ShowCloseButton = true;
            OperatorsModel.IsOpen = true;
        }

        private void AddClub()
        {
            EditOperator.Model.Clear();

            EditOperator.ShowCloseButton = true;
            EditOperator.IsOpen = true;

            EditOperator.Model.IsClub = true;
        }

        #endregion
    }
}
