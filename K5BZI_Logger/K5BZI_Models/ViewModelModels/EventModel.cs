using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using K5BZI_Models.Base;
using K5BZI_Models.EntityModels;
using K5BZI_Models.Enums;
using Prism.Commands;
using PropertyChanged;

namespace K5BZI_Models.ViewModelModels
{
    [AddINotifyPropertyChangedInterface]
    public class EventModel : BaseViewModel
    {
        #region Constructors

        public EventModel()
        {
            Events = new ObservableCollection<Event>();

            IsOpen = true;
        }

        #endregion

        #region Properties

        public bool EditEventIsOpen { get; set; }

        public bool EditAllEvents { get; set; }

        public string SelectEventTitle
        {
            get
            {
                return Events.Any() ? "Select Log" : "Create Log";
            }
        }

        public Event ActiveEvent
        {
            get
            {
                return Events.FirstOrDefault(_ => _.IsActive);
            }
            set
            {
                FirePropertyChanged();
            }
        }

        public ObservableCollection<Event> Events { get; private set; }

        public Visibility IsPotaVisibility
        {
            get
            {
                return ActiveEvent?.EventType == EventType.PARKSONTHEAIR ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility IsCustomVisibility
        {
            get
            {
                return ActiveEvent?.EventType == EventType.CUSTOM ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility SelectEventVisibility
        {
            get
            {
                return Events.Any() ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility NewEventVisibility
        {
            get
            {
                return !Events.Any() ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        [AlsoNotifyFor("EditAllEvents")]
        public Visibility EditEventsVisibility
        {
            get
            {
                return !EditAllEvents ?
                    Visibility.Visible :
                    Visibility.Collapsed;
            }
        }

        [AlsoNotifyFor("EditAllEvents")]
        public Visibility EditAllEventsVisibility
        {
            get
            {
                return EditAllEvents ?
                    Visibility.Visible :
                    Visibility.Collapsed;
            }
        }

        public string NewEventName { get; set; }

        public Operator EventClub { get; set; }

        public List<DXCC> DXCCValues { get; set; }

        public IEnumerable<CqZone> CqZoneValues
        {
            get { return Enum.GetValues(typeof(CqZone)).Cast<CqZone>(); }
        }

        public IEnumerable<EventType> EventTypeValues
        {
            get { return Enum.GetValues(typeof(EventType)).Cast<EventType>(); }
        }

        #endregion

        #region Commands

        private ICommand _viewFileStoreCommand;
        public ICommand ViewFileStoreCommand
        {
            get
            {
                return _viewFileStoreCommand ??
                    (_viewFileStoreCommand = new DelegateCommand<object>(ViewFileStoreAction, _ => { return true; }));
            }
        }
        public Action<object> ViewFileStoreAction { get; set; }

        private ICommand _selectEventCommand;
        public ICommand SelectEventCommand
        {
            get
            {
                return _selectEventCommand ?? (_selectEventCommand =
                    new DelegateCommand<object>(SelectEventAction, _ => { return ActiveEvent != null; }));
            }
        }
        public Action<object> SelectEventAction { get; set; }

        private ICommand _changeEventCommand;
        public ICommand ChangeEventCommand
        {
            get
            {
                return _changeEventCommand ??
                    (_changeEventCommand = new DelegateCommand<object>(ChangeEventAction, _ => { return true; }));
            }
        }
        public Action<object> ChangeEventAction { get; set; }

        private ICommand _editEventCommand;
        public ICommand EditEventCommand
        {
            get
            {
                return _editEventCommand ??
                    (_editEventCommand = new DelegateCommand<object>(EditEventAction, _ => { return true; }));
            }
        }
        public Action<object> EditEventAction { get; set; }

        private ICommand _deleteEventCommand;
        public ICommand DeleteEventCommand
        {
            get
            {
                return _deleteEventCommand ??
                    (_deleteEventCommand = new DelegateCommand<object>(DeleteEventAction, _ => { return true; }));
            }
        }
        public Action<object> DeleteEventAction { get; set; }

        private ICommand _editEventsCommand;
        public ICommand EditEventsCommand
        {
            get
            {
                return _editEventsCommand ??
                    (_editEventsCommand = new DelegateCommand<object>(EditEventsAction, _ => { return true; }));
            }
        }
        public Action<object> EditEventsAction { get; set; }

        private ICommand _updateEventCommand;
        public ICommand UpdateEventCommand
        {
            get
            {
                return _updateEventCommand ??
                    (_updateEventCommand = new DelegateCommand<object>(UpdateEventAction, _ => { return true; }));
            }
        }
        public Action<object> UpdateEventAction { get; set; }

        private ICommand _createNewEventCommand;
        public ICommand CreateNewEventCommand
        {
            get
            {
                return _createNewEventCommand ??
                    (_createNewEventCommand =
                        new DelegateCommand<object>(CreateNewEventAction, _ => { return true; }));
            }
        }
        public Action<object> CreateNewEventAction { get; set; }

        private ICommand _checkOperatorsCommand;
        public ICommand CheckOperatorsCommand
        {
            get
            {
                return _checkOperatorsCommand ??
                    (_checkOperatorsCommand =
                        new DelegateCommand<object>(CheckOperatorsAction, _ => { return true; }));
            }
        }
        public Action<object> CheckOperatorsAction { get; set; }

        private ICommand _createLogCommand;
        public ICommand CreateLogCommand
        {
            get
            {
                return _createLogCommand ??
                    (_createLogCommand =
                        new DelegateCommand<Event>(CreateLogAction, _ => { return true; }));
            }
        }
        public Action<Event> CreateLogAction { get; set; }

        private ICommand _getLogCommand;
        public ICommand GetLogCommand
        {
            get
            {
                return _getLogCommand ??
                    (_getLogCommand =
                        new DelegateCommand<Event>(GetLogAction, _ => { return true; }));
            }
        }
        public Action<Event> GetLogAction { get; set; }

        #endregion
    }
}
