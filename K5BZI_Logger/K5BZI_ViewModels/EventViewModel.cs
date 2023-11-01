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

        public SelectEventModel Model { get; private set; }
        public EditEventModel EditModel { get; private set; }
        private readonly IEventService _eventService;
        private readonly IMainViewModel _mainLoggerViewModel;
        private readonly IOperatorsViewModel _operatorsViewModel;
        private readonly IExcelFileService _excelFileService;

        #endregion

        #region Constructors

        public EventViewModel(
            IEventService eventService,
            IMainViewModel mainLoggerViewModel,
            IOperatorsViewModel operatorsViewModel,
            IExcelFileService excelFileService)
        {
            _eventService = eventService;
            _mainLoggerViewModel = mainLoggerViewModel;
            _operatorsViewModel = operatorsViewModel;
            _excelFileService = excelFileService;

            Initialize();
            GetExistingEvents();
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            Model = new SelectEventModel
            {
                CreateNewLogAction = (_) => CreateNewEvent(Model.EventName),
                SelectLogAction = (_) => SelectLogAsync(),
                ChangeEventAction = (_) => ChangeEvent(),
                DeleteEventAction = (_) => DeleteEvent((Guid)_)
            };

            EditModel = new EditEventModel
            {
                DxccEntities = _excelFileService.ReadDxccExcelData(),
                EditEventsAction = (_) => EditEvents(),
                EditAllEventsAction = (_) => EditAllEvents(),
                EditEventAction = (_) => EditEventAsync(),
                UpdateEventAction = (_) => UpdateEvent(),
                CreateEventAction = (_) => CreateNewEvent(String.Empty)
            };
        }

        private void GetExistingEvents()
        {
            Model.ExistingEvents.Clear();

            _eventService
                .GetAllEvents()?
                .OrderByDescending(_ => _.CreatedDate)
                .ToList()
                .ForEach(eventObj =>
                {
                    if (!eventObj.IsDeleted)
                    {
                        Model.ExistingEvents.Add(eventObj);
                    }
                });

            Model.SelectedEvent = Model.ExistingEvents.FirstOrDefault();
        }

        private void CreateNewEvent(string eventName)
        {
            var newEvent = _eventService.CreateNewEvent(eventName);

            _mainLoggerViewModel.CreateNewLog(newEvent);

            Model.SelectedEvent = newEvent;
            Model.ExistingEvents.Add(newEvent);
            Model.IsOpen = false;

            EditEventAsync();
        }

        private async void SelectLogAsync()
        {
            _mainLoggerViewModel.SelectEvent(Model.SelectedEvent);

            if (!_operatorsViewModel.Model.Operators.Any())
            {
                _operatorsViewModel.Model.IsOpen = true;
                _operatorsViewModel.AddOperator();
            }

            while (_operatorsViewModel.Model.IsOpen) { await Task.Delay(25); }

            _operatorsViewModel.PopulateEventOperators(Model.SelectedEvent);

            _operatorsViewModel.UpdateOperator(Model.SelectedEvent.ActiveOperator, true);

            Model.IsOpen = false;
        }

        private void ChangeEvent()
        {
            Model.ShowCloseButton = true;
            Model.IsOpen = true;
        }

        private void DeleteEvent(Guid Id)
        {
            var result = MessageBox.Show("Are you sure you want to delete this event?", "", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                var editEvent = Model.ExistingEvents.First(_ => _.Id == Id);

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

            var updatedOperators = EditModel.Operators
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
            foreach (var eventObj in Model.ExistingEvents)
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
            if (!_operatorsViewModel.Model.Operators.Any())
            {
                _operatorsViewModel.Model.IsOpen = true;
                _operatorsViewModel.AddOperator();
            }

            while (_operatorsViewModel.Model.IsOpen) { await Task.Delay(25); }

            EditModel.EditAllEvents = false;
            EditModel.Event = Model.SelectedEvent;

            EditModel.Operators.Clear();

            foreach (var op in _operatorsViewModel.Model.Operators)
            {
                if (_operatorsViewModel.Model.EventOperators.Contains(op))
                {
                    op.Selected = true;
                }

                EditModel.Operators.Add(op);
            };

            EditModel.Clubs.Clear();
            foreach (var club in _operatorsViewModel.Model.Operators.Where(_ => _.IsClub))
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
            _operatorsViewModel.Model.EventOperators.Clear();

            foreach (var op in operators)
            {
                _operatorsViewModel.Model.EventOperators.Add(op);
            }
        }

        #endregion
    }
}
