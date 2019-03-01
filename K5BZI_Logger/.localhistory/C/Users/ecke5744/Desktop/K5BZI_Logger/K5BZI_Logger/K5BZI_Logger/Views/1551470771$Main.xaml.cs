using System.Windows;
using K5BZI_Logger.Views.Interfaces;
using MahApps.Metro.Controls;

namespace K5BZI_Logger.Views
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : MetroWindow, IView
    {
        public Main()
        {
            InitializeComponent();
        }
        private async void ButtonBaseOnClick(object sender, RoutedEventArgs e)
        {
            ProgressBar.Visibility = Visibility.Visible;

            //await _dataContext.ProcessSelectionFileCommand.ExecuteAsync(null);

            //DataGridView1.DataSource = null;
            //DataGridView1.DataSource = _dataContext.DataTable;

            ProgressBar.Visibility = Visibility.Collapsed;
        }

    }
}
