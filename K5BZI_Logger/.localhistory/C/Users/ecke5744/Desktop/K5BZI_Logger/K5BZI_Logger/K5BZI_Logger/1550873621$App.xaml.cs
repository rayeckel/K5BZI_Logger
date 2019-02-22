using System.Windows;
using Ninject;
using K5BZI_Logger.Views;

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

            Current.MainWindow = new StandardKernel(new K5BZIViewModelBindings(), new TMToolServiceBindings())
                .Get<Main>();

            Current.MainWindow.Show();
        }
    }
}
