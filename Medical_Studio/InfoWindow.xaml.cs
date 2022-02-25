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
using System.Windows.Shapes;

namespace Medical_Studio
{
    /// <summary>
    /// Interaction logic for InfoWindow.xaml
    /// </summary>
    public partial class InfoWindow : Window
    {
        public InfoWindow()
        {
            InitializeComponent();
            version.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
#if X64
            version.Text += " (x64)";
#else
            version.Text += " (x86)";
#endif
        }

        private void Btn_OK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
