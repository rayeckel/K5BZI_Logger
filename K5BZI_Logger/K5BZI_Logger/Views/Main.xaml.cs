using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.Controls;

namespace K5BZI_Logger.Views
{
    public partial class Main : MetroWindow
    {
        public Main(
            IMainViewModel viewModel,
            IOperatorsViewModel operatorsViewModel,
            IEventViewModel eventViewModel,
            IDefaultsViewModel defaultsViewModel)
        {
            InitializeComponent();

            DefaultsButton.DataContext = defaultsViewModel.Model;
            EventsButton.DataContext = eventViewModel.EditModel;
            OperatorsButton.DataContext = operatorsViewModel.OperatorsModel;
        }
    }
}
