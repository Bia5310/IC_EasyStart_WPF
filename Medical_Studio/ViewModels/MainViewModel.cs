using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using TIS.Imaging;

namespace Medical_Studio.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private const string NameOfApplication = "Medical Studio";
        private const string AppVersion = "2.6 Beta";

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
                if (icImagingControl == null)
                    return false;

                return icImagingControl.LiveVideoRunning;
            }
            set
            {
                if (icImagingControl != null)
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
                if (icImagingControl == null)
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
                if (icImagingControl == null)
                    return;

                PauseVideoCapturing(value);
            }
        }

        public void StartLive()
        {
            if (icImagingControl == null)
                return;

            icImagingControl.LiveStart();

            OnPropertyChanged("IsLive");
        }

        public void StopLive()
        {
            if (icImagingControl == null)
                return;

            icImagingControl.LiveStop();

            OnPropertyChanged("IsLive");
        }

        private BaseSink oldSink = null;
        private MediaStreamSink mediaStreamSink = null;

        public void StartVideoCapturing()
        {
            if (icImagingControl == null)
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
            if (icImagingControl == null)
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
            if(mediaStreamSink != null)
            {
                mediaStreamSink.SinkModeRunning = pause;

                videoOnPause = mediaStreamSink.SinkModeRunning;
                OnPropertyChanged("VideoOnPause");
            }
        }

        private readonly string codecsFileName = "codecSettings.bin";

        public void LoadVideoCodecSettings()
        {
            

        }

        public void SaveVideoCodecSettings()
        {

        }
    }
}
