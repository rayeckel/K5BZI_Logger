using K5BZI_Logger.Views.Interfaces;
using K5BZI_Models.Main;
using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.Controls;
using System.Windows;

namespace K5BZI_Logger.Views
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : MetroWindow, IView
    {
        private readonly IMainModel _viewModel;

        public Main(IMainLoggerViewModel viewModel)
        {
            InitializeComponent();

            DataContext = _viewModel = viewModel.Model;

            _viewModel.CreateMockLogEntry();
        }

        private async void ButtonBaseOnClick(object sender, RoutedEventArgs e)
        {
            ProgressBar.Visibility = Visibility.Visible;

            _viewModel.SaveLogEntry();

            //await _dataContext.ProcessSelectionFileCommand.ExecuteAsync(null);

            //DataGridView1.DataSource = null;
            //DataGridView1.DataSource = _dataContext.DataTable;

            ProgressBar.Visibility = Visibility.Collapsed;
        }

    }
}
