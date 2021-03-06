using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Traveling_Sales_Person
{
    public partial class Form1 : Form
    {
        //Ignore Global I suck at threading 
        Bitmap bmp = new Bitmap(500, 500);
        int[,] pos = new int[100, 2];            //Cords are stored in a 2d array (X cord on left, Y cord on right)
        int[,] shortestpos = new int[100, 2];    //These cords will be used to stored the shortest path

        bool haspoints = false;
        
        double totsmall = 0;
        double totlength = 0;
        
        int cities;
        int iloop = 0;

        public Form1()
        {
            InitializeComponent();
        }

        public void generatePointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cities = Decimal.ToInt32(num.Value);
            Drawdot();
            haspoints = true;
            richlength.Clear();
        }

        public void Drawdot()
        {
            totsmall = 0;
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.FromArgb(40, 40, 40));
            richTextBox1.Clear();

            int x1;
            int y1;
            int x2;
            int y2;

            Random rnd = new Random();
            Pen redPen = new Pen(Color.Red, 5);

            for (int i = 0; i < cities; i++)
            {
                x1 = rnd.Next(1, 500);
                pos[i, 0] = x1;

                y1 = rnd.Next(1, 500);
                pos[i, 1] = y1;

                x2 = x1 + 5;
                y2 = y1;

                //Draw line to screen.
                using (var graphics = Graphics.FromImage(bmp))
                {
                    graphics.DrawLine(redPen, x1, y1, x2, y2);
                }
            }

            image.Image = bmp;

            for (int i = 0; i < cities; i++)
            {
                richTextBox1.AppendText("X pos : " + pos[i, 0].ToString() + "  \t");
                richTextBox1.AppendText(" Y pos : " + pos[i, 1].ToString() + "\n");
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

            for (int i = 0; i < cities; i++)
            {
                x1 = pos[i, 0];
                y1 = pos[i, 1];
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

        public bool alreadyused()
        {
            for (int x = 0; x < cities; x++)
            {
                if (pos[iloop, 0] == shortestpos[x, 0] && pos[iloop, 1] == shortestpos[x, 1])
                {
                    return true;
                }
            }
            return false;
        }

        //Cords are stored as such 
        //pos[0,0] X
        //pos[0,1] Y

        public void Drawfirst()
        {
            Graphics g = Graphics.FromImage(bmp);

            int x1;
            int y1;
            int x2;
            int y2;

            Random rnd = new Random();
            Pen blackPen = new Pen(Color.White, 2);

            double hypotenus;

            int xaxis = 0;
            int yaxis = 0;

            for (int i = 0; i < cities; i++)
            {
                if (i < cities - 1)
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
                    x2 = pos[cities - 1, 0];
                    y2 = pos[cities - 1, 1];
                }

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

                totsmall += hypotenus;

                // Draw line to screen.
                using (var graphics = Graphics.FromImage(bmp))
                {
                    graphics.DrawLine(blackPen, x1, y1, x2, y2);
                }

                Thread2();
                Thread.Sleep(20);
            }
            shortestpos = pos;
            Main();
        }

        // Prints the array
        public void workoutpoints(int[,] a, int n)
        {
            int x1 = 0;
            int y1 = 0;

            int x2 = 0;
            int y2 = 0;

            int xaxis = 0;
            int yaxis = 0;

            double hypotenus;

            totlength = 0;
            //change to 10 but watch for array out of range error
            for (int i = 0; i < cities; i++)
            {
                if (i < cities - 1)
                {
                    x1 = a[i, 0];
                    y1 = a[i, 1];
                    x2 = a[i + 1, 0];
                    y2 = a[i + 1, 1];

                }
                else
                {
                    x1 = a[cities - 1, 0];
                    y1 = a[cities - 1, 1];
                    x2 = a[0, 0];
                    y2 = a[0, 1];
                }

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
                totlength += hypotenus;
            }
            if (totlength < totsmall)
            {
                totsmall = totlength;
                shortestpos = a;

                if (richlength.InvokeRequired)
                {
                    richlength.Invoke(new MethodInvoker(delegate { richlength.AppendText (totlength.ToString() +"\n"); }));
                    richlength.Invoke(new MethodInvoker(delegate { richlength.ScrollToCaret(); }));
                }
                drawfinal();
                Thread.Sleep(20);
            }

        }

        // Generating permutation using Heap Algorithm
        public void heapPermutation(int[,] a, int size, int n)
        {
            // if size becomes 1 then prints the obtained
            // permutation
            if (size == 1)
                workoutpoints(a, n);

            for (int i = 0; i < size; i++)
            {
                heapPermutation(a, size - 1, n);

                // if size is odd, swap 0th i.e (first) and
                // (size-1)th i.e (last) element

                if (size % 2 == 1)
                {
                    int temp = a[0, 0];
                    a[0, 0] = a[size - 1, 0];
                    a[size - 1, 0] = temp;

                    temp = a[0, 1];
                    a[0, 1] = a[size - 1, 1];
                    a[size - 1, 1] = temp;
                }

                // If size is even, swap ith and
                // (size-1)th i.e (last) element
                else
                {
                    int temp = a[i, 0];
                    a[i, 0] = a[size - 1, 0];
                    a[size - 1, 0] = temp;

                    temp = a[i, 1];
                    a[i, 1] = a[size - 1, 1];
                    a[size - 1, 1] = temp;
                }
            }
        }

        // Driver code
        public void Main()
        {
            int[,] a = pos;

            heapPermutation(a, cities, a.Length);
        }

        public void drawfinal()
        {

            Redrawdrawdot();

            Graphics g = Graphics.FromImage(bmp);

            int x1;
            int y1;
            int x2;
            int y2;

            Random rnd = new Random();
            Pen blackPen = new Pen(Color.White, 2);

            for (int i = 0; i < cities; i++)
            {
                if (i < cities - 1)
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
                    x2 = shortestpos[cities - 1, 0];
                    y2 = shortestpos[cities - 1, 1];
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

        public void Thread1()
        {
            Drawfirst();
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
                tid1.IsBackground = true;
                Thread tid2 = new Thread(new ThreadStart(Thread2));
                tid2.IsBackground = true;
                tid1.Start();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}