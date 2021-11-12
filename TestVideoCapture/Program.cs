using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VideoWriterNET;

namespace TestVideoCapture
{
    class Program
    {
        static void Main(string[] args)
        {
            VideoWriter vw = VideoWriter.CreateVideoWriter();
            EncodingParameters encParams = new EncodingParameters();
            encParams.bitrate = 200000;
            encParams.fps = 30;
            encParams.height = 1280;
            encParams.width = 720;
            
            vw.Open("video1.wmv", encParams);

            Bitmap bmp = new Bitmap("testImage.png");

            BitmapData bd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            byte[] data = new byte[bmp.Width * bmp.Height * 4];
            Marshal.Copy(bd.Scan0, data, 0, data.Length);
            bmp.UnlockBits(bd);

            for(int i = 0; i < 10; i++)
            {
                vw.WriteFrame(data, i*1000);
            }

            vw.Close();
        }
    }
}
