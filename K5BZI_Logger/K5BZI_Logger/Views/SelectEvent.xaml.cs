using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.SimpleChildWindow;
using Microsoft.Extensions.DependencyInjection;

namespace K5BZI_Logger.Views
{
    public partial class SelectEvent : ChildWindow
    {
        public SelectEvent()
        {
            InitializeComponent();

            var eventViewModel = App.ServiceProvider.GetRequiredService<IEventViewModel>();

            DataContext = eventViewModel.Model;
        }
    }
}
