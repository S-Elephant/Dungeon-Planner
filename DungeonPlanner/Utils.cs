using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DungeonPlanner
{
    public static class Utils
    {
        public static int SnapToGrid(int location, int gridSize)
        {
            return location - location % gridSize;
        }

        public static Point SnapToGrid(Point location, Point gridSize)
        {
            return new Point(SnapToGrid(location.X, gridSize.X), SnapToGrid(location.Y, gridSize.Y));
        }

        public static bool PointIsInRect(Point point, Rectangle rect)
        {
            return point.X > rect.Left && point.X < rect.Right && point.Y < rect.Bottom && point.Y > rect.Top;
        }

        public static Point GetLocalMousePos(Control ctrl)
        {
            Point screenPos = System.Windows.Forms.Cursor.Position;
            return ctrl.PointToClient(screenPos);
        }
    }

    public static class DrawUtils
    {
        public static Bitmap AlphaBlendAbsolute(this Bitmap b, byte alphaValue)
        {
            Bitmap result = new Bitmap(b);
            for (int y = 0; y < b.Height; y++)
            {
                for (int x = 0; x < b.Width; x++)
                {
                    Color originalPixel = b.GetPixel(x,y);
                    result.SetPixel(x, y, Color.FromArgb(alphaValue, originalPixel.R, originalPixel.G, originalPixel.B));
                }
            }
            return result;
        }
        public static Bitmap AlphaBlendRelative(this Bitmap b, byte alphaDecrementValue)
        {
            Bitmap result = new Bitmap(b);
            for (int y = 0; y < b.Height; y++)
            {
                for (int x = 0; x < b.Width; x++)
                {
                    Color originalPixel = b.GetPixel(x, y);
                    int newAlphaValue = originalPixel.A - alphaDecrementValue;
                    if (newAlphaValue < 0)
                        newAlphaValue = 0;
                    if (newAlphaValue > 255)
                        newAlphaValue = 255;
                    result.SetPixel(x, y, Color.FromArgb(newAlphaValue, originalPixel.R, originalPixel.G, originalPixel.B));
                }
            }
            return result;
        }

        /// <summary>
        /// Rotates the input image by theta degrees around center.
        /// BUG: might leave a red border. This could be caused by the fact that the new size is divided by 2 or something and then it rounds up? just a guess.
        /// </summary>
        public static Bitmap Rotate(this Bitmap bmpSrc, float theta)
        {
            Matrix mRotate = new Matrix();
            mRotate.Translate(bmpSrc.Width / -2, bmpSrc.Height / -2, MatrixOrder.Append);
            mRotate.RotateAt(theta, new Point(0, 0), MatrixOrder.Append);
            using (GraphicsPath gp = new GraphicsPath())
            {  // transform image points by rotation matrix
                gp.AddPolygon(new Point[] { new Point(0, 0), new Point(bmpSrc.Width, 0), new Point(0, bmpSrc.Height) });
                gp.Transform(mRotate);
                PointF[] pts = gp.PathPoints;

                // create destination bitmap sized to contain rotated source image
                Rectangle bbox = boundingBox(bmpSrc, mRotate);
                Bitmap bmpDest = new Bitmap(bbox.Width, bbox.Height);

                using (Graphics gDest = Graphics.FromImage(bmpDest))
                {  // draw source into dest
                    Matrix mDest = new Matrix();
                    mDest.Translate(bmpDest.Width / 2, bmpDest.Height / 2, MatrixOrder.Append);
                    gDest.Transform = mDest;
                    gDest.DrawImage(bmpSrc, pts);
                    //gDest.DrawRectangle(Pens.Red, bbox);
                    //drawAxes(gDest, Color.Red, 0, 0, 1, 100, "");
                    return bmpDest;
                }
            }
        }

        private static Rectangle boundingBox(Image img, Matrix matrix)
        {
            GraphicsUnit gu = new GraphicsUnit();
            Rectangle rImg = Rectangle.Round(img.GetBounds(ref gu));

            // Transform the four points of the image, to get the resized bounding box.
            Point topLeft = new Point(rImg.Left, rImg.Top);
            Point topRight = new Point(rImg.Right, rImg.Top);
            Point bottomRight = new Point(rImg.Right, rImg.Bottom);
            Point bottomLeft = new Point(rImg.Left, rImg.Bottom);
            Point[] points = new Point[] { topLeft, topRight, bottomRight, bottomLeft };
            GraphicsPath gp = new GraphicsPath(points, new byte[] { (byte)PathPointType.Start, (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line });
            gp.Transform(matrix);
            return Rectangle.Round(gp.GetBounds());
        }

        public static void Fill(this Bitmap bmp, Color color)
        {
            using (Graphics gfx = Graphics.FromImage(bmp))
            using (SolidBrush brush = new SolidBrush(color))
                gfx.FillRectangle(brush, 0, 0, bmp.Width, bmp.Height);
        }

        public static void Fill(this Image img, Color color)
        {
            using (Graphics gfx = Graphics.FromImage(img))
            using (SolidBrush brush = new SolidBrush(color))
                gfx.FillRectangle(brush, 0, 0, img.Width, img.Height);
        }

        public static void DrawOnto(this Bitmap original, Bitmap layer, Point position)
        {
            Graphics g = Graphics.FromImage(original);
            g.DrawImage(layer, position);
            g.Dispose();
        }

        public static Bitmap BitmapFromFile(string path)
        {
            Bitmap result = (Bitmap)Image.FromFile(path);
            result.SetResolution(DungeonMap.DPI, DungeonMap.DPI);
            return result;
        }
    }
}
