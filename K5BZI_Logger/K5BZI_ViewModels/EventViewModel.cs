using K5BZI_Models;
using K5BZI_Models.ViewModelModels;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;
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

        #endregion

        #region Constructors

        public EventViewModel(
            IEventService eventService,
            IMainViewModel mainLoggerViewModel,
            IOperatorsViewModel operatorsViewModel)
        {
            _eventService = eventService;
            _mainLoggerViewModel = mainLoggerViewModel;
            _operatorsViewModel = operatorsViewModel;

            Initialize();
            GetExistingEvents();
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            Model = new SelectEventModel
            {
                CreateNewLogAction = (_) => CreateNewEvent(),
                SelectLogAction = (_) => SelectLog(),
                ChangeEventAction = (_) => ChangeEvent()
            };

            EditModel = new EditEventModel
            {
                EditEventAction = (_) => EditEvent(),
                UpdateEventAction = (_) => UpdateEvent()
            };
        }

        private void GetExistingEvents()
        {
            var events = _eventService.GetAllEvents();

            events.OrderByDescending(_ => _.CreatedDate);

            Model.SelectedEvent = events.FirstOrDefault();

            events.ForEach(item =>
            {
                Model.ExistingEvents.Add(item);
            });
        }

        private void CreateNewEvent()
        {
            var newEvent = _eventService.CreateNewEvent(Model.EventName);

            _mainLoggerViewModel.CreateNewLog(newEvent);

            Model.SelectedEvent = newEvent;
            Model.ExistingEvents.Add(newEvent);
            Model.IsOpen = false;

            EditEvent();
        }

        private void SelectLog()
        {
            _mainLoggerViewModel.SelectEvent(Model.SelectedEvent);

            _operatorsViewModel.PopulateOperators(Model.SelectedEvent);

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

        private void EditEvent()
        {
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
