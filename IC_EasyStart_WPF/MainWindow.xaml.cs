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

        string[] ConfigNames = new string[3] { "Default_config.xml", "Config_1.xml", "Config_2.xml" };
        int Config_num = 0; //0 - default
        int LastConfig_num = 0;
        string SaveVid_dir = "Video";
        string SavePhoto_dir = "Photo";

        string App_cfg_name = "App_prop.cfg";

        int IMG_W_now = 0;
        int IMG_H_now = 0;
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

        public MainWindow()
        {
            FLog = new ServiceFunctions.UI.Log.FileLogger("Log_" + ServiceFunctions.UI.Get_TimeNow_String() + ".txt");
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

            ChB_Config_0.Checked += ChB_Config_N_Checked;
            ChB_Config_0.Unchecked += ChB_Config_N_Unchecked;
            ChB_Config_1.Checked += ChB_Config_N_Checked;
            ChB_Config_1.Unchecked += ChB_Config_N_Unchecked;
            ChB_Config_2.Checked += ChB_Config_N_Checked;
            ChB_Config_2.Unchecked += ChB_Config_N_Unchecked;

            ChB_WhiteBalanceAuto.Checked += ChB_WhiteBalanceAuto_CheckedChanged;
            ChB_WhiteBalanceAuto.Unchecked += ChB_WhiteBalanceAuto_CheckedChanged;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IC_Control = new ICImagingControl();
            Host.Child = IC_Control;

            /*IC_Control.ShowDeviceSettingsDialog();
            if (IC_Control.DeviceValid) IC_Control.LiveStart();*/

            TB_CurrentDate.Text = ServiceFunctions.UI.GetDateString();
            TIS.Imaging.LibrarySetup.SetLocalizationLanguage("ru");
            //this.KeyPreview = true;
            if (File.Exists(ConfigNames[0]))
            {
                try
                {
                    Load_cfg(ConfigNames[0], true);
                    FLog.Log("Load_cfg() call finished succesfully");
                }
                catch (Exception exc)
                {
                    //  MessageBox.Show(exc.Message);
                    FLog.Log("ERROR - Load_cfg(). ORIGINAL:" + exc.Message);
                }
            }

            try
            {
                if (!IC_Control.DeviceValid)
                {
                    IC_Control.ShowDeviceSettingsDialog();
                    if (!IC_Control.DeviceValid)
                    {
                        MessageBox.Show("Не было выбрано ни одного устройства", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                        FLog.Log("No devices selected by user or loaded");
                        //Application.Exit();
                    }
                }
                try
                {
                    MainConfigPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\" + App_cfg_name;
                    Load_AppSettings();
                    FLog.Log("Load_AppSettings() call finished succesfully");
                }
                catch { FLog.Log("ERROR - Load_AppSettings() error"); }
                try
                {
                    Init_Sliders(IC_Control);
                    FLog.Log("Init_Sliders() call finished succesfully");
                    Load_ic_cam_easy(IC_Control);
                    FLog.Log("Load_ic_cam_easy() call finished succesfully");
                }
                catch { FLog.Log("ERROR - Init_Sliders or Load_ic_cam_easy  error"); }

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

                try
                {
                    Load_FlipState();
                    FLog.Log("Load_FlipState() call finished succesfully");
                }
                catch (Exception exc)
                { FLog.Log("ERROR - Load_flipstate error "); }

                try
                {
                    object sender_f = null;
                    if (Config_num == 1) sender_f = ChB_Config_1;
                    else if (Config_num == 2) sender_f = ChB_Config_2;
                    else sender_f = ChB_Config_0;

                    (sender_f as CheckBox).IsChecked = true;
                    FLog.Log("Check configs function call finished succesfully");
                }
                catch
                {
                    FLog.Log("ERROR - Check configs error");
                }

                if (vcdProp.AutoAvailable(VCDIDs.VCDID_WhiteBalance))
                    vcdProp.Automation[VCDIDs.VCDID_WhiteBalance] = false;

                //Prepare_encoder();

                try { Refresh_Values_on_Trackbars(); FLog.Log("Refresh_Values_on_Trackbars() call finished succesfully"); }
                catch { FLog.Log("ERROR - Refreshing Values on TrB error"); }

                try
                {
                    Prepare_encoder2("D:\\video.avi", (int)IC_Control.DeviceFrameRate, 25000 * 1000);
                    FLog.Log("Encoder preparing is succesful");
                }
                catch (Exception exc)
                {
                    FLog.Log("ERROR - Encoder preparing finished this error: " + exc.Message);
                }
                Device_name = IC_Control.Device;
                Timer_camera_checker.Start();
                STW_fps.Start();

                Everything_loaded = true;
                FLog.Log("Initialization end");
            }
            catch (Exception ext)
            {
                FLog.Log("ERROR - Form_Load() error");
                MessageBox.Show(ext.Message);
                Everything_loaded = true;
            }
            finally { if (IC_Control.DeviceValid) IC_Control.LiveStart(); }
        }

        //private void ChB_Config_N_CheckedChanged(object sender, RoutedEventArgs e)
        //{
        //    FLog.Log("ChB_Config_N_CheckedChanged");
        //    Timer_camera_checker.Stop();
        //    var ctrl = sender as CheckBox;
        //    //Config_num = Convert.ToInt32(ctrl.Name.Last().ToString());
        //    Config_num = (int) (sender as RadioButton).Tag;

        //    // IC_Control.SaveDeviceStateToFile(ConfigNames[LastConfig_num]);
            
        //    if (ctrl.IsChecked ?? false)
        //    {
        //        for (int i = 0; i < ConfigNames.Count(); i++)
        //        {
        //            /*var Current_ChB = (TLP_Configs.Controls.Find("ChB_Config_" + (i).ToString(), false)[0]);
        //            (Current_ChB as CheckBox).CheckedChanged -= ChB_Config_N_CheckedChanged;
        //            if (Convert.ToInt32((Current_ChB as CheckBox).Name.Last().ToString()) != Config_num)
        //                (Current_ChB as CheckBox).Checked = false;
        //            (Current_ChB as CheckBox).CheckedChanged += ChB_Config_N_CheckedChanged;*/
        //        }
        //        IC_Control.LiveStop();
        //        Save_cfg(ConfigNames[LastConfig_num]);
        //        Load_cfg(ConfigNames[Config_num]);
        //        // Refresh_Values_on_Trackbars();

        //        /*NUD_Gain.Value = vcdProp.RangeValue[VCDIDs.VCDID_Gain]; //Костыль. Почему-то именно усиление выставляется на неправильное значение. 
        //        TrB_GainVal.Value = vcdProp.RangeValue[VCDIDs.VCDID_Gain];*/

        //        Load_ic_cam_easy(IC_Control);
        //        IMG_H_now = IC_Control.ImageHeight;
        //        IMG_W_now = IC_Control.ImageWidth;
        //        Adapt_Size_ofCont((IC_Control as System.Windows.Forms.Control), IMG_W_now, IMG_H_now, 0.8, 1); // cam reselect
        //        FormatAdaptation(IMG_W_now, IMG_H_now);
        //        IC_Control.LiveStart();

        //    }
        //    else
        //    {
        //        /*var Current_ChB = ChB_Config_0;
        //        if (ctrl.Name != "ChB_Config_0")
        //        {
        //            Config_num = 0;
        //            (Current_ChB as CheckBox).Checked = true;
        //        }
        //        else
        //        {
        //            (Current_ChB as CheckBox).CheckedChanged -= ChB_Config_N_CheckedChanged;
        //            (Current_ChB as CheckBox).Checked = true;
        //            (Current_ChB as CheckBox).CheckedChanged += ChB_Config_N_CheckedChanged;
        //        }*/
        //    }
        //    LastConfig_num = Config_num;
        //    Refresh_Values_on_Trackbars();
        //    Timer_camera_checker.Start();
        //}

        private void ChB_Config_N_Checked(object sender, RoutedEventArgs e)
        {
            FLog.Log("ChB_Config_N_Checked");
            Timer_camera_checker.Stop();
            Config_num = (int)(sender as RadioButton).Tag; //Вычленяем номер конфигурации

            // IC_Control.SaveDeviceStateToFile(ConfigNames[LastConfig_num]);

            IC_Control.LiveStop();

            Load_cfg(ConfigNames[Config_num]);
            // Refresh_Values_on_Trackbars();

            /*NUD_Gain.Value = vcdProp.RangeValue[VCDIDs.VCDID_Gain]; //Костыль. Почему-то именно усиление выставляется на неправильное значение. 
            TrB_GainVal.Value = vcdProp.RangeValue[VCDIDs.VCDID_Gain];*/

            Load_ic_cam_easy(IC_Control);
            IMG_H_now = IC_Control.ImageHeight;
            IMG_W_now = IC_Control.ImageWidth;
            Adapt_Size_ofCont((IC_Control as System.Windows.Forms.Control), IMG_W_now, IMG_H_now, 0.8, 1); // cam reselect
            FormatAdaptation(IMG_W_now, IMG_H_now);
            IC_Control.LiveStart();

            LastConfig_num = Config_num;
            Refresh_Values_on_Trackbars();
            Timer_camera_checker.Start();
        }

        private void ChB_Config_N_Unchecked(object sender, RoutedEventArgs e)
        {
            Save_cfg(ConfigNames[LastConfig_num]);
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
            try { if (isRecording) B_StopCapture_Click(null, null); } catch { }
            if (IC_Control.LiveVideoRunning)
            {
                IC_Control.LiveStop();
            }
            try { Save_AppSettings(); } catch { }
            try { Save_Flipstate(); } catch { }
            try { Save_cfg(ConfigNames[LastConfig_num]); } catch { }
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

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            FLog.Log("Form1_KeyDown");
            if ((e.Key == Key.Q) && Keyboard.Modifiers == ModifierKeys.Alt)
            {
                Form1_FormClosing(null, null);
                Application.Current.Shutdown();
                return;
            }
            else if ((e.Key == Key.C) && Keyboard.Modifiers == ModifierKeys.Alt)
            {
                B_CodecProp_Click(null, null);
            }
            else if ((e.Key == Key.F) && Keyboard.Modifiers == ModifierKeys.Alt)
            {
                if (FullScrin) { MinimizeWindow(); FullScrin = false; }
                else { MaximizeWindow(); FullScrin = true; }
            }
        }

        private void TrB_ExposureVal_Scroll(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            FLog.Log("TrB_ExposureVal_Scroll");
            try
            {
                double value = Exposure_Slide2real(TrB_ExposureVal.Value);
                LoadExposure_ToCam(ref AbsValExp, value);
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
        }

        private void TrB_GainVal_Scroll(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            FLog.Log("TrB_GainVal_Scroll");
            vcdProp.RangeValue[VCDIDs.VCDID_Gain] = (int)TrB_GainVal.Value;
            NUD_Gain.Value = TrB_GainVal.Value;
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
        }

        private void TrB_Brightness_Scroll(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            FLog.Log("TrB_Brightness_Scroll");
            vcdProp.RangeValue[VCDIDs.VCDID_Brightness] = (int)TrB_Brightness.Value;
            NUD_Brightness.Value = TrB_Brightness.Value;
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
        bool AutoExp_wasEnabled = false;
        bool RecordingNeeded = false;
        List<Bitmap> bmp_list = new List<Bitmap>();
        private void B_StartCapture_Click(object sender, RoutedEventArgs e)
        {
            FLog.Log("B_StartCapture_Click");

            try
            {
                if (!isRecording)
                {
                    AutoExp_wasEnabled = Disable_AutoExposure_ctrl();
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
                    if (AutoExp_wasEnabled) Enable_AutoExposure_ctrl();
                    StopRecording();
                    FLog.Log("Recording stopped successfully");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void B_Properties_Click(object sender, RoutedEventArgs e)
        {
            FLog.Log("B_Properties_Click");
            IC_Control.ShowPropertyDialog();
            Refresh_Values_on_Trackbars();
        }

        private void ChB_WhiteBalanceAuto_CheckedChanged(object sender, RoutedEventArgs e)
        {
            FLog.Log("ChB_WhiteBalanceAuto_CheckedChanged");
            if (ChB_WhiteBalanceAuto.IsChecked ?? false)
            {
                vcdProp.OnePush(VCDIDs.VCDID_WhiteBalance);
                WhiteBalanceTimer.Start();
                WB_FinalSum = 0;
                Save_cfg(ConfigNames[LastConfig_num]);
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
        private void Timer_camera_checker_Tick(object sender, EventArgs e)
        {
            if (!Camera_restart_need)
            {
                if ((STW_fps.Elapsed.TotalMilliseconds > 1000) && (STW_fps.Elapsed.TotalMilliseconds > 20 * (double)NUD_Exposure.Value))
                {
                    // Timer_camera_checker.Dispose(); 
                    Camera_restart_need = true;

                    // this.BeginInvoke((Action)(() => this.Text = "Device state: " + (IC_Control.DeviceValid ? "valid" : "invalid")));
                    this.Dispatcher.BeginInvoke((Action)(() => FLog.Log("Device state: " + (IC_Control.DeviceValid ? "valid" : "invalid"))));
                    if (IC_Control.DeviceValid)//check validicity
                    {
                        this.Dispatcher.BeginInvoke((Action)(() => FLog.Log("Waiting for translation start....")));
                        Camera_restart_need = false;
                        STW_fps.Restart();
                    }
                    else
                    {
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

            while (!IC_Control.DeviceValid)
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
                    IC_Control.Device = Device_name;
                    if (Device_state != null)
                        this.Dispatcher.BeginInvoke((Action)(() => IC_Control.LoadDeviceState(Device_state, true)));
                    //this.BeginInvoke((Action)(() => IC_Control.LiveStop()));
                    while (!IC_Control.LiveVideoRunning)
                    {
                        System.Threading.Thread.Sleep(200);
                        this.Dispatcher.BeginInvoke((Action)(() => IC_Control.LiveStart()));
                    }
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

                if (AutoExp_wasEnabled) Enable_AutoExposure_ctrl();
                else Disable_AutoExposure_ctrl();
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

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            string themePath = "";
            if(toggleTheme.IsChecked == true)
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
        }
    }
}
