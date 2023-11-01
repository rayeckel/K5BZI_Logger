using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using K5BZI_Models.Base;
using Microsoft.VisualStudio.PlatformUI;
using PropertyChanged;

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

        public string OperatorTitle
        {
            get
            {
                return ShowEventOperators ?
                    $"EVENT Operators - {CurrentEvent.EventName}" :
                    "ALL Operators";
            }
        }

        public Event CurrentEvent { get; set; }

        public bool ShowEventOperators { get; set; }

        public Operator SelectedEventOperator { get; set; }

        public Operator CurrentOperator { get; set; }

        public ObservableCollection<Operator> Operators { get; private set; }

        public ObservableCollection<Operator> EventOperators { get; private set; }

        public ObservableCollection<Operator> ViewSelectedOperators
        {
            get
            {
                return ShowEventOperators ? EventOperators : Operators;
            }
        }

        #endregion

        #region Commands

        private ICommand _editOperatorCommand;
        public ICommand EditOperatorCommand
        {
            get
            {
                return _editOperatorCommand ??
                    (_editOperatorCommand = new DelegateCommand(EditOperatorAction, _ => { return true; }));
            }
        }
        public Action<object> EditOperatorAction { get; set; }

        private ICommand _editEventOperatorCommand;
        public ICommand EditEventOperatorCommand
        {
            get
            {
                return _editEventOperatorCommand ??
                    (_editEventOperatorCommand = new DelegateCommand(EditEventOperatorAction, _ => { return true; }));
            }
        }
        public Action<object> EditEventOperatorAction { get; set; }

        private ICommand _addOperatorToEventCommand;
        public ICommand AddOperatorToEventCommand
        {
            get
            {
                return _addOperatorToEventCommand ??
                    (_addOperatorToEventCommand = new DelegateCommand(AddOperatorToEventAction, _ => { return true; }));
            }
        }
        public Action<object> AddOperatorToEventAction { get; set; }

        private ICommand _currentEventOperatorCommand;
        public ICommand CurrentEventOperatorCommand
        {
            get
            {
                return _currentEventOperatorCommand ??
                    (_currentEventOperatorCommand =
                    new DelegateCommand(CurrentEventOperatorAction, _ => { return SelectedEventOperator != null; }));
            }
        }
        public Action<object> CurrentEventOperatorAction { get; set; }

        private ICommand _deleteEventOperatorCommand;
        public ICommand DeleteEventOperatorCommand
        {
            get
            {
                return _deleteEventOperatorCommand ??
                    (_deleteEventOperatorCommand =
                    new DelegateCommand(DeleteEventOperatorAction, _ => { return SelectedEventOperator != null; }));
            }
        }
        public Action<object> DeleteEventOperatorAction { get; set; }

        private ICommand _addClubToEventCommand;
        public ICommand AddClubToEventCommand
        {
            get
            {
                return _addClubToEventCommand ??
                    (_addClubToEventCommand =
                    new DelegateCommand(AddClubToEventAction, _ => { return SelectedEventOperator != null; }));
            }
        }
        public Action<object> AddClubToEventAction { get; set; }

        private ICommand _currentOperatorCommand;
        public ICommand CurrentOperatorCommand
        {
            get
            {
                return _currentOperatorCommand ??
                    (_currentOperatorCommand =
                    new DelegateCommand(CurrentOperatorAction, _ => { return SelectedEventOperator != null; }));
            }
        }
        public Action<object> CurrentOperatorAction { get; set; }

        private ICommand _addOperatorCommand;
        public ICommand AddOperatorCommand
        {
            get
            {
                return _addOperatorCommand ??
                    (_addOperatorCommand = new DelegateCommand(AddOperatorAction, _ => { return true; }));
            }
        }
        public Action<object> AddOperatorAction { get; set; }

        private ICommand _deleteOperatorCommand;
        public ICommand DeleteOperatorCommand
        {
            get
            {
                return _deleteOperatorCommand ??
                    (_deleteOperatorCommand = new DelegateCommand(DeleteOperatorAction, _ => { return SelectedEventOperator != null; }));
            }
        }
        public Action<object> DeleteOperatorAction { get; set; }

        private ICommand _addClubCommand;
        public ICommand AddClubCommand
        {
            get
            {
                return _addClubCommand ??
                    (_addClubCommand = new DelegateCommand(AddClubAction, _ => { return true; }));
            }
        }
        public Action<object> AddClubAction { get; set; }

        private ICommand _editOperatorsCommand;
        public ICommand EditOperatorsCommand
        {
            get
            {
                return _editOperatorsCommand ??
                    (_editOperatorsCommand = new DelegateCommand(EditOperatorsAction, _ => { return true; }));
            }
        }
        public Action<object> EditOperatorsAction { get; set; }

        private ICommand _changeOperatorCommand;
        public ICommand ChangeOperatorCommand
        {
            get
            {
                return _changeOperatorCommand ??
                    (_changeOperatorCommand = new DelegateCommand(ChangeOperatorAction, _ => { return true; }));
            }
        }
        public Action<object> ChangeOperatorAction { get; set; }

        #endregion
    }
}
