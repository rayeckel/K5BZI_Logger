using K5BZI_Models.Base;
using Microsoft.VisualStudio.PlatformUI;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace K5BZI_Models.ViewModelModels
{
    [AddINotifyPropertyChangedInterface]
    public class SelectEventModel : BaseViewModel
    {
        #region Constructors

        public SelectEventModel()
        {
            ExistingEvents = new ObservableCollection<Event>();

            IsOpen = true;
        }

        #endregion

        #region Properties

        public Event SelectedEvent { get; set; }

        public ObservableCollection<Event> ExistingEvents { get; private set; }

        public Visibility SelectLogVisibility
        {
            get
            {
                return ExistingEvents.Any() ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility NewLogVisibility
        {
            get
            {
                return !ExistingEvents.Any() ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        #endregion

        #region Commands

        private ICommand _selectLogCommand;
        public ICommand SelectLogCommand
        {
            get
            {
                return _selectLogCommand ?? (_selectLogCommand =
                    new DelegateCommand(SelectLogAction, _ => { return SelectedEvent != null; }));
            }
        }
        public Action<object> SelectLogAction { get; set; }

        private ICommand _createNewLogCommand;
        public ICommand CreateNewLogCommand
        {
            get
            {
                return _createNewLogCommand ??
                    (_createNewLogCommand =
                        new DelegateCommand(CreateNewLogAction, _ => { return !String.IsNullOrEmpty(EventName); }));
            }
        }
        public Action<object> CreateNewLogAction { get; set; }

        private ICommand _changeEventCommand;
        public ICommand ChangeEventCommand
        {
            get
            {
                return _changeEventCommand ??
                    (_changeEventCommand = new DelegateCommand(ChangeEventAction, _ => { return true; }));
            }
        }
        public Action<object> ChangeEventAction { get; set; }

        #endregion
    }
}
