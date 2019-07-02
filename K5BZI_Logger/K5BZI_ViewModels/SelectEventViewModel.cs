using K5BZI_Models.ViewModelModels;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;
using System.Linq;

namespace K5BZI_ViewModels
{
    public class SelectEventViewModel : ISelectEventViewModel
    {
        #region Properties

        public SelectEventModel Model { get; private set; }
        private readonly IEventService _eventService;
        private readonly IMainLoggerViewModel _mainLoggerViewModel;

        #endregion

        #region Constructors

        public SelectEventViewModel(
            IEventService eventService,
            IMainLoggerViewModel mainLoggerViewModel)
        {
            _eventService = eventService;
            _mainLoggerViewModel = mainLoggerViewModel;

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
                SelectLogAction = (_) => SelectLog()
            };

            _mainLoggerViewModel.Model.ChangeEventAction = () => ChangeEvent();
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
            _mainLoggerViewModel.CreateNewLog(Model.EventName);

            Model.IsOpen = false;
        }

        private void SelectLog()
        {
            _mainLoggerViewModel.SelectEvent(Model.SelectedEvent);

            Model.IsOpen = false;
        }

        private void ChangeEvent()
        {
            Model.ShowCloseButton = true;
            Model.IsOpen = true;
        }

        #endregion
    }
}
