using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using TIS.Imaging;
using LDZ_Code;
//using H264_Classes;
using AForge.Video;
using AForge.Video.FFMPEG;
using System.Windows.Media;

namespace Medical_Studio
{
    partial class MainWindow
    {
        Size Size_for_Resizing = new Size();
        //Exponential Exposure
        double alpha = 0, beta = 0, xenta = 0, zF = 1000000;
        
        //VideoRecording
        bool isRecording = false;
        private TIS.Imaging.BaseSink m_OldSink;
        private bool m_OldLiveMode;
        private TIS.Imaging.MediaStreamSink m_Sink;
        int VideoIndex = 0;
        int PhotoIndex = 0;

        Size Size_was = new Size(1280, 720);
        Point Location_was = new Point(0, 0);

        //videowriting new
        string aviPath;
        System.IO.FileStream aviFile;




        private void Set_appropriate_params()
        {
            var local_vcdprop = new TIS.Imaging.VCDHelpers.VCDSimpleProperty(IC_Control.VCDPropertyItems);
            var local_AbsValExp = (VCDAbsoluteValueProperty)IC_Control.VCDPropertyItems.FindInterface(VCDIDs.VCDID_Exposure +
                    ":" + VCDIDs.VCDElement_Value + ":" + VCDIDs.VCDInterface_AbsoluteValue);
            LoadExposure_ToCam(ref local_AbsValExp, 0.016f);
            local_vcdprop.RangeValue[VCDIDs.VCDID_Gain] = (local_vcdprop.RangeMin(VCDIDs.VCDID_Gain)  + local_vcdprop.RangeMax(VCDIDs.VCDID_Gain))/2;

        }
        private void Init_Properties(ICImagingControl ic)
        {
            vcdProp = new TIS.Imaging.VCDHelpers.VCDSimpleProperty(ic.VCDPropertyItems);
            var a = ic.VCDPropertyItems.CategoryMap;
            var b = ic.VCDPropertyItems.Count;
            var c = ic.VCDPropertyItems.get_Item(0);
            
            AbsValExp = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface(VCDIDs.VCDID_Exposure +
                    ":" + VCDIDs.VCDElement_Value + ":" + VCDIDs.VCDInterface_AbsoluteValue);
            
        }
        private void Init_Sliders(ICImagingControl ic) //функция инициализации ползунка для регулировки отдельных свойст камеры
        {
            bool locallogging = false; int il = 0; //отладочный режим
            if (locallogging) { FLog.Log("Init_sliders L"+ (il++).ToString());}
            Init_Properties(ic);
            string VCDID_Exp = VCDIDs.VCDID_Exposure;
            string VCDID_Gain = VCDIDs.VCDID_Gain;
            string VCDID_Brightness = VCDIDs.VCDID_Brightness;
            if (locallogging) { FLog.Log("Init_sliders L" + (il++).ToString()); }

            try
            {
                if (!vcdProp.AutoAvailable(VCDID_Exp))//если невозможна автоматическая регулировка,отключить возможность ее включения
                {
                    ChB_ExposureAuto.IsEnabled = false;
                    if (locallogging) { FLog.Log("Init_sliders L_tr" + (il++).ToString()); }
                }
                else
                {
                    if (locallogging) { FLog.Log("Init_sliders F_tr" + (il++).ToString()); }
                    ChB_ExposureAuto.IsEnabled = true;
                    ChB_ExposureAuto.IsChecked = Exposure_Auto;
                    vcdProp.Automation[VCDID_Exp] = false;
                   /* var a = vcdProp.RangeValue[VCDID_Exp] = 5;
                    var b = AbsValExp.Value;*/

                }
            }
            catch
            {
                FLog.Log("Error on autoexposure detecting. Deiniting autoexposure...");
                ChB_ExposureAuto.IsEnabled = false;
            }
            if (locallogging) { FLog.Log("Init_sliders L" + (il++).ToString()); }

            try
            {
                if (!vcdProp.AutoAvailable(VCDID_Gain))//если невозможна автоматическая регулировка,отключить возможность ее включения
                {
                    ChB_GainAuto.IsEnabled = false;
                }
                else
                {
                    ChB_GainAuto.IsEnabled = true;
                    ChB_GainAuto.IsChecked = Gain_Auto;
                    vcdProp.Automation[VCDID_Gain] = false;
                }
            }
            catch
            {
                FLog.Log("Error on autogain detecting. Deiniting autogain...");
                ChB_GainAuto.IsEnabled = false;
            }
            if (locallogging) { FLog.Log("Init_sliders L" + (il++).ToString()); }

            try
            {
                if (!vcdProp.AutoAvailable(VCDID_Brightness))//если невозможна автоматическая регулировка,отключить возможность ее включения
                {
                    ChB_BrightnessAuto.IsEnabled = false;
                }
                else
                {
                    ChB_BrightnessAuto.IsEnabled = true;
                    ChB_BrightnessAuto.IsChecked = false;
                    vcdProp.Automation[VCDID_Brightness] = false;
                }
            }
            catch
            {
                FLog.Log("Error on autobrightness detecting. Deiniting autobrightness...");
                ChB_BrightnessAuto.IsEnabled = false;
            }
            if (locallogging) { FLog.Log("Init_sliders L" + (il++).ToString()); }

            try
            {
                if (locallogging) { FLog.Log("Init_sliders L_exp" + (il++).ToString()); }
                if (!vcdProp.Available(VCDID_Exp))
                {
                    if (locallogging) { FLog.Log("Init_sliders L_noexp" + (il++).ToString()); }
                    TrB_ExposureVal.IsEnabled = false;
                    NUD_Exposure.IsEnabled = false;
                }
                else
                {
                    if (locallogging) { FLog.Log("Init_sliders L_exp" + (il++).ToString()); }
                    TrB_ExposureVal.IsEnabled = true;
                    NUD_Exposure.IsEnabled = true;
                    if (locallogging) { FLog.Log("Init_sliders L_exp" + (il++).ToString()); }
                    double Az = TrB_ExposureVal.Minimum = ServiceFunctions.Math.PerfectRounding((AbsValExp.RangeMin * zF), 0);
                    double Bz = TrB_ExposureVal.Maximum = ServiceFunctions.Math.PerfectRounding((AbsValExp.RangeMax * zF), 0);
                    alpha = (AbsValExp.RangeMin * AbsValExp.RangeMax - 0.25 * 0.25) / (AbsValExp.RangeMin + AbsValExp.RangeMax - 2 * 0.25);
                    xenta = Math.Pow((AbsValExp.RangeMax - alpha) / (AbsValExp.RangeMin - alpha), (zF / ((Bz - Az))));
                    beta = (0.25 - alpha) / Math.Pow(xenta, (Bz + Az) / (2 * zF));
                    if (locallogging) { FLog.Log("Init_sliders L_exp" + (il++).ToString()); }
                    double val1slide = Az;
                    if (locallogging) { FLog.Log("Init_sliders L_exp_this" + (il++).ToString()); }
                    try { TrB_ExposureVal.Value = Exposure_real2slide(AbsValExp.Value); }
                    catch
                    {
                        if (TrB_ExposureVal.Value < TrB_ExposureVal.Minimum) TrB_ExposureVal.Value = TrB_ExposureVal.Minimum;
                        else if (TrB_ExposureVal.Value > TrB_ExposureVal.Maximum) TrB_ExposureVal.Value = TrB_ExposureVal.Maximum;
                    }
                    if (locallogging) { FLog.Log("Init_sliders L_exp" + (il++).ToString()); }
                    TrB_ExposureVal.TickFrequency = (TrB_ExposureVal.Maximum - TrB_ExposureVal.Minimum) / 10;
                    // ChangingActivatedTextBoxExp = false;
                    if (locallogging) { FLog.Log("Init_sliders L_exp" + (il++).ToString()); }
                    NUD_Exposure.FormatString = "F" + DetectTheNumberOfDecimalPositions(AbsValExp.RangeMin);
                    NUD_Exposure.Value = ServiceFunctions.Math.PerfectRounding(Exposure_Slide2real(TrB_ExposureVal.Value), 4);
                    if (locallogging) { FLog.Log("Init_sliders L_exp" + (il++).ToString()); }
                    //ChangingActivatedTextBoxExp = true;
                }
            }
            catch
            {
                FLog.Log("Error on exposure detecting. Deiniting exposure...");
                TrB_ExposureVal.IsEnabled = false;
                NUD_Exposure.IsEnabled = false;
            }
            if (locallogging) { FLog.Log("Init_sliders L" + (il++).ToString()); }

            try
            {
                if (!vcdProp.Available(VCDID_Gain))
                {
                    TrB_GainVal.IsEnabled = false;
                    NUD_Gain.IsEnabled = false;
                }
                else
                {

                    var a = vcdProp.RangeValue[VCDID_Gain];
                    TrB_GainVal.IsEnabled = true;
                    NUD_Gain.IsEnabled = true;
                    TrB_GainVal.Minimum = vcdProp.RangeMin(VCDID_Gain);
                    TrB_GainVal.Maximum = vcdProp.RangeMax(VCDID_Gain);
                    TrB_GainVal.Value = a;
                    TrB_GainVal.TickFrequency = (TrB_GainVal.Maximum - TrB_GainVal.Minimum) / 10;
                    // ChangingActivatedTextBoxGain = false;
                    NUD_Gain.Minimum = vcdProp.RangeMin(VCDID_Gain);
                    NUD_Gain.Maximum = vcdProp.RangeMax(VCDID_Gain);
                    NUD_Gain.Value = TrB_GainVal.Value;
                    // ChangingActivatedTextBoxGain = true;
                }
            }
            catch
            {
                FLog.Log("Error on gain detecting. Deiniting gain...");
                TrB_GainVal.IsEnabled = false;
                NUD_Gain.IsEnabled = false;
            }
            if (locallogging) { FLog.Log("Init_sliders L" + (il++).ToString()); }

            try
            {
                if (!vcdProp.Available(VCDID_Brightness))
                {
                    TrB_Brightness.IsEnabled = false;
                    NUD_Brightness.IsEnabled = false;
                }
                else
                {
                    TrB_Brightness.IsEnabled = true;
                    NUD_Brightness.IsEnabled = true;

                    TrB_Brightness.Minimum = vcdProp.RangeMin(VCDID_Brightness);
                    TrB_Brightness.Maximum = vcdProp.RangeMax(VCDID_Brightness);
                    TrB_Brightness.Value = vcdProp.RangeValue[VCDID_Brightness];
                    TrB_Brightness.TickFrequency = (TrB_Brightness.Maximum - TrB_Brightness.Minimum) / 10;

                    // ChangingActivatedTextBoxGain = false;
                    NUD_Brightness.Minimum = vcdProp.RangeMin(VCDID_Brightness);
                    NUD_Brightness.Maximum = vcdProp.RangeMax(VCDID_Brightness);
                    NUD_Brightness.Value = TrB_Brightness.Value;
                    // ChangingActivatedTextBoxGain = true;
                }
            }
            catch
            {
                FLog.Log("Error on brightness detecting. Deiniting brightness...");
                TrB_Brightness.IsEnabled = false;
                NUD_Brightness.IsEnabled = false;
            }

            if (locallogging) { FLog.Log("Init_sliders L" + (il++).ToString()); }
            if (locallogging) { FLog.Log("Init_sliders completed"); }
        }
        
