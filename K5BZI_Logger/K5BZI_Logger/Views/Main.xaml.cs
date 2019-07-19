﻿using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.Controls;
using System;
using System.Windows.Threading;

namespace K5BZI_Logger.Views
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : MetroWindow
    {
        public Main(
            IMainViewModel viewModel,
            IOperatorsViewModel operatorsViewModel,
            IEventViewModel eventViewModel,
            IDefaultsViewModel defaultsViewModel)
        {
            InitializeComponent();

            DataContext = viewModel.Model;
            DefaultsButton.DataContext = defaultsViewModel.Model;
            EventsButton.DataContext = eventViewModel.EditModel;
            OperatorsButton.DataContext = operatorsViewModel.Model;

            var timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                viewModel.Model.LogEntry.ContactTime = DateTime.Now;
            }, Dispatcher);
        }
    }
}
