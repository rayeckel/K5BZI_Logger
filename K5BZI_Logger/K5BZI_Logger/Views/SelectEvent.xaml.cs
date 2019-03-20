using K5BZI_Logger.Views.Interfaces;
using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.Controls;
using Ninject;

namespace K5BZI_Logger.Views
{
    /// <summary>
    /// Interaction logic for SelectEvent.xaml
    /// </summary>
    public partial class SelectEvent : MetroWindow, IView
    {
        public SelectEvent()
        {
            InitializeComponent();

            var viewModel = App.Kernel.Get<ISelectEventViewModel>();

            DataContext = viewModel.Model;
        }
    }
}
