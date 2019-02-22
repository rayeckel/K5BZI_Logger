using System.Windows;

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

            Current.MainWindow = new StandardKernel(new TMToolViewModelBindings(), new TMToolServiceBindings())
                .Get<ProcessInvitations>();

            Current.MainWindow.Show();
        }
    }
}
