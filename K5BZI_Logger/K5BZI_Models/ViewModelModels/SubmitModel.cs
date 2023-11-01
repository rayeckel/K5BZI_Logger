using System.Collections.ObjectModel;
using System.Windows;
using K5BZI_Models.Base;
using PropertyChanged;

namespace K5BZI_Models.ViewModelModels
{
    [AddINotifyPropertyChangedInterface]
    public class SubmitModel : BaseViewModel
    {
        #region Properties

        public ObservableCollection<Operator> EventOperators { get; set; }

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

        #endregion

        #region Constructors

        public SubmitModel()
        {
            EventOperators = new ObservableCollection<Operator>();
        }

        #endregion

        #region Commands


        #endregion
    }
}
