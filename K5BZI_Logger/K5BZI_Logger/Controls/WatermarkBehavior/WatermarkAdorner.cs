using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace K5BZI_Logger.Controls
{
    public class WaterMarkAdorner : Adorner
    {
        private string text;
        private double fontSize;
        private string fontFamily;
        private Brush foreground;

        public WaterMarkAdorner(UIElement element, string text, double fontsize, string font, Brush foreground)
            : base(element)
        {
            this.IsHitTestVisible = false;
            this.Opacity = 0.6;
            this.text = text;
            this.fontSize = fontsize;
            this.fontFamily = font;
            this.foreground = foreground;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            var text = new FormattedText(
                    this.text,
                    System.Globalization.CultureInfo.CurrentCulture,
                    System.Windows.FlowDirection.LeftToRight,
                    new System.Windows.Media.Typeface(fontFamily),
                    fontSize,
                    foreground);

            drawingContext.DrawText(text, new Point(3, 3));
        }
    }
}
