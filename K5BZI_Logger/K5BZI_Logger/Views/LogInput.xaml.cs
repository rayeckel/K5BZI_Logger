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

            var watermark1 = FindName("Watermark1");
            //Panel.SetZIndex(watermark1, 1);

            Keyboard.Focus(lblCall);
        }

        public void SetFocusOnClick(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(lblCall);
        }
    }
}
