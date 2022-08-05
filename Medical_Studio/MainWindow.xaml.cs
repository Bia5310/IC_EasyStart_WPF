using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using TIS.Imaging.VCDHelpers;
using TIS.Imaging;
using LDZ_Code;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using System.Runtime.ExceptionServices;
using System.Windows.Interop;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Medical_Studio
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ICImagingControl IC_Control = null;

        string Config_tag = "0_0"; //0_0 - default
        string LastConfig_tag = "0_0";

        string App_cfg_name = "App_prop.cfg";
        string ConfigNames_filename = "ConfigNames.xml";

        int IMG_W_now = 0;
        int IMG_H_now = 0;

        //FS mode
        List<float> Widths_Cols = new List<float>();
        List<float> Heights_Rows = new List<float>();
        bool FullScrin = false;

        bool Everything_loaded = false;

        int seconds2write = 120 * 60;
        System.Diagnostics.Stopwatch stw = new System.Diagnostics.Stopwatch();

        ServiceFunctions.UI.Log.FileLogger FLog;
        System.Diagnostics.Stopwatch STW_fps = new System.Diagnostics.Stopwatch();

        //переменные для отрисовки. Ненужное - удалить
        int width_im = 1920, height_im = 1080;
        int WH = 1920 * 1080;
        int WH_camera = 1920 * 1080;
        int WH_camera_RGB = 3 * 1920 * 1080;
        Bitmap GplusR_res = new Bitmap(1920, 1080, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        static double coeff = 0.1;
        int coeff256 = (int)(coeff * 256);
        double Scaling_of_monitor = 1;

        BitmapData bd_res_r;
        Bitmap pRes_r;
        Bitmap dest_r = null;
        int sk = 0;

        int BytesPerLine;
        TIS.Imaging.ImageBuffer ImgBuffer_RGB;

        string Device_name = "";
        string Device_state = null;

        ViewModels.MainViewModel mainViewModel = null;

        //Timers
        DispatcherTimer TimerForRenew = null;
        DispatcherTimer WhiteBalanceTimer = null;
        DispatcherTimer Timer_recording = null;
        DispatcherTimer T1 = null;
        DispatcherTimer Timer_camera_checker = null;
        //Background Workers
        BackgroundWorker BGW_LiveVideoRecording = null;
        BackgroundWorker BGW_CamRestarter = null;

        System.Windows.Forms.Button B_FS_Switcher_form = null;
        System.Windows.Forms.Panel Panel = null;

        public static RoutedCommand FullScreenRoutedCommand = new RoutedCommand();
        public static RoutedCommand QuiteRoutedCommand = new RoutedCommand();
        public static RoutedCommand CRoutedCommand = new RoutedCommand();
        public static RoutedCommand VideoRecordCommand = new RoutedCommand();
        public static RoutedCommand VideoPauseCommand = new RoutedCommand();
        public static RoutedCommand VideoStopCommand = new RoutedCommand();
        public static RoutedCommand TakePhotoCommand = new RoutedCommand();

        System.Drawing.Image bmpFS_on = null;
        System.Drawing.Image bmpFS_off = null;

        private List<RenameableToggleButton> renameableButtonsConfigs = new List<RenameableToggleButton>();

        private Dictionary<string, string> ConfigsNamesDictionary = new Dictionary<string, string>();

        IntPtr windowHandle = IntPtr.Zero;

        private GlobalKeyboardHook globalKeyboardHook = new GlobalKeyboardHook();

        public MainWindow()
        {
            Directory.CreateDirectory("Logs");
            FLog = new ServiceFunctions.UI.Log.FileLogger("Logs\\Log_" + ServiceFunctions.UI.Get_TimeNow_String() + ".txt");
            FLog.Log("Programm started...");

            //Init timers
            TimerForRenew = new DispatcherTimer();
            TimerForRenew.Interval = new TimeSpan(0, 0, 0, 0, 100); //100 ms
            TimerForRenew.Tick += TimerForRenew_Tick;
            TimerForRenew.IsEnabled = false;

            WhiteBalanceTimer = new DispatcherTimer();
            WhiteBalanceTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000); //1000 ms
            WhiteBalanceTimer.Tick += WhiteBalanceTimer_Tick;
            WhiteBalanceTimer.IsEnabled = false;

            Timer_recording = new DispatcherTimer();
            Timer_recording.Interval = new TimeSpan(0, 0, 0, 0, 100);
            Timer_recording.Tick += Timer_recording_Tick;
            Timer_recording.IsEnabled = false;

            T1 = new DispatcherTimer();
            T1.Interval = new TimeSpan(0, 0, 0, 0, 50);
            T1.Tick += T1_Tick;
            T1.IsEnabled = false;

            Timer_camera_checker = new DispatcherTimer();
            Timer_camera_checker.Interval = new TimeSpan(0, 0, 0, 0, 100);
            Timer_camera_checker.Tick += Timer_camera_checker_Tick;
            Timer_camera_checker.IsEnabled = false;// true;

            BGW_LiveVideoRecording = new BackgroundWorker();
            BGW_LiveVideoRecording.DoWork += BGW_LiveVideoRecording_DoWork;
            BGW_LiveVideoRecording.WorkerSupportsCancellation = true;
            BGW_LiveVideoRecording.WorkerReportsProgress = false;

            BGW_CamRestarter = new BackgroundWorker();
            //BGW_CamRestarter.DoWork += BGW_CamRestarter_DoWork;
            //BGW_CamRestarter.WorkerSupportsCancellation = false;
            //BGW_CamRestarter.WorkerReportsProgress = false;

            InitializeComponent();

            windowHandle = new WindowInteropHelper(this).EnsureHandle();

            //Set main view model
            mainViewModel = new ViewModels.MainViewModel();
            mainViewModel.WindowHandle = windowHandle;
            mainViewModel.DeviceOpened += WhenDeviceOpened;

            DataContext = mainViewModel;
            mainViewModel.PropertyChanged += MainViewModel_PropertyChanged;

            ChB_WhiteBalanceAuto.Checked += ChB_WhiteBalanceAuto_CheckedChanged;
            ChB_WhiteBalanceAuto.Unchecked += ChB_WhiteBalanceAuto_CheckedChanged;

            FullScreenRoutedCommand.InputGestures.Add(new KeyGesture(Key.F, ModifierKeys.Alt));
            QuiteRoutedCommand.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Alt));
            CRoutedCommand.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Alt));
            VideoRecordCommand.InputGestures.Add(new KeyGesture(Key.R, ModifierKeys.Alt));
            VideoPauseCommand.InputGestures.Add(new KeyGesture(Key.P, ModifierKeys.Alt));
            VideoStopCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Alt));
            TakePhotoCommand.InputGestures.Add(new KeyGesture(Key.I, ModifierKeys.Alt));

            /*CommandBindings.Add(new CommandBinding(FullScreenRoutedCommand, FullScreenSwitchKey));
            CommandBindings.Add(new CommandBinding(QuiteRoutedCommand, QuiteKey));
            CommandBindings.Add(new CommandBinding(CRoutedCommand, CodecPropKey));
            CommandBindings.Add(new CommandBinding(VideoRecordCommand, commandRecordVideoMethod));
            CommandBindings.Add(new CommandBinding(VideoPauseCommand, commandPauseVideoMethod));
            CommandBindings.Add(new CommandBinding(VideoStopCommand, commandStopVideoMethod));
            CommandBindings.Add(new CommandBinding(TakePhotoCommand, commandTakePhotoMethod));*/

            globalKeyboardHook.KeyboardPressed += GlobalKeyboardHook_KeyboardPressed;

            B_FS_Switcher_form = Host.Child.Controls[0] as System.Windows.Forms.Button; //new System.Windows.Forms.Button();
            B_FS_Switcher_form.ForeColor = System.Drawing.Color.Gray;

            bmpFS_on = Bitmap.FromFile("FS_on_form.png");
            bmpFS_off = Bitmap.FromFile("FS_off_form.png");

            try
            {
                mainViewModel.LoadSettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            try
            {
                InitICCaptureControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public enum Keys : int { 
            None = 0, 
            Alt = 56, 
            Ctrl = 29,
            P = 25,
            R = 19, 
            Q = 16, 
            C = 46, 
            F = 33, 
            S = 31,
            I = 23 
        };

        private bool altPressed = false;
        private void GlobalKeyboardHook_KeyboardPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            switch(e.KeyboardState)
            {
                case GlobalKeyboardHook.KeyboardState.KeyDown:
                case GlobalKeyboardHook.KeyboardState.SysKeyDown:
                    if (altPressed)
                    {
                        e.Handled = true;
                        switch((Keys)e.KeyboardData.HardwareScanCode)
                        {
                            case Keys.F:
                                FullScreenSwitchKey(null, null);
                                break;
                            case Keys.Q:
                                QuiteKey(null, null);
                                break;
                            case Keys.C:
                                CodecPropKey(null, null);
                                break;
                            case Keys.I:
                                commandTakePhotoMethod(null, null);
                                break;
                            case Keys.R:
                                commandRecordVideoMethod(null, null);
                                break;
                            case Keys.S:
                                commandStopVideoMethod(null, null);
                                break;
                            case Keys.P:
                                commandPauseVideoMethod(null, null);
                                break;
                            default:
                                e.Handled = false;
                                break;
                        }
                    }

                    if (e.KeyboardData.HardwareScanCode == (int)Keys.Alt)
                        altPressed = true;
                    break;
                case GlobalKeyboardHook.KeyboardState.SysKeyUp:
                case GlobalKeyboardHook.KeyboardState.KeyUp:
                    if (e.KeyboardData.HardwareScanCode == (int)Keys.Alt)
                        altPressed = false;
                    break;
            }
            /*Keys key = (Keys)e.KeyboardData.HardwareScanCode;
            if(altPressed && (key == Keys.C || key == Keys.F || key == Keys.I || key == Keys.P || key == Keys.Q || key == Keys.R || key == Keys.S))
                e.Handled = true;
            e.Handled = false;*/
        }

        private void MainViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(mainViewModel.Scale))
            {
                //Set Zoom Factor
                SetLiveDisplayZoomFactor((float)mainViewModel.Scale);
            }
            if(e.PropertyName == nameof(mainViewModel.ConfigKey))
            {
                //mainViewModel.LoadCurrentCameraConfig();
                /*noEvents = true;

                for(int i = 0; i < renameableButtonsConfigs.Count; i++)
                {
                    if((string)renameableButtonsConfigs[i].Tag == mainViewModel.ConfigKey)
                    {
                        renameableButtonsConfigs[i].IsChecked = true;
                        if(stackPanelUserConfigs.Children.Contains(renameableButtonsConfigs[i]))
                        {
                            tabUser.IsSelected = true;
                        }
                        else if(stackPanelPhacoButtons.Children.Contains(renameableButtonsConfigs[i]))
                        {
                            tabPhaco.IsSelected = true;
                        }
                        else if(stackPanelVitreoButtons.Children.Contains(renameableButtonsConfigs[i]))
                        {
                            tabVitreo.IsSelected = true;
                        }
                    }
                    else
                    {
                        renameableButtonsConfigs[i].IsChecked = false;
                    }
                }

                noEvents = false;*/
            }
        }

        private void SetLiveDisplayZoomFactor(float zoomFactor)
        {
            IC_Control.LiveDisplayZoomFactor = zoomFactor;
            AdaptViewportControl();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;//  FormWindowState.Maximized;

            try
            {
                //B_FS_Switcher_form.UseVisualStyleBackColor = true;
                B_FS_Switcher_form.BackgroundImage = System.Drawing.Image.FromFile("FS_on_form.png");

                Scaling_of_monitor = GetScalingFactor_ofMonitor();
            }
            catch (Exception exc)
            {
                FLog.Log("ERROR:" + exc.Message);
            }

            try
            {
                TryOpenCamera();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            Everything_loaded = true;
            FLog.Log("Initialization end");

            try
            {
                toggleTheme.IsChecked = mainViewModel.IsLightTheme;
            }
            catch (Exception) { }
        }

        private void InitICCaptureControl()
        {
            try
            {
                TIS.Imaging.LibrarySetup.SetLocalizationLanguage("ru");

                IC_Control = new ICImagingControl();

                //Setup render view

                Host.Child.Controls.Add(IC_Control);
                
                IC_Control.SendToBack();
                //IC_Control.OverlayBitmapAtPath[PathPositions.Display].Enable = true;
                Panel = Host.Child as System.Windows.Forms.Panel;
                Refresh_IC_BackColor();

                mainViewModel.ICImagingControl = IC_Control;

                mainViewModel.ICImagingControl.ScrollbarsEnabled = true;
                mainViewModel.ICImagingControl.LiveDisplayDefault = false; //если false, то позволяет изменения размеров окна


                //CalculateZoomFactor((int)Host.ActualWidth, (int)Host.ActualHeight, IMG_W_now, IMG_H_now);
                if (mainViewModel.ScaleAuto)
                    CalculateZoomFactor((int)Host.ActualWidth, (int)Host.ActualHeight, IMG_W_now, IMG_H_now);
                else
                    AdaptViewportControl();
            }
            catch (Exception exc)
            {
                FLog.Log("ERROR:" + exc.Message);
            }
        }

        private void TryOpenCamera()
        {
            string lastDevice = Settings.Default.LastDevice;
            var lastDevices = mainViewModel.FindLastConnectedCameraConfigs();
            var devices = mainViewModel.ICImagingControl.Devices;

            if (devices.Length == 1)
            {
                bool deviceInList = false;
                for (int i = 0; i < lastDevices.Count; i++)
                {
                    if (devices[0].Name == lastDevices[i])
                    {
                        deviceInList = true;
                        break;
                    }
                }
                if(deviceInList)
                {
                    mainViewModel.OpenCamera(devices[0]);
                }
                else
                {
                    mainViewModel.ShowDevicePropsDialog();
                }
            }
            else
            {
                mainViewModel.ShowDevicePropsDialog();
            }
        }

        private void WhenDeviceOpened()
        {
            try
            {
                try
                {
                    var frameType = IC_Control.VideoFormatCurrent.FrameType;
                    IMG_W_now = frameType.Width;
                    IMG_H_now = frameType.Height;

                    if (mainViewModel.ScaleAuto)
                        CalculateZoomFactor((int)Host.ActualWidth, (int)Host.ActualHeight, IMG_W_now, IMG_H_now);
                    else
                        AdaptViewportControl();
                    //CalculateZoomFactor((int)Host.ActualWidth, (int)Host.ActualHeight, IMG_W_now, IMG_H_now);
                }
                catch
                {
                    FLog.Log("ERROR - Format adaptation error");
                }

                Init_ListOf_CheckButtons();
            }
            catch (Exception ext)
            {
                FLog.Log("ERROR - Form_Load() error");
                MessageBox.Show(ext.Message);
                Everything_loaded = true;
            }
        }

        private bool noEvents = false;
        public void Init_ListOf_CheckButtons()
        {
            /*noEvents = true;
            RenameableToggleButton rtb = null;
            for (int i = 0; i < stackPanelPhacoButtons.Children.Count; i++)
            {
                rtb = stackPanelPhacoButtons.Children[i] as RenameableToggleButton;
                renameableButtonsConfigs.Add(rtb);
                rtb.Text = mainViewModel.ConfigsDictionary[rtb.Tag as string].ConfigName;
                if ((string)rtb.Tag == mainViewModel.ConfigKey)
                {
                    rtb.IsChecked = true;
                    tabPhaco.Focus();
                }
            }

            for (int i = 0; i < stackPanelVitreoButtons.Children.Count; i++)
            {
                rtb = stackPanelVitreoButtons.Children[i] as RenameableToggleButton;
                renameableButtonsConfigs.Add(rtb);
                rtb.Text = mainViewModel.ConfigsDictionary[rtb.Tag as string].ConfigName;
                if ((string)rtb.Tag == mainViewModel.ConfigKey)
                {
                    rtb.IsChecked = true;
                    tabVitreo.Focus();
                }
            }

            for (int i = 0; i < stackPanelUserConfigs.Children.Count; i++)
            {
                rtb = stackPanelUserConfigs.Children[i] as RenameableToggleButton;
                renameableButtonsConfigs.Add(rtb);
                rtb.Text = mainViewModel.ConfigsDictionary[rtb.Tag as string].ConfigName;
                if ((string)rtb.Tag == mainViewModel.ConfigKey)
                {
                    rtb.IsChecked = true;
                    tabUser.Focus();
                }
            }
            noEvents = false;*/
        }

        public double GetScalingFactor_ofMonitor()
        {
            double resHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;  // 1440
            double actualHeight = SystemParameters.PrimaryScreenHeight;  // 960
            return (resHeight / actualHeight);
        }

        private void B_Browse_Vid_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mainViewModel.VideoFileInfo = ServiceFunctions.Files.OpenDirectory(mainViewModel.VideoFileInfo);
            }
            catch(Exception ex)
            {
                LogException(ex);
            }

        }

        private void B_Browse_Photo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mainViewModel.PhotoFileInfo = ServiceFunctions.Files.OpenDirectory(mainViewModel.PhotoFileInfo);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void Form1_FormClosing(object sender, CancelEventArgs e)
        {
            try
            {
                mainViewModel.SaveSettings();
            }
            catch (Exception) { }
            try
            {
                mainViewModel.CloseDevice();
                
            }
            catch(Exception ex) { 
            
            }
        }

        private void Load_FlipState()
        {
            try
            {
                List<string> Flips = ServiceFunctions.Files.Read_txt("FlipSettings.xml");
                if (IC_Control.DeviceFlipHorizontalAvailable && IC_Control.DeviceFlipVerticalAvailable)
                {
                    if (IC_Control.LiveVideoRunning) IC_Control.LiveStop();
                    IC_Control.DeviceFlipHorizontal = Convert.ToBoolean(Flips[1]);
                    IC_Control.DeviceFlipVertical = Convert.ToBoolean(Flips[2]);
                    IC_Control.LiveStart();
                }
            }
            catch
            {
                if (IC_Control.LiveVideoRunning) IC_Control.LiveStop();
                IC_Control.DeviceFlipHorizontal = Convert.ToBoolean(false);
                IC_Control.DeviceFlipVertical = Convert.ToBoolean(false);
                IC_Control.LiveStart();
            }
            finally
            {
                if (!IC_Control.LiveVideoRunning) IC_Control.LiveStart();
            }

        }

        private void QuiteKey(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void CodecPropKey(object sender, ExecutedRoutedEventArgs e)
        {
            B_codecSettings_Click(null, null);
        }

        private void FullScreenSwitchKey(object sender, ExecutedRoutedEventArgs e)
        {
            if (FullScrin)
            {
                B_FS_Switcher_form.BackgroundImage = System.Drawing.Image.FromFile("FS_on_form.png");
                MinimizeWindow();
            }
            else
            {
                B_FS_Switcher_form.BackgroundImage = System.Drawing.Image.FromFile("FS_off_form.png");
                MaximizeWindow();
            }

            //CalculateZoomFactor((int)Host.ActualWidth, (int)Host.ActualHeight, IMG_W_now, IMG_H_now);
        }

        private void TrB_ExposureVal_Scroll(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void NUD_Exposure_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }

        private void ChB_ExposureAuto_CheckedChanged(object sender, RoutedEventArgs e)
        {

        }

        private void TrB_GainVal_Scroll(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void NUD_Gain_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }

        private void ChB_GainAuto_CheckedChanged(object sender, RoutedEventArgs e)
        {

        }

        #region Scale

        private void NUD_Scale_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }

        private void ChB_ScaleAuto_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (IC_Control != null && IC_Control.DeviceValid)
            {
                if (mainViewModel.ScaleAuto)
                    CalculateZoomFactor((int)Host.ActualWidth, (int)Host.ActualHeight, IMG_W_now, IMG_H_now);
                else
                    AdaptViewportControl();
                //CalculateZoomFactor((int)Host.ActualWidth, (int)Host.ActualHeight, IMG_W_now, IMG_H_now);
            }
        }

        private void TrB_ScaleVal_Scroll(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
        
        #endregion

        private void TrB_Brightness_Scroll(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void NUD_Brightness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }

        private void ChB_BrightnessAuto_CheckedChanged(object sender, RoutedEventArgs e)
        {

        }

        private void TimerForRenew_Tick(object sender, EventArgs e)
        {
            
        }
        bool AutoExp_wasEnabled_beforeRecording = false;
        bool RecordingNeeded = false;
        List<Bitmap> bmp_list = new List<Bitmap>();

        private void B_StartCapture_Click(object sender, RoutedEventArgs e)
        {
            FLog.Log("B_StartCapture_Click");

            try
            {
                mainViewModel.StartVideoCapturing();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void B_StopCapture_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mainViewModel.VideoCapturing = false;
            }
            catch(Exception ex)
            {
                LogException(ex);
            }
        }

        private void B_Properties_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //FLog.Log("B_Properties_Click");
                mainViewModel.ShowAdditionalDeviceProps();

                /*if (IC_Control.DeviceValid)
                {
                    WhenDeviceOpened();
                }*/
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }



            //IC_Control.ShowPropertyDialog(windowHandle);
            //mainViewModel.SaveCurrentCameraConfig();
            //Device_state = IC_Control.SaveDeviceState();
        }

        private void ChB_WhiteBalanceAuto_CheckedChanged(object sender, RoutedEventArgs e)
        {

        }

        private void WhiteBalanceTimer_Tick(object sender, EventArgs e)
        {

        }

        private void IC_Control_Resize(object sender, EventArgs e)
        {
            FLog.Log("IC_Control_Resize");
            var a = IC_Control.Width;
        }

        private void B_FS_Switcher_Click(object sender, EventArgs e)
        {
            FLog.Log("B_FS_Switcher_Click");
            if (FullScrin) { MinimizeWindow(); }
            else { MaximizeWindow(); }
        }

        private void B_Snapshot_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mainViewModel.TakeSnapshot();

                /*FLog.Log("B_Snapshot_Click");
                string dataName = FindLast_Photo(SavePhoto_dir, (TB_FIO.Text + "_" + TB_CurrentDate.Text + "_" + TB_HistoryNumber.Text), ".tiff");
                string FullPathAndName = dataName;
                IC_Control.MemorySaveImage(FullPathAndName);*/

                //запустить анимаицю
                ThicknessAnimationUsingKeyFrames thicknessAnimationUsingKeyFrames = new ThicknessAnimationUsingKeyFrames();
                thicknessAnimationUsingKeyFrames.KeyFrames.Add(new DiscreteThicknessKeyFrame(new Thickness(0, 0, 0, 0), KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 50))));
                thicknessAnimationUsingKeyFrames.KeyFrames.Add(new DiscreteThicknessKeyFrame(new Thickness(4, 4, 4, 4), KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 200))));
                thicknessAnimationUsingKeyFrames.KeyFrames.Add(new DiscreteThicknessKeyFrame(new Thickness(0, 0, 0, 0), KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 50))));
                thicknessAnimationUsingKeyFrames.AutoReverse = false;
                thicknessAnimationUsingKeyFrames.FillBehavior = FillBehavior.Stop;
                thicknessAnimationUsingKeyFrames.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 500));

                ThicknessAnimation thicknessAnimation = new ThicknessAnimation(new Thickness(0, 0, 0, 0),
                    new Thickness(2, 2, 2, 2),
                    new TimeSpan(0, 0, 0, 0, 300),
                    FillBehavior.Stop);
                thicknessAnimation.AutoReverse = true;
                border_host.BeginAnimation(Border.BorderThicknessProperty, thicknessAnimationUsingKeyFrames);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void B_Cam_Select_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mainViewModel.ShowDevicePropsDialog();

                /*if (mainViewModel.DeviceValid)
                {
                    WhenDeviceOpened();
                }*/
            }
            catch (Exception ex) {
                
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            FLog.Log("Form1_Resize");
            if (Everything_loaded)
            {
                //ВРЕМЕННО
                /*if (FullScrin) Adapt_Size_ofCont((IC_Control as System.Windows.Forms.Control), IMG_W_now, IMG_H_now, 1, 1); // resize
                else Adapt_Size_ofCont((IC_Control as System.Windows.Forms.Control), IMG_W_now, IMG_H_now, 0.8, 1);
                FormatAdaptation(IMG_W_now, IMG_H_now);*/
                if (mainViewModel.ScaleAuto)
                    CalculateZoomFactor((int)Host.ActualWidth, (int)Host.ActualHeight, IMG_W_now, IMG_H_now);
                else
                    AdaptViewportControl();
                //CalculateZoomFactor((int)Host.ActualWidth, (int)Host.ActualHeight, IMG_W_now, IMG_H_now);
            }
        }
        int datacounter = 0;
        private void AdaptViewportControl()
        {
            try
            {
                datacounter++;
                if (datacounter >= 6)
                {
                    int a = 0;
                }
                double zf = mainViewModel.Scale;

                Rect rectHost = new Rect(0, 0, Host.ActualWidth * Scaling_of_monitor, Host.ActualHeight * Scaling_of_monitor);
                Rect imageRect = new Rect(0, 0, IMG_W_now, IMG_H_now);
                imageRect.Scale(zf, zf);
                imageRect.Offset((rectHost.Width - imageRect.Width) * 0.5d, (rectHost.Height - imageRect.Height) * 0.5d);

                Rect controlRect = Rect.Intersect(rectHost, imageRect);

                if((int)controlRect.Width == 0 || (int)controlRect.Height == 0)
                {

                }

                IC_Control.Width = (int)controlRect.Width;
                IC_Control.Height = (int)controlRect.Height;
                IC_Control.Left = (int)controlRect.Left;
                IC_Control.Top = (int)controlRect.Top;


                if (imageRect.Width > IC_Control.Width || imageRect.Height > IC_Control.Height)
                    IC_Control.ScrollbarsEnabled = true;
                else
                    IC_Control.ScrollbarsEnabled = false;

                ChangePos_of_FSBut();
            }
            catch (Exception) { }
            
        }

        private void CalculateZoomFactor(int panelWidth, int panelHeight, int IMG_Width, int IMG_Height)
        {
            double Ratio = 1d*panelWidth/panelHeight; // panel W/H
            double ratio = 1d*IMG_Width/ IMG_Height; // image w/h
            
            var ctrl = IC_Control;

            double delta_h = 30;
            if (FullScrin) delta_h = 0;
            int PanelNewWidth = (int)((Host.ActualWidth) * Scaling_of_monitor);
            int PanelNewHeight = (int)((Host.ActualHeight - delta_h) * Scaling_of_monitor);
            double Img_SizeRelation = (double)IMG_Width / (double)IMG_Height;

            double zoomFactor = 1f;
            if (ratio > Ratio)
            {//вписываем по горизонтали
                zoomFactor = (1d* PanelNewWidth / IMG_Width);
            }
            else
            {//вписываем по вертикали
                zoomFactor = (1d* PanelNewHeight / IMG_Height);
            }

            if(!FullScrin)
                zoomFactor = mainViewModel.RoundZoomFactor(zoomFactor);

            mainViewModel.Scale = zoomFactor;
            
            AdaptViewportControl();
        }

        private void B_CodecProp_Click(object sender, EventArgs e)
        {

        }

        private void CB_Codecs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Timer_recording_Tick(object sender, EventArgs e)
        {
            FLog.Log("Timer_recording_Tick");
            if (stw.Elapsed.TotalSeconds > seconds2write)
            {
                stw.Reset(); Timer_recording.Stop();
                B_StopCapture_Click(null, null);

            }
        }

        private void TB_FIO_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Contains('s'))
                e.Handled = true;

            FLog.Log("TB_FIO_TextChanged");
        }

        private void TB_CurrentDate_TextChanged(object sender, TextChangedEventArgs e)
        {
            FLog.Log("TB_CurrentDate_TextChanged");
        }

        private void TB_HistoryNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            FLog.Log("TB_HistoryNumber_TextChanged");
        }

        private void TB_FIO_KeyPress(object sender, KeyEventArgs e)
        {
            FLog.Log("TB_FIO_KeyPress");
            char l = ' ';// e.Key;// e.KeyChar;
            if ((l < '0' || l > '9') && (l < 'A' || l > 'z') && (l < 'А' || l > 'я') && l != '\b' && l != ' ' && l != '-')
            {
                e.Handled = true;
            }
        }

        private void TB_HistoryNumber_KeyPress(object sender, KeyEventArgs e)
        {
            FLog.Log("TB_HistoryNumber_KeyPress");
            char l = ' ';// e.KeyChar;
            if ((l < '0' || l > '9') && (l < 'A' || l > 'z') && (l < 'А' || l > 'я') && l != '\b' && l != ' ' && l != '-')
            {
                e.Handled = true;
            }
        }

        private void TB_CurrentDate_KeyPress(object sender, KeyEventArgs e)
        {
            FLog.Log("TB_CurrentDate_KeyPress");
            char l = ' ';// e.KeyChar;
            if ((l < '0' || l > '9') && l != '\b' && l != '_' && l != '-')
            {
                e.Handled = true;
            }
        }

        private void TB_Directory_Photo_KeyPress(object sender, KeyEventArgs e)
        {
            FLog.Log("TB_Directory_Photo_KeyPress");
            char l = ' ';// e.KeyChar;
            if ((l < '0' || l > '9') && (l < 'A' || l > 'z') && (l < 'А' || l > 'я') && l != '\b' && l != '_' && l != '\\')
            {
                e.Handled = true;
            }
        }

        private void TB_Directory_Vid_KeyPress(object sender, KeyEventArgs e)
        {
            FLog.Log("TB_Directory_Vid_KeyPress");
            char l = ' ';// e.KeyChar;
            if ((l < '0' || l > '9') && (l < 'A' || l > 'z') && (l < 'А' || l > 'я') && l != '\b' && l != '_' && l != '\\')
            {
                e.Handled = true;
            }
        }

        private void BGW_LiveVideoRecording_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void T1_Tick(object sender, EventArgs e)
        {
            FLog.Log("T1_Tick");
            IC_Control.Display();
        }

        int frames = 0;
        double curfps = 0;
        int frames2write = 50;
        System.Diagnostics.Stopwatch stw_frameproc = new System.Diagnostics.Stopwatch();
        
        private void ShowImage_only_invoke(Bitmap BMP)
        {
            //PB_Debug.Invoke(new ShowImage_only_del(ShowImage_only), BMP);
        }

        private delegate void ShowImage_only_del(Bitmap bmp);
        private void ShowImage_only(Bitmap BMP)
        {
            frames++;
            // PB_Debug.Image = BMP;
        }



        bool Camera_restart_need = false;
        int RestartAttempts = 0;
        private void Timer_camera_checker_Tick(object sender, EventArgs e)
        {
            if (!Camera_restart_need)//даже если рестарт не нужен
            {
                if ((STW_fps.Elapsed.TotalMilliseconds > 1000) && (STW_fps.Elapsed.TotalMilliseconds > 20 * (double)(mainViewModel.Exposure)))//но камера по непонятной причине отвалилась
                {
                    // Timer_camera_checker.Dispose(); 
                    Camera_restart_need = true;

                    // this.BeginInvoke((Action)(() => this.Text = "Device state: " + (IC_Control.DeviceValid ? "valid" : "invalid")));
                    this.Dispatcher.BeginInvoke((Action)(() => FLog.Log("Device state: " + (IC_Control.DeviceValid ? "valid" : "invalid"))));
                    /*if (IC_Control.DeviceValid && RestartAttempts==0)//check validicity
                     {
                         this.Dispatcher.BeginInvoke((Action)(() => FLog.Log("Waiting for translation start....")));
                         Camera_restart_need = false;
                         STW_fps.Restart();
                         RestartAttempts++;
                     }
                     else
                     {
                         this.Dispatcher.BeginInvoke((Action)(() => FLog.Log("Restarting Camera")));
                         if (!BGW_CamRestarter.IsBusy) BGW_CamRestarter.RunWorkerAsync();
                     }*/
                                     
                    {
                        this.Dispatcher.BeginInvoke((Action)(() => FLog.Log("Waiting for translation start....")));
                        this.Dispatcher.BeginInvoke((Action)(() => FLog.Log("Restarting Camera")));
                        if (!BGW_CamRestarter.IsBusy) BGW_CamRestarter.RunWorkerAsync();
                    }
                }
            }
            else
            {
                // this.BeginInvoke((Action)(() => this.Text = "Device state: " + (IC_Control.DeviceValid ? "valid" : "invalid")));
                this.Dispatcher.BeginInvoke((Action)(() => FLog.Log("Device state: " + (IC_Control.DeviceValid ? "valid" : "invalid"))));
                if (!BGW_CamRestarter.IsBusy) BGW_CamRestarter.RunWorkerAsync();
            }
        }

        private void BGW_CamRestarter_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!IC_Control.DeviceValid || 
                (STW_fps.Elapsed.TotalMilliseconds > 1000))
            {
                int i_rem = -1;
                for (int i = 0; i < IC_Control.Devices.Count(); i++)
                {
                    if (Device_name == IC_Control.Devices[i].ToString())
                    {
                        Device_name = IC_Control.Devices[i].ToString();
                        i_rem = i;
                        break;
                    }
                }
                if (i_rem != -1)
                {
                    try
                    {
                        IC_Control.Device = Device_name;
                    }
                    catch(Exception exc)
                    {
                        IC_Control.LiveStop();
                        IC_Control.Device = Device_name;
                    }


                    if (Device_state != null)
                        this.Dispatcher.BeginInvoke((Action)(() => IC_Control.LoadDeviceState(Device_state, true)));
                    //this.BeginInvoke((Action)(() => IC_Control.LiveStop()));
                    while (!IC_Control.LiveVideoRunning)
                    {
                        System.Threading.Thread.Sleep(200);
                        this.Dispatcher.BeginInvoke((Action)(() => IC_Control.LiveStart()));
                        RestartAttempts = 0;
                    }
                    STW_fps.Restart();
                    FLog.Log("Camera restarted!");
                }
            }
            //this.BeginInvoke((Action)(() => this.Text = "Device state: " + (IC_Control.DeviceValid ? "valid" : "invalid")));
            this.Dispatcher.BeginInvoke((Action)(() => FLog.Log("Device state: " + (IC_Control.DeviceValid ? "valid" : "invalid"))));
            Camera_restart_need = false;
            STW_fps.Restart();
        }

        /// <summary>
        /// Must returns false, if char is valid and true, if char is invalid
        /// </summary>
        /// <param name="c">char</param>
        /// <returns>false, if char is valid and true, if char is invalid</returns>
        private delegate bool CharInvalidator(char c);

        private bool IsTextValid(string text, CharInvalidator charInvalid)
        {
            for (int i = 0; i < text.Length; i++)
                if (charInvalid(text[i]))
                    return false;
            return true;
        }

        private void TB_FIO_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextValid(e.Text, (l) => (l < '0' || l > '9') && (l < 'A' || l > 'z') && (l < 'А' || l > 'я') && l != '\b' && l != ' ' && l != '-');
        }

        private void TB_CurrentDate_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextValid(e.Text, (l) => (l < '0' || l > '9') && l != '\b' && l != '_' && l != '-');
        }

        private void TB_HistoryNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextValid(e.Text, (l) => (l < '0' || l > '9') && (l < 'A' || l > 'z') && (l < 'А' || l > 'я') && l != '\b' && l != ' ' && l != '-');
        }

        private void TB_Directory_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextValid(e.Text, (l) => (l < '0' || l > '9') && (l < 'A' || l > 'z') && (l < 'А' || l > 'я') && l != '\b' && l != '_' && l != '\\');
        }

        private void B_FS_Switcher_Checked(object sender, RoutedEventArgs e)
        {
            MaximizeWindow();
        }

        private void B_FS_Switcher_Unchecked(object sender, RoutedEventArgs e)
        {
            MinimizeWindow();
        }

        private void Host_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            FLog.Log("Resize");
            if (Everything_loaded)
            {
                //ВРЕМЕННО
                /*if (FullScrin) Adapt_Size_ofCont((IC_Control as System.Windows.Forms.Control), IMG_W_now, IMG_H_now, 1, 1); // resize
                else Adapt_Size_ofCont((IC_Control as System.Windows.Forms.Control), IMG_W_now, IMG_H_now, 0.8, 1);
                FormatAdaptation(IMG_W_now, IMG_H_now);*/

                if (mainViewModel.ScaleAuto)
                    CalculateZoomFactor((int)Host.ActualWidth, (int)Host.ActualHeight, IMG_W_now, IMG_H_now);
                else
                    AdaptViewportControl();
            }
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                ToggleTheme(toggleTheme.IsChecked == true);
            }
            catch (Exception) { }
        }

        private void ToggleTheme(bool isLight)
        {
            string themePath = "";

            if (isLight)
            {
                themePath = "LightTheme.xaml";
            }
            else
            {
                themePath = "DarkTheme.xaml";
            }

            ResourceDictionary resourceDict = Application.LoadComponent(new Uri(themePath, UriKind.Relative)) as ResourceDictionary;
            // очищаем коллекцию ресурсов приложения
            Application.Current.Resources.Clear();
            // добавляем загруженный словарь ресурсов
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            Refresh_IC_BackColor();

            mainViewModel.IsLightTheme = isLight;
        }

        private void Host_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ToggleButton_Checked_1(object sender, RoutedEventArgs e)
        {
            MaximizeWindow();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;
            if(FullScrin)
            {
                B_FS_Switcher_form.BackgroundImage = System.Drawing.Image.FromFile("FS_on_form.png");
                MinimizeWindow();
            }
            else
            {
                B_FS_Switcher_form.BackgroundImage = System.Drawing.Image.FromFile("FS_off_form.png"); 
                MaximizeWindow();
            }
        }

        private void RenameableToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            RenameableToggleButton toggleButton = sender as RenameableToggleButton;

            //Обработка переключения до цикла!

            //FLog.Log("ChB_Config_N_Checked");
            if(!noEvents)
                try
                {
                    //Timer_camera_checker.Stop();
                    Config_tag = ((sender as RenameableToggleButton).Tag as string); //Вычленяем номер конфигурации

                    /*if (mainViewModel.ScaleAuto)
                        CalculateZoomFactor((int)Host.ActualWidth, (int)Host.ActualHeight, IMG_W_now, IMG_H_now);
                    else
                        AdaptViewportControl();*/

                    mainViewModel.SaveCurrentCameraConfig();
                    mainViewModel.ConfigKey = Config_tag;

                    //LastConfig_tag = Config_tag; // присваиваем последнему загруженному тегу тот, который загружен сейчас

                    //Timer_camera_checker.Start();
                    //WhenDeviceOpened();
                    
                    //Uncheck prvious
                    for (int i = 0; i < renameableButtonsConfigs.Count; i++)
                    {
                        if (renameableButtonsConfigs[i] != toggleButton && renameableButtonsConfigs[i].toggleButon.IsChecked == true)
                        {
                            renameableButtonsConfigs[i].toggleButon.IsChecked = false;
                        }
                    }
                }
                catch(Exception exc)
                {
                    FLog.Log("Ошибка при переключении конфигураций");
                }
        }

        private void RenameableToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            
        }

        private void RenameableToggleButton_OnApplyChanges(object sender, RoutedEventArgs eventArgs)
        {
            if (noEvents)
                return;

            string local_tag = (sender as RenameableToggleButton).Tag as string;
            string local_text = (sender as RenameableToggleButton).Text;

            mainViewModel.ConfigsDictionary[local_tag].ConfigName = local_text;
            mainViewModel.SaveCurrentCameraConfig();
        }

        private void RenameableToggleButton_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void RenameableToggleButton_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private void B_PauseCapture_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mainViewModel.VideoOnPause = !mainViewModel.VideoOnPause;
            }
            catch(Exception ex) { }
        }

        private void B_codecSettings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowSetupCodecsDislog();
            }
            catch(Exception ex)
            {
                LogException(ex);
            }
        }

        private bool? ShowSetupCodecsDislog()
        {
            /*CodecSettings codecSettingsDialog = new CodecSettings();
            codecSettingsDialog.DataContext = mainViewModel;
            return codecSettingsDialog.ShowDialog();*/
            ConfigureEncoder configureEncoderDialog = new ConfigureEncoder();
            configureEncoderDialog.Owner = this;
            configureEncoderDialog.DataContext = mainViewModel.Encoder;
            return configureEncoderDialog.ShowDialog();
        }

        private void Refresh_IC_BackColor()
        {
            System.Windows.Media.Color windowColor = (this.Background as SolidColorBrush).Color;
            IC_Control.BackColor = System.Drawing.Color.FromArgb(windowColor.A, windowColor.R, windowColor.G, windowColor.B);
            Host.Background = new SolidColorBrush(windowColor);
        }

        private void ChangePos_of_FSBut()
        {
            //Point NewLocation_onPanel = (new Point(IC_Control.Location.X + IC_Control.Width, IC_Control.Location.Y + IC_Control.Height));
            //B_FS_Switcher_form.Location = new Point((int)((double)NewLocation_onPanel.X-100), (int)((double)NewLocation_onPanel.Y-100));

            B_FS_Switcher_form.Location = new System.Drawing.Point((int)(Host.ActualWidth * Scaling_of_monitor - 30 - B_FS_Switcher_form.Width),
                                                     (int)(Host.ActualHeight * Scaling_of_monitor - 30 - B_FS_Switcher_form.Height));
        }

        System.Windows.GridLength column_width_old;
        System.Windows.WindowState state_old;


        System.Drawing.Size Size_was = new System.Drawing.Size(1280, 720);
        System.Drawing.Point Location_was = new System.Drawing.Point(0, 0);

        private void MaximizeWindow()
        {
            Size_was = new System.Drawing.Size((int)this.Width, (int)this.Height);
            Location_was = new System.Drawing.Point((int)Left, (int)Top);
            state_old = this.WindowState;

            this.WindowStyle = System.Windows.WindowStyle.None;
            this.WindowState = System.Windows.WindowState.Normal;
            this.WindowState = System.Windows.WindowState.Maximized;
            this.Top = 0;
            this.Left = 0;
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            toolBar.Visibility = System.Windows.Visibility.Collapsed;
            scrollViewver_left.Visibility = System.Windows.Visibility.Collapsed;
            gridSplitter_left.Visibility = System.Windows.Visibility.Collapsed;
            column_width_old = grid_main.ColumnDefinitions[0].Width;
            grid_main.ColumnDefinitions[0].Width = new System.Windows.GridLength(0, System.Windows.GridUnitType.Auto);

            FullScrin = true;

            if (mainViewModel.ScaleAuto)
                CalculateZoomFactor((int)Host.ActualWidth, (int)Host.ActualHeight, IMG_W_now, IMG_H_now);
            else
                AdaptViewportControl();
        }
        private void MinimizeWindow()
        {
            this.WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
            this.WindowState = state_old;
            this.Left = Location_was.X;
            this.Top = Location_was.Y;
            this.Width = Size_was.Width;
            this.Height = Size_was.Height;
            toolBar.Visibility = System.Windows.Visibility.Visible;
            scrollViewver_left.Visibility = System.Windows.Visibility.Visible;
            gridSplitter_left.Visibility = System.Windows.Visibility.Visible;
            grid_main.ColumnDefinitions[0].Width = column_width_old;

            FullScrin = false;
            //ВРЕМЕННО
            if (mainViewModel.ScaleAuto)
                CalculateZoomFactor((int)Host.ActualWidth, (int)Host.ActualHeight, IMG_W_now, IMG_H_now);
            else
                AdaptViewportControl();
            //CalculateZoomFactor((int)Host.ActualWidth, (int)Host.ActualHeight, IMG_W_now, IMG_H_now);
        }

        private bool offAutobalance = false;
        private void ChB_WhiteBalanceAuto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mainViewModel.WhiteBalanceAutoEnabled)
                {
                    if(mainViewModel.WhiteBalanceAuto)
                    {
                        Task task = new Task(new Action(() =>
                        {
                            offAutobalance = false;
                            Stopwatch sw = new Stopwatch();
                            sw.Start();
                            while (true)
                            {
                                if (offAutobalance)
                                {
                                    break;
                                }
                                if (sw.ElapsedMilliseconds >= 5000)
                                {
                                    mainViewModel.WhiteBalanceAuto = false;
                                    break;
                                }
                                System.Threading.Thread.Sleep(50);
                            }
                            offAutobalance = false;
                        }));
                        task.Start();
                    }
                    else
                    {
                        offAutobalance = true;
                    }
                }
            }
            catch(Exception ex)
            {
                LogException(ex);
            }
        }

        private void LogException(Exception ex)
        {
            FLog.Log(ex.ToString());
            MessageBox.Show(ex.ToString());
        }

        private void B_StartPauseVideo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ToggleVideoCapture();
            }
            catch(Exception ex)
            {
                LogException(ex);
            }
        }

        private void ToggleVideoCapture()
        {
            try
            {
                if (!mainViewModel.DeviceValid)
                    return;

                checkVideoFrameType();

                if (mainViewModel.VideoCapturing)
                {
                    mainViewModel.VideoOnPause = !mainViewModel.VideoOnPause;
                }
                else
                {
                    mainViewModel.StartVideoCapturing();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void checkVideoFrameType()
        {
            var frameType = mainViewModel.ICImagingControl.VideoFormatCurrent.FrameType;
            if (frameType.Height > 1080 || frameType.Width > 1920)
            {
                MessageBox.Show("Установлено слишком большое разрешение изображения.\n Максимально доступное разрешение для записи видео 1920x1080");
                return;
            }
        }

        private void commandRecordVideoMethod(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if (!mainViewModel.DeviceValid)
                    return;

                checkVideoFrameType();

                if (mainViewModel.VideoCapturing)
                {
                    mainViewModel.StopVideoCapturing();
                }
                mainViewModel.StartVideoCapturing();
            }
            catch (Exception) { }
        }

        private void commandPauseVideoMethod(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if (!mainViewModel.DeviceValid)
                    return;

                if (mainViewModel.VideoCapturing)
                {
                    mainViewModel.VideoOnPause = !mainViewModel.VideoOnPause;
                }
            }
            catch (Exception) { }
        }

        private void commandStopVideoMethod(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if (!mainViewModel.DeviceValid)
                    return;

                if (mainViewModel.VideoCapturing)
                {
                    mainViewModel.StopVideoCapturing();
                }
            }
            catch (Exception) { }
        }

        private void B_Info_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InfoWindow info = new InfoWindow();
                info.Owner = this;
                info.ShowDialog();
            }
            catch (Exception) { }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                TextBox tb = sender as TextBox;
                if (e.Key == Key.Enter)
                {
                    
                    tb.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }
            }
            catch (Exception) { }
        }

        private void commandTakePhotoMethod(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if (!mainViewModel.DeviceValid)
                    return;

                B_Snapshot_Click(null, null);
            }
            catch (Exception) { }
        }
    }
}
