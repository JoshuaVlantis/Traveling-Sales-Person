using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Traveling_Sales_Person
{
    public partial class Form1 : Form
    {
        Bitmap bmp = new Bitmap(500, 500);
        int[] pos = new int[20];

        public Form1()
        {
            InitializeComponent();
        }

        public void generatePointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread tid1 = new Thread(new ThreadStart(Thread1));
            Thread tid2 = new Thread(new ThreadStart(Thread2));

            //tid1.Start();
            Drawdot();
        }

        public void Drawdot()
        {
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.FromArgb(40,40,40));
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
                //Thread2();
                //Thread.Sleep(10);
            }
            image.Image = bmp;
            for (int i = 1; i < 10; i++)
            {
                richTextBox1.AppendText("X pos : " + pos[i].ToString() + "  \t" + " Y pos : "+ pos[i + i].ToString()  + "\n");
            }
        }
        public void Thread1()
        {

        }
        public void Thread2()
        {

        }
        
        //Draws line onto BMP
        public void DrawLineInt(Bitmap bmp, int x1,int y1)
        {
            Random rnd = new Random();

            Pen blackPen = new Pen(Color.White, 2);

            int ix1 = rnd.Next(1, 500);
            int iy1 = rnd.Next(1, 500);

            int ix2 = rnd.Next(1, 500);
            int iy2 = rnd.Next(1, 500);
            // Draw line to screen.
            using (var graphics = Graphics.FromImage(bmp))
            {
                graphics.DrawLine(blackPen, x1, y1, ix2, iy2);
            }
            image.Image = bmp;
        }
    }
}