        private int DetectTheNumberOfDecimalPositions(double value)
        {
           // if ((int)(value + (10e-6)) - (int)(value) != 0) value = value + (10e-6);
            double half = value; int decplaces =0;
            while((int)(half)==0)
            {
                half *= 10; decplaces++;
            }
            return decplaces;
        }
        private double Exposure_Slide2real(double arg)
        {           
            double a = Math.Pow(xenta, arg / zF);
            double b = beta * a;
            return (alpha + b);
        }

        private int Exposure_real2slide(double arg)
        {
            return (int)ServiceFunctions.Math.PerfectRounding((float)(zF * (Math.Log(((arg - alpha) / beta), xenta))), 0);
        }

        private static void LoadExposure_ToCam(ref VCDAbsoluteValueProperty var, double pvalue)
        {
            if (pvalue < var.RangeMin) var.Value = var.RangeMin;
            else if (pvalue > var.RangeMax) var.Value = var.RangeMax;
            else var.Value = pvalue;
        }

        private void Load_ic_cam_easy(ICImagingControl ic)
        {
            ic.ScrollbarsEnabled = true;
            // ReadAllSettingsFromFile(false);
            //    TestAvailability(false);
            bool liverun = ic.LiveVideoRunning;

            /* try
            {
                AnalyseFormats();
                m_oldSink = New_SetSelectedCamera_SignalStream_Format();
            }

            */


            try { IC_Control.ImageRingBufferSize = 2; } catch { }; //на всякий случай

            ic.LiveDisplayDefault = false; //если false, то позволяет изменения размеров окна

            ic.LiveCaptureLastImage = true; // отображает и захватывает последний фрейм при LiveStop;

            ic.LiveCaptureContinuous = true; //нужно для FormatAdaptation и граба фреймов


            if(!ic.LiveVideoRunning) ic.LiveStart();
           
        }

