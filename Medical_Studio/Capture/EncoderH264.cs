using MediaFoundation;
using MediaFoundation.Misc;
using Medical_Studio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medical_Studio.Capture
{
    public enum EncoderTypes : int { Unknown, H264 };
    public class Encoder : BaseViewModel
    {
        public virtual EncoderTypes EncoderType { get; } = EncoderTypes.Unknown;
        public virtual string Name { get; } = "";
    }

    public class EncoderH264 : Encoder
    {
        public override string Name => "H264";
        public override EncoderTypes EncoderType => EncoderTypes.H264;

        private int quality = 70;
        public int Quality
        {
            get => quality;
            set
            {
                quality = value;
                OnPropertyChanged("Quality");
            }
        }

        public int QualityMin => 1;
        public int QualityMax => 100;

        private int averageBitrate = 200000000;
        public int AverageBitrate
        {
            get => averageBitrate;
            set
            {
                averageBitrate = value;
                OnPropertyChanged("AverageBitrate");
            }
        }

        public int AverageBitrateMin => 1000000;
        public int AverageBitrateMax => 500000000;

        public enum H264Modes : int { Quality, AverageBitrate }
        private H264Modes mode = H264Modes.AverageBitrate;
        public H264Modes Mode
        {
            get => mode;
            set
            {
                mode = value;
                OnPropertyChanged("Mode");
            }
        }
    }
}
