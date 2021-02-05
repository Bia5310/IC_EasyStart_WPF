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

namespace IC_EasyStart_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ICImagingControl IC_Control = null;
        string MainConfigPath = null;

        private VCDSimpleProperty vcdProp = null;
        private VCDAbsoluteValueProperty AbsValExp = null;// специально для времени экспонирования [c]

        string Version = "2.51";
        string Config_tag = "0_0"; //0_0 - default
        string LastConfig_tag = "0_0";
        string SaveVid_dir = "Video";
        string SavePhoto_dir = "Photo";

        string App_cfg_name = "App_prop.cfg";
        string ConfigNames_filename = "ConfigNames.xml";

        int IMG_W_now = 0;
        int IMG_H_now = 0;
        bool Exposure_Auto = false;
        bool Gain_Auto = false;
        //WB
        int WB_FinalSum = 0;

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

        System.Drawing.Image bmpFS_on = null;
        System.Drawing.Image bmpFS_off = null;

        private List<RenameableToggleButton> renameableButtonsConfigs = new List<RenameableToggleButton>();

        private Dictionary<string, string> ConfigsNamesDictionary = new Dictionary<string, string>();

        public MainWindow()
        {
            this.Title = "IC EasyStart v" + Version;
           
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
            Timer_camera_checker.IsEnabled = true;

            BGW_LiveVideoRecording = new BackgroundWorker();
            BGW_LiveVideoRecording.DoWork += BGW_LiveVideoRecording_DoWork;
            BGW_LiveVideoRecording.WorkerSupportsCancellation = true;
            BGW_LiveVideoRecording.WorkerReportsProgress = false;

            BGW_CamRestarter = new BackgroundWorker();
            BGW_CamRestarter.DoWork += BGW_CamRestarter_DoWork;
            BGW_CamRestarter.WorkerSupportsCancellation = false;
            BGW_CamRestarter.WorkerReportsProgress = false;

            InitializeComponent();


            ChB_WhiteBalanceAuto.Checked += ChB_WhiteBalanceAuto_CheckedChanged;
            ChB_WhiteBalanceAuto.Unchecked += ChB_WhiteBalanceAuto_CheckedChanged;

            FullScreenRoutedCommand.InputGestures.Add(new KeyGesture(Key.F, ModifierKeys.Alt));
            QuiteRoutedCommand.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Alt));
            CRoutedCommand.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(FullScreenRoutedCommand, FullScreenSwitchKey));
            CommandBindings.Add(new CommandBinding(QuiteRoutedCommand, QuiteKey));
            CommandBindings.Add(new CommandBinding(CRoutedCommand, CodecPropKey));

            B_FS_Switcher_form = Host.Child.Controls[0] as System.Windows.Forms.Button;

            bmpFS_on = Bitmap.FromFile("FS_on_form.png");
            bmpFS_off = Bitmap.FromFile("FS_off_form.png");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FLog.Log("Stage 0 of loading is completed");
            IC_Control = new ICImagingControl();
            Host.Child.Controls.Add(IC_Control);

            FLog.Log("Stage 0.2 of loading is completed");
            IC_Control.SendToBack();
            IC_Control.Paint += IC_Control_Paint;
            FLog.Log("Stage 0.4 of loading is completed");
            //IC_Control.OverlayBitmapPosition = PathPositions.Display;
            IC_Control.OverlayBitmapAtPath[PathPositions.Display].Enable = true;
            //IC_Control.OverlayBitmap.Enable = true;
            IC_Control.LiveCaptureContinuous = true;

            FLog.Log("Stage 0.6 of loading is completed");
            IC_Control.ImageAvailable += IC_Control_ImageAvailable;
            IC_Control.Invalidated += IC_Control_Invalidated;
            FLog.Log("Stage 0.8 of loading is completed");
            Panel = Host.Child as System.Windows.Forms.Panel;
            FLog.Log("Stage 1 of loading is completed");
            //IC_Control.Anchor = System.Windows.Forms.AnchorStyles.Left|System.Windows.Forms.AnchorStyles.Right| System.Windows.Forms.AnchorStyles.Top| System.Windows.Forms.AnchorStyles.Bottom;
            //B_FS_Switcher_form.BackgroundImage = System.Drawing.Image.FromFile("B_FS_");//System.Drawing.Color.FromArgb(0, System.Drawing.Color.White);
            Refresh_IC_BackColor();

            B_FS_Switcher_form.UseVisualStyleBackColor = true;
            B_FS_Switcher_form.BackgroundImage = System.Drawing.Image.FromFile("FS_on_form.png");
            Init_ListOf_CheckButtons();


            Scaling_of_monitor = GetScalingFactor_ofMonitor();
            /*IC_Control.ShowDeviceSettingsDialog();
            if (IC_Control.DeviceValid) IC_Control.LiveStart();*/

            //SetDecimalPlaces(NUD_Exposure, 4);

            TB_CurrentDate.Text = ServiceFunctions.UI.GetDateString();
            TIS.Imaging.LibrarySetup.SetLocalizationLanguage("ru");
            //this.KeyPreview = true;
            FLog.Log("Stage 2 of loading is completed");

            try
            {
                try
                {
                    Dictionary_Load();
                    FLog.Log("Dictionary loading is successful");
                }
                catch
                {
                    FLog.Log("Dictionary loading fault");
                }
             
                try
                {
                    MainConfigPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\" + App_cfg_name;
                    Load_AppSettings();
                    FLog.Log("Load_AppSettings() call finished succesfully");
                }
                catch { FLog.Log("ERROR - Load_AppSettings() error"); }
                FLog.Log("Stage 3 of loading is completed");
                try //Комплексная проверка. Если конфиг не дефолт, то пытаемся его загрузить. В противном случае просим у юзера
                {
                    if(Config_tag!= "default")
                    {
                        if (!File.Exists("Config_" + Config_tag + ".xml")) Config_tag = "default";
                    }
                    if (Config_tag == "default")
                    {
                        if (!IC_Control.DeviceValid)
                        {
                            IC_Control.ShowDeviceSettingsDialog();
                            if (!IC_Control.DeviceValid)
                            {
                                MessageBox.Show("Не было выбрано ни одного устройства", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                                FLog.Log("No devices selected by user or loaded");
                                Application.Current.Shutdown();
                            }    
                        }
                    }
                   

                    if(Config_tag == "default")
                    {
                        Set_appropriate_params();
                        Save_AllTheConfigs();
                    }

                    try
                    {
                        Find_RenameableBut_byTag(Config_tag).IsChecked = true;
                        if (!IC_Control.DeviceValid)
                        {
                            MessageBox.Show("Не было выбрано ни одного устройства", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                            FLog.Log("No devices selected by user or loaded");
                            Application.Current.Shutdown();
                        }
                    }
                    catch { Config_tag = "0_0"; renameableButtonsConfigs[0].IsChecked = true; }
                    FLog.Log("Check configs function call finished succesfully");
                    
                }
                catch
                {
                    FLog.Log("ERROR - Check configs error");
                }

                FLog.Log("Stage 4 of loading is completed");
                try
                {
                    Init_Sliders(IC_Control);
                    FLog.Log("Init_Sliders() call finished succesfully");
                }
                catch(Exception exc)
                {

                    FLog.Log("ERROR - Init_Sliders or Load_ic_cam_easy  error");
                    FLog.Log("ORIGINAL:" + exc.Message);
                }

                try
                {
                    Load_ic_cam_easy(IC_Control);
                    FLog.Log("Load_ic_cam_easy() call finished succesfully");
                }
                catch(Exception exc)
                {
                    FLog.Log("ERROR - Init_Sliders or Load_ic_cam_easy  error");
                    FLog.Log("ORIGINAL:" + exc.Message);
                }

                FLog.Log("Stage 5 of loading is completed");
                try
                {
                    IMG_H_now = IC_Control.ImageHeight;
                    IMG_W_now = IC_Control.ImageWidth;
                    this.WindowState = WindowState.Maximized;//  FormWindowState.Maximized;
                    Adapt_Size_ofCont((IC_Control as System.Windows.Forms.Control), IMG_W_now, IMG_H_now, 0.8, 1); //on load
                    FLog.Log("Adapt_Size_ofCont() call finished succesfully");
                    FormatAdaptation(IMG_W_now, IMG_H_now);
                    FLog.Log("FormatAdaptation() call finished succesfully");
                }
                catch
                {
                    FLog.Log("ERROR - Format adaptation error");
                }
                FLog.Log("Stage 6 of loading is completed");
                try
                {
                    Load_FlipState();
                    FLog.Log("Load_FlipState() call finished succesfully");
                }
                catch (Exception exc)
                { FLog.Log("ERROR - Load_flipstate error "); }

                

                if (vcdProp.AutoAvailable(VCDIDs.VCDID_WhiteBalance))
                    vcdProp.Automation[VCDIDs.VCDID_WhiteBalance] = false;

                //Prepare_encoder();

                try { Refresh_Values_on_Trackbars(); FLog.Log("Refresh_Values_on_Trackbars() call finished succesfully"); }
                catch { FLog.Log("ERROR - Refreshing Values on TrB error"); }

                FLog.Log("Stage 7 of loading is completed");
                try
                {
                    Prepare_encoder2("Video\\video.avi", (int)IC_Control.DeviceFrameRate, 25000 * 1000);
                    B_StopCapture.IsEnabled = false;
                    FLog.Log("Encoder preparing is succesful");
                }
                catch (Exception exc)
                {
                    B_StartCapture.IsEnabled = false;
                    B_StopCapture.IsEnabled = false;
                    FLog.Log("ERROR - Encoder preparing finished this error: " + exc.Message);
                }
                Device_name = IC_Control.Device;
                Timer_camera_checker.Start();
                STW_fps.Start();
                FLog.Log("Stage 8 of loading is completed");
                Everything_loaded = true;
            }
            catch (Exception ext)
            {
                FLog.Log("ERROR - Form_Load() error");
                MessageBox.Show(ext.Message);
                Everything_loaded = true;
            }
            finally
            {
                if (IC_Control.DeviceValid)
                {
                    IC_Control.LiveStart();
                    Adapt_Size_ofCont(IC_Control as System.Windows.Forms.Control, IC_Control.ImageWidth, IC_Control.ImageHeight, 0.8, 1);
                }
                FLog.Log("Stage 9 of loading is completed");
            }

            var ov = IC_Control.OverlayBitmapAtPath[PathPositions.Display];
            ov.Enable = true;
            ov.ColorMode = OverlayColorModes.Color;
            ov.DropOutColor = System.Drawing.Color.Magenta;
            ov.Fill(IC_Control.OverlayBitmap.DropOutColor);
            ov.FontTransparent = true;
            ov.DrawText(System.Drawing.Color.Red, 10, 10, "IC Imaging Control 2.0");
            ov.DrawSolidRect(System.Drawing.Color.FromArgb(187, 100, 0, 0), 10, 10, 100, 100);

            FLog.Log("Initialization end");
        }
        public void Init_ListOf_CheckButtons()
        {
            for (int i = 0; i < stackPanelPhacoButtons.Children.Count; i++)
            {
                renameableButtonsConfigs.Add(stackPanelPhacoButtons.Children[i] as RenameableToggleButton);
            }

            for (int i = 0; i < stackPanelVitreoButtons.Children.Count; i++)
            {
                renameableButtonsConfigs.Add(stackPanelVitreoButtons.Children[i] as RenameableToggleButton);
            }

            for (int i = 0; i < stackPanelUserConfigs.Children.Count; i++)
            {
                renameableButtonsConfigs.Add(stackPanelUserConfigs.Children[i] as RenameableToggleButton);
            }

        }
        public double GetScalingFactor_ofMonitor()
        {
            double resHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;  // 1440
            double actualHeight = SystemParameters.PrimaryScreenHeight;  // 960
            return (resHeight / actualHeight);            
        }
        private void IC_Control_Invalidated(object sender, System.Windows.Forms.InvalidateEventArgs e)
        {
            
        }

        private void IC_Control_ImageAvailable1(object sender, ICImagingControl.ImageAvailableEventArgs e)
        {
            /*int width = IC_Control.ImageWidth;
            int height = IC_Control.ImageHeight;

            Bitmap overlay = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(overlay);

            g.DrawImage(bmpFS_on, 10, 10, bmpFS_on.Width, bmpFS_on.Height);*/

            //IC_Control.OverlayBitmap.D
        }

        private void IC_Control_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            /*int width = IC_Control.Width;
            int height = IC_Control.Height;

            System.Drawing.Color colorBack = System.Drawing.Color.FromArgb(128, 128, 0, 0);
            
            System.Drawing.Pen pBack = new System.Drawing.Pen(colorBack);
            e.Graphics.DrawRectangle(pBack, width - 120, height - 120, 80, 80);
            e.Graphics.FillRectangle(System.Drawing.Brushes.Red, 0, 0, width, height);*/
        }


        private void B_Browse_Vid_Click(object sender, RoutedEventArgs e)
        {
            FLog.Log("B_Browse_Vid_Click");
            SaveVid_dir = ServiceFunctions.Files.OpenDirectory(SaveVid_dir);
            TB_Directory_Vid.Text = SaveVid_dir;
        }

        private void B_Browse_Photo_Click(object sender, RoutedEventArgs e)
        {
            FLog.Log("B_Browse_Photo_Click");
            SavePhoto_dir = ServiceFunctions.Files.OpenDirectory(SavePhoto_dir);
            TB_Directory_Photo.Text = SavePhoto_dir;
        }

        private void Form1_FormClosing(object sender, CancelEventArgs e)
        {
            FLog.Log("Form1_FormClosing");
            try { Save_cfg(Config_tag); } catch { }
            try { if (isRecording) B_StopCapture_Click(null, null); } catch { }
            if (IC_Control.LiveVideoRunning)
            {
                IC_Control.LiveStop();
            }

            try { Save_AppSettings(); } catch { }
            try { Dictionary_Save(); } catch { }
            try { Save_Flipstate(); } catch { }
          //  try { Save_cfg(ConfigNames[LastConfig_num]); } catch { }
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

        private void Save_Flipstate()
        {
            bool Flip_Hor = IC_Control.DeviceFlipHorizontal;
            bool Flip_Ver = IC_Control.DeviceFlipVertical;
            List<string> Flips = new List<string>();
            Flips.Add("<Flip Settings>");
            Flips.Add(Flip_Hor.ToString());
            Flips.Add(Flip_Ver.ToString());
            Flips.Add("</Flip Settings>");
            ServiceFunctions.Files.Write_txt("FlipSettings.xml", Flips);
        }

        private void QuiteKey(object sender, ExecutedRoutedEventArgs e)
        {
            Form1_FormClosing(null, null);
            Application.Current.Shutdown();
            return;
        }

        private void CodecPropKey(object sender, ExecutedRoutedEventArgs e)
        {
            B_CodecProp_Click(null, null);
        }

        private void FullScreenSwitchKey(object sender, ExecutedRoutedEventArgs e)
        {
            if (FullScrin) { MinimizeWindow(); FullScrin = false; }
            else { MaximizeWindow(); FullScrin = true; }
        }

        private void TrB_ExposureVal_Scroll(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            FLog.Log("TrB_ExposureVal_Scroll");
            try
            {
                double value = Exposure_Slide2real(TrB_ExposureVal.Value);
                LoadExposure_ToCam(ref AbsValExp, value);
                Device_state = IC_Control.SaveDeviceState();
                double promval = (Exposure_Slide2real(TrB_ExposureVal.Value));
                if (promval > 1) NUD_Exposure.Value = promval;
                else NUD_Exposure.Value = promval;
                // ChangingActivatedTextBoxExp = true;

            }
            catch (Exception ex) { }
        }
        private void NUD_Exposure_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            FLog.Log("NUD_Exposure_ValueChanged");
            if ((!vcdProp.Automation[VCDIDs.VCDID_Exposure]))
            {
                int toslide = 0;
                toslide = Exposure_real2slide((float)NUD_Exposure.Value);
                if ((toslide < (TrB_ExposureVal.Maximum + 1)) && (toslide > (TrB_ExposureVal.Minimum - 1)))
                    TrB_ExposureVal.Value = toslide;
                else
                    TrB_ExposureVal.Value = Exposure_real2slide(AbsValExp.Default);

                Device_state = IC_Control.SaveDeviceState();
            }
        }

        private void ChB_ExposureAuto_CheckedChanged(object sender, RoutedEventArgs e)
        {
            FLog.Log("ChB_ExposureAuto_CheckedChanged");
            vcdProp.Automation[VCDIDs.VCDID_Exposure] = ChB_ExposureAuto.IsChecked ?? false;
            TrB_ExposureVal.IsEnabled = !(ChB_ExposureAuto.IsChecked ?? false);
            if ((!(ChB_ExposureAuto.IsChecked ?? false)) && (!(ChB_GainAuto.IsChecked ?? false)) && (!(ChB_BrightnessAuto.IsChecked ?? false)))
            {
                TimerForRenew.Stop();
                TimerForRenew.IsEnabled = false;
            }
            else
            {
                TimerForRenew.IsEnabled = true;
                TimerForRenew.Start();
            }
            Exposure_Auto = vcdProp.Automation[VCDIDs.VCDID_Exposure];
            Device_state = IC_Control.SaveDeviceState();
        }

        private void TrB_GainVal_Scroll(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            FLog.Log("TrB_GainVal_Scroll");
            vcdProp.RangeValue[VCDIDs.VCDID_Gain] = (int)TrB_GainVal.Value;
            NUD_Gain.Value = TrB_GainVal.Value;
            Device_state = IC_Control.SaveDeviceState();
        }

        private void NUD_Gain_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            FLog.Log("NUD_Gain_ValueChanged");
            if ((!vcdProp.Automation[VCDIDs.VCDID_Gain]))
            {
                int toslide = 0;
                toslide = Convert.ToInt32(NUD_Gain.Value);
                if ((toslide < (TrB_GainVal.Maximum + 1)) && (toslide > (TrB_GainVal.Minimum - 1)))
                    TrB_GainVal.Value = toslide;
                else
                    TrB_GainVal.Value = vcdProp.DefaultValue(VCDIDs.VCDID_Gain);
                Device_state = IC_Control.SaveDeviceState();
            }
        }

        private void ChB_GainAuto_CheckedChanged(object sender, RoutedEventArgs e)
        {
            FLog.Log("ChB_GainAuto_CheckedChanged");
            vcdProp.Automation[VCDIDs.VCDID_Gain] = ChB_GainAuto.IsChecked ?? false;
            TrB_GainVal.IsEnabled = !(ChB_GainAuto.IsChecked ?? false);
            if ((!(ChB_ExposureAuto.IsChecked ?? false)) && (!(ChB_GainAuto.IsChecked ?? false)) && (!(ChB_BrightnessAuto.IsChecked) ?? false))
            {
                TimerForRenew.Stop();
                TimerForRenew.IsEnabled = false;
            }
            else
            {
                TimerForRenew.IsEnabled = true;
                TimerForRenew.Start();
            }
            Gain_Auto = vcdProp.Automation[VCDIDs.VCDID_Gain];
            Device_state = IC_Control.SaveDeviceState();
        }

        private void TrB_Brightness_Scroll(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            FLog.Log("TrB_Brightness_Scroll");
            vcdProp.RangeValue[VCDIDs.VCDID_Brightness] = (int)TrB_Brightness.Value;
            NUD_Brightness.Value = TrB_Brightness.Value;
            Device_state = IC_Control.SaveDeviceState();
        }

        private void NUD_Brightness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            FLog.Log("NUD_Brightness_ValueChanged");
            if (vcdProp.AutoAvailable(VCDIDs.VCDID_Brightness))
            {
                if ((!vcdProp.Automation[VCDIDs.VCDID_Brightness]))
                {
                    int toslide = 0;
                    toslide = Convert.ToInt32(NUD_Brightness.Value);
                    if ((toslide < (TrB_Brightness.Maximum + 1)) && (toslide > (TrB_Brightness.Minimum - 1)))
                        TrB_Brightness.Value = toslide;
                    else
                        TrB_Brightness.Value = vcdProp.DefaultValue(VCDIDs.VCDID_Brightness);
                }
            }
            else
            {

                int toslide = 0;
                toslide = Convert.ToInt32(NUD_Brightness.Value);
                if ((toslide < (TrB_Brightness.Maximum + 1)) && (toslide > (TrB_Brightness.Minimum - 1)))
                    TrB_Brightness.Value = toslide;
                else
                    TrB_Brightness.Value = vcdProp.DefaultValue(VCDIDs.VCDID_Brightness);

            }
            Device_state = IC_Control.SaveDeviceState();
        }

        private void ChB_BrightnessAuto_CheckedChanged(object sender, RoutedEventArgs e)
        {
            FLog.Log("ChB_BrightnessAuto_CheckedChanged");
            vcdProp.Automation[VCDIDs.VCDID_Brightness] = ChB_BrightnessAuto.IsChecked ?? false;
            TrB_Brightness.IsEnabled = !(ChB_BrightnessAuto.IsChecked ?? false);
            if ((!(ChB_ExposureAuto.IsChecked ?? false)) && (!(ChB_GainAuto.IsChecked ?? false)) && (!(ChB_BrightnessAuto.IsChecked) ?? false))
            {
                TimerForRenew.Stop();
                TimerForRenew.IsEnabled = false;
            }
            else
            {
                TimerForRenew.IsEnabled = true;
                TimerForRenew.Start();
            }
            Device_state = IC_Control.SaveDeviceState();

        }

        private void TimerForRenew_Tick(object sender, EventArgs e)
        {
            FLog.Log("TimerForRenew_Tick");
            try
            {
                Refresh_Values_on_Trackbars();
            }
            catch { }
        }
        bool AutoExp_wasEnabled_beforeRecording = false;
        bool RecordingNeeded = false;
        List<Bitmap> bmp_list = new List<Bitmap>();
        private void B_StartCapture_Click(object sender, RoutedEventArgs e)
        {
            FLog.Log("B_StartCapture_Click");

            try
            {
                if (!isRecording)
                {
                    AutoExp_wasEnabled_beforeRecording = Disable_AutoExposure_ctrl();
                    StartRecording();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void B_StopCapture_Click(object sender, RoutedEventArgs e)
        {
            FLog.Log("B_StopCapture_Click");
            /*RecordingNeeded = false;
            writer.Close();*/


            try
            {
                if (isRecording)
                {
                    try { Enable_AutoExposure_ctrl(AutoExp_wasEnabled_beforeRecording); } 
                    catch(Exception exc)
                    {
                        FLog.Log("Error on B_StopCapture_Click: error in Enable_AutoExposure_ctrl(). ORIGINAL: " + exc.Message);
                    }
                    StopRecording();
                    FLog.Log("Recording stopped successfully");
                    Enable_AutoExposure_ctrl(AutoExp_wasEnabled_beforeRecording);
                }
            }
            catch (Exception exc)
            {
                FLog.Log("Error on record stop. ORIGINAL: " + exc.Message);
                MessageBox.Show(exc.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void B_Properties_Click(object sender, RoutedEventArgs e)
        {
            FLog.Log("B_Properties_Click");
            IC_Control.ShowPropertyDialog();
            Refresh_Values_on_Trackbars();
            Device_state = IC_Control.SaveDeviceState();
        }

        private void ChB_WhiteBalanceAuto_CheckedChanged(object sender, RoutedEventArgs e)
        {
            FLog.Log("ChB_WhiteBalanceAuto_CheckedChanged");
            if (ChB_WhiteBalanceAuto.IsChecked ?? false)
            {
                vcdProp.OnePush(VCDIDs.VCDID_WhiteBalance);
                WhiteBalanceTimer.Start();
                WB_FinalSum = 0;
                Save_cfg(Config_tag);
            }
        }

        private void WhiteBalanceTimer_Tick(object sender, EventArgs e)
        {
            FLog.Log("WhiteBalanceTimer_Tick");
            var Cur_WBSum = Get_WB_Sum();
            if (WB_FinalSum != Cur_WBSum) WB_FinalSum = Cur_WBSum;
            else
            {
                WhiteBalanceTimer.Stop();
                ChB_WhiteBalanceAuto.IsChecked = false;
            }
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
            FLog.Log("B_Snapshot_Click");
            string dataName = FindLast_Photo(SavePhoto_dir, (TB_FIO.Text + "_" + TB_CurrentDate.Text + "_" + TB_HistoryNumber.Text), ".tiff");
            string FullPathAndName = dataName;
            IC_Control.MemorySaveImage(FullPathAndName);
        }

        private void B_Cam_Select_Click(object sender, RoutedEventArgs e)
        {
            FLog.Log("B_Cam_Select_Click");
            try
            {
                Save_AppSettings();
                if (isRecording) StopRecording();
                if (IC_Control.LiveVideoRunning) IC_Control.LiveStop();
                IC_Control.ShowDeviceSettingsDialog();

                Init_Sliders(IC_Control);
                Load_ic_cam_easy(IC_Control);

                IMG_H_now = IC_Control.ImageHeight;
                IMG_W_now = IC_Control.ImageWidth;
                Adapt_Size_ofCont((IC_Control as System.Windows.Forms.Control), IMG_W_now, IMG_H_now, 0.8, 1); // cam reselect
                FormatAdaptation(IMG_W_now, IMG_H_now);

                vcdProp.Automation[VCDIDs.VCDID_WhiteBalance] = false;
                Load_AppSettings();
                Device_state = IC_Control.SaveDeviceState();
            }
            catch (Exception exc)
            { MessageBox.Show(exc.Message); }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            FLog.Log("Form1_Resize");
            if (Everything_loaded)
            {
                if (FullScrin) Adapt_Size_ofCont((IC_Control as System.Windows.Forms.Control), IMG_W_now, IMG_H_now, 1, 1); // resize
                else Adapt_Size_ofCont((IC_Control as System.Windows.Forms.Control), IMG_W_now, IMG_H_now, 0.8, 1);
                FormatAdaptation(IMG_W_now, IMG_H_now);
            }
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
            VideoIndex = 0;
            PhotoIndex = 0;
        }

        private void TB_CurrentDate_TextChanged(object sender, TextChangedEventArgs e)
        {
            FLog.Log("TB_CurrentDate_TextChanged");
            VideoIndex = 0;
            PhotoIndex = 0;
        }

        private void TB_HistoryNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            FLog.Log("TB_HistoryNumber_TextChanged");
            VideoIndex = 0;
            PhotoIndex = 0;
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
            FLog.Log("BGW_LiveVideoRecording_DoWork");
            System.Diagnostics.Stopwatch STWRec = new System.Diagnostics.Stopwatch();
            STWRec.Start();
            while (isRecording || !(sender as BackgroundWorker).CancellationPending)
            {
                if (STWRec.Elapsed.TotalMilliseconds > 300)
                {
                    STWRec.Restart();
                    Action action = () => IC_Control.Display();
                    IC_Control.BeginInvoke(action);
                }
            }
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
        private void IC_Control_ImageAvailable(object sender, ICImagingControl.ImageAvailableEventArgs e)
        {
            frames++;
            STW_fps.Restart();
            //this.BeginInvoke((Action)(() => this.Text = (frames.ToString()+" "+ Timer_camera_checker.Enabled.ToString()+" " + Timer_camera_checker.Interval.ToString())));

            if (Everything_loaded)
            {

                try
                {

                    ImgBuffer_RGB = IC_Control.ImageActiveBuffer;

                    if (RecordingNeeded)
                    {
                        //this.BeginInvoke((Action)(() => FLog.Log("RECORDING NEEDED")));
                        //5 attempt. ffmpeg

                        // using (var final = new Bitmap(ImgBuffer_RGB.Bitmap))
                        //{
                        writer_ffmpeg.WriteVideoFrame(ImgBuffer_RGB.Bitmap);

                        if (frames % 30 == 0) this.Dispatcher.BeginInvoke((Action)(() => FLog.Log("+30 frames recorded")));
                        if ((!RecordingNeeded) && (writer_ffmpeg.IsOpen))
                            try { writer_ffmpeg.Close(); } catch { }


                        // }

                        // this.BeginInvoke((Action)(() => this.Text = (stw_frameproc.Elapsed.TotalMilliseconds).ToString()));
                    }
                }
                catch (Exception ex)
                {
                    // An exception that occurs here cannot be handled elsewhere. 
                    // Therefore, if you are using the ImageAvailable event, watch the debug
                    // output window of your Visual Studio because the message (see below)
                    // will appear there.
                    //LogError(ex.Message);
                    // MessageBox.Show(ex.Message);
                    this.Dispatcher.BeginInvoke((Action)(() => FLog.Log("Frame record error: " + ex.Message)));
                    IC_Control.LiveStop();
                }

            }
        }
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
                if ((STW_fps.Elapsed.TotalMilliseconds > 1000) && (STW_fps.Elapsed.TotalMilliseconds > 20 * (double)(NUD_Exposure.Value??0)))//но камера по непонятной причине отвалилась
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
            bool wasrecording = isRecording;
            if (isRecording) this.Dispatcher.BeginInvoke((Action)(() => StopRecording()));

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
                }
            }
            //this.BeginInvoke((Action)(() => this.Text = "Device state: " + (IC_Control.DeviceValid ? "valid" : "invalid")));
            this.Dispatcher.BeginInvoke((Action)(() => FLog.Log("Device state: " + (IC_Control.DeviceValid ? "valid" : "invalid"))));
            Camera_restart_need = false;
            STW_fps.Restart();

            this.Dispatcher.BeginInvoke((Action)(() =>
            {                
                Init_Sliders(IC_Control);
                Load_ic_cam_easy(IC_Control);
                Refresh_Values_on_Trackbars();
                if (wasrecording) StartRecording();

                Enable_AutoExposure_ctrl(Exposure_Auto);
                File.Delete("Config_" + LastConfig_tag + ".xml");
                try { Save_cfg(LastConfig_tag); } catch { }
            }));

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
                if (FullScrin) Adapt_Size_ofCont((IC_Control as System.Windows.Forms.Control), IMG_W_now, IMG_H_now, 1, 1); // resize
                else Adapt_Size_ofCont((IC_Control as System.Windows.Forms.Control), IMG_W_now, IMG_H_now, 0.8, 1);
                FormatAdaptation(IMG_W_now, IMG_H_now);
            }
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            string themePath = "";

            if (toggleTheme.IsChecked == true)
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
            try
            {
                Timer_camera_checker.Stop();
                Config_tag = ((sender as RenameableToggleButton).Tag as string); //Вычленяем номер конфигурации

                //IC_Control.SaveDeviceStateToFile(ConfigNames[LastConfig_num]);

                IC_Control.LiveStop();
                try { Save_cfg(LastConfig_tag); } catch { }
                Load_cfg(Config_tag);
               /* NUD_Gain.Value = local_vcdprop.RangeValue[VCDIDs.VCDID_Gain]; //Костыль. Почему-то именно усиление выставляется на неправильное значение. 
                TrB_GainVal.Value = local_vcdprop.RangeValue[VCDIDs.VCDID_Gain];*/

                Load_ic_cam_easy(IC_Control);
                IMG_H_now = IC_Control.ImageHeight;
                IMG_W_now = IC_Control.ImageWidth;
                Adapt_Size_ofCont((IC_Control as System.Windows.Forms.Control), IMG_W_now, IMG_H_now, 0.8, 1); // cam reselect
                FormatAdaptation(IMG_W_now, IMG_H_now);
                IC_Control.LiveStart();

                LastConfig_tag = Config_tag;
                try { Refresh_Values_on_Trackbars(); }
                catch
                {
                    FLog.Log("Не удалось обновить значения на ползунках");
                }
                Timer_camera_checker.Start();

            }
            catch(Exception exc)
            {
                FLog.Log("Ошибка при переключении конфигураций");
            }
            for (int i = 0; i < renameableButtonsConfigs.Count; i++)
            {
                if(renameableButtonsConfigs[i] != toggleButton && renameableButtonsConfigs[i].toggleButon.IsChecked == true)
                {
                    renameableButtonsConfigs[i].toggleButon.IsChecked = false;
                }
            }
        }

        private void RenameableToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            
        }

        private void RenameableToggleButton_OnApplyChanges(object sender, RoutedEventArgs eventArgs)
        {
            string local_tag = (sender as RenameableToggleButton).Tag as string;
            string local_text = (sender as RenameableToggleButton).Text;
            ConfigsNamesDictionary[local_tag] = local_text;
            Dictionary_Save();
        }

        private void RenameableToggleButton_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void RenameableToggleButton_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private void Refresh_IC_BackColor()
        {
            System.Windows.Media.Color windowColor = (this.Background as SolidColorBrush).Color;
            IC_Control.BackColor = System.Drawing.Color.FromArgb(windowColor.A, windowColor.R, windowColor.G, windowColor.B);
            Host.Background = new SolidColorBrush(windowColor);
        }
    }
}
