using K5BZI_Models.Base;
using System;
using System.Windows.Input;

namespace K5BZI_Models.ViewModelModels
{
    public class EditOperatorModel : BaseViewModel
    {
        public Operator Model { get; set; }

        private ICommand _updateOperatorCommand;
        public ICommand UpdateOperatorCommand
        {
            get
            {
                return _updateOperatorCommand ?? (_updateOperatorCommand = new CommandHandler(UpdateOperatorAction, true));
            }
        }
        public Action UpdateOperatorAction { get; set; }
    }
}
