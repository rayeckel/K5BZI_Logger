using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.Controls;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace K5BZI_Logger.Views
{
    public partial class LogTab : MetroTabItem
    {
        public LogTab()
        {
            InitializeComponent();

            DataContext = App.ServiceProvider.GetRequiredService<IMainViewModel>();
        }
    }

    public class BindingProxy : Freezable
    {
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy), new UIPropertyMetadata(null));

        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }
    }
}
