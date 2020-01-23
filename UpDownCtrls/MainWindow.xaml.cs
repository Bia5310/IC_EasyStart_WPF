using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UpDownCtrls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Can also add valid time entries this way:
            TimeCtrl_3.Children.Add(new ValidTimeItem(new TimeEntry(20,02,17), new TimeEntry(21,11,49)));
        }
        void ChangeTimeCtrlAlignment(TimeCtrl tc)
        {
            if (tc.TextAlignment == TextAlignment.Left)
                tc.TextAlignment = TextAlignment.Center;
            else if (tc.TextAlignment == TextAlignment.Center)
                tc.TextAlignment = TextAlignment.Right;
            else
                tc.TextAlignment = TextAlignment.Left;
        }
        void ChangeNumericUpDownAlignment(NumericUpDown nud)
        {
            if (nud.TextAlignment == TextAlignment.Left)
                nud.TextAlignment = TextAlignment.Center;
            else if (nud.TextAlignment == TextAlignment.Center)
                nud.TextAlignment = TextAlignment.Right;
            else
                nud.TextAlignment = TextAlignment.Left;
        }
        private void btChangeAlign_Click(object sender, RoutedEventArgs e)
        {
            ChangeTimeCtrlAlignment(TimeCtrl_1);
            ChangeTimeCtrlAlignment(TimeCtrl_2);
            ChangeTimeCtrlAlignment(TimeCtrl_3);
            ChangeNumericUpDownAlignment(NumericUpDown_1);
            ChangeNumericUpDownAlignment(NumericUpDown_2);
        }
        private void btDisEnAll_Click(object sender, RoutedEventArgs e)
        {
            TimeCtrl_1.IsEnabled = !TimeCtrl_1.IsEnabled;
            TimeCtrl_2.IsEnabled = !TimeCtrl_2.IsEnabled;
            TimeCtrl_3.IsEnabled = !TimeCtrl_3.IsEnabled;
            NumericUpDown_1.IsEnabled = !NumericUpDown_1.IsEnabled;
            NumericUpDown_2.IsEnabled = !NumericUpDown_2.IsEnabled;

            btDisEnAll.Content = (TimeCtrl_1.IsEnabled) ? "Disable all" : "Enable all";
        }
        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
