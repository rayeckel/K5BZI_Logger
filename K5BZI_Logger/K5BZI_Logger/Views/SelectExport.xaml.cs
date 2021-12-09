using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.SimpleChildWindow;

namespace K5BZI_Logger.Views
{
    public partial class SelectExport : ChildWindow
    {
        public SelectExport()
        {
            InitializeComponent();

            DataContext = ((IExportViewModel)App.ServiceProvider
                .GetService(typeof(IExportViewModel)))
                .Model;
        }
    }
}
