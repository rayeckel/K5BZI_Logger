using PropertyChanged;
using System.Windows.Input;

namespace K5BZI_Models.Base
{
    [AddINotifyPropertyChangedInterface]
    public class BaseModel
    {
        public BaseModel()
        {
            _canExecute = true;
        }
        private ICommand _clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(() => MyAction(), _canExecute));
            }
        }
        private bool _canExecute;
        public void MyAction()
        {

        }
    }
}