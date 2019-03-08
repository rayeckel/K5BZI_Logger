using K5BZI_Logger.Views.Interfaces;
using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.Controls;

namespace K5BZI_Logger.Views
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : MetroWindow, IView
    {
        public Main(IMainLoggerViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel.Model;
        }
    }
}
