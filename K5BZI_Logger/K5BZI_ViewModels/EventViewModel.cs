using K5BZI_Models;
using K5BZI_Models.ViewModelModels;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

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
                SelectLogAction = (_) => SelectLog(),
                ChangeEventAction = (_) => ChangeEvent()
            };

            EditModel = new EditEventModel
            {
                DxccEntities = _excelFileService.ReadDxccExcelData(),
                EditEventsAction = (_) => EditEvents(),
                EditAllEventsAction = (_) => EditAllEvents(),
                EditEventAction = (_) => EditEvent(),
                UpdateEventAction = (_) => UpdateEvent(),
                CreateEventAction = (_) => CreateNewEvent(String.Empty)
            };
        }

        private void GetExistingEvents()
        {
            var events = _eventService.GetAllEvents();

            Model.SelectedEvent = events.FirstOrDefault();

            events.ForEach(item =>
            {
                Model.ExistingEvents.Add(item);
            });

            Model.ExistingEvents.OrderBy(_ => _.CreatedDate);
        }

        private void CreateNewEvent(string eventName)
        {
            var newEvent = _eventService.CreateNewEvent(eventName);

            _mainLoggerViewModel.CreateNewLog(newEvent);

            Model.SelectedEvent = newEvent;
            Model.ExistingEvents.Add(newEvent);
            Model.IsOpen = false;

            EditEvent();
        }

        private void SelectLog()
        {
            _mainLoggerViewModel.SelectEvent(Model.SelectedEvent);

            _operatorsViewModel.PopulateEventOperators(Model.SelectedEvent);

            _operatorsViewModel.UpdateOperator(Model.SelectedEvent.ActiveOperator, true);

            Model.IsOpen = false;
        }

        private void ChangeEvent()
        {
            Model.ShowCloseButton = true;
            Model.IsOpen = true;
        }

        private void UpdateEvent()
        {
            if (EditModel.Event.ItuZone < 0 || EditModel.Event.ItuZone > 90)
            {
                MessageBox.Show("Please enter an ITU one value between 0 and 90", "Invalid input");
                return;
            }

            EditModel.IsOpen = false;

            var updatedOperators = EditModel.Operators
                .Where(_ => _.Selected)
                .ToList();

            UpdateOperators(updatedOperators);

            EditModel.Event.Club = EditModel.EventClub;
            EditModel.Event.DXCC = EditModel.EventDxcc;

            _eventService.UpdateEvent(EditModel.Event, updatedOperators);
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

        private void EditEvent()
        {
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
