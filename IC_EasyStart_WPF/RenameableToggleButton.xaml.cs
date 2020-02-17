using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IC_EasyStart_WPF
{
    /// <summary>
    /// Логика взаимодействия для RenameableToggleButton.xaml
    /// </summary>
    public partial class RenameableToggleButton : UserControl
    {
        public RenameableToggleButton()
        {
            InitializeComponent();
            
            textBox.KeyDown += TextBox_KeyDown1;
            textBox.LostFocus += TextBox_LostFocus;
            textBox.MouseDown += TextBox_MouseDown;
            MouseDown += TextBox_MouseDown;
        }


        private void TextBox_KeyDown1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Escape)
            {
                ApplyChanges();
            }
        }

        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right)
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

    }
}
