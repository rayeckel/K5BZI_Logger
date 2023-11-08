using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.SimpleChildWindow;
using Microsoft.Extensions.DependencyInjection;

namespace K5BZI_Logger.Views
{
    public partial class Park2Park : ChildWindow
    {
        public Park2Park()
        {
            InitializeComponent();

            DataContext = App.ServiceProvider
                    .GetRequiredService<ILogViewModel>()
                    .LogModel;
        }
    }
}
