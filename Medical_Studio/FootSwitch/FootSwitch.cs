using Medical_Studio.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;
using System.Management;

namespace Medical_Studio.FootSwitch
{
    public enum Keys : int { None = 0, LAlt = 56, LCtrl = 29, P = 25, G = 34 };

    public enum ClickType : int { None = 0, Short = 1, Long = 2, DoubleShort = 3,
                                  LongShort = 4, ShortLong = 5, DoubleLong = 6 };

    public class FootSwitch : BaseViewModel, IDisposable
    {
        private GlobalKeyboardHook globalKeyboardHook;

        public delegate void ClickResultHandler(ClickType clickType, DateTime clickTime);
        public event ClickResultHandler OnClickResult;
        public delegate void FirstClickHandler(DateTime firstClickTime);
        public event FirstClickHandler OnFirstClick;

        public FootSwitch()
        {
            try
            {
                //Load intervals from file
                LoadIntervalsFromFile("FootSwitchConfig.cfg");
            }
            catch (Exception) { }

            globalKeyboardHook = new GlobalKeyboardHook();
            globalKeyboardHook.KeyboardPressed += GlobalKeyboardHook_KeyboardPressed;

            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 40);
            timer.IsEnabled = false;

            CheckDeviceConnected();

            deviceCheckTimer.Tick += DeviceCheckTimer_Tick;
            deviceCheckTimer.Interval = new TimeSpan(0, 0, 0, 5);
            deviceCheckTimer.IsEnabled = true;
        }

        private bool CheckDeviceConnected()
        {
            DeviceConnected = IsUsbDeviceConnected(devicePID, deviceVID);
            return deviceConnected;
        }

