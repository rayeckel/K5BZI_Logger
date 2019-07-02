using K5BZI_Logger.Views.Interfaces;
using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.Controls;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Windows.Threading;

namespace K5BZI_Logger.Views
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : MetroWindow, IView
    {
        public Main(IMainLoggerViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel.Model;

            var operatorsViewModel = ServiceLocator.Current.GetInstance<IOperatorsViewModel>();

            OperatorsButton.DataContext = operatorsViewModel.Model;

            var timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                viewModel.Model.LogEntry.ContactTime = DateTime.Now;
            }, Dispatcher);
        }
    }
}
