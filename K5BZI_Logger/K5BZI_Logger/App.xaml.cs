using System;
using System.Windows;
using K5BZI_Logger.Views;
using K5BZI_Services.Interfaces;
using K5BZI_Services.Services;
using K5BZI_ViewModels;
using K5BZI_ViewModels.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace K5BZI_Logger
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string Version = "1.0.11";

        public static IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            var services = new ServiceCollection();

            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDefaultsService, DefaultsService>();
            services.AddScoped<IExcelFileService, ExcelFileService>();
            services.AddScoped<IFileStoreService, FileStoreService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IOperatorService, OperatorService>();
            services.AddScoped<IExportService, ExportService>();
            services.AddSingleton<ISubmitService, SubmitService>();
            services.AddSingleton<IEventService, EventService>();
            services.AddSingleton<INetworkService, NetworkService>();

            services.AddScoped<IExportViewModel, ExportViewModel>();
            services.AddSingleton<IOperatorViewModel, OperatorViewModel>();
            services.AddSingleton<ISubmitViewModel, SubmitViewModel>();
            services.AddSingleton<IMainViewModel, MainViewModel>();
            services.AddSingleton<ILogViewModel, LogViewModel>();
            services.AddSingleton<IEventViewModel, EventViewModel>();
            services.AddSingleton<IDefaultsViewModel, DefaultsViewModel>();
            services.AddSingleton<INetworkViewModel, NetworkViewModel>();

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