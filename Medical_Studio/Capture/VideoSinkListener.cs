using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIS.Imaging;
using MediaFoundation;
using System.Diagnostics;
using static Medical_Studio.NativeFunctions;
using System.Windows.Media.Imaging;

namespace Medical_Studio.Capture
{
    class VideoSinkListener : IFrameNotificationSinkListener, ISnapFrame
    {
        static VideoSinkListener()
        {
            if (videoSnapSinkCounter == 0)
                MF.Startup();

            videoSnapSinkCounter++;
        }

        private static int videoSnapSinkCounter = 0;

        private MediaFoundation.ReadWrite.IMFSinkWriter sinkWriter = null;
        private int streamIndex = 0;
        private IMFMediaBuffer mediaBuffer = null;

        private int fps = 0;
        public int FPS => fps;

        private bool pause = false;
        public bool Pause
        {
            get => pause;
            set
            {
                pause = value;
                if (pause)
                    stopwatch.Stop();
                else
                    stopwatch.Start();
            }
        }

        private FrameType frameType = null;
        public FrameType FrameType => frameType;

        private bool isDisposed = false;
        public bool IsDisposed => isDisposed;

        private Stopwatch stopwatch = new Stopwatch();
        private long lastTime = 0;

        private int bufferLength = 0;
        private int stride = 0;

        private bool successInitialized = false;
        private string filename = "";

        private CopyImageHandler copyImageHandler = null;

        //encoder settings
        private int bitrate = 200000000;
        private bool qualityMode = false;
        private int quality = 70;

        public VideoSinkListener(string filename, FrameType frameType, int fps, EncoderH264 encoderH264)
        {
            this.fps = fps;
            this.filename = filename;
            this.frameType = frameType;

            this.quality = encoderH264.Quality;
            this.qualityMode = encoderH264.Mode == EncoderH264.H264Modes.Quality;
            this.bitrate = encoderH264.AverageBitrate;
        }

        private void Init()
        {
            HResult HR = InitializeSinkWriter(filename, frameType.Width, frameType.Height, fps, out sinkWriter, out streamIndex);

            stride = frameType.Width * 4;
            bufferLength = stride * frameType.Height;

            HR = MF.CreateMemoryBuffer(bufferLength, out mediaBuffer);

            if (HR != HResult.S_OK)
                throw new Exception("Errors while MF initialization: " + Enum.GetName(typeof(HResult), HR));

            successInitialized = true;
        }

        private void CloseWriterThread()
        {
            if (sinkWriter != null)
            {
                sinkWriter.Finalize_();
                sinkWriter = null;
            }
        }

        public void CloseWriter()
        {
            System.Threading.Thread thread = new System.Threading.Thread(CloseWriterThread);
            thread.SetApartmentState(System.Threading.ApartmentState.MTA);
            thread.Start();
            thread.Join();
        }

        public void Dispose()
        {
            CloseWriter();

            videoSnapSinkCounter--;
            if (videoSnapSinkCounter == 0)
                MF.Shutdown();
            isDisposed = true;
        }

        public void FrameReceived(IFrame frame)
        {
            if (!successInitialized)
            {
                return;
            }

            if(pause)
            {
                return;
            }

            HResult hr = mediaBuffer.Lock(out IntPtr destPtr, out int pcbMaxLength, out int pcbCurrentLength);

            if (hr == HResult.S_OK)
            {
                IntPtr srcPtr = frame.GetIntPtr();

                unsafe
                {
                    copyImageHandler?.Invoke(
                        (byte*)srcPtr,
                        frame.FrameType.BytesPerLine,
                        (byte*)destPtr,
                        stride,
                        frameType.Height,
                        true
                        );
                }

                //hr = MF.CopyImage(destPtr, stride, srcPtr, frame.FrameType.BytesPerLine, frame.FrameType.BytesPerLine, frameType.Height);
            }

            mediaBuffer.Unlock();

            if (hr == HResult.S_OK)
                mediaBuffer.SetCurrentLength(bufferLength);

            IMFSample sample = null;

            if (hr == HResult.S_OK)
                hr = MF.CreateSample(out sample);

            if (hr == HResult.S_OK)
                hr = sample.AddBuffer(mediaBuffer);

            if (hr == HResult.S_OK)
                hr = sample.SetSampleTime(stopwatch.ElapsedMilliseconds * 10000);

            if (hr == HResult.S_OK)
            {
                hr = sample.SetSampleDuration(10 * 1000 * 1000 / fps);
            }

            if (hr == HResult.S_OK && sinkWriter != null)
            {
                hr = sinkWriter.WriteSample(streamIndex, sample);
            }
        }

        public void SinkConnected(FrameType frameType)
        {
            unsafe
            {
                switch (frameType.PixelFormat)
                {
                    case System.Drawing.Imaging.PixelFormat.Format32bppRgb:
                    case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
                        copyImageHandler = NativeFunctions.CopyImageRGB32toRGB32;
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
                        copyImageHandler = NativeFunctions.CopyImageRGB24toRGB32;
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format16bppGrayScale:
                    case System.Drawing.Imaging.PixelFormat.DontCare:
                        copyImageHandler = NativeFunctions.CopyImageGray16toRGB32;
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format8bppIndexed:
                        copyImageHandler = NativeFunctions.CopyImageGray8toRGB32;
                        break;
                    default:
                        throw new FormatException("This FrameType not supporded");
                }
            }

            /*if (frameType.PixelFormat != System.Drawing.Imaging.PixelFormat.Format32bppRgb &&
                frameType.PixelFormat != System.Drawing.Imaging.PixelFormat.Format24bppRgb &&
                frameType.PixelFormat != System.Drawing.Imaging.PixelFormat.Format32bppArgb &&
                frameType.PixelFormat != System.Drawing.Imaging.PixelFormat.Format32bppPArgb)
                throw new FormatException("This FrameType not supporded");*/

            System.Threading.Thread thread = new System.Threading.Thread(Init);
            thread.SetApartmentState(System.Threading.ApartmentState.MTA);
            thread.Start();
            thread.Join();
            stopwatch.Restart();
        }

