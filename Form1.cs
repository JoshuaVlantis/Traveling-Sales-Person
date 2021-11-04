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
        int iloop;

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

        public bool alreadyused()
        {
            for (int x = 0; x < 10; x++)
            {
                if (pos[iloop,0] == shortestpos[x,0] && pos[iloop, 1] == shortestpos[x, 1])
                {
                    return true;
                }
            }
            return false;
        }
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
                if (x1 + y1 + x2 + y2 != 0)
                {
                    using (var graphics = Graphics.FromImage(bmp))
                    {
                        graphics.DrawLine(blackPen, x1, y1, x2, y2);
                    }
                }

                Thread2();

                Thread.Sleep(10);
            }
        }

        //Cords are stored as such 
        //pos[0,0] X
        //pos[0,1] Y

        public void workoutpoints()
        {

            int x1 = 0;
            int y1 = 0;

            int x2 = 0;
            int y2 = 0;

            int xaxis = 0;
            int yaxis = 0;

            double hypotenus;
            double smallesthypotenus;
            double totlength = 0;

            for (int i = 0; i < 9; i++)
            {
                x1 = pos[i, 0];
                y1 = pos[i, 0];

                x2 = pos[i + 1, 0];
                y2 = pos[i + 1, 0];


                //Get lenght of line
                if (x1 < x2)
                {
                    xaxis = x2 - x1;
                }
                else
                {
                    xaxis = x1 - x2;
                }

                if (y1 < y2)
                {
                    yaxis = y2 - y1;
                }
                else
                {
                    yaxis = y1 - x2;
                }

                hypotenus = (xaxis * xaxis) + (yaxis * yaxis);
                hypotenus = (Math.Sqrt(hypotenus));

                totlength = hypotenus + hypotenus;

                //drawfinal();
                Thread.Sleep(5);
                string test;                        
            }
        }

        public void Thread1()
        {
            Drawfirst();
            //workoutpoints();
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