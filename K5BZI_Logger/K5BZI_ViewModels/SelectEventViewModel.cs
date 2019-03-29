using K5BZI_Models.ViewModelModels;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Windows.Forms;
using System.Linq;

namespace K5BZI_ViewModels
{
    public class SelectEventViewModel : ISelectEventViewModel
    {
        #region Properties

        public SelectEventModel Model { get; private set; }
        private IEventService _eventService;
        private readonly IMainLoggerViewModel _mainLoggerViewModel;

        #endregion

        #region Constructors

        public SelectEventViewModel()
        {
            _eventService = ServiceLocator.Current.GetInstance<IEventService>();
            _mainLoggerViewModel = ServiceLocator.Current.GetInstance<IMainLoggerViewModel>();

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

            Model.IsOpen = false;
        }

        private void ChangeEvent()
        {
            Model.IsOpen = true;
        }

        #endregion
    }
}
