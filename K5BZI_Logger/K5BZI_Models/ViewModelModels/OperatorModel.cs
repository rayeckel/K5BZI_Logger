using K5BZI_Models.Base;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace K5BZI_Models.ViewModelModels
{
    [AddINotifyPropertyChangedInterface]
    public class OperatorModel : BaseViewModel
    {
        #region Constructors

        public OperatorModel()
        {
            Operators = new ObservableCollection<Operator>();
            EventOperators = new ObservableCollection<Operator>();
        }

        #endregion

        #region Properties

        public Operator SelectedOperator { get; set; }

        public Operator CurrentOperator { get; set; }

        public ObservableCollection<Operator> Operators { get; private set; }

        public ObservableCollection<Operator> EventOperators { get; private set; }

        #endregion

        private ICommand _editOperatorCommand;
        public ICommand EditOperatorCommand
        {
            get
            {
                return _editOperatorCommand ??
                    (_editOperatorCommand = new CommandHandler(EditOperatorAction, true));
            }
        }
        public Action EditOperatorAction { get; set; }

        private ICommand _addOperatorToEventCommand;
        public ICommand AddOperatorToEventCommand
        {
            get
            {
                return _addOperatorToEventCommand ??
                    (_addOperatorToEventCommand = new CommandHandler(AddOperatorToEventAction, true));
            }
        }
        public Action AddOperatorToEventAction { get; set; }

        private ICommand _addClubToEventCommand;
        public ICommand AddClubToEventCommand
        {
            get
            {
                return _addClubToEventCommand ??
                    (_addClubToEventCommand = new CommandHandler(AddClubToEventAction, true));
            }
        }
        public Action AddClubToEventAction { get; set; }

        private ICommand _addOperatorCommand;
        public ICommand AddOperatorCommand
        {
            get
            {
                return _addOperatorCommand ??
                    (_addOperatorCommand = new CommandHandler(AddOperatorAction, true));
            }
        }
        public Action AddOperatorAction { get; set; }

        private ICommand _addClubCommand;
        public ICommand AddClubCommand
        {
            get
            {
                return _addClubCommand ??
                    (_addClubCommand = new CommandHandler(AddClubAction, true));
            }
        }
        public Action AddClubAction { get; set; }
    }
}
