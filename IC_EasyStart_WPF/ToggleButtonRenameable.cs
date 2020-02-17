using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace IC_EasyStart_WPF
{
    public class ToggleButtonRenameable : ToggleButton
    {
        public enum TextBoxThemes : int {Night, Day}

        private TextBoxThemes textBoxTheme = TextBoxThemes.Night;
        public TextBoxThemes TextBoxTheme
        {
            get { return textBoxTheme; }
            set
            {
                textBoxTheme = value;
                switch(value)
                {
                    case TextBoxThemes.Day:
                        
                        break;
                    case TextBoxThemes.Night:
                        
                        break;
                }
            }
        }
        
        private TextBox textBox = null;

        public static readonly DependencyProperty TextBoxProperty;
        public static readonly DependencyProperty TextBoxStyleProperty;
        public static readonly DependencyProperty TextBoxForegroundProperty;
        public static readonly DependencyProperty TextProperty;

        static ToggleButtonRenameable()
        {
            TextBoxProperty = DependencyProperty.Register("TextBox", typeof(TextBox), typeof(ToggleButtonRenameable));
        }

        public TextBox TextBox
        {
            get { return (TextBox) GetValue(TextBoxProperty); }
            set { SetValue(TextBoxProperty, textBox); }
        }

        public ToggleButtonRenameable() : base()
        {
            VerticalAlignment = System.Windows.VerticalAlignment.Center;
            HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
            
            textBox = new TextBox();
            textBox.Background = new SolidColorBrush(Colors.Transparent);
            textBox.IsEnabled = false;
            textBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            //textBox.Foreground = new SolidColorBrush(Colors.White);
            Content = textBox;
            
            textBox.LostFocus += TextBox_LostFocus;
            textBox.MouseDown += TextBox_MouseDown;
            textBox.KeyDown += TextBox_KeyDown1;
            MouseDown += TextBox_MouseDown;
            
        }

        private void TextBox_KeyDown1(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter || e.Key == Key.Escape)
            {
                ApplyChanges();
            }
        }

        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Right)
            {
                textBox.IsEnabled = true;
                textBox.Focus();
            }
        }

        private void TextBox_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            ApplyChanges();
        }

        private void ApplyChanges()
        {
            textBox.IsEnabled = false;
            OnApplyChanges?.Invoke(this, new RoutedEventArgs());
        }

        public delegate void OnApplyChangesHandler(object sender, RoutedEventArgs eventArgs);

        public event OnApplyChangesHandler OnApplyChanges;

        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //if(e.Equals(Mouse.))
        }
    }
}
