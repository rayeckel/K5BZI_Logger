using PropertyChanged;

namespace K5BZI_Models.Base
{
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : IBaseViewModel
    {
        public BaseViewModel()
        {
            IsOpen = false;
            ShowCloseButton = true;
        }

        public string EventName { get; set; }

        public bool IsOpen { get; set; }

        public bool ShowCloseButton { get; set; }
    }
}