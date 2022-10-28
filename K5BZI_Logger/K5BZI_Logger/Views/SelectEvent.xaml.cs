using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.SimpleChildWindow;

namespace K5BZI_Logger.Views
{
    public partial class SelectEvent : ChildWindow
    {
        public SelectEvent(IEventViewModel eventViewModel)
        {
            InitializeComponent();

            DataContext = eventViewModel.Model;
        }
    }
}
