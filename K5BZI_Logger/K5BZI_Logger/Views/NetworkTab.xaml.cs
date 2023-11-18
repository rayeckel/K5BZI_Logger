using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace K5BZI_Logger.Views
{
    public partial class NetworkTab : MetroTabItem
    {
        public NetworkTab()
        {
            InitializeComponent();

            DataContext = App.ServiceProvider
                .GetRequiredService<INetworkViewModel>()
                .NetworkModel;
        }
    }
}