        private void Refresh_Values_on_Trackbars()
        {
            if (NUD_Exposure.Value != null)
            {
                NUD_Exposure.Value = AbsValExp.Value;
                TrB_ExposureVal.Value = Exposure_real2slide(AbsValExp.Value);
                vcdProp.Automation[VCDIDs.VCDID_Exposure] = Exposure_Auto; //добавлено 05022021. После перезапуска необходимо вручную восстанавливать значения 
                ChB_ExposureAuto.IsChecked = vcdProp.Automation[VCDIDs.VCDID_Exposure];
            }
            if (NUD_Gain.Value != null)
            {
                NUD_Gain.Value = vcdProp.RangeValue[VCDIDs.VCDID_Gain];
                TrB_GainVal.Value = vcdProp.RangeValue[VCDIDs.VCDID_Gain];
                vcdProp.Automation[VCDIDs.VCDID_Gain] = Gain_Auto; //добавлено 05022021. После перезапуска необходимо вручную восстанавливать значения 
                ChB_GainAuto.IsChecked = vcdProp.Automation[VCDIDs.VCDID_Gain];
            }
            if (NUD_Brightness.Value != null)
            {
                NUD_Brightness.Value = vcdProp.RangeValue[VCDIDs.VCDID_Brightness];
                TrB_Brightness.Value = vcdProp.RangeValue[VCDIDs.VCDID_Brightness];
            }
        }

