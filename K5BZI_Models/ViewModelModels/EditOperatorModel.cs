using K5BZI_Models.Base;
using Microsoft.VisualStudio.PlatformUI;
using PropertyChanged;
using System;
using System.Windows.Input;

namespace K5BZI_Models.ViewModelModels
{
    [AddINotifyPropertyChangedInterface]
    public class EditOperatorModel : BaseViewModel
    {
        #region Properties

        public Operator Model { get; set; }

        #endregion

        #region Commands

        private ICommand _updateOperatorCommand;
        public ICommand UpdateOperatorCommand
        {
            get
            {
                return _updateOperatorCommand ??
                    (_updateOperatorCommand = new DelegateCommand(UpdateOperatorAction, _ => { return true; }));
            }
        }
        public Action<object> UpdateOperatorAction { get; set; }

        private ICommand _updateEventOperatorCommand;
        public ICommand UpdateEventOperatorCommand
        {
            get
            {
                return _updateEventOperatorCommand ??
                    (_updateEventOperatorCommand = new DelegateCommand(UpdateEventOperatorAction, _ => { return true; }));
            }
        }
        public Action<object> UpdateEventOperatorAction { get; set; }

        #endregion
    }
}
