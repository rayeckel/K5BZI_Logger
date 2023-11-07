using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.SimpleChildWindow;
using Microsoft.Extensions.DependencyInjection;

namespace K5BZI_Logger.Views
{
    public partial class SelectExport : ChildWindow
    {
        public SelectExport()
        {
            InitializeComponent();

            DataContext = new
            {
                ExportModel = App.ServiceProvider
                    .GetRequiredService<IExportViewModel>()
                    .ExportModel,

                OperatorModel = App.ServiceProvider
                    .GetRequiredService<IOperatorViewModel>()
                    .OperatorModel
            };
        }
    }
}
