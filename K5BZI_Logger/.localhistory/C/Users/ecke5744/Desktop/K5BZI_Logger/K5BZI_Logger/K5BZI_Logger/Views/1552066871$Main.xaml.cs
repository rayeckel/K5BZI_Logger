﻿using System.Windows;
using K5BZI_Logger.Views.Interfaces;
using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.Controls;

namespace K5BZI_Logger.Views
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : MetroWindow, IView
    {
        private readonly IMainLoggerViewModel _dataContext;

        public Main(IMainLoggerViewModel viewModel)
        {
            InitializeComponent();

            //DataContext = _dataContext = viewModel.Model;
        }

        private async void ButtonBaseOnClick(object sender, RoutedEventArgs e)
        {
            ProgressBar.Visibility = Visibility.Visible;

            //await _dataContext.ProcessSelectionFileCommand.ExecuteAsync(null);

            //DataGridView1.DataSource = null;
            //DataGridView1.DataSource = _dataContext.DataTable;

            ProgressBar.Visibility = Visibility.Collapsed;
        }

    }
}
