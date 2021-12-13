using PropertyChanged;

namespace K5BZI_Models.Base
{
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : IBaseViewModel
    {
        public BaseViewModel()
        {
            IsSelectOpen = false;
            IsEditOpen = false;
            IsOperatorsOpen = false;
            IsDefaultsOpen = false;
            ShowCloseButton = true;
        }

        public string EventName { get; set; }

        public bool IsSelectOpen { get; set; }

        public bool IsEditOpen { get; set; }

        public bool IsOperatorsOpen { get; set; }

        public bool IsExportOpen { get; set; }

        public bool IsDefaultsOpen { get; set; }

        public bool ShowCloseButton { get; set; }
    }
}