        private void Save_AppSettings()
        {
            List<string> Str_2_write = new List<string>();
            if (SaveVid_dir == "") SaveVid_dir = "Video";
            if (SavePhoto_dir == "") SaveVid_dir = "Photo";
            Str_2_write.Add("<SaveVideo_dir>" + SaveVid_dir + "</SaveVideo_dir>");
            Str_2_write.Add("<SavePhoto_dir>" + SavePhoto_dir + "</SavePhoto_dir>");
            Str_2_write.Add("<LastConfig_tag>" + Config_tag + "</LastConfig_tag>");
            
            ServiceFunctions.Files.Write_txt(MainConfigPath, Str_2_write);
            
        }
        private void Load_AppSettings()
        {
            try
            {
                if (System.IO.File.Exists(App_cfg_name))
                {
                    string[] AllLines = System.IO.File.ReadAllLines(App_cfg_name);
                    for (int i = 0; i < AllLines.Count(); i++)
                    {
                        int startind2 = AllLines[i].IndexOf('<');
                        int finishind2 = AllLines[i].IndexOf('>');
                        string data = AllLines[i].Substring(startind2 + 1, finishind2 - startind2 - 1);
                        switch (data)
                        {
                            case "SaveVideo_dir":
                                {
                                    string toObject = CutFromEdges(AllLines[i]);
                                    if (System.IO.Directory.Exists(toObject)) SaveVid_dir = toObject;
                                    else
                                    {
                                        string data_path = System.IO.Path.Combine(Application.StartupPath, "Video");
                                        try { System.IO.Directory.CreateDirectory(data_path);
                                            //this.GetType().GetProperty("SaveVid_dir").SetValue(this, data_path);//let's try this
                                            SaveVid_dir = data_path; }
                                        catch { SaveVid_dir = ""; }
                                    }
                                    break;
                                }
                            case "SavePhoto_dir":
                                {
                                    string toObject = CutFromEdges(AllLines[i]);
                                    if (System.IO.Directory.Exists(toObject)) SavePhoto_dir = toObject;
                                    else
                                    {
                                        string data_path = System.IO.Path.Combine(Application.StartupPath, "Photo");
                                        try { System.IO.Directory.CreateDirectory(data_path); SavePhoto_dir = data_path; }
                                        catch { SavePhoto_dir = ""; }
                                    }
                                    break;
                                }

                            case "LastConfig_tag":
                                {
                                    string toObject = CutFromEdges(AllLines[i]);
                                    if (!string.IsNullOrEmpty(toObject)) Config_tag = toObject;
                                    else Config_tag = "default";
                                    LastConfig_tag = Config_tag;
                                    break;
                                }
                        }
                    }
                }
                else
                {
                    Load_Default_Settings();
                    Save_AppSettings();
                    Create_Directs_forPhotoVideo();
                }
            }
            catch
            {
                Load_Default_Settings();
            }
            TB_Directory_Vid.Text = SaveVid_dir;
            TB_Directory_Photo.Text = SavePhoto_dir;
        }
        private void Dictionary_Load()
        {
            try
            {
                Dictionary_Load_fromFile();
            }
            catch
            {
                Dictionary_Load_Default();
                Dictionary_Save();
            }
        }

        private void Dictionary_Load_Default()
        {
            int Max_modes = 4;
            ConfigsNamesDictionary = new Dictionary<string, string>();
            for (int i =0;i< Max_modes;i++)
                ConfigsNamesDictionary.Add("0_"+i.ToString(), "Config Phaco " + i.ToString());

            for (int i = 0; i < Max_modes; i++)
                ConfigsNamesDictionary.Add("1_" + i.ToString(), "Config Vitreo " + i.ToString());

            for (int i = 0; i < Max_modes; i++)
                ConfigsNamesDictionary.Add("2_" + i.ToString(), "Config User " + i.ToString());
        }
        private void Dictionary_Save()
        {
            List<string> List_2_file = new List<string>();
            for(int i = 0;i< ConfigsNamesDictionary.Count;i++)
            {
                string local_key = (i / 4).ToString() + "_" + (i % 4).ToString();
                List_2_file.Add(String.Format("<{0}>{1}</{0}>", local_key, ConfigsNamesDictionary[local_key]));
            }
            Files.Write_txt(ConfigNames_filename, List_2_file);
        }
        private void Dictionary_Load_fromFile()
        {
            var alpha = "beta";
            var gamma = nameof(alpha);

            ConfigsNamesDictionary = new Dictionary<string, string>();

            if (System.IO.File.Exists(ConfigNames_filename))
            {
                string[] AllLines = System.IO.File.ReadAllLines(ConfigNames_filename);
                for (int i = 0; i < AllLines.Count(); i++)
                {
                    int startind2 = AllLines[i].IndexOf('<');
                    int finishind2 = AllLines[i].IndexOf('>');
                    string data_tag = AllLines[i].Substring(startind2 + 1, finishind2 - startind2 - 1);
                    string text_on_but = CutFromEdges(AllLines[i]);
                    ConfigsNamesDictionary.Add(data_tag, text_on_but);
                    /*var modenum = data_tag.Substring(0,1);
                    var conf_num = data_tag.Substring(2, 1);
                    int local_index = Convert.ToInt32(modenum) * 4 + Convert.ToInt32(conf_num);
                    renameableButtonsConfigs[local_index].Text = text_on_but;*/
                    Find_RenameableBut_byTag(data_tag).Text = text_on_but;
                }
            }
            else
            {
                throw new Exception("Файл не существует");
            }
            
            
        }
        private RenameableToggleButton Find_RenameableBut_byTag(string Tag)
        {
            var modenum = Tag.Substring(0, 1);
            var conf_num = Tag.Substring(2, 1);
            int local_index = Convert.ToInt32(modenum) * 4 + Convert.ToInt32(conf_num);
            try
            {
                if ((renameableButtonsConfigs.Count > local_index))
                {
                    if ((string)renameableButtonsConfigs[local_index].Tag == Tag)
                    {
                        var reference = renameableButtonsConfigs[local_index];
                        return reference;
                    }
                    else
                    {
                        var list = FindVisualChildren<RenameableToggleButton>(this).Where(x => x.Tag != null && x.Tag.ToString() == Tag);
                        return list.First();
                    }
                }
                else return null;
            }
            catch { return null; }
        }
        public static IEnumerable<T> FindVisualChildren<T>(System.Windows.DependencyObject depObj) where T : System.Windows.DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < System.Windows.Media.VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    System.Windows.DependencyObject child = System.Windows.Media.VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
        private void Create_Directs_forPhotoVideo()
        {
            try
            {
                System.IO.Directory.CreateDirectory(SaveVid_dir);
                System.IO.Directory.CreateDirectory(SavePhoto_dir);
            }
            catch
            {

            }
        }
    
