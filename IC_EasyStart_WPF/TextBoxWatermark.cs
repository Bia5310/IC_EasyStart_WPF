using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace IC_EasyStart_WPF
{
    public class TextBoxWatermark : TextBox
    {
        public TextBoxWatermark()
        {
            
        }

        static TextBoxWatermark()
        {
            WatermarkProperty = DependencyProperty.Register("Watermark", typeof(string), typeof(TextBoxWatermark), new FrameworkPropertyMetadata(""));
            Color color = Colors.LightGray;
            SolidColorBrush brush = new SolidColorBrush(Colors.LightGray);
            brush.Opacity = 0.75;
            WatermarkForegroundProperty = DependencyProperty.Register("WatermarkForeground", typeof(Brush), typeof(TextBoxWatermark), new FrameworkPropertyMetadata(brush));
            WatermarkVisibilityProperty = DependencyProperty.Register("WatermarkVisibility", typeof(Visibility), typeof(TextBoxWatermark), new FrameworkPropertyMetadata(Visibility.Visible));
        }

        public static readonly DependencyProperty WatermarkProperty;
        public static readonly DependencyProperty WatermarkForegroundProperty;
        public static readonly DependencyProperty WatermarkVisibilityProperty;

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            SetValue(WatermarkVisibilityProperty, Text == ""? Visibility.Visible : Visibility.Collapsed);
        }

        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        public Brush WatermarkForeground
        {
            get { return (Brush)GetValue(WatermarkForegroundProperty); }
            set { SetValue(WatermarkForegroundProperty, value); }
        }

        public Visibility WatermarkVisibility
        {
            get
            {
                //return (Visibility)GetValue(WatermarkVisibilityProperty);
                if (Text == null || Text.Length == 0)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
            set
            {
                SetValue(WatermarkVisibilityProperty, value);
            }
        }
    }
}
