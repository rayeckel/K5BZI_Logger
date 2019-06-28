using MahApps.Metro.Controls;
using System.Windows.Controls;

namespace K5BZI_Logger.Views
{
    public partial class LogGrid : MetroTabItem
    {
        public LogGrid()
        {
            InitializeComponent();
        }

        private void DataGrid1_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            //((MainModel)DataContext).UpdateLogEntryCommand.Execute(this);
        }
    }
}
