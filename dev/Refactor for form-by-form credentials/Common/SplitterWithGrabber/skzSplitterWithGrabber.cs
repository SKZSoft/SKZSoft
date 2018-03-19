using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SKZSoft.Common.SplitterWithGrabber
{
    public partial class skzSplitterWithGrabber : SplitContainer
    {
        public skzSplitterWithGrabber()
        {
            InitializeComponent();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Refresh();
        }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool GrabberDots { get; set; }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool GrabberLine { get; set; }


        /// <summary>
        /// Pain the splitter bar
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);

            int width = (SplitterRectangle.Left - 1 + SplitterRectangle.Width / 2) - 1;
            int height = SplitterRectangle.Top - 1 + SplitterRectangle.Height / 2;

            if (GrabberLine)
            {
                // Do dashed line dowwn the splitter
                float[] dashValues = { 1, 2 };
                Pen pen = new Pen(SystemColors.ControlDark, 3);
                pen.DashPattern = dashValues;

                if (Orientation == Orientation.Horizontal)
                {
                    e.Graphics.DrawLine(pen, new Point(0, SplitterRectangle.Top), new Point(SplitterRectangle.Width, SplitterRectangle.Top));
                }
                else
                {
                    e.Graphics.DrawLine(pen, new Point(width, 0), new Point(width, SplitterRectangle.Height));
                }
            }

            if (GrabberDots)
            {
                // get center point of the splitter area and draw dots
                Point centerPoint = new Point(width, height);

                e.Graphics.FillEllipse(SystemBrushes.ControlText, centerPoint.X, centerPoint.Y, 3, 3);
                if (Orientation == System.Windows.Forms.Orientation.Horizontal)
                {
                    e.Graphics.FillEllipse(SystemBrushes.ControlText, centerPoint.X - 10, centerPoint.Y, 3, 3);
                    e.Graphics.FillEllipse(SystemBrushes.ControlText, centerPoint.X + 10, centerPoint.Y, 3, 3);
                }
                else
                {
                    e.Graphics.FillEllipse(SystemBrushes.ControlText, centerPoint.X, centerPoint.Y - 10, 3, 3);
                    e.Graphics.FillEllipse(SystemBrushes.ControlText, centerPoint.X, centerPoint.Y + 10, 3, 3);
                }
            }

        }
    }
}
