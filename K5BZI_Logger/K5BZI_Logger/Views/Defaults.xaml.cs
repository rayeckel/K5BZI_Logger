using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.SimpleChildWindow;
using Microsoft.Extensions.DependencyInjection;

namespace K5BZI_Logger.Views
{
    /// <summary>
    /// Interaction logic for SelectEvent.xaml
    /// </summary>
    public partial class Defaults : ChildWindow
    {
        public Defaults()
        {
            InitializeComponent();

            var defaultsViewModel = App.ServiceProvider.GetRequiredService<IDefaultsViewModel>();

            DataContext = defaultsViewModel.Model;
        }
    }
}
