using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BallCatchGame
{
    class Player:UserControl
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            Brush brush1 = new SolidBrush(Color.Black);
            Brush brush2 = new SolidBrush(Color.LightPink);
            Rectangle head = new Rectangle(Width / 3, 0, Width / 3, Height / 4);
            Rectangle body = new Rectangle(Width / 6, Height / 4, Width * 2 / 3, Height / 2);
            Rectangle leftHand = new Rectangle(0, Height / 4, Width / 6, Height / 3);
            Rectangle rightHand = new Rectangle(Width * 5 / 6, Height / 4, Width / 6, Height / 3);
            Rectangle leftLeg = new Rectangle(Width * 3 / 12, Height * 3 / 4, Width * 2 / 12, Height / 4);
            Rectangle rightLeg = new Rectangle(Width * 7 / 12, Height * 3 / 4, Width * 2 / 12, Height / 4);

            e.Graphics.FillRectangle(brush1, body);
            e.Graphics.FillRectangles(brush2, new Rectangle[] { head, leftHand, rightHand, leftLeg, rightLeg });
            brush1.Dispose();
            brush2.Dispose();
        }
    }
}
