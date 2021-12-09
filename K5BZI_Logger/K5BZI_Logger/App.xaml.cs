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
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; internal set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ServiceProvider = ConfigureServices()
                .BuildServiceProvider();

            ServiceProvider.GetService<Main>()
                .Show();
        }

        private IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            services
                .AddSingleton<IMainViewModel, MainViewModel>()
                .AddSingleton<IEventViewModel, EventViewModel>()
                .AddSingleton<IOperatorsViewModel, OperatorsViewModel>()
                .AddSingleton<IDefaultsViewModel, DefaultsViewModel>()
                .AddSingleton<IExportViewModel, ExportViewModel>();

            services
                .AddSingleton<IEventService, EventService>()
                .AddSingleton<IFileStoreService, FileStoreService>()
                .AddSingleton<ILogListingService, LogListingService>()
                .AddSingleton<IOperatorService, OperatorService>()
                .AddSingleton<IExportService, ExportService>()
                .AddSingleton<IDefaultsService, DefaultsService>()
                .AddSingleton<IExcelFileService, ExcelFileService>();

            services.AddSingleton<Main>();

            return services;
        }
    }
}
