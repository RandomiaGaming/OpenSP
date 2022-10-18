using System;
using System.Drawing;
using System.Windows.Forms;

namespace YAGMCBSoundPanel
{
    public sealed class ScaledButton : PictureBox
    {
        public double XMin;
        public double YMin;
        public double XMax;
        public double YMax;
        public ScaledButton(Image image, double xMin, double yMin, double xMax, double yMax, Form form)
        {
            BackgroundImage = image;
            BackgroundImageLayout = ImageLayout.Stretch;
            form.Controls.Add(this);
            XMin = xMin;
            YMin = yMin;
            XMax = xMax;
            YMax = yMax;
            form.Resize += OnResizeEvent;
            Resize += OnResizeEvent;
            RefreshSize();
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            pevent.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            base.OnPaint(pevent);
        }
        private void OnResizeEvent(object sender, EventArgs e)
        {
            RefreshSize();
        }
        public void RefreshSize()
        {
            Location = new Point((int)(Parent.ClientSize.Width * XMin), (int)(Parent.ClientSize.Height * YMin));
            Size = new Size((int)(Parent.Width * (XMax - XMin)), (int)(Parent.Height * (XMax - XMin)));
        }
    }
}
