using Medical_Studio.Capture;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using TIS.Imaging;

namespace Medical_Studio.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private const string NameOfApplication = "Medical Studio";
        private const string AppVersion = "3.0 Beta";

        public IntPtr WindowHandle { get; set; } = IntPtr.Zero;

        DispatcherTimer updatePropertiesTimer = null;

        public static string CameraConfigsFolder = "Configs";

        static MainViewModel()
        {
            try
            {
                CameraConfigsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Medical Studio Configs";
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }

        public MainViewModel()
        {
            updatePropertiesTimer = new DispatcherTimer(DispatcherPriority.Background);
            updatePropertiesTimer.Tick += UpdatePropertiesTimer_Tick;
            updatePropertiesTimer.Interval = new TimeSpan(0, 0, 0, 0, 250); //ms
            updatePropertiesTimer.IsEnabled = false;
        }

        public string WindowTitle
        {
            get
            {
                string title = NameOfApplication;
                //string title = String.Format("{0} {1}", NameOfApplication, System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(3));
                /*if (DeviceValid)
                {
                    try
                    {
                        var frameType = icImagingControl.VideoFormatCurrent.FrameType;
                        return title + " " + String.Format("[{2} ({0}x{1}) {3:F2} fps]", frameType.Width, frameType.Height,
                            icImagingControl.DeviceCurrent.Name,
                            icImagingControl.DeviceFrameRate);
                    }
                    catch (Exception) { }
                }*/
                return title;
            }
        }

        private ICImagingControl icImagingControl = null;
        public ICImagingControl ICImagingControl
        {
            get => icImagingControl;
            set
            {
                if(icImagingControl != null)
                {
                    icImagingControl.DeviceLost -= IcImagingControl_DeviceLost;
                    icImagingControl.DeviceListChanged -= IcImagingControl_DeviceListChanged;
                }

                icImagingControl = value;

                if(icImagingControl != null)
                {
                    icImagingControl.DeviceLost += IcImagingControl_DeviceLost;
                    icImagingControl.DeviceListChanged += IcImagingControl_DeviceListChanged;
                }

                OnPropertyChanged("ICImagingControl");
            }
        }

        private void IcImagingControl_DeviceListChanged(object sender, EventArgs e)
        {
            try
            {
                if(icImagingControl.Devices.Length > 0)
                {
                    foreach(var device in icImagingControl.Devices)
                    {
                        if(device.Name == lostedDevice)
                        {
                            OpenCamera(lostedDevice);
                            //icImagingControl.Device = lostedDevice;
                            deviceLost = false;
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        private void IcImagingControl_DeviceLost(object sender, ICImagingControl.DeviceLostEventArgs e)
        {
            try
            {
                lostedDevice = e.Device;
                Settings.Default.LastDevice = lostedDevice;
                Settings.Default.Save();

                deviceLost = true;
                CloseLostDevice();
            }
            catch (Exception) { }
        }

        private bool muteSounds = false;
        public bool MuteSounds
        {
            get => muteSounds;
            set
            {
                muteSounds = value;
                OnPropertyChanged(nameof(MuteSounds));
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

        private string videoFileInfo = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
        public string VideoFileInfo
        {
            get => videoFileInfo;
            set
            {
                videoFileInfo = value;
                OnPropertyChanged("VideoFileInfo");
            }
        }

        private string photoFileInfo = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
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
                if (DeviceValid)
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

            try
            {
                icImagingControl.LiveStart();
            }
            catch(Exception ex)
            {
                icImagingControl.Sink = GetDefaultSink();
                icImagingControl.LiveStart();
            }
            
            OnPropertyChanged("IsLive");
        }

        private ISnapFrame snapFrame = null;

        private BaseSink GetDefaultSink()
        {
            SnapSinkListener ssl = new SnapSinkListener();
            BaseSink bs = new FrameNotificationSink(ssl);
            snapFrame = ssl;
            return bs;
        }

        public void StopLive()
        {
            if (!DeviceValid)
                return;

            icImagingControl.LiveStop();

            OnPropertyChanged("IsLive");
        }

        private BaseSink oldSink = null;
        private ISnapFrame oldSnapFrame = null;
        private MediaStreamSink mediaStreamSink = null;

        private VideoSinkListener videoSinkListener = null;
        private FrameNotificationSink frameNotificationSink = null;

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
            oldSnapFrame = snapFrame;

            videoSinkListener = PrepareVideoSinkListener();
            snapFrame = videoSinkListener;

            frameNotificationSink = new FrameNotificationSink(videoSinkListener);
            icImagingControl.Sink = frameNotificationSink;

            videoOnPause = false;
            videoCapturing = true;

            if (wasLive)
                StartLive();

            OnPropertyChanged("VideoOnPause");
            OnPropertyChanged("VideoCapturing");
        }

        public string VideoFileName { get; set; } = "video";
        public string PhotoFileName { get; set; } = "photo";

        private VideoSinkListener PrepareVideoSinkListener()
        {
            string baseName = BuildBaseMediaFileName();
            if (baseName == null)
                throw new InvalidOperationException("MediaFile base name error");

            string videoName = BuildFileNameWithoutExt(VideoFileInfo, baseName, "mp4");
            
            VideoFileName = videoName;

            DirectoryInfo dir = Directory.CreateDirectory(VideoFileInfo);
            string videoFullName = dir.FullName + '/' + VideoFileName;

            VideoSinkListener videoSinkListener = new VideoSinkListener(
                videoFullName,
                icImagingControl.VideoFormatCurrent.FrameType,
                (int)icImagingControl.DeviceFrameRate,
                encoder);
            
            return videoSinkListener;
        }

        private static string BuildFileNameWithoutExt(string directory, string baseName, string ext)
        {
            DirectoryInfo dir = Directory.CreateDirectory(directory);

            string tryname = "";
            for (int i = 0; ; ++i)
            {
                tryname = String.Format("{0} {1}.{2}", baseName, i, ext);
                if (!File.Exists(dir.FullName + '/' + tryname))
                {
                    return tryname;
                }
            }
        }

        private string BuildBaseMediaFileName()
        {
            string fio = !string.IsNullOrEmpty(FIOString) ? FIOString + " " : "";
            string H_num = !string.IsNullOrEmpty(HistoryNumberString) ? HistoryNumberString + " " : "";
            return fio + H_num + DateString;
        }

        public void StopVideoCapturing()
        {
            if (!DeviceValid)
                return;
            
            bool wasLive = IsLive;
            if (wasLive)
                StopLive();

            if(oldSink != null)
            {
                icImagingControl.Sink = oldSink;
                snapFrame = oldSnapFrame;
            }

            videoOnPause = false;
            videoCapturing = false;

            if (wasLive)
                StartLive();

            OnPropertyChanged("VideoOnPause");
            OnPropertyChanged("VideoCapturing");
        }

        public void TakeSnapshot()
        {
            string baseName = BuildBaseMediaFileName();
            if (baseName == null)
                throw new InvalidOperationException("MediaFile base name error");

            string snapName = BuildFileNameWithoutExt(PhotoFileInfo, baseName, "tiff");

            PhotoFileName = snapName;

            DirectoryInfo dir = Directory.CreateDirectory(PhotoFileInfo);
            string photoFullName = dir.FullName + '/' + PhotoFileName;
            try
            {
                snapFrame.SnapImage(photoFullName, Convert.ToInt32(Exposure * 1e3d) + 2000);
            }
            catch(Exception ex)
            {
            }
        }

        public void PauseVideoCapturing(bool pause)
        {
            if (DeviceValid)
            {
                if (videoSinkListener != null)
                {
                    videoSinkListener.Pause = pause;

                    videoOnPause = videoSinkListener.Pause;
                    OnPropertyChanged("VideoOnPause");
                }
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
                /*if (configKey == "")
                    return;*/

                configKey = value;
                LoadCurrentCameraConfig();
                OnPropertyChanged("ConfigKey");
            }
        }

        private string dateString = DateTime.Now.ToString();
        public string DateString { get => dateString; set { dateString = value; OnPropertyChanged("DateString"); } }
        private string fioString = "";
        public string FIOString { get => fioString; set { fioString = value; OnPropertyChanged("FIOString"); } }
        private string historyNumberString = "";
        public string HistoryNumberString { get => historyNumberString; set { historyNumberString = value; OnPropertyChanged("HistoryNumberString"); } }

        public bool IsLightTheme { get; set; } = false;

        public Dictionary<string, ConfigElement> configsDictionary = new Dictionary<string, ConfigElement>();
        public Dictionary<string, ConfigElement> ConfigsDictionary { get => configsDictionary; set { configsDictionary = value; OnPropertyChanged("ConfigsDictionary"); } } 

        public void LoadSettings()
        {
            DateString = DateTime.Now.ToString("ddMMyy");//Settings.Default.Date;
            HistoryNumberString = Settings.Default.HistoryNumber;
            FIOString = Settings.Default.FIO;

            IsLightTheme = Settings.Default.IsLightTheme;
            MuteSounds = Settings.Default.MuteSounds;

            if (Settings.Default.VideoPath != "")
                VideoFileInfo = Settings.Default.VideoPath;
            if(Settings.Default.PhotoPath != "")
                PhotoFileInfo = Settings.Default.PhotoPath;

            Encoder = new Capture.EncoderH264();
            encoder.Quality = Settings.Default.H264Quality;
            encoder.AverageBitrate = Settings.Default.H264Bitrate;
            encoder.Mode = Settings.Default.IsH264QualityMode ? Capture.EncoderH264.H264Modes.Quality : Capture.EncoderH264.H264Modes.AverageBitrate;

            LoadCodecAndCompressor();
        }

        public void SaveSettings()
        {
            Settings.Default.Date = DateString;
            Settings.Default.HistoryNumber = HistoryNumberString;
            Settings.Default.FIO = FIOString;
            Settings.Default.MuteSounds = MuteSounds;

            Settings.Default.IsLightTheme = IsLightTheme;

            Settings.Default.VideoPath = VideoFileInfo;
            Settings.Default.PhotoPath = PhotoFileInfo;
            
            //Settings.Default.AviCompressorName = aviCompressor?.Name ?? "";
            //Settings.Default.MediaContainerID = mediaStreamContainer?.ID ?? new Guid();

            //Settings.Default.LastConfigKey = configKey;

            if(encoder != null)
            {
                Settings.Default.H264Quality = encoder.Quality;
                Settings.Default.H264Bitrate = encoder.AverageBitrate;
                Settings.Default.IsH264QualityMode = encoder.Mode == Capture.EncoderH264.H264Modes.Quality;
            }

            Settings.Default.Save();

            if (icImagingControl.Device != null && icImagingControl.Device != "")
                SaveCurrentCameraConfig();// SaveConfigsDictionary(icImagingControl.Device);
        }

        public bool LoadCurrentCameraConfig()
        {
            if (icImagingControl == null)
                return false;

            bool wasLive = IsLive;
            if(wasLive)
                StopLive();

            string path = CameraConfigsFolder + '/' + ConfigsDictionary[configKey].ConfigPath;

            try
            {
                if (System.IO.File.Exists(path))
                {
                    icImagingControl.LoadDeviceStateFromFile(path, true);
                }

                if(DeviceValid)
                {
                    DeviceOpened?.Invoke();
                }

                CameraOpened();

                if (wasLive)
                    StartLive();
                //IsLive = true;
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public List<string> FindLastConnectedCameraConfigs()
        {
            List<string> cameraConfigs = new List<string>();

            System.IO.DirectoryInfo dirInfo = new DirectoryInfo(CameraConfigsFolder);
            if (!dirInfo.Exists)
                return cameraConfigs;

            var fileInfos = dirInfo.GetFiles();

            for(int i = 0; i < fileInfos.Length; i++)
            {
                if (fileInfos[i].Extension.ToLower() == ".xml" && fileInfos[i].Name.StartsWith("Configs_"))
                {
                    string cameraName = fileInfos[i].Name.Substring(8, fileInfos[i].Name.Length - 12);
                    cameraConfigs.Add(cameraName);
                }
            }

            return cameraConfigs;
        }

        public void SaveCurrentCameraConfig()
        {
            if (icImagingControl == null)
                return;
            if (!DeviceValid)
                return;
            if(configKey == null || configKey == "")
                return;

            bool wasLive = IsLive;
            if (wasLive)
                StopLive();

            string path = String.Format("{0}/{1}", CameraConfigsFolder, icImagingControl.Device); //путь до конфигов
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string configPath = String.Format("{0}/CameraConfig_{1}.xml", icImagingControl.Device, configKey); //путь от словаря до файла с конфигом
            configsDictionary[configKey].ConfigPath = configPath;

            icImagingControl.SaveDeviceStateToFile(CameraConfigsFolder + "/" + configPath);
            SaveConfigsDictionary(icImagingControl.Device);

            if (wasLive)
                StartLive();
        }

        private void LoadConfigsDictionary(string cameraName)
        {
            try
            {
                ConfigsDictionary = ConfigElement.LoadConfigsDictionary(String.Format(CameraConfigsFolder + "/" + ConfigElement.DictionaryFilePattern, cameraName), out configKey);
            }
            catch(Exception)
            {
                NewConfigsDictionary();
                configKey = "0_0";
            }
            OnPropertyChanged(nameof(ConfigKey));
        }

        private void NewConfigsDictionary()
        {
            ConfigsDictionary = ConfigElement.GetEmptyDictionary();
        }

        public bool DeviceValid
        {
            get => icImagingControl?.DeviceValid ?? false;
        }

        public bool OpenCamera(string device)
        {
            if (icImagingControl.DeviceValid)
            {
                CloseDevice();
            }

            bool deviceInList = false;
            for(int i = 0; i < icImagingControl.Devices.Length; i++)
            {
                if (icImagingControl.Devices[i].Name == device)
                {
                    deviceInList = true;
                    break;
                }
            }

            if(deviceInList)
            {
                //поиск и загрузка всех пресетов камеры, затем загр. конкретный пресет
                LoadConfigsDictionary(device);
                //ConfigKey = Settings.Default.LastConfigKey;                 //LoadCurrentCameraConfig();
                //OnPropertyChanged(nameof(ConfigKey));
            }

            if (!DeviceValid || !deviceInList)
            {
                ShowDevicePropsDialog();
            }

            if(DeviceValid)
            {
                Settings.Default.LastDevice = device;
                Settings.Default.Save();
                deviceLost = false;
                CameraOpened();
                StartLive();
            }

            return icImagingControl.DeviceValid;
        }

        private void CameraOpened()
        {
            if (DeviceValid)
            {
                bool wasLive = IsLive;
                if (IsLive)
                    IsLive = false;
                icImagingControl.Sink = GetDefaultSink();
                if (wasLive)
                    IsLive = true;

                InitCameraProperties();
                RefreshPropertiesValues();
                updatePropertiesTimer.Start();
            }

            OnPropertyChanged("DeviceValid");
            OnPropertyChanged("VideoOnPause");
            OnPropertyChanged("VideoCapturing");
            OnPropertyChanged("WindowTitle");
        }

        public event Action DeviceOpened;

        public void ShowDevicePropsDialog()
        {
            //bool wasLive = IsLive;
            //IsLive = false;
            //SaveCurrentCameraConfig();

            string wasDevice = icImagingControl.Device;

            CloseDevice();

            var result = icImagingControl.ShowDeviceSettingsDialog(WindowHandle);

            if (DeviceValid)
            {
                if(wasDevice == null || wasDevice == "" || wasDevice != icImagingControl.Device)
                {
                    LoadConfigsDictionary(icImagingControl.Device);
                    
                    configKey = "0_0";

                    if(!LoadCurrentCameraConfig())
                    {
                        SaveCurrentCameraConfig();
                    }
                }
                else
                    SaveCurrentCameraConfig();

                DeviceOpened?.Invoke();
                deviceLost = false;
            }

            CameraOpened();

            //if (wasLive)
            //    IsLive = wasLive;
            IsLive = true;
        }

        public TimeSpan VideoTimeSpan
        {
            get
            {
                if (videoCapturing)
                {
                    if(videoSinkListener != null)
                    {
                        return videoSinkListener.TimeSpan;
                    }
                }
                return new TimeSpan(0);
            }
        }

        public void ShowAdditionalDeviceProps()
        {
            icImagingControl.ShowPropertyDialog(WindowHandle);
        }

        private void UpdatePropertiesTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (icImagingControl != null && icImagingControl.DeviceValid)
                {
                    OnPropertyChanged("Exposure");
                    OnPropertyChanged("ExposureAuto");

                    OnPropertyChanged("Gain");
                    OnPropertyChanged("GainAuto");

                    OnPropertyChanged("WhiteBalanceAuto");
                }
                OnPropertyChanged("VideoTimeSpan");
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

        public void CloseDevice()
        {
            if(DeviceValid)
            {
                Settings.Default.LastDevice = icImagingControl.Device;
                Settings.Default.Save();
            }

            if (videoCapturing)
            {
                StopVideoCapturing();
            }

            if (IsLive)
                StopLive();
            
            SaveCurrentCameraConfig();

            updatePropertiesTimer.Stop();
            DeinitCameraProperties();
            deviceLost = false;
        }

        private bool deviceLost = false;
        private string lostedDevice = "";

        private void CloseLostDevice()
        {
            videoCapturing = false;
            videoOnPause = false;

            try
            {
                if (videoCapturing)
                {
                    if (videoSinkListener != null)
                        videoSinkListener.CloseWriter();
                }
            }
            catch (Exception) { }

            IsLive = false;

            updatePropertiesTimer.Stop();
            DeinitCameraProperties();

            OnPropertyChanged("DeviceValid");
            OnPropertyChanged("VideoOnPause");
            OnPropertyChanged("VideoCapturing");
            OnPropertyChanged("WindowTitle");
            RefreshPropertiesValues();

            SaveCurrentCameraConfig();
        }

        private Capture.EncoderH264 encoder = null;
        public Capture.EncoderH264 Encoder
        {
            get => encoder;
            set
            {
                encoder = value;
                OnPropertyChanged("Encoder");
            }
        }

        public void SaveConfigsDictionary(string cameraName)
        {
            ConfigElement.SaveConfigsDictionary(ConfigsDictionary, String.Format(CameraConfigsFolder + "/" + ConfigElement.DictionaryFilePattern, cameraName), configKey);
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
