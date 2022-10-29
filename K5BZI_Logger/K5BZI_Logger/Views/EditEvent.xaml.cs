using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.SimpleChildWindow;
using Microsoft.Extensions.DependencyInjection;

namespace K5BZI_Logger.Views
{
    /// <summary>
    /// Interaction logic for SelectEvent.xaml
    /// </summary>
    public partial class EditEvent : ChildWindow
    {
        public EditEvent()
        {
            InitializeComponent();

            var eventViewModel = App.ServiceProvider.GetRequiredService<IEventViewModel>();

            DataContext = eventViewModel.EditModel;
        }
    }
}
