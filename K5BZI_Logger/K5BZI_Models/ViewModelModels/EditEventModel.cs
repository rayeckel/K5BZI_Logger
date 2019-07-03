using K5BZI_Models.Base;
using Microsoft.VisualStudio.PlatformUI;
using PropertyChanged;
using System;
using System.Windows.Input;

namespace K5BZI_Models.ViewModelModels
{
    [AddINotifyPropertyChangedInterface]
    public class EditEventModel : BaseViewModel
    {
        #region Properties

        public Event Event { get; set; }

        #endregion

        #region Commands

        private ICommand _editEventCommand;
        public ICommand EditEventCommand
        {
            get
            {
                return _editEventCommand ??
                    (_editEventCommand = new DelegateCommand(EditEventAction, _ => { return true; }));
            }
        }
        public Action<object> EditEventAction { get; set; }

        private ICommand _updateEventCommand;
        public ICommand UpdateEventCommand
        {
            get
            {
                return _updateEventCommand ??
                    (_updateEventCommand = new DelegateCommand(UpdateEventAction, _ => { return true; }));
            }
        }
        public Action<object> UpdateEventAction { get; set; }

        #endregion
    }
}
