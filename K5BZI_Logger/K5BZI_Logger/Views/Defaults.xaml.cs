using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.SimpleChildWindow;

namespace K5BZI_Logger.Views
{
    public partial class Defaults : ChildWindow
    {
        public Defaults()
        {
            InitializeComponent();

            DataContext = ((IDefaultsViewModel)App.ServiceProvider
                .GetService(typeof(IDefaultsViewModel)))
                .Model;
        }
    }
}
