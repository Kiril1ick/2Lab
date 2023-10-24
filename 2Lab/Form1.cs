using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2Lab
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            int isp;
            double intensiv;
            int.TryParse(textBox2.Text, out isp);
            double.TryParse(textBox1.Text, out intensiv);
            try
            {
                raschet(isp, intensiv);
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        
        void raschet(int isp, double intensiv)
        {
            Random random = new Random();
            List<double> list = new List<double>();
            for (int i = 0; i < isp; i++)
            {
                list.Add(-Math.Log(1 - random.NextDouble()) / intensiv);
            }
            double maxValue = list.Max();
            double interval = maxValue / 20;
            double minIntervalVal = 0;
            while (minIntervalVal < maxValue)
            {
                double hitRate = list.Count(timeFromList => timeFromList >= minIntervalVal && timeFromList < minIntervalVal + interval);
                double freq = hitRate / isp;
                double p = freq / interval;
                double counter = intensiv * Math.Exp(-intensiv * minIntervalVal);
                chart1.Series[0].Points.AddXY(minIntervalVal + interval / 2, p);
                chart1.Series[1].Points.AddXY(minIntervalVal, counter);
                minIntervalVal += interval;
            }
        }
    }
}
