using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.Controls;
using Microsoft.Practices.ServiceLocation;

namespace K5BZI_Logger.Views
{
    public partial class OperatorsGrid : MetroTabItem
    {
        public OperatorsGrid()
        {
            InitializeComponent();

            var viewModel = ServiceLocator.Current.GetInstance<IOperatorsViewModel>();

            DataContext = viewModel.Model;
        }
    }
}
