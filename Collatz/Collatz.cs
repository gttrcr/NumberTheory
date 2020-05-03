using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Collatz
{
    public partial class Collatz : Form
    {
        public Collatz()
        {
            InitializeComponent();

            textBoxStart.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    if (Int32.TryParse(textBoxStart.Text, out int number))
                        Calculate(number);
            };
        }

        private List<int> CollatzAlgh(int number, out List<int> numberOfDivision)
        {
            int nOD = 0;
            List<int> ret = new List<int>();
            numberOfDivision = new List<int>(nOD);

            if (number == 0)
                return ret;

            while (number != 1)
            {
                ret.Add(number);
                if (number % 2 == 0)
                {
                    number /= 2;
                    nOD++;
                }
                else
                {
                    number = 3 * number + 1;
                    numberOfDivision.Add(nOD);
                    nOD = 0;
                }
            }

            ret.Add(1);
            if (numberOfDivision.Count == 0)
                numberOfDivision.Add(nOD);

            return ret;
        }

        private void Calculate(int number)
        {
            chart.Series["sequence"].Points.Clear();
            int i = number;
            //for (int i = 1; i < number; i++)
            //{
            List<int> sequence = CollatzAlgh(i, out List<int> numberOfDivision);
            for (int s = 0; s < numberOfDivision.Count; s++)
                PutPoint("sequence", s, numberOfDivision[s]);
            //PutPoint("sequence", i, sequence.Count);
            //sequence.ForEach(x => richTextBoxSequence.AppendText(x + "\n"));
            //}
        }

        public void PutPoint(string seriesName, int x, int y)
        {
            chart.Series[seriesName].CustomProperties = "IsXAxisQuantitative=True";
            chart.Series[seriesName].Points.AddXY(x, y);
            //chart.Update();
        }
    }
}