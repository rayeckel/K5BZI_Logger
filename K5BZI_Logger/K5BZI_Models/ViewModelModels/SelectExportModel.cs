using K5BZI_Models.Base;
using K5BZI_Models.Enums;
using Microsoft.VisualStudio.PlatformUI;
using PropertyChanged;
using System;
using System.Windows.Input;

namespace K5BZI_Models.ViewModelModels
{
    [AddINotifyPropertyChangedInterface]
    public class SelectExportModel : BaseModel
    {
        #region Properties

        public LogType SelectedExport { get; set; }

        #endregion

        #region Commands

        private ICommand _selectExportCommand;
        public ICommand SelectExportCommand
        {
            get
            {
                return _selectExportCommand ?? (_selectExportCommand =
                    new DelegateCommand(SelectExportAction, _ => { return SelectedExport >= 0; }));
            }
        }
        public Action<object> SelectExportAction { get; set; }

        #endregion
    }
}
