using K5BZI_ViewModels.Interfaces;
using System.Windows.Controls;

namespace K5BZI_Logger.Views
{
    /// <summary>
    /// Interaction logic for MainHeader.xaml
    /// </summary>
    public partial class Header : UserControl
    {
        public Header(IOperatorsViewModel operatorsViewModel, IEventViewModel eventViewModel)
        {
            InitializeComponent();

            OperatorGrid.DataContext = operatorsViewModel.Model;

            EventGrid.DataContext = eventViewModel;
        }
    }
}
