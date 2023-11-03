using System.Windows.Controls;
using K5BZI_ViewModels.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace K5BZI_Logger.Views
{
    public partial class Header : UserControl
    {
        public Header()
        {
            InitializeComponent();

            DataContext = new
            {
                OperatorModel = App.ServiceProvider
                    .GetRequiredService<IOperatorViewModel>()
                    .OperatorModel,

                EventModel = App.ServiceProvider
                    .GetRequiredService<IEventViewModel>()
                    .EventModel
            };
        }
    }
}
