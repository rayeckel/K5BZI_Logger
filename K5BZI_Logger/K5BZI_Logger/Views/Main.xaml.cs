using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.Controls;

namespace K5BZI_Logger.Views
{
    public partial class Main : MetroWindow
    {
        public Main(
            IOperatorViewModel operatorsViewModel,
            IEventViewModel eventViewModel,
            IDefaultsViewModel defaultsViewModel,
            ILogViewModel logViewModel,
            ISubmitViewModel submitViewModel)
        {
            InitializeComponent();

            ViewFileStoreButton.DataContext = eventViewModel.EventModel;
            DefaultsButton.DataContext = defaultsViewModel.Model;
            EventsButton.DataContext = eventViewModel.EventModel;
            OperatorsButton.DataContext = operatorsViewModel.OperatorModel;
            ExportLogButton.DataContext = logViewModel.LogModel;
            SubmitLogButton.DataContext = submitViewModel.SubmitModel;
        }
    }
}
