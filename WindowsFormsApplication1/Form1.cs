/*



*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int x1, x2, y1, y2, x_incr, y_incr; 
        int count = 0, dx, dy, x, y, r, i, const1, const2, p;
        int passos, k;
        double xincr, yincr, x3, y3;
        int boxX, boxY;
        bool draw = false;
        int pX = 0;
        int pY = 0;

        Bitmap drawing;

        public Form1()
        {
            InitializeComponent();
            drawing = new Bitmap(panel1.Width, panel1.Height, panel1.CreateGraphics());
            Graphics.FromImage(drawing).Clear(Color.White);
        }



        //PANEL AND CLICK FUNCTIONS
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(drawing, new Point(0, 0));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            //if (draw)
            //{
            //    Graphics panel = Graphics.FromImage(drawing);
            //    Pen pen = new Pen(Color.BlueViolet, 12);
            //    pen.EndCap = LineCap.Round;
            //    pen.StartCap = LineCap.Round;
            //    panel.DrawLine(pen, pX, pY, e.X, e.Y);
            //    panel1.CreateGraphics().DrawImageUnscaled(drawing, new Point(0, 0));
            //}
            pX = e.X;
            pY = e.Y;
            label3.Text = pX.ToString();
            label4.Text = pY.ToString();
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            draw = true;
            pX = e.X;
            pY = e.Y;
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            draw = false;
        }
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if ((count == 0))
            {
                count = 1;
                x1 = e.X;
                y1 = e.Y;
            }
            else
                if (count == 1)
                {
                    x2 = e.X;
                    y2 = e.Y;
                }
        }
        
        //DDA BUTTON
        private void button1_Click(object sender, EventArgs e)
        {
            if (count == 1)
            {
                count = 0;
                Brush aBrush = (Brush)Brushes.Red;
                Graphics g = panel1.CreateGraphics();
                dx = x2 - x1;
                dy = y2 - y1;
                if (Math.Abs(dx) > Math.Abs(dy))
                {
                    passos = Math.Abs(dx);
                }
                else
                {
                    if (Math.Abs(dy) > Math.Abs(dx))
                        passos = Math.Abs(dy);
                }
                xincr = Convert.ToDouble(dx) / Convert.ToDouble(passos);
                yincr = Convert.ToDouble(dy) / Convert.ToDouble(passos);
                x3 = Convert.ToDouble(x1);
                y3 = Convert.ToDouble(y1);
                g.FillRectangle(aBrush, Convert.ToInt32(Math.Round(x3)), Convert.ToInt32(Math.Round(y3)), 1, 1);

                for (int i = 1; i != passos; i++)
                {
                    x3 = x3 + xincr;
                    y3 = y3 + yincr;
                    g.FillRectangle(aBrush, Convert.ToInt32(Math.Round(x3)), Convert.ToInt32(Math.Round(y3)), 1, 1);

                }
            }
        }
        //BRESENHAM BUTTON
        private void button2_Click(object sender, EventArgs e)
        {
            if (count == 1)
            {
                count = 0;
                Brush aBrush = (Brush)Brushes.Black;
                Graphics g = panel1.CreateGraphics();
                dx = x2 - x1;
                dy = y2 - y1;
                if (dx >= 0)
                    x_incr = 1;
                else
                {
                    x_incr = -1;
                    dx = -dx;
                }
                if (dy >= 0)
                    y_incr = 1;
                else
                {
                    y_incr = -1;
                    dy = -dy;
                }
                y = y1;
                x = x1;
                g.FillRectangle(aBrush, x, y, 1, 1);
                if (dy < dx)
                {
                    p = 2 * dy - dx;
                    const1 = 2 * dy;
                    const2 = 2 * (dy - dx);
                    for (i = 0; i < dx; i++)
                    {
                        x += x_incr;
                        if (p < 0)
                            p += const1;
                        else
                        {
                            y += y_incr;
                            p += const2;
                        }
                        g.FillRectangle(aBrush, x, y, 1, 1);
                    }
                }
                else
                {
                    p = 2 * dx - dy;
                    const1 = 2 * dx;
                    const2 = 2 * (dx - dy);
                    for (i = 0; i < dy; i++)
                    {
                        y += y_incr;
                        if (p < 0)
                            p += const1;
                        else
                        {
                            x += x_incr;
                            p += const2;
                        }
                        g.FillRectangle(aBrush, x, y, 1, 1);
                    }
                }
            }

        }
        //CLEAR BUTTON
        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Invalidate();
        }
        //BRESENHAM CIRC BUTTON
        private void button4_Click(object sender, EventArgs e)
        {
            if (count == 1)
            {
                count = 0;
                Brush aBrush = (Brush)Brushes.CadetBlue;
                Graphics g = panel1.CreateGraphics();
                r = Convert.ToInt32(Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((x2 - x1), 2)));
                x = 0;
                y = r;
                p = 1 - r;
                g.FillRectangle(aBrush, (x1 + x), (y1 + y), 1, 1);
                g.FillRectangle(aBrush, (x1 - x), (y1 + y), 1, 1);
                g.FillRectangle(aBrush, (x1 + x), (y1 - y), 1, 1);
                g.FillRectangle(aBrush, (x1 - x), (y1 - y), 1, 1);
                g.FillRectangle(aBrush, (x1 + y), (y1 + x), 1, 1);
                g.FillRectangle(aBrush, (x1 - y), (y1 + x), 1, 1);
                g.FillRectangle(aBrush, (x1 + y), (y1 - x), 1, 1);
                g.FillRectangle(aBrush, (x1 - y), (y1 - x), 1, 1);
                while (x < y)
                {
                    x++;
                    if (p < 0)
                        p += 2 * x + 1;
                    else
                    {
                        y--;
                        p += 2 * (x - y) + 1;
                    }
                    g.FillRectangle(aBrush, (x1 + x), (y1 + y), 1, 1);
                    g.FillRectangle(aBrush, (x1 - x), (y1 + y), 1, 1);
                    g.FillRectangle(aBrush, (x1 + x), (y1 - y), 1, 1);
                    g.FillRectangle(aBrush, (x1 - x), (y1 - y), 1, 1);
                    g.FillRectangle(aBrush, (x1 + y), (y1 + x), 1, 1);
                    g.FillRectangle(aBrush, (x1 - y), (y1 + x), 1, 1);
                    g.FillRectangle(aBrush, (x1 + y), (y1 - x), 1, 1);
                    g.FillRectangle(aBrush, (x1 - y), (y1 - x), 1, 1);

                }
            }
        }
        //TRANSLATION BUTTON - USING DDA ALGORITHM
        private void button5_Click(object sender, EventArgs e)
        {
            if (count == 0)
            {
                boxX = int.Parse(boxX1.Text);
                boxY = int.Parse(boxY1.Text);
                x1 = x1 + boxX;
                y1 = y1 + boxY;
                x2 = x2 + boxX;
                y2 = y2 + boxY;

                // Applying DDA to the new points
                Brush aBrush = (Brush)Brushes.LimeGreen;
                Graphics g = panel1.CreateGraphics();
                dx = x2 - x1;
                dy = y2 - y1;
                if (Math.Abs(dx) > Math.Abs(dy))
                {
                    passos = Math.Abs(dx);
                }
                else
                {
                    if (Math.Abs(dy) > Math.Abs(dx))
                        passos = Math.Abs(dy);
                }
                xincr = Convert.ToDouble(dx) / Convert.ToDouble(passos);
                yincr = Convert.ToDouble(dy) / Convert.ToDouble(passos);
                x3 = Convert.ToDouble(x1);
                y3 = Convert.ToDouble(y1);
                g.FillRectangle(aBrush, Convert.ToInt32(Math.Round(x3)), Convert.ToInt32(Math.Round(y3)), 1, 1);

                for (int i = 1; i != passos; i++)
                {
                    x3 = x3 + xincr;
                    y3 = y3 + yincr;
                    g.FillRectangle(aBrush, Convert.ToInt32(Math.Round(x3)), Convert.ToInt32(Math.Round(y3)), 1, 1);

                }


            }

        }
        //SCALE BUTTON - USING DDA ALGORITHM
        private void button6_Click(object sender, EventArgs e)
        {
            if (count == 0)
            {
                boxX = int.Parse(boxX2.Text);
                boxY = int.Parse(boxY2.Text);
                x1 = boxX * x1;
                y1 = boxY * y1;
                x2 = boxX * x2;
                y2 = boxY * y2;
            }
            // Applying DDA to the new points
            Brush aBrush = (Brush)Brushes.OrangeRed;
            Graphics g = panel1.CreateGraphics();
            dx = x2 - x1;
            dy = y2 - y1;
            if (Math.Abs(dx) > Math.Abs(dy))
            {
                passos = Math.Abs(dx);
            }
            else
            {
                if (Math.Abs(dy) > Math.Abs(dx))
                    passos = Math.Abs(dy);
            }
            xincr = Convert.ToDouble(dx) / Convert.ToDouble(passos);
            yincr = Convert.ToDouble(dy) / Convert.ToDouble(passos);
            x3 = Convert.ToDouble(x1);
            y3 = Convert.ToDouble(y1);
            g.FillRectangle(aBrush, Convert.ToInt32(Math.Round(x3)), Convert.ToInt32(Math.Round(y3)), 1, 1);

            for (int i = 1; i != passos; i++)
            {
                x3 = x3 + xincr;
                y3 = y3 + yincr;
                g.FillRectangle(aBrush, Convert.ToInt32(Math.Round(x3)), Convert.ToInt32(Math.Round(y3)), 1, 1);

            }
        }

        /*#################### UNUSED #########################*/
        private void panel1_MouseHover(object sender, EventArgs e)
        {
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void label3_MouseHover(object sender, EventArgs e)
        {

        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void boxX1_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
