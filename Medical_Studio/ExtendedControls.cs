using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TIS.Imaging;

namespace IC_EasyStart_WPF
{
    class ExtendedButton : Button
    {
        public ExtendedButton()
        {
            SetStyle(ControlStyles.Opaque, true);
        }

        private const int WS_EX_TRANSPARENT = 0x20;
        private int opacity = 100;
        public int Opacity
        {
            get
            {
                return opacity;
            }
            set
            {
                if(opacity != value)
                {
                    opacity = value;
                    Invalidate();
                }
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | WS_EX_TRANSPARENT;
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            using (var brush = new SolidBrush(Color.FromArgb(this.opacity*255/100, this.BackColor)))
            {
                pevent.Graphics.FillRectangle(brush, this.ClientRectangle);
            }

            base.OnPaint(pevent);
        }
    }

    class ICImagingControlExt : ICImagingControl
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Pen p = new Pen(Color.FromArgb(128, 128, 0, 0));

            e.Graphics.DrawRectangle(p, 10, 10, 80, 80);
        }
    }
}