        private void Load_Default_Settings()
        {
            SaveVid_dir = System.IO.Path.Combine(Application.StartupPath,"Video"); 
            SavePhoto_dir = System.IO.Path.Combine(Application.StartupPath, "Photo");
            Config_tag = "default";
            LastConfig_tag = "default";
        }
        private string CutFromEdges(string target)
        {
            try
            {
                int startind = target.IndexOf('>');
                int finishind = target.LastIndexOf('<');
                string val = target.Substring(startind + 1, finishind - startind - 1);
                return val;
            }
            catch
            {
                return "1";
            }
        }

        private void FormatAdaptation(int WidthOfImage = -1,int HeightOfImage = -1)
        {
            var ic = IC_Control;
            if(WidthOfImage==-1) WidthOfImage = ic.ImageWidth;
            if(HeightOfImage==-1) HeightOfImage = ic.ImageHeight;
            int ControlWidth = ic.Width;
            int ControlHeight = ic.Height;
            float ZFactWidth = (float)ControlWidth / (float)WidthOfImage;
            float ZFactHeight = (float)ControlHeight / (float)HeightOfImage;
            float ZFactFinal = 100.0f;
            if (ZFactWidth >= ZFactHeight) ZFactFinal = ZFactHeight;
            else ZFactFinal = ZFactWidth;

            if (ic.LiveDisplayDefault == false)
            {
                ic.LiveDisplayZoomFactor = ZFactFinal;
                
                if (ic.LiveDisplayZoomFactor > 1.0) ic.ScrollbarsEnabled = true;
                else ic.ScrollbarsEnabled = false;
            }
            else
            {
                
            }

        }
        
        private void SetLiveDisplayZoomFactor(float zoomFactor)
        {
            IC_Control.LiveDisplayZoomFactor = zoomFactor;

            /*if (IC_Control.LiveDisplayZoomFactor > 1.0)
                IC_Control.ScrollbarsEnabled = true;
            else
                IC_Control.ScrollbarsEnabled = false;*/

            AdaptViewportControl();
        }

