using System.Windows;
using System.Windows.Controls;

namespace K5BZI_Services.Utilities
{
    public class TextboxBehavior : DependencyObject
    {
        public static readonly DependencyProperty ClearOnFocusedProperty = DependencyProperty.RegisterAttached(
               "ClearOnFocused",
               typeof(bool),
               typeof(TextboxBehavior),
               new PropertyMetadata(default(bool), PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var textBox = dependencyObject as TextBox;

            if (textBox != null)
            {
                if ((bool)dependencyPropertyChangedEventArgs.NewValue == true)
                {
                    textBox.GotFocus += TextBoxGotFocus;
                }
            }
        }

        private static void TextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox != null)
            {
                textBox.Text = "";
                /*
                if (textBox.Text.ToLower() == "search")
                {
                    
                }*/
            }
        }

        public static void SetClearOnFocused(DependencyObject element, bool value)
        {
            element.SetValue(ClearOnFocusedProperty, value);
        }

        public static bool GetClearOnFocused(DependencyObject element)
        {
            return (bool)element.GetValue(ClearOnFocusedProperty);
        }
    }
}
