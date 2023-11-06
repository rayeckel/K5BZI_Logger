using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using K5BZI_Models;
using K5BZI_Models.ViewModelModels;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;
using MessageBox = System.Windows.Forms.MessageBox;

namespace K5BZI_ViewModels
{
    public class OperatorViewModel : IOperatorViewModel
    {
        #region Properties

        public OperatorModel OperatorModel { get; private set; }

        private readonly IOperatorService _operatorService;
        private readonly IEventService _eventService;
        private readonly IEventViewModel _eventViewModel;

        #endregion

        #region Constructors

        public OperatorViewModel(
            IOperatorService operatorService,
            IEventService eventService,
            IEventViewModel eventViewModel)
        {
            _operatorService = operatorService;
            _eventService = eventService;
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
                UpdateOperatorAction = async (_) => await UpdateOperatorAsync(),
                UpdateEventOperatorAction = async (_) => await UpdateOperatorAsync(),
                SelectOperatorAction = (_) => EditOperators(),
                AddToEventAction = async (_) => await UpdateOperatorAsync()
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

        private async Task UpdateOperatorAsync()
        {
            if (OperatorModel.ActiveEvent == null) return;

            //ActiveEvent.EventName will be empty on first run of the app.
            if (String.IsNullOrEmpty(OperatorModel.ActiveEvent.EventName) || OperatorModel.ShowEventOperators)
            {
                if (!OperatorModel.ActiveEvent.Operators.Any())
                    OperatorModel.ViewSelectedOperator.IsActive = true;
                else
                {
                    var current = OperatorModel.ActiveEvent.Operators.First(_ => _.IsActive);

                    if (current.CallSign != OperatorModel.ViewSelectedOperator.CallSign)
                        current.IsActive = false;

                    OperatorModel.ViewSelectedOperator.IsActive = true;
                }

                if (!OperatorModel.Operators.Any(_ => _.CallSign == OperatorModel.ViewSelectedOperator.CallSign))
                    OperatorModel.Operators.Add(OperatorModel.ViewSelectedOperator);

                if (!OperatorModel.ActiveEvent.Operators.Any(_ => _.CallSign?.ToUpper() == OperatorModel.ViewSelectedOperator.CallSign?.ToUpper()))
                    OperatorModel.ActiveEvent.Operators.Add(OperatorModel.ViewSelectedOperator);

                OperatorModel.IsOpen = false;
            }

            if (!OperatorModel.ViewOperators.Any(_ => _.CallSign == OperatorModel.ViewSelectedOperator.CallSign))
                OperatorModel.ViewOperators.Add(OperatorModel.ViewSelectedOperator);

            OperatorModel.ActiveOperator = new Operator(); //Trigger update

            OperatorModel.ShowEventOperators = false;
            OperatorModel.EditOperatorIsOpen = false;
            OperatorModel.AddToEventVisibility = Visibility.Hidden;

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
            if (OperatorModel.ViewSelectedOperator.IsClub)
            {
                MessageBox.Show("Only individuals can be set as active operator", "You can't do that", MessageBoxButtons.OK);
                return;
            }

            await UpdateOperatorAsync();

            OperatorModel.IsOpen = false;
        }

        private void EditOperators(bool eventOnly = false)
        {
            if (OperatorModel.IsOpen) // If the user clicked the "Select Existing" button
            {
                OperatorModel.IsOpen = false;
                OperatorModel.EditOperatorIsOpen = false;
                OperatorModel.AddToEventVisibility = Visibility.Visible;
            }
            else
            {
                OperatorModel.ShowEventOperators = eventOnly;
            }

            OperatorModel.ViewOperators.Clear();

            if (eventOnly)
            {
                foreach (var eventOperator in OperatorModel.ActiveEvent.Operators)
                    OperatorModel.ViewOperators.Add(eventOperator);
            }
            else
            {
                foreach (var appOperator in OperatorModel.Operators)
                    OperatorModel.ViewOperators.Add(appOperator);
            }

            OperatorModel.ShowCloseButton = true;
            OperatorModel.IsOpen = true;
        }

        public void AddOperator(bool isClub = false)
        {
            //There is a strange bug where ViewSelectedOperator is nul here the first time the sapp is run
            if (OperatorModel.ViewSelectedOperator == null) OperatorModel.ViewSelectedOperator = new Operator();

            OperatorModel.ShowCloseButton = true;
            OperatorModel.EditOperatorIsOpen = true;
        }

        #endregion
    }
}
