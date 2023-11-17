using System;
using System.Windows.Input;
using K5BZI_Models.Base;
using K5BZI_Models.Enums;
using Prism.Commands;
using PropertyChanged;

namespace K5BZI_Models.ViewModelModels
{
    [AddINotifyPropertyChangedInterface]
    public class SelectExportModel : BaseViewModel
    {
        #region Properties

        public LogType SelectedExport { get; set; }

        public Operator SelectedOperator { get; set; }

        #endregion

        #region Commands

        private ICommand _selectExportCommand;
        public ICommand SelectExportCommand
        {
            get
            {
                return _selectExportCommand ?? (_selectExportCommand =
                    new DelegateCommand<object>(SelectExportAction, _ => { return SelectedExport >= 0; }));
            }
        }
        public Action<object> SelectExportAction { get; set; }

        #endregion
    }
}