        public void SinkDisconnected()
        {
            CloseWriter();
        }

        public static readonly Guid CODECAPI_AVEncCommonRateControlMode = new Guid(0x1c0608e9, 0x370c, 0x4710, 0x8a, 0x58, 0xcb, 0x61, 0x81, 0xc4, 0x24, 0x23);
        public static readonly Guid CODECAPI_AVEncCommonQuality = new Guid(0xfcbf57a3, 0x7ea5, 0x4b0c, 0x96, 0x44, 0x69, 0xb4, 0x0c, 0x39, 0xc3, 0x91);

        public enum eAVEncCommonRateControlMode : int
        {
            eAVEncCommonRateControlMode_CBR = 0,
            eAVEncCommonRateControlMode_PeakConstrainedVBR = 1,
            eAVEncCommonRateControlMode_UnconstrainedVBR = 2,
            eAVEncCommonRateControlMode_Quality = 3,
            eAVEncCommonRateControlMode_LowDelayVBR = 4,
            eAVEncCommonRateControlMode_GlobalVBR = 5,
            eAVEncCommonRateControlMode_GlobalLowDelayVBR = 6
        };

        private HResult InitializeSinkWriter(string filename, int width, int height, int fps, out MediaFoundation.ReadWrite.IMFSinkWriter sinkWriter, out int streamIndex)
        {
            HResult hr = MF.CreateAttributes(0, out IMFAttributes sinkWriterAttributes);

            sinkWriterAttributes.SetUINT32(MFAttributesClsid.MF_READWRITE_ENABLE_HARDWARE_TRANSFORMS, 1);

            if(qualityMode)
            {
                sinkWriterAttributes.SetUINT32(CODECAPI_AVEncCommonRateControlMode, (int)eAVEncCommonRateControlMode.eAVEncCommonRateControlMode_Quality);
                sinkWriterAttributes.SetUINT32(CODECAPI_AVEncCommonQuality, quality);
            }
            else
            {
                sinkWriterAttributes.SetUINT32(CODECAPI_AVEncCommonRateControlMode, (int)eAVEncCommonRateControlMode.eAVEncCommonRateControlMode_UnconstrainedVBR);
            }

            sinkWriterAttributes.SetUINT32(MFAttributesClsid.MF_READWRITE_ENABLE_HARDWARE_TRANSFORMS, 1);

            hr = MF.CreateSinkWriterFromURL(filename, null, sinkWriterAttributes, out sinkWriter);

            //set output
            IMFMediaType outMediaType = MF.CreateMediaType();
            outMediaType.SetGUID(MFAttributesClsid.MF_MT_MAJOR_TYPE, MFMediaType.Video);
            outMediaType.SetGUID(MFAttributesClsid.MF_MT_SUBTYPE, MFMediaType.H264);
            outMediaType.SetUINT32(MediaFoundation.MFAttributesClsid.MF_MT_AVG_BITRATE, 500000000);
            outMediaType.SetUINT32(MFAttributesClsid.MF_MT_INTERLACE_MODE, (int)MFVideoInterlaceMode.Progressive);
            outMediaType.SetSize(MediaFoundation.MFAttributesClsid.MF_MT_FRAME_SIZE, (uint)width, (uint)height);
            outMediaType.SetRatio(MediaFoundation.MFAttributesClsid.MF_MT_FRAME_RATE, (uint)fps, 1);
            outMediaType.SetRatio(MediaFoundation.MFAttributesClsid.MF_MT_PIXEL_ASPECT_RATIO, 1, 1);

            hr = sinkWriter.AddStream(outMediaType, out streamIndex);
            

            //set input
            IMFMediaType inMediaType = MF.CreateMediaType();
            inMediaType.SetGUID(MFAttributesClsid.MF_MT_MAJOR_TYPE, MFMediaType.Video);
            inMediaType.SetGUID(MFAttributesClsid.MF_MT_SUBTYPE, MFMediaType.ARGB32);
            inMediaType.SetUINT32(MFAttributesClsid.MF_MT_INTERLACE_MODE, (int)MFVideoInterlaceMode.Progressive);
            inMediaType.SetSize(MediaFoundation.MFAttributesClsid.MF_MT_FRAME_SIZE, (uint)width, (uint)height);
            inMediaType.SetRatio(MediaFoundation.MFAttributesClsid.MF_MT_FRAME_RATE, (uint)fps, 1);
            inMediaType.SetRatio(MediaFoundation.MFAttributesClsid.MF_MT_PIXEL_ASPECT_RATIO, 1, 1);

            hr = sinkWriter.SetInputMediaType(streamIndex, inMediaType, null);

            hr = sinkWriter.BeginWriting();

            return hr;
        }

        public void SnapImage(string filename, BitmapEncoder bitmapEncoder)
        {
            
        }
    }
}
