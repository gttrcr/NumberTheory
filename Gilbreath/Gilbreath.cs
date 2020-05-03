using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Gilbreath
{
    public partial class Gilbreath : Form
    {
        public Gilbreath()
        {
            InitializeComponent();

            richTextBox.Text = "min\t\t\tmax\n";
            startToolStripMenuItem.Click += (sender, e) =>
            {
                Start();
            };

            textBoxStart.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    Solutions();
            };
        }

        public List<int> Solutions(List<int> seq)
        {
            List<int> sol = new List<int>();
            for (int i = -100000; i < 100000; i++)
                if (Check(seq, i))
                    sol.Add(i);

            return sol;
        }

        public void Solutions()
        {
            int maxLength = 8;
            List<string> sequences = new List<string>();
            sequences.Add(string.Join(" ", textBoxStart.Text.Split(' ')));
            int number = sequences.Count;
            for (int i = 0; i < sequences.Count;)
            {
                number++;
                List<int> sol = Solutions(sequences[i].Split(' ').Select(x => Convert.ToInt32(x)).ToList());
                string seq = sequences[i];
                sequences[i] = seq + " " + sol[0];
                for (int a = 1; a < sol.Count; a++)
                    sequences.Insert(i, seq + " " + sol[a]);

                if (sequences.TrueForAll(x => x.Split(' ').Count() == number))
                {
                    i = 0;
                    if (number == maxLength)
                        break;
                }
                else
                {
                    i += sol.Count;
                    number--;
                }
            }

            richTextBox.Clear();
            if (sequences.TrueForAll(x => Check(x.Split(' ').Select(y => Convert.ToInt32(y)).ToList())))
                sequences.ForEach(x => richTextBox.AppendText(x + "\n"));
            else
                richTextBox.AppendText("Error");
        }

        public void Start()
        {
            int elementNumber = 15;
            int range = 50000;
            List<int> firstElement = new List<int>() { 2, 3, 5, 7, 11, 13, 17, 19};
            elementNumber += firstElement.Count;
            for (int i = 0; i < firstElement.Count; i++)
            {
                PutPoint("min", i + 1, firstElement[i]);
                PutPoint("max", i + 1, firstElement[i]);
                Write(firstElement[i], firstElement[i]);
            }

            ////Calculate random Gilbreath sequences
            //Random rnd = new Random();
            //int randomSequencesNumber = 500;
            //List<int> solutions = new List<int>();
            //List<int> sequence = new List<int>();
            //for (int number = 0; number < randomSequencesNumber; number++)
            //{
            //    sequence.Clear();
            //    sequence.AddRange(firstElement);
            //    while (sequence.Count < elementNumber)
            //    {
            //        solutions.Clear();
            //        //for (int i = -range; i < range; i++)
            //        for (int i = 0; i < range; i++)
            //        if (Check(sequence, i))
            //                solutions.Add(i);
            //
            //        sequence.Add(solutions[rnd.Next(solutions.Count)]);
            //        PutPoint("seq", sequence.Count, sequence[sequence.Count - 1]);
            //    }
            //}

            ////Calculate min and max
            int min = range;
            int max = -range;
            List<int> minS = new List<int>();
            List<int> maxS = new List<int>();
            minS.AddRange(firstElement);
            maxS.AddRange(firstElement);
            for (int number = 0; number < elementNumber; number++)
            {
                for (int i = -range; i < range; i++)
                {
                    if (Check(minS, i))
                        if (i < min)
                            min = i;

                    if (Check(maxS, i))
                        if (i > max)
                            max = i;
                }

                minS.Add(min);
                maxS.Add(max);

                PutPoint("min", minS.Count, min);
                PutPoint("max", maxS.Count, max);
                Write(min, max);
            }
        }

        public bool Check(List<int> sequence)
        {
            int length = sequence.Count;
            while (length-- > 0)
            {
                for (int i = 0; i < length; i++)
                    sequence[i] = Math.Abs(sequence[i + 1] - sequence[i]);
                if (sequence[0] != 1)
                    return false;
            }

            return true;
        }

        public bool Check(List<int> sequence, int next)
        {
            List<int> check = new List<int>(sequence) { next };
            return Check(check);
        }

        public void PutPoint(string seriesName, int x, int y)
        {
            chart.Series[seriesName].CustomProperties = "IsXAxisQuantitative=True";
            chart.Series[seriesName].Points.AddXY(x, y);
            chart.Update();
        }

        public void Write(int value1, int value2)
        {
            richTextBox.AppendText(value1 + "\t\t\t" + value2 + "\n");
        }
    }
}