using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.SimpleChildWindow;
using CommonServiceLocator;

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

            DataContext = ServiceLocator.Current
                .GetInstance<IEventViewModel>()
                .EditModel;
        }
    }
}
