using K5BZI_Logger.Views;
using K5BZI_Services;
using K5BZI_ViewModels;
using Ninject;
using System.Windows;

namespace K5BZI_Logger
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static StandardKernel Kernel { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Kernel = new StandardKernel(new K5BZIViewModelBindings(), new K5BZIServiceModelBindings()); ;

            Current.MainWindow = Kernel.Get<Main>();

            Current.MainWindow.Show();
        }
    }
}