        private string FindLast_Video(string VidPathName,string CurrentName,string Extension = ".avi")
        {
            string dataName = CurrentName + "_" + VideoIndex.ToString() + Extension;
            while (System.IO.File.Exists(System.IO.Path.Combine(VidPathName, dataName)))
            {
                VideoIndex++;
                dataName = CurrentName + "_" + VideoIndex.ToString() + Extension;
            }
            return System.IO.Path.Combine(VidPathName, dataName);
        }
        private string FindLast_Photo(string PhotoPathName, string CurrentName, string Extension = ".tiff")
        {
            string dataName = CurrentName + "_" + PhotoIndex.ToString() + Extension;
            if (!System.IO.File.Exists(System.IO.Path.Combine(PhotoPathName, CurrentName + "_0" + Extension)))
            {
                PhotoIndex = 0;
                dataName = CurrentName + "_" + PhotoIndex.ToString() + Extension;
            }
            else
            while (System.IO.File.Exists(System.IO.Path.Combine(PhotoPathName, dataName)))
            {
                PhotoIndex++;
                dataName = CurrentName + "_" + PhotoIndex.ToString() + Extension;
            }
            return System.IO.Path.Combine(PhotoPathName, dataName);
        }
        private void StartRecording()
        {
            if (IC_Control.DeviceValid)
            {
                try
                {
                    string FIO = !string.IsNullOrEmpty(TB_FIO.Text) ? TB_FIO.Text + " " : "";
                    string H_num = !string.IsNullOrEmpty(TB_HistoryNumber.Text) ? TB_HistoryNumber.Text + " " : "";
                    string dataName = FindLast_Video(SaveVid_dir, FIO + H_num + TB_CurrentDate.Text);
                    string FullPathAndName = dataName;

                    Prepare_encoder2(FullPathAndName, (int)IC_Control.DeviceFrameRate, 20000 * 1000);
                    RecordingNeeded = true;
                    isRecording = true;
                    mainViewModel.VideoCapturing = true;
                    
                    //disabling all the staff

                    Switch_state_of_ctrls();
                }
                catch(Exception e)
                {
                    MessageBox.Show("Ошибка при подготовке к записи. Проверьте настройки. ");
                    //noErrors = false;
                }
             }   
        }
        private void StopRecording()
        {
            
            RecordingNeeded = false;
            isRecording = false;
            FLog.Log("L1 of stop....");
            // System.Threading.Thread.Sleep((int)(2 * NUD_Exposure.Value*1000)+100);
            try
            {
                System.Threading.Thread.Sleep((int)(2 * AbsValExp.Value * 1000) + 100);
            }
            catch
            {
                System.Threading.Thread.Sleep(500);
                FLog.Log("Can't read exposure...");
            }
            FLog.Log("L2 of stop....");
            if (writer_ffmpeg!=null)
                if (writer_ffmpeg.IsOpen) //запись закрывается в ImageAvalible, но если вдруг не закрылась, то тут
                try { writer_ffmpeg.Close(); FLog.Log("L2_special_closing of stop...."); } catch { FLog.Log("Error on L3...."); }
            //enabling all the staff
            Switch_state_of_ctrls();
            mainViewModel.VideoCapturing = false;
            FLog.Log("L3 of stop....");
        }
        private bool Disable_AutoExposure_ctrl()
        {
            //disabling autoexposure
            bool AutoExp_wasChecked = false;
            if ((vcdProp.AutoAvailable(VCDIDs.VCDID_Exposure)) && (!Camera_restart_need))
            {
                AutoExp_wasChecked = vcdProp.Automation[VCDIDs.VCDID_Exposure];
                ChB_ExposureAuto.IsChecked = false;
                ChB_ExposureAuto.IsEnabled = false;
                vcdProp.Automation[VCDIDs.VCDID_Exposure] = false;
            }
            return AutoExp_wasChecked;
        }
        private void Enable_AutoExposure_ctrl(bool wasChecked)
        {
            //enabling autoexposure, if was
            if ((vcdProp.AutoAvailable(VCDIDs.VCDID_Exposure)) && (!Camera_restart_need))
            {
                ChB_ExposureAuto.IsEnabled = vcdProp.AutoAvailable(VCDIDs.VCDID_Exposure);
                ChB_ExposureAuto.IsChecked = wasChecked;
                vcdProp.Automation[VCDIDs.VCDID_Exposure] = wasChecked;
            }
        }
        private void Switch_state_of_ctrls()
        {
            B_StartCapture.IsEnabled = !isRecording;
            B_StopCapture.IsEnabled = isRecording;
            B_Snapshot.IsEnabled = !isRecording;
            GB_Configs.IsEnabled = !isRecording;
            B_Cam_Select.IsEnabled = !isRecording;
            B_Properties.IsEnabled = !isRecording;
            ChB_WhiteBalanceAuto.IsEnabled = !isRecording;

            TB_Directory_Vid.IsEnabled = !isRecording;
            TB_Directory_Photo.IsEnabled = !isRecording;
            TB_FIO.IsEnabled = !isRecording;
            TB_CurrentDate.IsEnabled = !isRecording;
            TB_HistoryNumber.IsEnabled = !isRecording;
            B_Browse_Photo.IsEnabled = !isRecording;
            B_Browse_Vid.IsEnabled = !isRecording;
        }
       
        private void Load_cfg(string CFG_tag, bool Open_dev = true)
        {
            string CFG_name = "Config_" + CFG_tag + ".xml";
            bool isDataSaved = true;
            try { IC_Control.SaveDeviceStateToFile("data.xml"); } catch { isDataSaved = false; }
            if (System.IO.File.Exists(CFG_name))
            {
                try
                {
                    IC_Control.LoadDeviceStateFromFile(CFG_name, Open_dev);
                    Init_Properties(IC_Control);
                    Device_state = IC_Control.SaveDeviceState();
                }
                catch(Exception e)
                {
                    IC_Control.ShowDeviceSettingsDialog();
                    Init_Properties(IC_Control);
                    Device_state = IC_Control.SaveDeviceState();
                    if(isDataSaved) IC_Control.LoadDeviceStateFromFile("data.xml", Open_dev);
                }
            }
            else
            {
                // do nothing
            }
            System.IO.File.Delete("data.xml");
        }
        private void Save_cfg(string CFG_tag)
        {
            string CFG_name = "Config_" + CFG_tag + ".xml";
            IC_Control.SaveDeviceStateToFile(CFG_name);
            Device_state = IC_Control.SaveDeviceState();
        }
        private void Save_AllTheConfigs()
        {
            for(int i=0;i<3;i++)
                for(int j=0;j<4;j++)
                {
                    Save_cfg(i.ToString() + "_" + j.ToString());
                }
        }
        private static void SetDecimalPlaces(Xceed.Wpf.Toolkit.DoubleUpDown doubleUpDown, int decimalPlaces)
        {
            if (doubleUpDown == null)
                throw new NullReferenceException("DoubleUpDoun is NULL");
            if (decimalPlaces == 0)
                throw new Exception("decimal places must be > 0");

            doubleUpDown.FormatString = "F" + decimalPlaces.ToString();
        }

        VideoFileWriter writer_ffmpeg;

