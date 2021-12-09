using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.Controls;

namespace K5BZI_Logger.Views
{
    public partial class OperatorsTab : MetroTabItem
    {
        public OperatorsTab()
        {
            InitializeComponent();

            DataContext = ((IOperatorsViewModel)App.ServiceProvider
                .GetService(typeof(IOperatorsViewModel)))
                .Model;
        }
    }
}
