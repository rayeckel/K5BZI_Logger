using K5BZI_ViewModels.Interfaces;
using Microsoft.Practices.ServiceLocation;
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

            var operatorsViewModel = ServiceLocator.Current.GetInstance<IOperatorsViewModel>();
            OperatorsGrid.DataContext = operatorsViewModel.Model;

            var eventViewModel = ServiceLocator.Current.GetInstance<IEventViewModel>();
            EventGrid.DataContext = eventViewModel;
        }
    }
}
