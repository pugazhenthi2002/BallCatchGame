using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BallCatchGame
{
    class Pipe:UserControl
    {
        public int BallRadius;
        protected override void OnPaint(PaintEventArgs e)
        {
            Brush brush1 = new SolidBrush(Color.Brown);
            Brush brush2 = new SolidBrush(Color.Black);
            Rectangle rec1 = new Rectangle(Width / 20, Height / 5, Width * 19 / 20, Height * 3 / 5);
            Rectangle rec2 = new Rectangle(0, Height / 5, Width / 10, Height * 3 / 5);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.FillRectangle(brush1, rec1);
            e.Graphics.FillEllipse(brush2, rec2);
            BallRadius = Height * 3 / 5;
            brush1.Dispose();
            brush2.Dispose();
        }
    }
}
