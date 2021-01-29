using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace IC_EasyStart_WPF.Viewport
{
    public class ViewportControl : FrameworkElement
    {
        #region Dependency Properties
        public static readonly DependencyProperty ScaleProperty;
        public static readonly DependencyProperty ScrollHorizontalProperty = null;
        public static readonly DependencyProperty ScrollVerticalProperty = null;

        public double Scale
        {
            get => (double)GetValue(ScaleProperty);
            set => SetValue(ScaleProperty, value);
        }

        public double ScrollHorizontal
        {
            get => (double)GetValue(ScrollHorizontalProperty);
            set => SetValue(ScrollHorizontalProperty, value);
        }

        public double ScrollVertical
        {
            get => (double)GetValue(ScrollVerticalProperty);
            set => SetValue(ScrollVerticalProperty, value);
        }
        #endregion

        static ViewportControl()
        {
            ScaleProperty = DependencyProperty.Register("Scale", typeof(double), typeof(ViewportControl), new FrameworkPropertyMetadata(1d,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
            ScrollHorizontalProperty = DependencyProperty.Register("ScrollHorizontal", typeof(double), typeof(ViewportControl), new FrameworkPropertyMetadata(0d,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
            ScrollVerticalProperty = DependencyProperty.Register("ScrollVertical", typeof(double), typeof(ViewportControl), new FrameworkPropertyMetadata(0d,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
        }

        private VisualCollection visualCollection;

        private ViewportDrawingVisual viewportDrawing;
        public ViewportDrawingVisual ViewportDrawing
        {
            get => viewportDrawing;
        }

        #region Coordinates and Transform

        private Matrix imageTransformMatrix;

        #endregion

        public ViewportControl()
        {
            visualCollection = new VisualCollection(this);

            viewportDrawing = new ViewportDrawingVisual();

            visualCollection.Add(viewportDrawing);

            CreateDrawingVisualRectangle();

            //TransformMatrix = new Matrix();

            Test();
        }

        // Provide a required override for the VisualChildrenCount property.
        protected override int VisualChildrenCount
        {
            get { return visualCollection.Count; }
        }

        // Provide a required override for the GetVisualChild method.
        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= visualCollection.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return visualCollection[index];
        }

        // Create a DrawingVisual that contains a rectangle.
        private void CreateDrawingVisualRectangle()
        {
            //DrawingVisual drawingVisual = new DrawingVisual();

            // Retrieve the DrawingContext in order to create new drawing content.
            DrawingContext drawingContext = viewportDrawing.RenderOpen();

            // Create a rectangle and draw it in the DrawingContext.
            Rect rect = new Rect(new System.Windows.Point(0, 100), new System.Windows.Size(320, 80));
            drawingContext.DrawRectangle(System.Windows.Media.Brushes.LightBlue, (System.Windows.Media.Pen)null, rect);
            
            // Persist the drawing content.
            drawingContext.Close();
        }

        public void Test()
        {
            var render = viewportDrawing.RenderOpen();

            Rect rect = new Rect(new System.Windows.Point(100, 100), new System.Windows.Size(320, 80));
            render.DrawRectangle(System.Windows.Media.Brushes.Red, (System.Windows.Media.Pen)null, rect);

            render.Close();
            
            //render = viewportDrawing.RenderOpen();

            //BitmapImage bmp = BitmapImage.Create();
            //render.DrawImage(wb, new Rect());

            //render.Close();

            
        }

        private WriteableBitmap writeableBitmap = null;

        public WriteableBitmap WriteableBitmap
        {
            get => writeableBitmap;
        }

        private int img_width = 0, img_height = 0;
        private PixelFormat pixelFormat;
        private static object lockerWriteableBitmap = new object();

        public unsafe void SetImageByBuffer(byte* ptr, int stride, int width, int height, PixelFormat pixelFormat, System.Windows.Threading.Dispatcher dispatcher)
        {
            lock(writeableBitmap)
            {
                if (writeableBitmap == null || writeableBitmap.Format != pixelFormat || writeableBitmap.Width != width || writeableBitmap.Height != height)
                {

                }
                writeableBitmap = new WriteableBitmap(width, height, 96, 96, pixelFormat, null);

                this.img_width = width;
                this.img_height = height;
                this.pixelFormat = pixelFormat;

                writeableBitmap.Lock();

                unsafe
                {
                    if (pixelFormat == PixelFormats.Bgr32)
                    {
                        byte* dest = (byte*)writeableBitmap.BackBuffer;
                        byte* src = (byte*)ptr;

                        int src_stride = writeableBitmap.BackBufferStride;

                        for (int j = 0; j < height; j++)
                        {
                            for (int i = 0; i < width * 4; i++)
                            {
                                dest[i] = src[i];
                            }

                            dest += stride;
                            src += stride;
                        }
                    }
                    else if (pixelFormat == PixelFormats.Bgr24)
                    {
                        //writeableBitmap
                        byte* dest = (byte*)writeableBitmap.BackBuffer;
                        byte* src = (byte*)ptr;

                        int src_stride = writeableBitmap.BackBufferStride;

                        for (int j = 0; j < height; j++)
                        {
                            for (int i = 0; i < width * 3; i++)
                            {
                                dest[i] = src[i];
                            }

                            dest += stride;
                            src += stride;
                        }
                    }
                }

                writeableBitmap.AddDirtyRect(new Int32Rect(0, 0, width, height));
                writeableBitmap.Unlock();
                writeableBitmap.Freeze();

                dispatcher.Invoke((Action)(() =>
                {
                    var render = viewportDrawing.RenderOpen();
                    render.DrawImage(writeableBitmap, new Rect(0, 0, width, height));
                    render.Close();
                }));

            }

        }
    }
}
