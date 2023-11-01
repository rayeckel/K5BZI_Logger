using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.SimpleChildWindow;
using Microsoft.Extensions.DependencyInjection;

namespace K5BZI_Logger.Views
{
    public partial class EditEvent : ChildWindow
    {
        public EditEvent()
        {
            InitializeComponent();

            DataContext = new
            {
                EventModel = App.ServiceProvider.GetRequiredService<IEventViewModel>().EditModel,

                OperatorModel = App.ServiceProvider.GetRequiredService<IOperatorsViewModel>().Model,
            };
        }
    }
}
