using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.SimpleChildWindow;
using Microsoft.Practices.ServiceLocation;

namespace K5BZI_Logger.Views
{
    /// <summary>
    /// Interaction logic for SelectEvent.xaml
    /// </summary>
    public partial class SelectEvent : ChildWindow
    {
        public SelectEvent()
        {
            InitializeComponent();

            DataContext = ServiceLocator.Current
                .GetInstance<IEventViewModel>()
                .Model;
        }
    }
}
