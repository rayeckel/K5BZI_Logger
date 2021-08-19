using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.SimpleChildWindow;
using CommonServiceLocator;

namespace K5BZI_Logger.Views
{
    /// <summary>
    /// Interaction logic for SelectEvent.xaml
    /// </summary>
    public partial class Defaults : ChildWindow
    {
        public Defaults()
        {
            InitializeComponent();

            DataContext = ServiceLocator.Current
                .GetInstance<IDefaultsViewModel>()
                .Model;
        }
    }
}
