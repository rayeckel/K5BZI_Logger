using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.SimpleChildWindow;
using Microsoft.Practices.ServiceLocation;

namespace K5BZI_Logger.Views
{
    public partial class Operators : ChildWindow
    {
        public Operators()
        {
            InitializeComponent();

            var viewModel = ServiceLocator.Current.GetInstance<IOperatorsViewModel>();

            DataContext = viewModel.Model;
        }
    }
}
