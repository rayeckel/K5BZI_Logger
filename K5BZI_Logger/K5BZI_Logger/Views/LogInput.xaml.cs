using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace K5BZI_Logger.Views
{
    public partial class LogInput : UserControl
    {
        public LogInput()
        {
            InitializeComponent();

            Keyboard.Focus(lblCall);
        }

        public void SetFocusOnClick(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(lblCall); // Or your own logic
        }
    }
}
