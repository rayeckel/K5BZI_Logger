using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace K5BZI_Logger.Views
{
    public partial class LogTab : MetroTabItem
    {
        public LogTab()
        {
            InitializeComponent();

            DataContext = App.ServiceProvider.GetRequiredService<ILogViewModel>();
        }
    }
}
