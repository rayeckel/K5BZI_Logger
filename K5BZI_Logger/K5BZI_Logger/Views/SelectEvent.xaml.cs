using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.SimpleChildWindow;
using Microsoft.Practices.ServiceLocation;

namespace K5BZI_Logger.Views
{
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
