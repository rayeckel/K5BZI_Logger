using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.Controls;
using Microsoft.Practices.ServiceLocation;

namespace K5BZI_Logger.Views
{
    public partial class OperatorsTab : MetroTabItem
    {
        public OperatorsTab()
        {
            InitializeComponent();

            var viewModel = ServiceLocator.Current.GetInstance<IOperatorsViewModel>();

            DataContext = viewModel.Model;
        }
    }
}
