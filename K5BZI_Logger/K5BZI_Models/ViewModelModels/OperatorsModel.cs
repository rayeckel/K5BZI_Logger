using K5BZI_Models.Base;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace K5BZI_Models.ViewModelModels
{
    [AddINotifyPropertyChangedInterface]
    public class OperatorsModel : BaseModel
    {
        #region Constructors

        public OperatorsModel()
        {
            Operators = new ObservableCollection<Operator>();
        }

        #endregion

        #region Properties

        public Operator SelectedOperator { get; set; }

        public Operator CurrentOperator { get; set; }

        public ObservableCollection<Operator> Operators { get; private set; }

        #endregion

        private ICommand _editOperatorsCommand;
        public ICommand EditOperatorsCommand
        {
            get
            {
                return _editOperatorsCommand ??
                    (_editOperatorsCommand = new CommandHandler(EditOperatorsAction, true));
            }
        }
        public Action EditOperatorsAction { get; set; }
    }
}
