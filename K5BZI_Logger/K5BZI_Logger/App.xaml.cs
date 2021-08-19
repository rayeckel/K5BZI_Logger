using CommonServiceLocator;
using CommonServiceLocator.NinjectAdapter.Unofficial;
using Microsoft.Practices.ServiceLocation;
using K5BZI_Logger.Views;
using K5BZI_Services;
using K5BZI_ViewModels;
using Ninject;
using System;
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

            Kernel = new StandardKernel();

            Microsoft.Practices.ServiceLocation.ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(Kernel));

            Kernel.Load(new K5BZIServiceModelBindings(), new K5BZIViewModelBindings());

            Current.MainWindow = Kernel.Get<Main>();

            Current.MainWindow.Show();
        }
    }
}
