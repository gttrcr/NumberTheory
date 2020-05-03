using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Collatz
{
    public partial class LogSequence : Form
    {
        public LogSequence()
        {
            InitializeComponent();

            textBoxStart.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    if (Int32.TryParse(textBoxStart.Text, out int number))
                        Calculate(number);
            };
        }

        private List<double> LogSequenceAlgh(int number, out List<double> serie, out List<double> trend)
        {
            List<double> sequence = new List<double>();
            for (int i = 0; i < 20; i++)
            {
                double previous = 0;
                for (int a = 0; a < i; a++)
                    previous += sequence[a];
                sequence.Add(Math.Floor(Math.Log(Math.Floor(Math.Log((double)number / Math.Pow(2, previous), 2)), 2)));
            }
            
            serie = new List<double>();
            serie.Add(0);
            for (int i = 0; i < 20; i++)
                serie.Add(serie[i] + sequence[i]);
            serie.RemoveAt(0);

            trend = new List<double>();
            for (int i = 0; i < 20; i++)
                trend.Add((double)number / Math.Pow(2, serie[i]));

            return sequence;
        }

        private void Calculate(int number)
        {

            for (int i = 1; i < int.MaxValue; i++)
                if (Math.Floor(Math.Log(Math.Floor(Math.Log(i)))) != Math.Floor(Math.Log(Math.Log(i))))
                    richTextBoxSequence.AppendText(i + "\n");

            chart.Series["sequence"].Points.Clear();
            chart.Series["serie"].Points.Clear();
            chart.Series["trend"].Points.Clear();
            List<double> sequence = LogSequenceAlgh(number, out List<double> serie, out List<double> trend);
            for (int s = 0; s < sequence.Count; s++)
            {
                PutPoint("sequence", s + 1, sequence[s]);
                PutPoint("serie", s + 1, serie[s]);
                PutPoint("trend", s + 1, trend[s]);
            }
        }

        public void PutPoint(string seriesName, double x, double y)
        {
            chart.Series[seriesName].CustomProperties = "IsXAxisQuantitative=True";
            chart.Series[seriesName].Points.AddXY(x, y);
            //chart.Update();
        }
    }
}