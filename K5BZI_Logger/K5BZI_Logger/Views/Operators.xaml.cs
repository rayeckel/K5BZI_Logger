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

            DataContext = ServiceLocator.Current
                .GetInstance<IOperatorsViewModel>()
                .Model;
        }
    }
}