        private void Prepare_encoder2(string path, int pFPS, int BitsPerSecond)
        {
            int a = 0;
            int b = a + 3;
            writer_ffmpeg = new VideoFileWriter();
            writer_ffmpeg.Open(path, IMG_W_now, IMG_H_now, pFPS, VideoCodec.MPEG4, BitsPerSecond);
        }
        private int Get_WB_Sum()
        {
            var R = vcdProp.RangeValue[VCDIDs.VCDElement_WhiteBalanceRed];
            var G = vcdProp.RangeValue[VCDIDs.VCDElement_WhiteBalanceGreen];
            var B = vcdProp.RangeValue[VCDIDs.VCDElement_WhiteBalanceBlue];
            return (R + G + B);
        }
        private void Adapt_Size_ofCont(Control ctrl, int ImgW, int ImgH,double Width_Modifier, double Height_Modifier)
        {
            //double w = canvas.ActualWidth, h = canvas.ActualHeight;

            //double ratio = 

            //IC_Control.Width = (int) (0.5*w);
            //IC_Control.Height = (int) (0.5*h);

            //Size_for_Resizing = new Size((int)((double)Screen.PrimaryScreen.Bounds.Width * Width_Modifier), (int)(Screen.PrimaryScreen.Bounds.Height * Height_Modifier));

            Width_Modifier = 1;
            Height_Modifier = 1;

            double delta_h = 30;
            if (FullScrin) delta_h = 0;
            Size_for_Resizing = new Size((int)((Host.ActualWidth) * Width_Modifier * Scaling_of_monitor), 
                                         (int)((Host.ActualHeight- delta_h) * Height_Modifier * Scaling_of_monitor));
            int PanelNewWidth = Size_for_Resizing.Width;
            int PanelNewHeight = Size_for_Resizing.Height;
            ctrl.Dock = DockStyle.None;
            double Img_SizeRelation = (double)ImgW / (double)ImgH;
            double Pan_SizeRelation = (double)PanelNewWidth / (double)PanelNewHeight;
            if (Pan_SizeRelation > Img_SizeRelation)
            {//vertical is size limit
                ctrl.Height = PanelNewHeight;
                ctrl.Width = (int)((double)Img_SizeRelation * (double)ctrl.Height);
                ctrl.Location = new Point((PanelNewWidth - ctrl.Width) / 2, 0);
            }
            else
            {//Horizontal is size limit
                ctrl.Width = PanelNewWidth;
                ctrl.Height = (int)((double)ctrl.Width / (double)Img_SizeRelation);
                ctrl.Location = new Point(0, (PanelNewHeight - ctrl.Height + (int)delta_h) / 2);
            }
            ChangePos_of_FSBut();
            Font_Adaptation();
        }
        private void Font_Adaptation()
        {
            /*double CurHeight = this.Height;
            int SizeFact = 10;
            if (CurHeight > 1200) SizeFact = 12;
            else if (CurHeight < 720) SizeFact = 8;
            else SizeFact = (int)Math.Round(LDZ_Code.ServiceFunctions.Math.Interpolate_value(720, 8, 1200, 12, CurHeight));

            var Cur_Font = new Font("Microsoft Sans Serif", SizeFact);

            GrB_PatInfo.Font = Cur_Font;

            GB_Configs.Font = Cur_Font;
            ChB_Config_0.Font = Cur_Font;
            ChB_Config_1.Font = Cur_Font;
            ChB_Config_2.Font = Cur_Font;

            GrB_Settings.Font = Cur_Font;
            L_exp.Font = Cur_Font;
            L_gain.Font = Cur_Font;
            L_brightness.Font = Cur_Font;
            NUD_Exposure.Font = Cur_Font;
            NUD_Gain.Font = Cur_Font;
            NUD_Brightness.Font = Cur_Font;
            TrB_ExposureVal.Font = Cur_Font;
            TrB_GainVal.Font = Cur_Font;
            TrB_Brightness.Font = Cur_Font;
            ChB_ExposureAuto.Font = Cur_Font;
            ChB_GainAuto.Font = Cur_Font;
            ChB_BrightnessAuto.Font = Cur_Font;
            B_Cam_Select.Font = Cur_Font;
            B_Properties.Font = Cur_Font;
            ChB_WhiteBalanceAuto.Font = Cur_Font;

            B_StartCapture.Font = Cur_Font;
            B_StopCapture.Font = Cur_Font;
            B_Snapshot.Font = Cur_Font;

            GrB_Grabbing.Font = Cur_Font;
            TB_Directory_Vid.Font = Cur_Font;
            TB_Directory_Photo.Font = Cur_Font;
            B_Browse_Photo.Font = Cur_Font;
            B_Browse_Vid.Font = Cur_Font;
            L_photo_save.Font = Cur_Font;
            L_video_save.Font = Cur_Font;
            B_Snapshot.Font = Cur_Font;
            B_StartCapture.Font = Cur_Font;
            B_StopCapture.Font = Cur_Font;*/
        }

        private void ChangePos_of_FSBut()
        {
            //Point NewLocation_onPanel = (new Point(IC_Control.Location.X + IC_Control.Width, IC_Control.Location.Y + IC_Control.Height));
            //B_FS_Switcher_form.Location = new Point((int)((double)NewLocation_onPanel.X-100), (int)((double)NewLocation_onPanel.Y-100));

            B_FS_Switcher_form.Location = new Point((int) (Host.ActualWidth* Scaling_of_monitor - 30 - B_FS_Switcher_form.Width),
                                                     (int)(Host.ActualHeight* Scaling_of_monitor - 30 - B_FS_Switcher_form.Height));
        }

