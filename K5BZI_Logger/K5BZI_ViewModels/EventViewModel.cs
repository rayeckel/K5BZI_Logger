using K5BZI_Models.ViewModelModels;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;
using System.Linq;

namespace K5BZI_ViewModels
{
    public class EventViewModel : IEventViewModel
    {
        #region Properties

        public SelectEventModel Model { get; private set; }
        public EditEventModel EditModel { get; private set; }
        private readonly IEventService _eventService;
        private readonly IMainLoggerViewModel _mainLoggerViewModel;
        private readonly IOperatorsViewModel _operatorsViewModel;

        #endregion

        #region Constructors

        public EventViewModel(
            IEventService eventService,
            IMainLoggerViewModel mainLoggerViewModel,
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
                CreateNewLogAction = (_) => CreateNewLog(),
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

        private void CreateNewLog()
        {
            var newEvent = _eventService.CreateNewEvent(Model.EventName);

            _mainLoggerViewModel.CreateNewLog(newEvent);

            Model.IsOpen = false;
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
            EditModel.IsOpen = false;

            _eventService.UpdateEvent(Model.SelectedEvent);
        }

        public void EditEvent()
        {
            EditModel.Event = Model.SelectedEvent;
            EditModel.ShowCloseButton = true;
            EditModel.IsOpen = true;
        }

        #endregion
    }
}
