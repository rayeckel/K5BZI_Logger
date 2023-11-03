using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.SimpleChildWindow;
using Microsoft.Extensions.DependencyInjection;

namespace K5BZI_Logger.Views
{
    public partial class SubmitLog : ChildWindow
    {
        public SubmitLog()
        {
            InitializeComponent();

            DataContext = new
            {
                OperatorModel = App.ServiceProvider
                    .GetRequiredService<IOperatorViewModel>()
                    .OperatorModel,

                SubmitModel = App.ServiceProvider
                    .GetRequiredService<ISubmitViewModel>()
                    .SubmitModel
            };
        }
    }
}