        private void DeviceCheckTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                CheckDeviceConnected();
            }
            catch(Exception ex) { }
        }

        private bool deviceConnected = false;
        public bool DeviceConnected
        {
            get => deviceConnected;
            set
            {
                deviceConnected = value;
                OnPropertyChanged("DeviceConnected");
            }
        }

        public bool Pressed
        {
            get => sysPressed && keyPressed;
        }

        private void GlobalKeyboardHook_KeyboardPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            switch (e.KeyboardState)
            {
                case GlobalKeyboardHook.KeyboardState.KeyDown:
                case GlobalKeyboardHook.KeyboardState.SysKeyDown:
                    KeyDown(e);
                    break;
                case GlobalKeyboardHook.KeyboardState.KeyUp:
                case GlobalKeyboardHook.KeyboardState.SysKeyUp:
                    KeyUp(e);
                    /*MessageBox.Show(String.Format("Flags {0}\nHwScan {1}\nVirtual Code {2}", e.KeyboardData.Flags,
               e.KeyboardData.HardwareScanCode, e.KeyboardData.VirtualCode));*/

                    break;
            }
            e.Handled = false;
        }

        private bool sysPressed = false;
        public bool SysPressed
        {
            get => sysPressed;
        }

        private bool keyPressed = false;
        public bool KeyPressed
        {
            get => keyPressed;
        }

        private int sysKey = (int)Keys.LCtrl;
        private int key = (int)Keys.G;

        public int SysKey => sysKey;
        public int Key => key;

        private int state = state_idle;

        private const int state_idle = 0;
        private const int state_firstMes = 1;
        private const int state_gapMes = 2;
        private const int state_secondMes = 3;
        private const int state_resulting = 4;

        private string devicePID = "E026", deviceVID = "1A86";

        private ClickType firstClickType = ClickType.None;
        private ClickType secondClickType = ClickType.None;

        private DateTime pressedTime = new DateTime(0);

        private DispatcherTimer timer = new DispatcherTimer();
        private DispatcherTimer deviceCheckTimer = new DispatcherTimer();

        //Интервалы
        private int long_interval = 1000; //Левая граница лонг клика
        private int long_interval_ex = 2000; //Если педаль не была отпущена, то это время тоже означает long click
        private int gap_interval = 350;
        private int short_interval = 400;

        private Stopwatch watch = new Stopwatch();

        private void KeyDown(GlobalKeyboardHookEventArgs e)
        {
            if(e.KeyboardData.HardwareScanCode ==  (int)sysKey)
            {
                if (!sysPressed)
                {
                    sysPressed = true;
                }

            }
            if (e.KeyboardData.HardwareScanCode == (int)key)
            {
                if (!keyPressed)
                {
                    keyPressed = true;
                }

            }

            if (keyPressed && sysPressed)
            {
                if(state == state_idle)
                {
                    state = state_firstMes;
                    pressedTime = DateTime.Now;
                    firstClickType = ClickType.None;
                    secondClickType = ClickType.None;
                    watch.Restart();
                    timer.IsEnabled = true;
                    OnFirstClick?.Invoke(pressedTime);
                }
                else if(state == state_gapMes)
                {
                    state = state_secondMes;
                    watch.Restart();
                }
            }
            OnPropertyChanged("Pressed");
            OnPropertyChanged("PressProgress");
        }

        public double PressProgress
        {
            get
            {
                if (state == state_firstMes || state == state_secondMes)
                {
                    double pr = 1d*watch.ElapsedMilliseconds / long_interval;
                    if (pr > 1) pr = 1d;
                    if (pr < 0) pr = 0d;
                    return pr;
                }
                else
                    return 0;
            }
        }

        private void KeyUp(GlobalKeyboardHookEventArgs e)
        {
            if (e.KeyboardData.HardwareScanCode == (int)sysKey)
            {
                sysPressed = false;
            }
            if (e.KeyboardData.HardwareScanCode == (int)key)
            {
                keyPressed = false;
            }
            if(!keyPressed || !sysPressed)
            {
                long elapsed = watch.ElapsedMilliseconds;
                if(state == state_firstMes)
                {
                    if(elapsed >= long_interval)
                    {
                        //Long Click
                        firstClickType = ClickType.Long;
                    }
                    else
                    {
                        if (elapsed <= short_interval)
                        {
                            //Short Click
                            firstClickType = ClickType.Short;
                        }
                    }
                    watch.Restart();
                    state = state_gapMes;
                }
                else if(state == state_secondMes)
                {
                    if(watch.ElapsedMilliseconds >= long_interval)
                    {
                        //Long Click
                        secondClickType = ClickType.Long;
                    }
                    else
                    {
                        if (elapsed <= short_interval)
                        {
                            //Short Click
                            secondClickType = ClickType.Short;
                        }
                    }
                    Resulting();
                }
            }
            OnPropertyChanged("Pressed");
            OnPropertyChanged("PressProgress");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(state == state_gapMes)
            {
                //Проверка на OneClick.
                if(watch.ElapsedMilliseconds >= gap_interval)
                {
                    //Это One Click (long or short)
                    secondClickType = ClickType.None;
                    Resulting();
                }
            }
            else if (keyPressed && sysPressed)
            {
                if(state == state_firstMes)
                {
                    if(watch.ElapsedMilliseconds >= long_interval_ex)
                    {
                        //first long
                        firstClickType = ClickType.Long;
                        Resulting();
                    }
                }
                else if(state == state_secondMes)
                {
                    if(watch.ElapsedMilliseconds >= long_interval_ex)
                    {
                        //second long
                        secondClickType = ClickType.Long;
                        Resulting();
                    }
                }
            }

            OnPropertyChanged("PressProgress");
            CommandManager.InvalidateRequerySuggested();
        }

        private void Resulting()
        {
            state = state_resulting;
            timer.IsEnabled = false;

            //ИтОГИ
            ClickType clickType = GetTypeByTwoClicks(firstClickType, secondClickType);
            if (clickType != ClickType.None)
                OnClickResult?.Invoke(clickType, pressedTime);

            state = state_idle;
        }

        private static ClickType GetTypeByTwoClicks(ClickType firstClickType, ClickType secondClickType)
        {
            if (firstClickType == ClickType.Short)
            {
                if (secondClickType == ClickType.Short)
                {
                    return ClickType.DoubleShort;
                }
                else if (secondClickType == ClickType.Long)
                {
                    return ClickType.ShortLong;
                }
            }
            else if (firstClickType == ClickType.Long)
            {
                if (secondClickType == ClickType.Short)
                {
                    return ClickType.LongShort;
                }
                else if (secondClickType == ClickType.Long)
                {
                    return ClickType.DoubleLong;
                }
            }
            if (firstClickType == ClickType.Short && secondClickType == ClickType.None)
            {
                return ClickType.Short;
            }
            else if (firstClickType == ClickType.Long && secondClickType == ClickType.None)
            {
                return ClickType.Long;
            }

            return ClickType.None;
        }

        public static bool IsUsbDeviceConnected(string pid, string vid)
        {
            using (var searcher = 
              new System.Management.ManagementObjectSearcher(@"Select * From Win32_USBControllerDevice"))
            {
                using (var collection = searcher.Get())
                {
                    foreach (var device in collection)
                    {
                        var usbDevice = Convert.ToString(device);
                        var props = device.Properties;
                        if (usbDevice.Contains(pid) && usbDevice.Contains(vid))
                            return true;
                    }
                }
            }
            return false;
        }

        private void LoadIntervalsFromFile(string filename)
        {
            if(File.Exists(filename))
            {
                using(StreamReader sr = new StreamReader(filename))
                {
                    char[] splitChars = new char[] { '=', ';', '#' };
                    while(!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();

                        string[] parts = line.Split(splitChars, StringSplitOptions.RemoveEmptyEntries);
                        if(parts.Length > 1)
                        {
                            int value = 0;
                            try
                            {
                                if (parts[1].ToLower().Contains('x'))
                                    value = Convert.ToInt32(parts[1], 16);
                                else
                                    int.TryParse(parts[1], out value);
                            }
                            catch (Exception) { }
                            
                            string varname = parts[0].ToLower().Replace(" ", String.Empty);
                            
                            switch(varname)
                            {
                                case "short":
                                    short_interval = value;
                                    break;
                                case "gap":
                                    gap_interval = value;
                                    break;
                                case "long":
                                    long_interval = value;
                                    break;
                                case "long_ex":
                                    short_interval = value;
                                    break;
                                case "pid":
                                    devicePID = parts[1].Replace(" ", String.Empty);
                                    break;
                                case "vid":
                                    deviceVID = parts[1].Replace(" ", String.Empty);
                                    break;
                                case "sysKey":
                                    sysKey = value;
                                    break;
                                case "Key":
                                    key = value;
                                    break;
                            }
                        }
                    }

                    sr.Close();
                }
            }
        }

        public void Dispose()
        {
            globalKeyboardHook?.Dispose();
            deviceCheckTimer.Stop();
        }
    }
}
