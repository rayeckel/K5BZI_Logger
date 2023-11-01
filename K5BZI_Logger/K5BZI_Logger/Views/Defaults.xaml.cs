using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.SimpleChildWindow;
using Microsoft.Extensions.DependencyInjection;

namespace K5BZI_Logger.Views
{
    public partial class Defaults : ChildWindow
    {
        public Defaults()
        {
            InitializeComponent();

            DataContext = App.ServiceProvider.GetRequiredService<IDefaultsViewModel>().Model;
        }
    }
}
