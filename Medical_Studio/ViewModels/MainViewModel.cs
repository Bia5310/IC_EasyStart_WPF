using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Threading;
using TIS.Imaging;

namespace Medical_Studio.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private const string NameOfApplication = "Medical Studio";
        private const string AppVersion = "3.0 Beta";

        public string WindowTitle
        {
            get => String.Format("{0} {1}", NameOfApplication, AppVersion);
        }

        public void OnPropertyChanged(/*[CallerMemberName]*/string prop = "")
        {
            if(System.Windows.Application.Current.Dispatcher.CheckAccess())
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(prop));
                return;
            }
            else
            {
                System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
                {
                    OnPropertyChanged(prop);
                    return;
                }));
            }
        }

        private ICImagingControl icImagingControl = null;
        public ICImagingControl ICImagingControl
        {
            get => icImagingControl;
            set
            {
                icImagingControl = value;
                OnPropertyChanged("ICImagingControl");
            }
        }

        private AviCompressor aviCompressor = null;
        public AviCompressor AviCompressor
        {
            get => aviCompressor;
            set
            {
                aviCompressor = value;
                OnPropertyChanged("AviCompressor");
                OnPropertyChanged("AviCompressorName");
            }
        }
        public string AviCompressorName
        {
            get
            {
                if (aviCompressor != null)
                    return aviCompressor.Name;
                else
                    return null;
            }
        }

        private MediaStreamContainer mediaStreamContainer = null;
        public MediaStreamContainer MediaStreamContainer
        {
            get => mediaStreamContainer;
            set
            {
                mediaStreamContainer = value;
                OnPropertyChanged("MediaStreamContainer");
            }
        }

        private string videoFileInfo = null;
        public string VideoFileInfo
        {
            get => videoFileInfo;
            set
            {
                videoFileInfo = value;
                OnPropertyChanged("VideoFileInfo");
            }
        }

        private string photoFileInfo = null;
        public string PhotoFileInfo
        {
            get => photoFileInfo;
            set
            {
                photoFileInfo = value;
                OnPropertyChanged("PhotoFileInfo");
            }
        }

        private double scale = 1;
        public double Scale
        {
            get => scale;
            set
            {
                scale = value;
                OnPropertyChanged("Scale");
            }
        }

        private double scaleMax = 5;
        public double ScaleMax
        {
            get => scaleMax;
            set
            {
                scaleMax = value;
                OnPropertyChanged("ScaleMax");
            }
        }

        private double scaleMin = 0.1d;
        public double ScaleMin
        {
            get => scaleMin;
            set
            {
                scaleMin = value;
                OnPropertyChanged("ScaleMin");
            }
        }

        private double scaleIncrement = 0.01d; //10%
        public double ScaleIncrement
        {
            get => scaleIncrement;
            set
            {
                scaleIncrement = value;
                OnPropertyChanged("ScaleIncrement");
            }
        }

        private bool scaleAuto = true;
        public bool ScaleAuto
        {
            get => scaleAuto;
            set
            {
                scaleAuto = value;
                OnPropertyChanged("ScaleAuto");
            }
        }

        public double RoundZoomFactor(double zf)
        {
            if (zf > scaleMax)
                return scaleMax;
            else if (zf < scaleMin)
                return scaleMin;
            else
            {
                return (Math.Floor(zf / scaleIncrement) * scaleIncrement);
            }
        }

        public bool IsLive
        {
            get
            {
                if (!DeviceValid)
                    return false;

                return icImagingControl.LiveVideoRunning;
            }
            set
            {
                if (!DeviceValid)
                {
                    if (value)
                    {
                        StartLive();
                    }
                    else
                    {
                        StopLive();
                    }
                }
            }
        }

        private bool videoOnPause = false;
        private bool videoCapturing = false;

        public bool VideoCapturing
        {
            get => videoCapturing;
            set
            {
                if (!DeviceValid)
                    return;

                if(value)
                {
                    StartVideoCapturing();
                }
                else
                {
                    StopVideoCapturing();
                }
            }
        }

        public bool VideoOnPause
        {
            get => videoOnPause;
            set
            {
                if (!DeviceValid)
                    return;

                PauseVideoCapturing(value);
            }
        }

        public void StartLive()
        {
            if (!DeviceValid)
                return;

            icImagingControl.LiveStart();

            OnPropertyChanged("IsLive");
        }

        public void StopLive()
        {
            if (!DeviceValid)
                return;

            icImagingControl.LiveStop();

            OnPropertyChanged("IsLive");
        }

        private BaseSink oldSink = null;
        private MediaStreamSink mediaStreamSink = null;

        public void StartVideoCapturing()
        {
            if (!DeviceValid)
                return;

            if (videoCapturing)
                StopVideoCapturing();

            bool wasLive = IsLive;
            if(wasLive)
                StopLive();

            oldSink = icImagingControl.Sink;

            mediaStreamSink = PrepareVideoSink();
            icImagingControl.Sink = mediaStreamSink;
            mediaStreamSink.SinkModeRunning = true;

            videoOnPause = false;
            videoCapturing = true;

            if (wasLive)
                StartLive();

            OnPropertyChanged("VideoOnPause");
            OnPropertyChanged("VideoCapturing");
        }

        public MediaStreamSink PrepareVideoSink()
        {
            MediaStreamSink mediaStreamSink = new MediaStreamSink(mediaStreamContainer, aviCompressor, videoFileInfo);
            return mediaStreamSink;
        }

        public void StopVideoCapturing()
        {
            if (!DeviceValid)
                return;
            
            bool wasLive = IsLive;
            if (wasLive)
                StopLive();

            icImagingControl.Sink = oldSink;
            mediaStreamSink.Dispose();
            mediaStreamSink = null;

            videoOnPause = false;
            videoCapturing = false;

            if (wasLive)
                StartLive();

            OnPropertyChanged("VideoOnPause");
            OnPropertyChanged("VideoCapturing");
        }

        public void PauseVideoCapturing(bool pause)
        {
            if(DeviceValid)
                if(mediaStreamSink != null)
                {
                    mediaStreamSink.SinkModeRunning = pause;

                    videoOnPause = mediaStreamSink.SinkModeRunning;
                    OnPropertyChanged("VideoOnPause");
                }
        }

        private readonly string codecsFileName = "codecSettings.bin";

        private void LoadCodecAndCompressor()
        {
            if (icImagingControl == null)
                return;

            mediaStreamContainer = null;
            TIS.Imaging.MediaStreamContainer[] containers = TIS.Imaging.MediaStreamContainer.MediaStreamContainers;
            for(int i = 0; i < containers.Length; i++)
            {
                if(containers[i].ID == Settings.Default.MediaContainerID)
                {
                    mediaStreamContainer = containers[i];
                    break;
                }
            }

            TIS.Imaging.AviCompressor[] compressors =  TIS.Imaging.AviCompressor.AviCompressors;
            if(mediaStreamContainer != null)
            {
                for(int i = 0; i < compressors.Length; i++)
                {
                    if(compressors[i].Name == Settings.Default.AviCompressorName)
                    {
                        aviCompressor = compressors[i];
                        break;
                    }
                }
            }
            else
            {
                aviCompressor = null;
            }
        }

        public bool CheckCodecAndCompressorSupported()
        {
            if (mediaStreamContainer == null)
                return false;
            else
            {
                if (aviCompressor == null)
                    return false;

                if (!mediaStreamContainer.IsCodecSupported(aviCompressor))
                    return false;
                else
                    return true;
            }
        }

        private string configKey = "";
        public string ConfigKey
        {
            get => configKey;
            set
            {
                configKey = value;
                LoadCurrentConfig();
                OnPropertyChanged("ConfigKey");
            }
        }

        private string dateString = DateTime.Now.ToString();
        public string DateString { get => dateString; set { dateString = value; OnPropertyChanged("DateString"); } }
        private string fioString = "";
        public string FIOString { get => fioString; set { fioString = value; OnPropertyChanged("FIOString"); } }
        private string historyNumberString = "";
        public string HistoryNumberString { get => historyNumberString; set { historyNumberString = value; OnPropertyChanged("HistoryNumberString"); } }

        public Dictionary<string, ConfigElement> configsDictionary = new Dictionary<string, ConfigElement>();
        public Dictionary<string, ConfigElement> ConfigsDictionary { get => configsDictionary; set { configsDictionary = value; OnPropertyChanged("ConfigsDictionary"); } } 

        public void LoadSettings()
        {
            DateString = Settings.Default.Date;
            HistoryNumberString = Settings.Default.HistoryNumber;
            FIOString = Settings.Default.FIO;

            VideoFileInfo = Settings.Default.VideoPath;
            PhotoFileInfo = Settings.Default.PhotoPath;

            LoadCodecAndCompressor();

            LoadConfigsDictionary();
            ConfigKey = Settings.Default.LastConfigKey;
        }

        public void SaveSettings()
        {
            Settings.Default.Date = DateString;
            Settings.Default.HistoryNumber = HistoryNumberString;
            Settings.Default.FIO = FIOString;

            Settings.Default.VideoPath = VideoFileInfo;
            Settings.Default.PhotoPath = PhotoFileInfo;
            
            Settings.Default.AviCompressorName = aviCompressor?.Name ?? "";
            Settings.Default.MediaContainerID = mediaStreamContainer?.ID ?? new Guid();

            Settings.Default.LastConfigKey = configKey;

            Settings.Default.Save();
            SaveConfigsDictionary();
        }

        public void LoadCurrentConfig()
        {
            if (icImagingControl == null)
                return;

            bool wasLive = IsLive;
            if(wasLive)
                StopLive();

            string path = ConfigsDictionary[configKey].ConfigPath;
            if (System.IO.File.Exists(path))
            {
                icImagingControl.LoadDeviceStateFromFile(path, true);
            }

            if (wasLive)
                StartLive();
        }

        public void SaveCurrentConfig()
        {
            if (icImagingControl == null)
                return;

            bool wasLive = IsLive;
            if (wasLive)
                StopLive();

            string path = String.Format("CameraConfig_{0}.xml", configKey); ;
            configsDictionary[configKey].ConfigPath = path;

            if (System.IO.File.Exists(path))
            {
                icImagingControl.SaveDeviceStateToFile(path);
            }
            SaveConfigsDictionary();

            if (wasLive)
                StartLive();
        }

        private void LoadConfigsDictionary()
        {
            try
            {
                ConfigsDictionary = ConfigElement.LoadConfigsDictionary(ConfigElement.DictionaryFileName);
            }
            catch(Exception)
            {
                ConfigsDictionary = ConfigElement.GetEmptyDictionary();
            }
        }

        public bool DeviceValid
        {
            get => icImagingControl?.DeviceValid ?? false;
        }


        public bool OpenCamera(string deviceName = "")
        {
            if (icImagingControl.DeviceValid)
            {
                CloseDevice();
            }

            if (deviceName == null || deviceName == "")
            {
                IntPtr hwnd = new System.Windows.Interop.WindowInteropHelper(System.Windows.Application.Current.MainWindow).Handle;
                icImagingControl.ShowDeviceSettingsDialog(hwnd);
            }
            else
            {
                icImagingControl.Device = deviceName;
            }

            if(icImagingControl.DeviceValid)
            {
                InitCameraProperties();
            }

            OnPropertyChanged("DeviceValid");
            RefreshPropertiesValues();

            return icImagingControl.DeviceValid;
        }

        DispatcherTimer updatePropertiesTimer = null;
        private void StartPropertiesUpdateTimer()
        {
            updatePropertiesTimer = new DispatcherTimer(DispatcherPriority.Background);
            updatePropertiesTimer.Tick += UpdatePropertiesTimer_Tick;
            updatePropertiesTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000); //ms
            updatePropertiesTimer.IsEnabled = false;
            updatePropertiesTimer.Start();
        }

        private void UpdatePropertiesTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (icImagingControl != null && icImagingControl.DeviceValid)
                {
                    if (ExposureAuto)
                        OnPropertyChanged("Exposure");

                    if (GainAuto)
                        OnPropertyChanged("Gain");

                    if (WhiteBalanceAuto)
                        OnPropertyChanged("WhiteBalanceAuto");
                }
            }
            catch (Exception) { }
        }

        private void RefreshPropertiesValues()
        {
            OnPropertyChanged("Exposure");
            OnPropertyChanged("ExposureMax");
            OnPropertyChanged("ExposureMin");
            OnPropertyChanged("ExposureEnabled");
            OnPropertyChanged("ExposureAuto");
            OnPropertyChanged("ExposureAutoEnabled");

            OnPropertyChanged("Gain");
            OnPropertyChanged("GainMax");
            OnPropertyChanged("GainMin");
            OnPropertyChanged("GainEnabled");
            OnPropertyChanged("GainAuto");
            OnPropertyChanged("GainAutoEnabled");

            OnPropertyChanged("Brightness");
            OnPropertyChanged("BrightnessMax");
            OnPropertyChanged("BrightnessMin");
            OnPropertyChanged("BrightnessEnabled");
            OnPropertyChanged("BrightnessAuto");
            OnPropertyChanged("BrightnessAutoEnabled");

            OnPropertyChanged("WhiteBalanceEnabled");
            OnPropertyChanged("WhiteBalanceAuto");
            OnPropertyChanged("WhiteBalanceAutoEnabled");
        }

        private void CloseDevice()
        {
            if (videoCapturing)
                StopVideoCapturing();
            if (IsLive)
                StopLive();

            updatePropertiesTimer.Stop();
            DeinitCameraProperties();
        }

        public void SaveConfigsDictionary()
        {
            ConfigElement.SaveConfigsDictionary(ConfigsDictionary, ConfigElement.DictionaryFileName);
        }

        private void InitCameraProperties()
        {
            exposureAutoProperty = icImagingControl.VCDPropertyItems.Find<VCDSwitchProperty>(VCDGUIDs.VCDID_Exposure, VCDGUIDs.VCDElement_Auto);
            exposureAbsProperty = icImagingControl.VCDPropertyItems.Find<VCDAbsoluteValueProperty>(VCDGUIDs.VCDID_Exposure, VCDGUIDs.VCDElement_Value);

            gainAutoProperty = icImagingControl.VCDPropertyItems.Find<VCDSwitchProperty>(VCDGUIDs.VCDID_Gain, VCDGUIDs.VCDElement_Auto);
            gainAbsProperty = icImagingControl.VCDPropertyItems.Find<VCDAbsoluteValueProperty>(VCDGUIDs.VCDID_Gain, VCDGUIDs.VCDElement_Value);

            whiteBalanceProperty = icImagingControl.VCDPropertyItems.Find<VCDButtonProperty>(VCDGUIDs.VCDID_WhiteBalance, VCDGUIDs.VCDElement_OnePush);
            whiteBalanceAutoProperty = icImagingControl.VCDPropertyItems.Find<VCDSwitchProperty>(VCDGUIDs.VCDID_WhiteBalance, VCDGUIDs.VCDElement_Auto);
        }

        private void DeinitCameraProperties()
        {
            exposureAutoProperty = null;
            exposureAbsProperty = null;

            gainAutoProperty = null;
            gainAbsProperty = null;

            whiteBalanceProperty = null;
            whiteBalanceAutoProperty = null;
        }

        private TIS.Imaging.VCDSwitchProperty exposureAutoProperty;
        private TIS.Imaging.VCDAbsoluteValueProperty exposureAbsProperty;

        private TIS.Imaging.VCDSwitchProperty gainAutoProperty;
        private TIS.Imaging.VCDAbsoluteValueProperty gainAbsProperty;

        private TIS.Imaging.VCDButtonProperty whiteBalanceProperty;
        private TIS.Imaging.VCDSwitchProperty whiteBalanceAutoProperty;

        #region Camera Propertyes Fields

        //Exposure

        public bool ExposureAutoEnabled
        {
            get => exposureAutoProperty != null;//?.Available ?? false;
        }

        public bool ExposureEnabled
        {
            get => exposureAbsProperty != null;//?.Available ?? false;
        }

        public bool ExposureAuto
        {
            get => exposureAutoProperty?.Switch ?? false;
            set
            {
                try
                {
                    if (exposureAutoProperty != null && exposureAutoProperty.Switch != value)
                    {
                        exposureAutoProperty.Switch = value;
                        OnPropertyChanged("ExposureAuto");
                    }
                }
                catch (Exception) { }
            }
        }

        public double Exposure
        {
            get
            {
                try
                {
                    return exposureAbsProperty?.Value ?? 0;
                }
                catch (Exception) { }
                return 0;
            }
            set
            {
                try
                {
                    if (exposureAbsProperty != null && exposureAbsProperty.Value != value)
                    {
                        exposureAbsProperty.Value = value;
                        OnPropertyChanged("Exposure");
                    }
                }
                catch (Exception) { }
            }
        }

        public double ExposureMax
        {
            get => exposureAbsProperty?.RangeMax ?? 0;
        }

        public double ExposureMin
        {
            get => exposureAbsProperty?.RangeMin ?? 0;
        }

        //Gain

        public bool GainAutoEnabled
        {
            get => gainAutoProperty != null;//?.Available ?? false;
        }

        public bool GainEnabled
        {
            get => gainAbsProperty != null;//?.Available ?? false;
        }

        public bool GainAuto
        {
            get => gainAutoProperty?.Switch ?? false;
            set
            {
                try
                {
                    if (gainAutoProperty != null && gainAutoProperty.Switch != value)
                    {
                        gainAutoProperty.Switch = value;
                        OnPropertyChanged("GainAuto");
                    }
                }
                catch (Exception) { }
            }
        }

        public double Gain
        {
            get => gainAbsProperty?.Value ?? 0;
            set
            {
                try
                {
                    if (gainAbsProperty != null && gainAbsProperty.Value != value)
                    {
                        gainAbsProperty.Value = value;
                        OnPropertyChanged("Gain");
                    }
                }
                catch (Exception) { }
            }
        }

        public double GainMax
        {
            get => gainAbsProperty?.RangeMax ?? 0;
        }

        public double GainMin
        {
            get => gainAbsProperty?.RangeMin ?? 0;
        }

        //White Balance

        public bool WhiteBalanceAutoEnabled
        {
            get => whiteBalanceAutoProperty != null;//?.Available ?? false;
        }

        public bool WhiteBalanceEnabled
        {
            get => whiteBalanceProperty != null;//?.Available ?? false;
        }

        public bool WhiteBalanceAuto
        {
            get => whiteBalanceAutoProperty?.Switch ?? false;
            set
            {
                try
                {
                    if (whiteBalanceAutoProperty != null && whiteBalanceAutoProperty.Switch != value)
                    {
                        if (!WhiteBalanceAuto)
                            whiteBalanceProperty.Push();
                        else
                            whiteBalanceAutoProperty.Switch = value;
                        OnPropertyChanged("WhiteBalanceAuto");
                    }
                }
                catch (Exception) { }
            }
        }

        #endregion

    }
}
