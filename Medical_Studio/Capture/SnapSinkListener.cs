using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TIS.Imaging;

namespace Medical_Studio.Capture
{
    public class SnapSinkListener : IFrameNotificationSinkListener, ISnapFrame
    {
        private Bitmap bitmapImage = null;

        private bool snapImage = false;
        private bool imageSnaped = true;

        public virtual void FrameReceived(IFrame frame)
        {
            if(snapImage)
            {
                snapImage = false;
                imageSnaped = false;
                
                bitmapImage = frame.CreateBitmapCopy();
                
                imageSnaped = true;
            }
        }

        public virtual void SinkConnected(FrameType frameType)
        {
            snapImage = false;
            imageSnaped = true;
        }

        public virtual void SinkDisconnected()
        {
            
        }

        public void SnapImage(string filename, int timeout_ms)
        {
            snapImage = true;
            imageSnaped = false;

            Stopwatch sw = new Stopwatch();
            sw.Start();
            while(true)
            {
                if(sw.ElapsedMilliseconds >= timeout_ms || imageSnaped)
                {
                    break;
                }
                System.Threading.Thread.Sleep(50);
            }
            sw.Stop();

            if(imageSnaped && bitmapImage != null)
            {
                bitmapImage.Save(filename, ImageFormat.Tiff);
            }

            snapImage = false;
            imageSnaped = true;
        }
    }
}
