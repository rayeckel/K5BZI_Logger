using System.Windows.Controls;
using System.Windows.Input;

namespace K5BZI_Logger.Views
{
    /// <summary>
    /// Interaction logic for MainInput.xaml
    /// </summary>
    public partial class LogInput : UserControl
    {
        public LogInput()
        {
            InitializeComponent();

            Keyboard.Focus(lblCall);
        }
    }
}
