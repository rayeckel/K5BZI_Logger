using K5BZI_ViewModels.Interfaces;
using System.Windows.Controls;

namespace K5BZI_Logger.Views
{
    public partial class Header : UserControl
    {
        public Header()
        {
            InitializeComponent();

            OperatorGrid.DataContext = ((IOperatorsViewModel)App.ServiceProvider
                .GetService(typeof(IOperatorsViewModel)))
                .Model;

            EventGrid.DataContext = ((IEventViewModel)App.ServiceProvider
                .GetService(typeof(IEventViewModel)));
        }
    }
}
