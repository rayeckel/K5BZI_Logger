using K5BZI_Logger.Controls;
using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace K5BZI_Logger.Controls
{
    public class WatermarkBehavior : Behavior<ComboBox>
    {
        private WaterMarkAdorner adorner;

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(WatermarkBehavior), new PropertyMetadata("Watermark"));


        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FontSizeProperty =
            DependencyProperty.Register("FontSize", typeof(double), typeof(WatermarkBehavior), new PropertyMetadata(12.0));


        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Foreground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(Brush), typeof(WatermarkBehavior), new PropertyMetadata(Brushes.Black));



        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FontFamily.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FontFamilyProperty =
            DependencyProperty.Register("FontFamily", typeof(string), typeof(WatermarkBehavior), new PropertyMetadata("Segoe UI"));


        protected override void OnAttached()
        {
            adorner = new WaterMarkAdorner(this.AssociatedObject, this.Text, this.FontSize, this.FontFamily, this.Foreground);

            this.AssociatedObject.Loaded += this.OnLoaded;
            this.AssociatedObject.GotFocus += this.OnFocus;
            this.AssociatedObject.LostFocus += this.OnLostFocus;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!this.AssociatedObject.IsFocused)
            {
                if (String.IsNullOrEmpty(this.AssociatedObject.Text))
                {
                    var layer = AdornerLayer.GetAdornerLayer(this.AssociatedObject);
                    try
                    {
                        layer.Add(adorner);
                    }
                    catch { }//Tab navigation causes the app to try to re-add the watermark resouce resulting in a crash
                }
            }
        }

        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(this.AssociatedObject.Text))
            {
                var layer = AdornerLayer.GetAdornerLayer(this.AssociatedObject);
                try
                {
                    layer.Add(adorner);
                }
                catch { }//Tab navigation causes the app to try to re-add the watermark resouce resulting in a crash
            }
        }

        private void OnFocus(object sender, RoutedEventArgs e)
        {
            var layer = AdornerLayer.GetAdornerLayer(this.AssociatedObject);
            try
            {
                layer.Remove(adorner);
            }
            catch { }//Tab navigation causes the app to try to re-add the watermark resouce resulting in a crash
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
        }
    }
}
