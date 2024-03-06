using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BallCatchGame
{
    class ProgressBar: UserControl
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.FillEllipse(new SolidBrush(Color.AliceBlue), new Rectangle(0, 0, Width, Height));
            e.Graphics.FillPie(new SolidBrush(Color.Blue), new Rectangle(0, 0, Width, Height), 270, 359);
            e.Graphics.FillEllipse(new SolidBrush(Color.White), new Rectangle(Width / 10, Height / 10, Width * 8 / 10, Height * 8 / 10));

        }
    }
}
