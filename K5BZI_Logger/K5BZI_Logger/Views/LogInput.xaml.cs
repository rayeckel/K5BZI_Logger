using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using K5BZI_ViewModels.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace K5BZI_Logger.Views
{
    public partial class LogInput : UserControl
    {
        public LogInput()
        {
            InitializeComponent();

            var viewModel = App.ServiceProvider.GetRequiredService<ILogViewModel>();

            DataContext = viewModel.LogModel;

            viewModel.LogModel.Timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                viewModel.LogModel.LogEntry.ContactTime = DateTime.Now;
            }, Dispatcher);

            Keyboard.Focus(lblCall);
        }

        public void SetFocusOnClick(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(lblCall);
        }
    }
}
