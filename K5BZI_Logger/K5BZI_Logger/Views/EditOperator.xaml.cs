using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.SimpleChildWindow;

namespace K5BZI_Logger.Views
{
    public partial class EditOperator : ChildWindow
    {
        public EditOperator()
        {
            InitializeComponent();

            DataContext = ((IOperatorsViewModel)App.ServiceProvider
                .GetService(typeof(IOperatorsViewModel)))
                .Model;
        }
    }
}
