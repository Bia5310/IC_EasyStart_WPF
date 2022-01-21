using Medical_Studio.ViewModels;
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
using System.Windows.Shapes;
using TIS.Imaging;

namespace Medical_Studio
{
    /// <summary>
    /// Interaction logic for CodecSettings.xaml
    /// </summary>
    public partial class CodecSettings : Window
    {
        private MainViewModel mainViewModel = null;

        public CodecSettings()
        {
            InitializeComponent();
            this.DataContextChanged += CodecSettings_DataContextChanged;
        }

        private void CodecSettings_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            mainViewModel = (MainViewModel)DataContext;
            FillContainerCombobox();
        }

        private void selectUsersContainer()
        {
            MediaStreamContainer[] containers = (MediaStreamContainer[])Cb_Containers.ItemsSource;

            if(mainViewModel.MediaStreamContainer == null)
            {
                if (Cb_Containers.Items.Count > 0)
                    Cb_Containers.SelectedIndex = 0;
                FillCodecsCombobox(containers[0]);
                return;
            }
            
            for(int i = 0; i < containers.Length; i++)
            {
                if(containers[i].ID == mainViewModel.MediaStreamContainer.ID)
                {
                    Cb_Containers.SelectedIndex = i;
                    FillCodecsCombobox(containers[i]);
                    return;
                }
            }

            if (Cb_Containers.Items.Count > 0)
                Cb_Containers.SelectedIndex = 0;
            FillCodecsCombobox(containers[0]);
        }

        private void selectUsersCodec()
        {
            List<AviCompressor> compressors = (List<AviCompressor>)Cb_Codecs.ItemsSource;

            if(mainViewModel.AviCompressor == null)
            {
                if (Cb_Codecs.Items.Count > 0)
                    Cb_Codecs.SelectedIndex = 0;
                return;
            }

            for (int i = 0; i < compressors.Count; i++)
            {
                if (compressors[i].Name == mainViewModel.AviCompressor.Name)
                {
                    Cb_Codecs.SelectedIndex = i;
                    return;
                }
            }

            if (Cb_Codecs.Items.Count > 0)
                Cb_Codecs.SelectedIndex = 0;
        }

        private void FillContainerCombobox(MediaStreamContainer mediaStreamContainer = null)
        {
            MediaStreamContainer[] containers = MediaStreamContainer.MediaStreamContainers;
            Cb_Containers.ItemsSource = containers;
            selectUsersContainer();
        }

        private void FillCodecsCombobox(MediaStreamContainer mediaStreamContainer)
        {
            AviCompressor[] compressors = AviCompressor.AviCompressors;
            List<AviCompressor> availableCompressors = new List<AviCompressor>();
            for(int i = 0; i < compressors.Length; i++)
            {
                if(mediaStreamContainer.IsCodecSupported(compressors[i]))
                {
                    availableCompressors.Add(compressors[i]);
                }
            }
            Cb_Codecs.ItemsSource = availableCompressors;
            selectUsersCodec();
        }

        private void Btn_OK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mainViewModel.AviCompressor = (AviCompressor)Cb_Codecs.SelectedItem;
                mainViewModel.MediaStreamContainer = (MediaStreamContainer)Cb_Containers.SelectedItem;
                DialogResult = true;
            }
            catch (Exception) { }
            Close();
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Btn_Preferences_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(Cb_Codecs.SelectedIndex >= 0)
                    ((AviCompressor)Cb_Codecs.SelectedItem).ShowPropertyPage();
            }
            catch (Exception) { }
        }

        private void Cb_Containers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Cb_Containers.SelectedIndex < 0)
                    return;

                MediaStreamContainer mediaStreamContainer = (MediaStreamContainer) Cb_Containers.SelectedItem;
                FillCodecsCombobox(mediaStreamContainer);
                selectUsersCodec();
            }
            catch(Exception) { }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Cb_Codecs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Cb_Codecs.SelectedIndex < 0)
                    return;

                Btn_Preferences.IsEnabled = ((AviCompressor)Cb_Codecs.SelectedItem).PropertyPageAvailable;
            }
            catch (Exception) { }
        }
    }
}
