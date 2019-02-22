using System.Windows;
using K5BZI_Logger.Views;
using K5BZI_Services;
using K5BZI_ViewModels;
using Ninject;

namespace K5BZI_Logger
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Current.MainWindow = new StandardKernel(new K5BZIViewModelBindings(), new K5BZIServiceModelBindings())
                .Get<Main>();

            Current.MainWindow.Show();
        }
    }
}
