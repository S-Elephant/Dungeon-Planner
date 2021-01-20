using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace DungeonPlanner
{
    public class DrawControl : Panel
    {
        public const int DPI = 96;
        public Bitmap TheImage = null;
        public Timer RefreshTimer = new Timer();

        public DrawControl()
        {
            RefreshTimer.Enabled = true;
            RefreshTimer.Interval = 150;
            RefreshTimer.Tick += new EventHandler(t_Tick);
            DoubleBuffered = true;
            this.BackgroundImageLayout = ImageLayout.None;
        }

        void t_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

        public void Fill(Color color)
        {
            TheImage.Fill(color);
        }

        public void DrawOnCursorLocation(Bitmap bmp)
        {
            TheImage.DrawOnto(bmp, Utils.GetLocalMousePos(this));
        }

        public void AutoResize()
        {
            this.Size = TheImage.Size;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (TheImage != null)
            {
                this.BackgroundImage = TheImage;

                // Call the OnPaint method of the base class.
                //base.OnPaint(pe);

                //pe.Graphics.DrawImage(TheImage, Point.Empty);
            }
        }
    }
}