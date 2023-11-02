using System;
using System.Collections.Generic;
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
    public class EventViewModel : IEventViewModel
    {
        #region Properties

        public EventModel EventModel { get; private set; }
        public EditEventModel EditModel { get; private set; }

        private readonly IEventService _eventService;
        private readonly IExcelFileService _excelFileService;
        private readonly ILogViewModel _logViewModel;
        private readonly IOperatorsViewModel _operatorsViewModel;
        private readonly ISubmitViewModel _submitViewModel;

        #endregion

        #region Constructors

        public EventViewModel(
            IEventService eventService,
            IExcelFileService excelFileService,
            ILogViewModel logViewModel,
            IOperatorsViewModel operatorsViewModel,
            ISubmitViewModel submitViewModel)
        {
            _eventService = eventService;
            _excelFileService = excelFileService;
            _logViewModel = logViewModel;
            _operatorsViewModel = operatorsViewModel;
            _submitViewModel = submitViewModel;

            Initialize();
            GetExistingEvents();
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            EventModel = new EventModel
            {
                ViewFileStoreAction = (_) => _eventService.OpenEventList(),
                SelectEventAction = (_) => SelectEvent(),
                ChangeEventAction = (_) => ChangeEvent(),
                EditEventAction = (_) => EditEventAsync(),
                DeleteEventAction = (_) => DeleteEvent((Guid)_)
            };

            EditModel = new EditEventModel
            {
                DxccEntities = _excelFileService.ReadDxccExcelData(),
                CreateNewEventAction = (_) => CreateNewEvent(),
                EditEventsAction = (_) => EditEvents(),
                EditAllEventsAction = (_) => EditAllEvents(),
                UpdateEventAction = (_) => UpdateEvent()
            };
        }

        private void GetExistingEvents()
        {
            EventModel.ExistingEvents.Clear();

            _eventService
                .GetAllEvents()?
                .OrderByDescending(_ => _.CreatedDate)
                .ToList()
                .ForEach(eventObj =>
                {
                    if (!eventObj.IsDeleted)
                    {
                        EventModel.ExistingEvents.Add(eventObj);
                    }
                });

            EventModel.Event = EventModel.ExistingEvents.FirstOrDefault();
        }

        private void CreateNewEvent()
        {
            var newEvent = _eventService.CreateNewEvent(EditModel.NewEventName);

            _logViewModel.CreateNewLog(newEvent);

            EventModel.Event = newEvent;
            EventModel.ExistingEvents.Add(newEvent);
            EventModel.IsOpen = false;

            EditEventAsync();
        }

        private void SelectEvent()
        {
            _logViewModel.GetLog(EventModel.Event);

            _operatorsViewModel.PopulateEventOperators(EventModel.Event);

            EventModel.IsOpen = false;
        }

        private void ChangeEvent()
        {
            EventModel.ShowCloseButton = true;
            EventModel.IsOpen = true;
        }

        private void DeleteEvent(Guid Id)
        {
            var result = MessageBox.Show("Are you sure you want to delete this event?", "", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                var editEvent = EventModel.ExistingEvents.First(_ => _.Id == Id);

                editEvent.IsDeleted = true;

                _eventService.UpdateEvent(editEvent, editEvent.Operators.ToList());

                GetExistingEvents();
            }
        }

        private void UpdateEvent()
        {
            if (EditModel.Event.ItuZone < 0 || EditModel.Event.ItuZone > 90)
            {
                MessageBox.Show("Please enter an ITU one value between 0 and 90", "Invalid input");
                return;
            }

            var updatedOperators = _submitViewModel.SubmitModel.EventOperators
                .Where(_ => _.Selected)
                .ToList();

            UpdateOperators(updatedOperators);

            EditModel.Event.Club = EditModel.EventClub;
            EditModel.Event.DXCC = EditModel.EventDxcc;

            _eventService.UpdateEvent(EditModel.Event, updatedOperators);

            EditModel.IsOpen = false;
        }

        private void EditEvents()
        {
            EditModel.EditAllEvents = true;

            EditModel.ExistingEvents.Clear();
            foreach (var eventObj in EventModel.ExistingEvents)
            {
                if (!eventObj.IsDeleted)
                {
                    EditModel.ExistingEvents.Add(eventObj);
                }
            };

            EditModel.ShowCloseButton = true;
            EditModel.IsOpen = true;
        }

        private void EditAllEvents()
        {
            if (EditModel.Event.ItuZone < 0 || EditModel.Event.ItuZone > 90)
            {
                MessageBox.Show("Please enter an ITU one value between 0 and 90", "Invalid input");
                return;
            }

            EditModel.IsOpen = false;

            _eventService.UpdateEvent(EditModel.Event, EditModel.Event.Operators.ToList());
        }

        private async void EditEventAsync()
        {
            if (!_operatorsViewModel.OperatorModel.Operators.Any())
            {
                _operatorsViewModel.OperatorModel.IsOpen = true;
                _operatorsViewModel.AddOperator();
            }

            while (_operatorsViewModel.OperatorModel.IsOpen) { await Task.Delay(25); }

            EditModel.EditAllEvents = false;
            EditModel.Event = EventModel.Event;

            _submitViewModel.SubmitModel.EventOperators.Clear();

            foreach (var op in _operatorsViewModel.OperatorModel.Operators)
            {
                if (_operatorsViewModel.OperatorModel.EventOperators.Contains(op))
                {
                    op.Selected = true;
                }

                _submitViewModel.SubmitModel.EventOperators.Add(op);
            };

            EditModel.Clubs.Clear();
            foreach (var club in _operatorsViewModel.OperatorModel.Operators.Where(_ => _.IsClub))
            {
                EditModel.Clubs.Add(club);
            };

            EditModel.EventClub = EditModel.Clubs
                .FirstOrDefault(_ => _.CallSign == EditModel.Event.Club?.CallSign);

            EditModel.EventDxcc = EditModel.DxccEntities
                .FirstOrDefault(_ => _.Prefix == EditModel.Event.DXCC?.Prefix);

            EditModel.ShowCloseButton = true;
            EditModel.IsOpen = true;
        }

        private void UpdateOperators(List<Operator> operators)
        {
            _operatorsViewModel.OperatorModel.EventOperators.Clear();

            foreach (var op in operators)
            {
                _operatorsViewModel.OperatorModel.EventOperators.Add(op);
            }
        }

        #endregion
    }
}
