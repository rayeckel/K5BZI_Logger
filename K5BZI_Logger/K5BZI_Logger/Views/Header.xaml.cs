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

            OperatorGrid.DataContext =
                App.ServiceProvider
                .GetRequiredService<IOperatorsViewModel>()
                .OperatorModel;

            EventGrid.DataContext =
                App.ServiceProvider
                .GetRequiredService<IEventViewModel>();
        }
    }
}
