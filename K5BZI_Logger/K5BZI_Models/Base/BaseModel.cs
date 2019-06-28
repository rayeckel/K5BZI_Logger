using PropertyChanged;

namespace K5BZI_Models.Base
{
    [AddINotifyPropertyChangedInterface]
    public class BaseModel : IBaseModel
    {
        public BaseModel()
        {
            IsOpen = false;
            ShowCloseButton = true;
        }

        public string EventName { get; set; }

        public bool IsOpen { get; set; }

        public bool ShowCloseButton { get; set; }
    }
}