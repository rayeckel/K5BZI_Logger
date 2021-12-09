using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.SimpleChildWindow;

namespace K5BZI_Logger.Views
{
    public partial class EditEvent : ChildWindow
    {
        public EditEvent()
        {
            InitializeComponent();

            DataContext = ((IEventViewModel)App.ServiceProvider
                .GetService(typeof(IEventViewModel)))
                .Model;
        }
    }
}
