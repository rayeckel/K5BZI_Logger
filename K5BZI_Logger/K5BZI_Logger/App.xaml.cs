using K5BZI_Logger.Views;
using K5BZI_Services;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels;
using K5BZI_ViewModels.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace K5BZI_Logger
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            var services = new ServiceCollection();

            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IFileStoreService, FileStoreService>();
            services.AddScoped<ILogListingService, LogListingService>();
            services.AddScoped<IOperatorService, OperatorService>();
            services.AddScoped<IExportService, ExportService>();
            services.AddScoped<IDefaultsService, DefaultsService>();
            services.AddScoped<IExcelFileService, ExcelFileService>();
            services.AddScoped<IExportViewModel, ExportViewModel>();
            services.AddSingleton<IMainViewModel, MainViewModel>();
            services.AddSingleton<IEventViewModel, EventViewModel>();
            services.AddSingleton<IOperatorsViewModel, OperatorsViewModel>();
            services.AddSingleton<IDefaultsViewModel, DefaultsViewModel>();
            services.AddSingleton<IEventService, EventService>();
            services.AddSingleton<Main>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = ServiceProvider.GetService<Main>();

            mainWindow.Show();

            base.OnStartup(e);
        }
    }
}