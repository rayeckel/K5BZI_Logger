using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using K5BZI_Models.Base;
using K5BZI_Models.Enums;
using Microsoft.VisualStudio.PlatformUI;
using PropertyChanged;

namespace K5BZI_Models.ViewModelModels
{
    [AddINotifyPropertyChangedInterface]
    public class SubmitModel : BaseViewModel
    {
        #region Properties

        public ObservableCollection<Operator> EventOperators { get; set; }

        public Operator SelectedSubmitOperator { get; set; }

        public LogType SelectedLogType { get; set; }

        public Services SelectedService { get; set; }

        public Visibility SelectOperatorVisibility
        {
            get
            {
                return EventOperators.Count > 1 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility DisplayOperatorVisibility
        {
            get
            {
                return EventOperators.Count == 1 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public IEnumerable<LogType> LogTypeValues
        {
            get { return Enum.GetValues(typeof(LogType)).Cast<LogType>(); }
        }

        public IEnumerable<Services> ServiceValues
        {
            get { return Enum.GetValues(typeof(Services)).Cast<Services>(); }
        }

        #endregion

        #region Constructors

        public SubmitModel()
        {
            EventOperators = new ObservableCollection<Operator>();
        }

        #endregion

        #region Commands

        private ICommand _submitLogCommand;
        public ICommand SubmitLogCommand
        {
            get
            {
                return _submitLogCommand ??
                    (_submitLogCommand = new DelegateCommand(SubmitLogAction, _ => { return true; }));
            }
        }
        public Action<object> SubmitLogAction { get; set; }

        #endregion
    }
}
