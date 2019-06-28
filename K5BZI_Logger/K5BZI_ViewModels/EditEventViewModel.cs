using K5BZI_Models.ViewModelModels;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace K5BZI_ViewModels
{
    public class EditEventViewModel : IEditEventViewModel
    {
        #region Properties

        public EditEventModel Model { get; private set; }

        #endregion

        #region Constructors

        public EditEventViewModel()
        {
            Initialize();
        }

        #endregion

        #region Public Methods

        public void EditEvent()
        {
            Model.ShowCloseButton = true;
            Model.IsOpen = true;
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            var mainLoggerViewModel = ServiceLocator.Current.GetInstance<IMainLoggerViewModel>();
            mainLoggerViewModel.Model.EditEventAction = () => EditEvent();

            Model = new EditEventModel
            {
                Event = mainLoggerViewModel.Model.Event,
                EditEventAction = () => UpdateEvent()
            };
        }

        private void UpdateEvent()
        {
            Model.IsOpen = false;

            var eventService = ServiceLocator.Current.GetInstance<IEventService>();
            eventService.UpdateEvent(Model.Event);
        }

        #endregion
    }
}
