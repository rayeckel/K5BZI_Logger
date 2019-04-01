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

        #endregion

        #region Constructors

        public SelectEventViewModel()
        {
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

            var mainLoggerViewModel = ServiceLocator.Current.GetInstance<IMainLoggerViewModel>();
            mainLoggerViewModel.Model.ChangeEventAction = () => ChangeEvent();
        }

        private void GetExistingEvents()
        {
            var eventService = ServiceLocator.Current.GetInstance<IEventService>();
            var events = eventService.GetAllEvents();

            events.OrderByDescending(_ => _.CreatedDate);

            Model.SelectedEvent = events.FirstOrDefault();

            events.ForEach(item =>
            {
                Model.ExistingEvents.Add(item);
            });
        }

        private void CreateNewLog()
        {
            var eventService = ServiceLocator.Current.GetInstance<IEventService>();
            var newEvent = eventService.CreateNewEvent(Model.EventName);

            var mainLoggerViewModel = ServiceLocator.Current.GetInstance<IMainLoggerViewModel>();
            mainLoggerViewModel.CreateNewLog(newEvent);

            Model.IsOpen = false;
        }

        private void SelectLog()
        {
            var mainLoggerViewModel = ServiceLocator.Current.GetInstance<IMainLoggerViewModel>();
            mainLoggerViewModel.SelectEvent(Model.SelectedEvent);

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
