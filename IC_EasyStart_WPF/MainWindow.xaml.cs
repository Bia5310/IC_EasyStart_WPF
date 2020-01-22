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
using TIS.Imaging.VCDHelpers;
using TIS.Imaging;
using System.Windows.Forms;

namespace IC_EasyStart_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();

        }

        ICImagingControl IC_Control = null;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();

            IC_Control = new ICImagingControl();
            Host.Child = IC_Control;


            IC_Control.ShowDeviceSettingsDialog();
            if (IC_Control.DeviceValid) IC_Control.LiveStart();
        }
    }
}
