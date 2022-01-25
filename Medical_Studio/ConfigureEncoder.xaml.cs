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
    /// Логика взаимодействия для ConfigureEncoder.xaml
    /// </summary>
    public partial class ConfigureEncoder : Window
    {
        Capture.EncoderH264 encoderH264 = null;
        int oldBitrate = 0;
        int oldQuality = 0;

        public ConfigureEncoder()
        {
            InitializeComponent();
            this.Closing += ConfigureEncoder_Closing;
            this.DataContextChanged += ConfigureEncoder_DataContextChanged;
        }

        private void ConfigureEncoder_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(!DialogResult ?? false)
            {
                encoderH264.AverageBitrate = oldBitrate;
                encoderH264.Quality = oldQuality;
                DialogResult = false;
            }
        }

        private void ConfigureEncoder_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                encoderH264 = (Capture.EncoderH264)DataContext;
                oldBitrate = encoderH264.AverageBitrate;
                oldQuality = encoderH264.Quality;

                if (encoderH264.Mode == Capture.EncoderH264.H264Modes.AverageBitrate)
                    rb_bitrate.IsChecked = true;
                if (encoderH264.Mode == Capture.EncoderH264.H264Modes.Quality)
                    rb_quality.IsChecked = true;
            }
            catch (Exception) { }
        }

        private void Btn_OK_Click(object sender, RoutedEventArgs e)
        {
            if (rb_bitrate.IsChecked ?? false)
                encoderH264.Mode = Capture.EncoderH264.H264Modes.AverageBitrate;
            else if (rb_bitrate.IsChecked ?? false)
                encoderH264.Mode = Capture.EncoderH264.H264Modes.Quality;
            
            DialogResult = true;
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
