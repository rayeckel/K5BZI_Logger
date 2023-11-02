﻿using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.SimpleChildWindow;
using Microsoft.Extensions.DependencyInjection;

namespace K5BZI_Logger.Views
{
    public partial class SelectEvent : ChildWindow
    {
        public SelectEvent()
        {
            InitializeComponent();

            DataContext = new
            {
                EventModel = App.ServiceProvider
                    .GetRequiredService<IEventViewModel>()
                    .EventModel,
                EditModel = App.ServiceProvider
                    .GetRequiredService<IEventViewModel>()
                    .EditModel
            };
        }
    }
}
