using System.Windows.Controls;
using K5BZI_ViewModels.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace K5BZI_Logger.Views
{
    public partial class Duplicates : UserControl
    {
        public Duplicates()
        {
            InitializeComponent();

            DataContext = App.ServiceProvider.GetRequiredService<ILogViewModel>().LogModel;
        }
    }
}
