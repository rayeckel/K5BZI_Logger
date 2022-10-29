using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace K5BZI_Logger.Views
{
    public partial class OperatorsTab : MetroTabItem
    {
        public OperatorsTab()
        {
            InitializeComponent();

            var operatorsViewModel = App.ServiceProvider.GetRequiredService<IOperatorsViewModel>();

            DataContext = operatorsViewModel.Model;
        }
    }
}
