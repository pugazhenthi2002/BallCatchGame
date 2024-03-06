using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BallCatchGame
{
    public enum BallColor
    {
        Black,
        Red,
        Blue
    }
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, panel1, new object[] { true });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BallCollection = new List<Ball>();
            pipe1.Location = new Point(Width - pipe1.Width, 0);
            movementY = 5;

            pipeTimer = new Timer();
            pipeTimer.Interval = 1;
            pipeTimer.Tick += OnPipeMovementTick;

            ballGeneratorTimer = new Timer();
            ballGeneratorTimer.Interval = 1000;
            ballGeneratorTimer.Tick += OnBallGeneratorTick;

            ballMovementTimer = new Timer();
            ballMovementTimer.Interval = 100;
            ballMovementTimer.Tick += OnBallMovementTick;

            KeyPreview = true;
            pipeTimer.Start();
            ballGeneratorTimer.Start();
            ballMovementTimer.Start();
        }

        private void OnBallMovementTick(object sender, EventArgs e)
        {
            for(int ctr=0; ctr < BallCollection.Count; ctr++)
            {
                BallCollection[ctr].BallRectangle = new Rectangle(new Point(BallCollection[ctr].BallRectangle.Location.X - 3, BallCollection[ctr].BallRectangle.Location.Y), BallCollection[ctr].BallRectangle.Size);
            }

            for (int ctr = 0; ctr < BallCollection.Count; ctr++)
            {

                currScore = CheckBallCollide(BallCollection[ctr]);
                if (currScore != 0)
                    ctr--;

                score += currScore;
                richTextBox1.Text = score.ToString();
            }
            
            panel1.Invalidate();
        }

        private void OnBallGeneratorTick(object sender, EventArgs e)
        {
            BallCollection.Add(new Ball()
            {
                BallColor = Color.FromName(((BallColor)rnd.Next(0, 3)).ToString()),
                BallRectangle = new Rectangle(pipe1.Location.X - pipe1.BallRadius - 6, pipe1.Location.Y + pipe1.Height / 5, pipe1.BallRadius, pipe1.BallRadius)
            });
        }

        private void OnPipeMovementTick(object sender, EventArgs e)
        {
            pipe1.Location = new Point(pipe1.Location.X, pipe1.Location.Y + movementY);

            if(pipe1.Location.Y+pipe1.Height+movementY>panel1.Height)
            {
                movementY = -5;
            }
            if(pipe1.Location.Y+movementY<0)
            {
                movementY = 5;
            }
        }

        private void OnPlayerKeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.W:    SetPlayerLocation(0, -5);    break;
                case Keys.A:    SetPlayerLocation(-5, 0);    break;
                case Keys.S:    SetPlayerLocation(0, 5);    break;
                case Keys.D:    SetPlayerLocation(5, 0);    break;
            }
        }

        private void SetPlayerLocation(int x, int y)
        {
            if(player1.Location.X + x <= 0 || player1.Location.X + player1.Width + x >= pipe1.Location.X)
            {
                return;
            }
            if(player1.Location.Y + y <= 0 || player1.Location.Y + player1.Height + y >= panel1.Height)
            {
                return;
            }

            player1.Location = new Point(player1.Location.X + x, player1.Location.Y + y);
        }
        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Brush brush;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            foreach(var Iter in BallCollection)
            {
                brush = new SolidBrush(Iter.BallColor);
                e.Graphics.FillEllipse(brush, Iter.BallRectangle);
                brush.Dispose();
            }
        }

        private int CheckBallCollide(Ball ball)
        {
                if((player1.Left < ball.BallRectangle.Location.X && ball.BallRectangle.Location.X < player1.Right) && (player1.Top < ball.BallRectangle.Location.Y && ball.BallRectangle.Location.Y < player1.Bottom))
                {
                    BallCollection.Remove(ball);
                    return GetBallColorPoints(ball.BallColor);
                }
                if((player1.Left < ball.BallRectangle.Location.X && ball.BallRectangle.Location.X < player1.Right) && (player1.Top < ball.BallRectangle.Location.Y + ball.BallRectangle.Height && ball.BallRectangle.Location.Y + ball.BallRectangle.Height < player1.Bottom))
                {
                    BallCollection.Remove(ball);
                    return GetBallColorPoints(ball.BallColor);
                }
                if((player1.Left < ball.BallRectangle.Location.X + ball.BallRectangle.Width && ball.BallRectangle.Location.X + ball.BallRectangle.Width < player1.Right) && (player1.Top < ball.BallRectangle.Location.Y && ball.BallRectangle.Location.Y < player1.Bottom))
                {
                    BallCollection.Remove(ball);
                    return GetBallColorPoints(ball.BallColor);
                }
                if ((player1.Left < ball.BallRectangle.Location.X + ball.BallRectangle.Width && ball.BallRectangle.Location.X + ball.BallRectangle.Width < player1.Right) && (player1.Top < ball.BallRectangle.Location.Y + ball.BallRectangle.Height && ball.BallRectangle.Location.Y + ball.BallRectangle.Height < player1.Bottom))
                {
                    BallCollection.Remove(ball);
                    return GetBallColorPoints(ball.BallColor);
                }
                return 0;
        }

        private int GetBallColorPoints(Color c)
        {
            if (c == Color.Blue)
                return 5;
            if (c == Color.Red)
                return 10;

            MessageBox.Show("Game Over");
            return -1;
        }

        private Random rnd = new Random();
        private int movementY, currScore;
        private int score;
        private Timer pipeTimer;
        private Timer ballGeneratorTimer;
        private Timer ballMovementTimer;
        private List<Ball> BallCollection;

    }

}
