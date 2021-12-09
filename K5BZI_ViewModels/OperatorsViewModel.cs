using System;
using System.Linq;
using System.Windows.Forms;
using CommonServiceLocator;
using K5BZI_Models;
using K5BZI_Models.ViewModelModels;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;

namespace K5BZI_ViewModels
{
    public class OperatorsViewModel : IOperatorsViewModel
    {
        #region Properties

        public OperatorModel Model { get; private set; }
        public EditOperatorModel EditOperator { get; private set; }
        private readonly IOperatorService _operatorService;
        private readonly IEventService _eventService;
        private Event currentEvent;
        private bool _addToEvent;

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

        public void PopulateOperators(Event eventModel)
        {
            currentEvent = eventModel;
            Model.EventOperators.Clear();

            var currentEventOperators = currentEvent.Operators
                .Select(x => x.CallSign);
            var eventOperators = Model.Operators
                .Where(_ => currentEventOperators.Contains(_.CallSign))
                .ToList();

            if (eventOperators.Any())
            {
                eventOperators.ForEach(_ => Model.EventOperators.Add(_));
            }
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            Model = new OperatorModel
            {
                EditOperatorAction = (_) => UpdateOperator(Model.SelectedEventOperator, false),
                EditEventOperatorAction = (_) => UpdateOperator(Model.SelectedEventOperator, true),
                CurrentOperatorAction = (_) => SetCurrentOperator(Model.SelectedEventOperator),
                DeleteOperatorAction = (_) => DeleteOperator(Model.SelectedEventOperator),
                CurrentEventOperatorAction = (_) => SetCurrentOperator(Model.SelectedEventOperator),
                DeleteEventOperatorAction = (_) => DeleteOperator(Model.SelectedEventOperator),
                AddOperatorToEventAction = (_) => AddOperatorToEvent(),
                AddClubToEventAction = (_) => AddClubToEvent(),
                AddOperatorAction = (_) => AddOperator(),
                AddClubAction = (_) => AddClub(),
                EditOperatorsAction = (_) => EditOperators()
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
                operators.ForEach(_ => Model.Operators.Add(_));

                Model.CurrentOperator = operators.First();
            }
        }

        private void SetCurrentOperator(Operator operatorObj)
        {
            if (operatorObj != null && operatorObj.IsClub)
            {
                MessageBox.Show("Only individuals can be set as active operator", "You can't do that", MessageBoxButtons.OK);

                return;
            }

            Model.CurrentOperator = operatorObj;

            ServiceLocator.Current.GetInstance<IMainViewModel>().UpdateCurrentOperator(operatorObj);
        }

        private void DeleteOperator(Operator operatorObj)
        {
            var deleteConfirmName = String.Format("Delete {0}?", operatorObj.CallSign);
            var confirmResult = MessageBox.Show(deleteConfirmName, "Are you sure?", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                _operatorService.DeleteOperator(operatorObj);

                Model.Operators.Remove(operatorObj);
            }
        }

        private void UpdateOperator(Operator operatorObj, bool isEvent)
        {
            var newOperator = _operatorService.UpdateOperator(operatorObj);

            if (isEvent && !Model.EventOperators.Any(_ => _.CallSign == newOperator.CallSign))
            {
                Model.EventOperators.Add(newOperator);
            }

            if (!isEvent && !Model.Operators.Any(_ => _.CallSign == newOperator.CallSign))
            {
                Model.Operators.Add(newOperator);
            }

            if (_addToEvent)
            {
                currentEvent.Operators.Add(EditOperator.Model);

                _eventService.UpdateEvent(currentEvent, currentEvent.Operators.ToList());

                _addToEvent = false;
            }
        }

        private void AddOperatorToEvent()
        {
            _addToEvent = true;
            AddOperator();
        }

        private void AddClubToEvent()
        {
            _addToEvent = true;
            AddClub();
        }

        private void EditOperators()
        {
            Model.ShowCloseButton = true;
            Model.IsOpen = true;
        }

        private void AddOperator()
        {
            EditOperator.Model.Clear();

            EditOperator.ShowCloseButton = true;
            EditOperator.IsOpen = true;

            EditOperator.Model.IsClub = false;
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
