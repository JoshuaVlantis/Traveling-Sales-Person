using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Traveling_Sales_Person
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Bitmap bmp = new Bitmap(500, 500);
            DrawDot(bmp);
        }

        public void DrawDot(Bitmap bmp)
        {

            Random rnd = new Random();

            Pen redPen = new Pen(Color.Red, 5);
            // for (int i = 1; i < 10; i++)
            //{
            int x1 = rnd.Next(1, 500);
            int y1 = rnd.Next(1, 500);
            int x2 = x1 + 5;
            int y2 = y1;
            // Draw line to screen.
            using (var graphics = Graphics.FromImage(bmp))
            {
                graphics.DrawLine(redPen, x1, y1, x2, y2);
            }
            image.Image = bmp;
            // }
            DrawLineInt(bmp, x1, y1);
        }

        //Draws line onto BMP
        public void DrawLineInt(Bitmap bmp, int x1,int y1)
        {
            Random rnd = new Random();

            Pen blackPen = new Pen(Color.Black, 2);

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
