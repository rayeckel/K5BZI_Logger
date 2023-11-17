using System;
using System.Windows.Input;
using K5BZI_Models.Base;
using K5BZI_Models.EntityModels;
using Prism.Commands;
using PropertyChanged;

namespace K5BZI_Models.ViewModelModels
{
    [AddINotifyPropertyChangedInterface]
    public class DefaultsModel : BaseViewModel
    {
        #region Properties

        public Defaults Defaults { get; set; }

        #endregion

        #region Commands

        private ICommand _editDefaultsCommand;
        public ICommand EditDefaultsCommand
        {
            get
            {
                return _editDefaultsCommand ??
                    (_editDefaultsCommand = new DelegateCommand<object>(EditDefaultsAction, _ => { return true; }));
            }
        }
        public Action<object> EditDefaultsAction { get; set; }

        private ICommand _updateDefaultsCommand;
        public ICommand UpdateDefaultsCommand
        {
            get
            {
                return _updateDefaultsCommand ??
                    (_updateDefaultsCommand = new DelegateCommand<object>(UpdateDefaultsAction, _ => { return true; }));
            }
        }
        public Action<object> UpdateDefaultsAction { get; set; }

        #endregion
    }
}
