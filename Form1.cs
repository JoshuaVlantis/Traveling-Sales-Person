using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Traveling_Sales_Person
{
    public partial class Form1 : Form
    {
        Bitmap bmp = new Bitmap(500, 500);
        int[,] pos = new int[10,2];            //Cords are stored in a 2d array (X cord on left, Y cord on right)
        int[,] shortestpos = new int[10,2];    //These cords will be used to stored the shortest path
        bool haspoints = false;

        public Form1()
        {
            InitializeComponent();
        }

        public void generatePointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Drawdot();
            haspoints = true;
        }

        public void Drawdot()
        {
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.FromArgb(40, 40, 40));
            richTextBox1.Clear();

            int x1;
            int y1;
            int x2;
            int y2;

            Random rnd = new Random();
            Pen redPen = new Pen(Color.Red, 5);

            for (int i = 0; i < 10; i++)
            {
                x1 = rnd.Next(1, 500);
                pos[i,0] = x1;
                
                y1 = rnd.Next(1, 500);
                pos[i,1] = y1;

                x2 = x1 + 5;
                y2 = y1;

                //Draw line to screen.
                using (var graphics = Graphics.FromImage(bmp))
                {
                    graphics.DrawLine(redPen, x1, y1, x2, y2);
                }
            }

            image.Image = bmp;
            
            for (int i = 0; i < 10; i++)
            {
                richTextBox1.AppendText("X pos : " + pos[i,0].ToString() + "  \t");
                richTextBox1.AppendText(" Y pos : " + pos[i,1].ToString() + "\n");
            }
        }

        public void Redrawdrawdot()
        {
            Graphics g = Graphics.FromImage(bmp);

            g.Clear(Color.FromArgb(40, 40, 40));

            int x1;
            int y1;
            int x2;
            int y2; 

            Random rnd = new Random();
            Pen redPen = new Pen(Color.Red, 5);

            for (int i = 0; i < 10; i++)
            {
                x1 = pos[i,0]; 
                y1 = pos[i,1];
                x2 = x1 + 5;
                y2 = y1;

                // Draw line to screen.
                using (var graphics = Graphics.FromImage(bmp))
                {
                    graphics.DrawLine(redPen, x1, y1, x2, y2);
                }
            }
            image.Image = bmp;
        }
        
        //Draws lines on BMP
        public void drawfinal()
        {
            Graphics g = Graphics.FromImage(bmp);

            int x1;
            int y1;
            int x2;
            int y2;

            Random rnd = new Random();
            Pen blackPen = new Pen(Color.White, 2);

            for (int i = 0; i < 10; i++)
            {
                if (i != 9)
                {
                    x1 = shortestpos[i, 0];
                    y1 = shortestpos[i, 1];
                    x2 = shortestpos[i + 1, 0];
                    y2 = shortestpos[i + 1, 1];
                }
                else
                {
                    x1 = shortestpos[0, 0];
                    y1 = shortestpos[0, 1];
                    x2 = shortestpos[9, 0];
                    y2 = shortestpos[9, 1];
                }

                // Draw line to screen.
                using (var graphics = Graphics.FromImage(bmp))
                {
                    graphics.DrawLine(blackPen, x1, y1, x2, y2);
                }
                Thread2();

                Thread.Sleep(10);
            }
        }
        public void Drawfirst()
        {
            Graphics g = Graphics.FromImage(bmp);

            int x1;
            int y1;
            int x2;
            int y2;

            Random rnd = new Random();
            Pen blackPen = new Pen(Color.White, 2);

            for (int i = 0; i < 10; i++)
            {
                if(i != 9)
                {
                    x1 = pos[i, 0];
                    y1 = pos[i, 1];
                    x2 = pos[i + 1, 0];
                    y2 = pos[i + 1, 1];
                }
                else
                {
                    x1 = pos[0, 0];
                    y1 = pos[0, 1];
                    x2 = pos[9, 0];
                    y2 = pos[9, 1];
                }

                // Draw line to screen.
                using (var graphics = Graphics.FromImage(bmp))
                {
                    graphics.DrawLine(blackPen, x1, y1, x2, y2);
                }
                Thread2();
             
                Thread.Sleep(10);
            }
        }

        public bool alreadyused(int i)
        {
            for (int x = 0; x < 10; x++)
            {
                if (pos[i,0] == shortestpos[x,0] && pos[i, 1] == shortestpos[x, 1])
                {
                    return true;
                }
            }
            return false;
        }

        public void workoutpoints()
        {
            int i = 0;
            int ii;

            int xaxis;
            int yaxis;

            int x1;
            int y1;
            int x2;
            int y2;

            Double hypotenuse = 0;
            Double smallesthypotenuse = 0;

            for (; i < 10; i++)
            {
                ii = 0;
                x1 = pos[i, 0];
                y1 = pos[i, 1];
                for (; ii < 10; ii++)
                {
                    x2 = pos[ii, 0];
                    y2 = pos[ii, 1];

                    if (x1 > x2)
                    {
                        xaxis = x1 - x2;
                    }
                    else
                    {
                        xaxis = x2 - x1;
                    }
                    // Y Gets distance between points
                    if (y1 > y2)
                    {
                        yaxis = y1 - y2;
                    }
                    else
                    {
                        yaxis = y2 - y1;
                    }

                    hypotenuse = (xaxis * xaxis) + (yaxis * yaxis);
                    hypotenuse = (Math.Sqrt(hypotenuse));

                    if (hypotenuse <= smallesthypotenuse || i == 0)
                    {
                        if (!alreadyused(i))
                        {
                            smallesthypotenuse = hypotenuse;

                            shortestpos[i, 0] = x2;
                            shortestpos[i, 1] = y2;
                        }
                    }
                }

            }
            drawfinal();
        }

        public void Thread1()
        {
            //Drawfirst();
            workoutpoints();
        }

        public void Thread2()
        {
            image.Image = bmp;
        }

        private void goToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (haspoints)
            {
                Thread tid1 = new Thread(new ThreadStart(Thread1));
                Thread tid2 = new Thread(new ThreadStart(Thread2));
                tid1.Start();
            }
        }
    }
}