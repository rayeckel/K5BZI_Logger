using System;
using System.Collections.Generic;
using System.Linq;
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

        public Operator SelectedSubmitOperator { get; set; }

        public LogType SelectedLogType { get; set; }

        public IEnumerable<LogType> LogTypeValues
        {
            get { return Enum.GetValues(typeof(LogType)).Cast<LogType>(); }
        }

        public IEnumerable<EventType> ServiceValues
        {
            get { return Enum.GetValues(typeof(EventType)).Cast<EventType>(); }
        }

        #endregion

        #region Constructors

        public SubmitModel()
        {
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
