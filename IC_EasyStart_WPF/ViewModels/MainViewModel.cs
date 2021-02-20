using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

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
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
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

        private bool videoCapturing = false;
        public bool VideoCapturing
        {
            get => videoCapturing;
            set
            {
                videoCapturing = value;
                OnPropertyChanged("VideoCapturing");
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
    }
}
