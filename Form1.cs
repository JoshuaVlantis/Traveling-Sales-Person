using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Traveling_Sales_Person
{
    public partial class Form1 : Form
    {
        Bitmap bmp = new Bitmap(500, 500);
        int[] pos = new int[20];            //Cords are stored in pairs of 2 eg pos 0 and 1 are x and y cords of a point
        int[] shortestpos = new int[20];    //These cords will be used to stored the shortest pathf
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

            for (int i = 0; i < 10 * 2; i++)
            {
                x1 = rnd.Next(1, 500);
                pos[i] = x1;
                i++;
                y1 = rnd.Next(1, 500);
                pos[i] = y1;
                x2 = x1 + 5;
                y2 = y1;

                // Draw line to screen.
                using (var graphics = Graphics.FromImage(bmp))
                {
                    graphics.DrawLine(redPen, x1, y1, x2, y2);
                }
            }
            image.Image = bmp;
            for (int i = 0; i < 10 * 2; i++)
            {
                richTextBox1.AppendText("X pos : " + pos[i].ToString() + "  \t");
                i++;
                richTextBox1.AppendText(" Y pos : " + pos[i].ToString() + "\n");
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

            for (int i = 0; i < 10 * 2; i++)
            {
                x1 = pos[i];
                i++;
                y1 = pos[i];
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

            Redrawdrawdot();

            int x1;
            int y1;
            int x2;
            int y2;
            int ii = 0;
            

            Random rnd = new Random();
            Pen blackPen = new Pen(Color.White, 2);

            for (int i = 0; i < 10 * 2 - 2; i++)
            {
                x1 = shortestpos[i];
                i++;
                y1 = shortestpos[i];
                ii++;
                x2 = shortestpos[ii];
                ii++;
                y2 = shortestpos[ii];

                // Draw line to screen.
                using (var graphics = Graphics.FromImage(bmp))
                {
                    graphics.DrawLine(blackPen, x1, y1, x2, y2);
                }
            }
            image.Image = bmp;
        }

        public void Thread1()
        {
            //Method swap two points 
            //If new swap is better then last save new pos

            Graphics g = Graphics.FromImage(bmp);

            int x1;
            int y1;
            int x2;
            int y2;
            
            int x;
            int y;
            
            Double hypotenuse = 0;
            Double smallesthypotenuse = 0;


            Pen blackPen = new Pen(Color.White, 2);
            for (int xx = 0; xx < 1; xx++)
            {
                Redrawdrawdot();
                x1 = pos[xx];
                xx++;
                y1 = pos[xx];

                for (int i = 0; i < 20; i++)
                {
                    x2 = pos[i];
                    i++;
                    y2 = pos[i];
                    if (x1 != x2 && y1 != y2)
                    {
                        using (var graphics = Graphics.FromImage(bmp))
                        {
                            graphics.DrawLine(blackPen, x1, y1, x2, y2);
                        }

                        if (x1 > x2)
                        {
                            x = x1 - x2;
                        }
                        else
                        {
                            x = x2 - x1;
                        }
                        // Y Gets distance between points
                        if (y1 > y2)
                        {
                            y = y1 - y2;
                        }
                        else
                        {
                            y = y2 - y1;
                        }
                        //Pythagoras theorem to get length of line
                        hypotenuse = (x * x) + (y * y);
                        hypotenuse = (Math.Sqrt(hypotenuse));

                        if (hypotenuse <= smallesthypotenuse || i == 3)
                        {
                            smallesthypotenuse = hypotenuse;
                            
                            shortestpos[xx] = x2;

                            shortestpos[xx + 1] = y2;
                        }
                        Thread2();
                        Thread.Sleep(20);
                    }
                    drawfinal();
                    Thread.Sleep(20);
                }
            }
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