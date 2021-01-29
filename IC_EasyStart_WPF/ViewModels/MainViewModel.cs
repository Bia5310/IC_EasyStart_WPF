using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace IC_EasyStart_WPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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

        private double scaleMin = 0.5d;
        public double ScaleMin
        {
            get => scaleMin;
            set
            {
                scaleMin = value;
                OnPropertyChanged("ScaleMin");
            }
        }

        private double scaleIncrement = 0.1d; //10%
        public double ScaleIncrement
        {
            get => scaleIncrement;
            set
            {
                scaleIncrement = value;
                OnPropertyChanged("ScaleIncrement");
            }
        }

        private bool scaleAuto = false;
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
    }
}
