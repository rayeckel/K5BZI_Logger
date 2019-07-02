using K5BZI_Models.Base;
using System;
using System.Windows.Input;

namespace K5BZI_Models.ViewModelModels
{
    public class EditEventModel : BaseModel
    {
        public Event Event { get; set; }

        #region Commands

        private ICommand _editEventCommand;
        public ICommand EditEventCommand
        {
            get
            {
                return _editEventCommand ?? (_editEventCommand = new CommandHandler(EditEventAction, true));
            }
        }
        public Action EditEventAction { get; set; }

        private ICommand _updateEventCommand;
        public ICommand UpdateEventCommand
        {
            get
            {
                return _updateEventCommand ?? (_updateEventCommand = new CommandHandler(UpdateEventAction, true));
            }
        }
        public Action UpdateEventAction { get; set; }

        #endregion
    }
}
