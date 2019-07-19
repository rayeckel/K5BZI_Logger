using K5BZI_Models.Base;
using K5BZI_Models.EntityModels;
using Microsoft.VisualStudio.PlatformUI;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace K5BZI_Models.ViewModelModels
{
    [AddINotifyPropertyChangedInterface]
    public class EditEventModel : BaseViewModel
    {
        #region Properties

        public bool EditAllEvents { get; set; }

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

        public Event Event { get; set; }

        public Operator EventClub { get; set; }

        public DXCC EventDxcc { get; set; }

        public ObservableCollection<Event> ExistingEvents { get; private set; }

        public ObservableCollection<Operator> Operators { get; set; }

        public ObservableCollection<Operator> Clubs { get; set; }

        public List<DXCC> DxccEntities { get; set; }

        #endregion

        #region Constructors

        public EditEventModel()
        {
            Operators = new ObservableCollection<Operator>();
            Clubs = new ObservableCollection<Operator>();
            ExistingEvents = new ObservableCollection<Event>();
        }

        #endregion

        #region Commands

        private ICommand _editEventsCommand;
        public ICommand EditEventsCommand
        {
            get
            {
                return _editEventsCommand ??
                    (_editEventsCommand = new DelegateCommand(EditEventsAction, _ => { return true; }));
            }
        }
        public Action<object> EditEventsAction { get; set; }

        private ICommand _editAllEventsCommand;
        public ICommand EditAllEventsCommand
        {
            get
            {
                return _editAllEventsCommand ??
                    (_editAllEventsCommand = new DelegateCommand(EditAllEventsAction, _ => { return true; }));
            }
        }
        public Action<object> EditAllEventsAction { get; set; }

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

        private ICommand _updateEventCommand;
        public ICommand UpdateEventCommand
        {
            get
            {
                return _updateEventCommand ??
                    (_updateEventCommand = new DelegateCommand(UpdateEventAction, _ => { return true; }));
            }
        }
        public Action<object> UpdateEventAction { get; set; }

        #endregion
    }
}
