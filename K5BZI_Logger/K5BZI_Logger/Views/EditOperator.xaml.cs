using K5BZI_ViewModels.Interfaces;
using MahApps.Metro.SimpleChildWindow;

namespace K5BZI_Logger.Views
{
    /// <summary>
    /// Interaction logic for SelectEvent.xaml
    /// </summary>
    public partial class EditOperator : ChildWindow
    {
        public EditOperator(IOperatorsViewModel operatorsViewModel)
        {
            InitializeComponent();

            DataContext = operatorsViewModel.EditOperator;
        }
    }
}
