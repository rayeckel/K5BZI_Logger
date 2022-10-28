using K5BZI_Logger.Views;
using K5BZI_Services;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels;
using K5BZI_ViewModels.Interfaces;
using Microsoft.Extensions.Configuration;
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
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<Main>();

            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Main>();
            services.AddSingleton<IMainViewModel, MainViewModel>();
            services.AddSingleton<IEventViewModel, EventViewModel>();
            services.AddSingleton<IOperatorsViewModel, OperatorsViewModel>();
            services.AddSingleton<IDefaultsViewModel, DefaultsViewModel>();
            services.AddSingleton<IEventService, EventService>();
            services.AddScoped<IFileStoreService, FileStoreService>();
            services.AddScoped<ILogListingService, LogListingService>();
            services.AddScoped<IOperatorService, OperatorService>();
            services.AddScoped<IExportService, ExportService>();
            services.AddScoped<IDefaultsService, DefaultsService>();
            services.AddScoped<IExcelFileService, ExcelFileService>();
            services.AddScoped<IExportViewModel, ExportViewModel>();
        }
    }
}