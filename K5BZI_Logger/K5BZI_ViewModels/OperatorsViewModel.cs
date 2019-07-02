using K5BZI_Models;
using K5BZI_Models.ViewModelModels;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;
using System.Linq;

namespace K5BZI_ViewModels
{
    public class OperatorsViewModel : IOperatorsViewModel
    {
        public OperatorModel Model { get; private set; }
        public EditOperatorModel EditOperator { get; private set; }
        private readonly IOperatorService _operatorService;
        private readonly IEventService _eventService;
        private Event currentEvent;
        private bool _addToEvent;

        public OperatorsViewModel(
            IOperatorService operatorService,
            IEventService eventService)
        {
            _operatorService = operatorService;
            _eventService = eventService;

            Initialize();
        }

        public void PopulateOperators(Event eventModel)
        {
            currentEvent = eventModel;
            Model.Operators.Clear();
            Model.EventOperators.Clear();

            var operators = _operatorService.GetFullOperatorListing();

            if (operators.Any())
            {
                operators.ForEach(_ => Model.Operators.Add(_));

                Model.CurrentOperator = operators.First();
            }

            var eventOperators = operators.Where(_ => currentEvent.Operators.Contains(_.CallSign))
                .ToList();

            if (eventOperators.Any())
            {
                eventOperators.ForEach(_ => Model.EventOperators.Add(_));
            }
        }

        private void Initialize()
        {
            Model = new OperatorModel
            {
                EditOperatorAction = () => UpdateOperator(Model.SelectedOperator, false),
                EditEventOperatorAction = () => UpdateOperator(Model.SelectedEventOperator, true),
                AddOperatorToEventAction = () => AddOperatorToEvent(),
                AddClubToEventAction = () => AddClubToEvent(),
                AddOperatorAction = () => AddOperator(),
                AddClubAction = () => AddClub(),
                EditOperatorsAction = () => EditOperators()
            };

            EditOperator = new EditOperatorModel
            {
                Model = new Operator(),
                UpdateOperatorAction = () => UpdateOperator(EditOperator.Model, false),
                UpdateEventOperatorAction = () => UpdateOperator(EditOperator.Model, true)
            };
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
                currentEvent.Operators.Add(EditOperator.Model.CallSign);

                _eventService.UpdateEvent(currentEvent);

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
    }
}
