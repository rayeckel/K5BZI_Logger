using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using K5BZI_Models.Base;
using Microsoft.VisualStudio.PlatformUI;
using PropertyChanged;

namespace K5BZI_Models.ViewModelModels
{
    [AddINotifyPropertyChangedInterface]
    public class EventModel : BaseViewModel
    {
        #region Constructors

        public EventModel()
        {
            ExistingEvents = new ObservableCollection<Event>();

            IsOpen = true;
        }

        #endregion

        #region Properties

        public Event Event { get; set; }

        public ObservableCollection<Event> ExistingEvents { get; private set; }

        public string SelectEventTitle
        {
            get
            {
                return ExistingEvents.Any() ? "Select Log" : "Create Log";
            }
        }

        public Visibility SelectEventVisibility
        {
            get
            {
                return ExistingEvents.Any() ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility NewEventVisibility
        {
            get
            {
                return !ExistingEvents.Any() ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        #endregion

        #region Commands

        private ICommand _viewFileStoreCommand;
        public ICommand ViewFileStoreCommand
        {
            get
            {
                return _viewFileStoreCommand ??
                    (_viewFileStoreCommand = new DelegateCommand(ViewFileStoreAction, _ => { return true; }));
            }
        }
        public Action<object> ViewFileStoreAction { get; set; }

        private ICommand _selectEventCommand;
        public ICommand SelectEventCommand
        {
            get
            {
                return _selectEventCommand ?? (_selectEventCommand =
                    new DelegateCommand(SelectEventAction, _ => { return Event != null; }));
            }
        }
        public Action<object> SelectEventAction { get; set; }

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

        private ICommand _deleteEventCommand;
        public ICommand DeleteEventCommand
        {
            get
            {
                return _deleteEventCommand ??
                    (_deleteEventCommand = new DelegateCommand(DeleteEventAction, _ => { return true; }));
            }
        }
        public Action<object> DeleteEventAction { get; set; }

        #endregion
    }
}
