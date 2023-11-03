using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.SimpleChildWindow;
using Microsoft.Extensions.DependencyInjection;

namespace K5BZI_Logger.Views
{
    public partial class EditOperator : ChildWindow
    {
        public EditOperator()
        {
            InitializeComponent();

            DataContext =
                App.ServiceProvider.GetRequiredService<IOperatorViewModel>().OperatorModel;
        }
    }
}