        System.Windows.GridLength column_width_old;
        System.Windows.WindowState state_old;

        private void MaximizeWindow()
        {
            Size_was = new Size((int) this.Width, (int) this.Height);
            Location_was = new Point((int)Left, (int)Top);
            state_old = this.WindowState; 

            this.WindowStyle = System.Windows.WindowStyle.None;
            this.WindowState = System.Windows.WindowState.Normal;
            this.WindowState = System.Windows.WindowState.Maximized;
            this.Top = 0;
            this.Left = 0;
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;
            toolBar.Visibility = System.Windows.Visibility.Collapsed;
            scrollViewver_left.Visibility = System.Windows.Visibility.Collapsed;
            gridSplitter_left.Visibility = System.Windows.Visibility.Collapsed;
            column_width_old = grid_main.ColumnDefinitions[0].Width;
            grid_main.ColumnDefinitions[0].Width = new System.Windows.GridLength(0, System.Windows.GridUnitType.Auto);

            FullScrin = true;
            //ВРЕМЕННО УБРАЛ
            /*Adapt_Size_ofCont((IC_Control as System.Windows.Forms.Control), IC_Control.ImageWidth, IC_Control.ImageHeight, 0.8, 1);
            FormatAdaptation(IMG_W_now, IMG_H_now);*/

            /*Size_was = this.Size;
            Location_was = new Point(this.Location.X,this.Location.Y);

            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = new Point(0, 0);
            this.Height = Screen.PrimaryScreen.Bounds.Height;
            this.Width = Screen.PrimaryScreen.Bounds.Width;

            for (int i = 0; i < TLP_BasicPanel.ColumnStyles.Count; i++)
                Widths_Cols.Add(TLP_BasicPanel.ColumnStyles[i].Width);
            for (int i = 0; i < TLP_BasicPanel.RowStyles.Count; i++)
                Heights_Rows.Add(TLP_BasicPanel.RowStyles[i].Height);
            TLP_BasicPanel.ColumnStyles[0].Width = 0;
            TLP_BasicPanel.ColumnStyles[1].Width = 100;
            FullScrin = true;
            

            Adapt_Size_ofCont((IC_Control as Control), IMG_W_now, IMG_H_now, 1, 1);
            FormatAdaptation(IMG_W_now, IMG_H_now);*/
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
            //Adapt_Size_ofCont((IC_Control as System.Windows.Forms.Control), IC_Control.ImageWidth, IC_Control.ImageHeight, 0.8, 1);
            //FormatAdaptation(IMG_W_now, IMG_H_now);
            CalculateZoomFactor((int)Host.ActualWidth, (int)Host.ActualHeight, IMG_W_now, IMG_H_now);

            /*this.FormBorderStyle = FormBorderStyle.Sizable;
            this.Size = Size_was;
            this.Location = Location_was;

            for (int i = 0; i < TLP_BasicPanel.ColumnStyles.Count; i++)
                TLP_BasicPanel.ColumnStyles[i].Width = Widths_Cols[i];
            for (int i = 0; i < TLP_BasicPanel.RowStyles.Count; i++)
                TLP_BasicPanel.RowStyles[i].Height = Heights_Rows[i];
            FullScrin = false;

            Adapt_Size_ofCont((IC_Control as Control), IMG_W_now, IMG_H_now, 0.8, 1); // Minimizing
            FormatAdaptation(IMG_W_now, IMG_H_now);*/
        }

        public static PixelFormat ConvertPixelFormats(System.Drawing.Imaging.PixelFormat pixelFormat)
        {//Не работает!
            switch(pixelFormat)
            {
                case System.Drawing.Imaging.PixelFormat.Alpha:
                    return PixelFormats.Gray8;
                case System.Drawing.Imaging.PixelFormat.Canonical:
                    return PixelFormats.Bgra32;
                case System.Drawing.Imaging.PixelFormat.Format16bppGrayScale:
                    return PixelFormats.Gray16;
                case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
                    return PixelFormats.Bgra32;
                case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
                    return PixelFormats.Bgr24;
                case System.Drawing.Imaging.PixelFormat.Format32bppPArgb:
                    return PixelFormats.Pbgra32;
                case System.Drawing.Imaging.PixelFormat.Format32bppRgb:
                    return PixelFormats.Bgr32;
                case System.Drawing.Imaging.PixelFormat.Format8bppIndexed:
                    return PixelFormats.Indexed8;
                case System.Drawing.Imaging.PixelFormat.Format1bppIndexed:
                    return PixelFormats.Indexed1;
                case System.Drawing.Imaging.PixelFormat.Format16bppRgb555:
                    return PixelFormats.Bgr555;
                case System.Drawing.Imaging.PixelFormat.Format16bppRgb565:
                    return PixelFormats.Bgr565;
                case System.Drawing.Imaging.PixelFormat.Format48bppRgb:
                    return PixelFormats.Rgb48;
            }
            return PixelFormats.Pbgra32;
        }

    }
}
