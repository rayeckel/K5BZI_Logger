using K5BZI_ViewModels.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace K5BZI_Logger.Views
{
    /// <summary>
    /// Interaction logic for MainHeader.xaml
    /// </summary>
    public partial class Header : UserControl
    {
        public Header()
        {
            InitializeComponent();

            var operatorsViewModel = App.ServiceProvider.GetRequiredService<IOperatorsViewModel>();
            var eventViewModel = App.ServiceProvider.GetRequiredService<IEventViewModel>();

            OperatorGrid.DataContext = operatorsViewModel.Model;

            EventGrid.DataContext = eventViewModel;
        }
    }
}
