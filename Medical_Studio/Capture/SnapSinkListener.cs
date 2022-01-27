using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TIS.Imaging;

namespace Medical_Studio.Capture
{
    public class SnapSinkListener : IFrameNotificationSinkListener, ISnapFrame
    {
        

        public void FrameReceived(IFrame frame)
        {
            
        }

        public void SinkConnected(FrameType frameType)
        {
            
        }

        public void SinkDisconnected()
        {
            
        }

        public void SnapImage(string filename, BitmapEncoder bitmapEncoder)
        {
            
        }
    }
}
