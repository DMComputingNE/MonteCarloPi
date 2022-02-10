using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace montecarlo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
         
            panel1.Paint += new PaintEventHandler(panel1_Paint);
            chart1.ChartAreas[0].AxisY.ScaleView.MinSize = 1;
            chart1.ChartAreas[0].CursorY.Interval = 0.00001;
        }

       

        private void panel1_Paint(object sender, PaintEventArgs e)

        {
            Random rand = new Random();
            Pen pen = new Pen(Color.Black, (float)2);
           
            Graphics g = e.Graphics;
            int count = 0;
            int square = 0;
            int circle = 0;
            double pi;
                g.DrawRectangle(pen, 0, 0, 400, 400);
                g.DrawEllipse(pen, 0,0, 400, 400);
            chart1.Series.Add("Pi");
            chart1.Series[ "Pi"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
           
            System.Windows.Forms.DataVisualization.Charting.HorizontalLineAnnotation PiY = new System.Windows.Forms.DataVisualization.Charting.HorizontalLineAnnotation();
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].CursorY.AutoScroll = true;
            PiY.AxisY = chart1.ChartAreas[0].AxisY;
            PiY.AxisX = chart1.ChartAreas[0].AxisX;
            PiY.IsSizeAlwaysRelative = false;
            PiY.AnchorY = 3.141592;
            PiY.IsInfinitive = true;
            PiY.ClipToChartArea = chart1.ChartAreas[0].Name;
            PiY.LineColor = Color.Red; 
            PiY.LineWidth = 1;
            chart1.Annotations.Add(PiY);
            List<double> xAxis = new List<double>();
            List<double> yAxis = new List<double>();
            yAxis.Add(3.1415);
            for (int i = 0; i < 50000; i++)
            {
                count++;
                xAxis.Add(count);
                int x = rand.Next(0, 400);
                int y = rand.Next(0, 400);
                if ((x - 200) * (x - 200) +  (y - 200) * (y - 200) <= 200 * 200)
                {
                    pen.Color = Color.Red;
                    circle++;

                }
                else if (x>=0&& x <=400 &&y>=0 && y<=400)
                {
                    pen.Color = Color.Blue;
                    square++;
                }
                else
                {
                    pen.Color = Color.Black;
                }
                lblSquareCount.Text = Convert.ToString(square);
                lblCircleCount.Text = Convert.ToString(circle);
                pi = (double)4 * circle / (square + circle);
                lblPiApprox.Text = Convert.ToString(pi);
                yAxis.Add(count);
                lblSquareCount.Update();
                lblCircleCount.Update();
                lblPiApprox.Update();
                g.DrawEllipse(pen,x,y,1,1);
                chart1.Series["Pi"].Points.AddXY(count, pi);
                if (count % 50==0)
                {
                chart1.Update();
                }
            }
            chart1.Update();
        }
       
    }
}
