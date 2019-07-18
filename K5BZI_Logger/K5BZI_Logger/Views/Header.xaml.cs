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

            OperatorGrid.DataContext = ServiceLocator.Current
                .GetInstance<IOperatorsViewModel>()
                .Model;

            EventGrid.DataContext = ServiceLocator.Current
                .GetInstance<IEventViewModel>();
        }
    }
}
