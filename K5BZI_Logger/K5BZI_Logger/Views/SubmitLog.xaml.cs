using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.SimpleChildWindow;
using Microsoft.Extensions.DependencyInjection;

namespace K5BZI_Logger.Views
{
    public partial class SubmitLog : ChildWindow
    {
        public SubmitLog()
        {
            InitializeComponent();

            DataContext = new
            {
                EventModel = App.ServiceProvider.GetRequiredService<IEventViewModel>().EditModel,
                SubmitModel = App.ServiceProvider.GetRequiredService<ISubmitViewModel>().SubmitModel,
            };
        }
    }
}
