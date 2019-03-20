using K5BZI_ViewModels.Interfaces;
using Ninject;
using System.Windows.Controls;

namespace K5BZI_Logger.Views
{
    /// <summary>
    /// Interaction logic for SelectEvent.xaml
    /// </summary>
    public partial class SelectEvent : UserControl
    {
        public SelectEvent()
        {
            InitializeComponent();

            var viewModel = App.Kernel.Get<ISelectEventViewModel>();

            DataContext = viewModel.Model;
        }
    }
}
