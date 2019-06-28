using K5BZI_Models.Main;
using K5BZI_ViewModels;
using System.Windows.Controls;

namespace K5BZI_Logger.Views
{
    /// <summary>
    /// Interaction logic for MainGrid.xaml
    /// </summary>
    public partial class MainGrid : UserControl
    {
        public MainGrid()
        {
            InitializeComponent();
        }

        private void DataGrid1_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            ((MainModel)DataContext).UpdateLogEntryCommand.Execute(this);
        }
    }
}
