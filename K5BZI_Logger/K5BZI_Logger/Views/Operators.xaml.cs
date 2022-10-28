using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.SimpleChildWindow;

namespace K5BZI_Logger.Views
{
    public partial class Operators : ChildWindow
    {
        public Operators(IOperatorsViewModel operatorsViewModel)
        {
            InitializeComponent();

            DataContext = operatorsViewModel.Model;
        }
    }
}
