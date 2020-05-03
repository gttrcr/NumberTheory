using System;
using System.Collections.Generic;
using System.Linq;

namespace GilbreathConsole
{
    class Program
    {

        static bool Check(List<int> seq, int number)
        {
            List<int> inputList = new List<int>(seq) { number };
            int length = inputList.Count;
            while (length > 0)
            {
                for (int i = 0; i < length - 1; i++)
                    inputList[i] = Math.Abs(inputList[i] - inputList[i + 1]);

                if (inputList[0] == 1)
                    length--;
                else
                    return false;
            }

            return true;
        }

        static void Calculate(List<int> seq, out List<int> minSeq, out List<int> maxSeq)
        {
            int elements = 30;
            int range = 10000000;
            minSeq = new List<int>(seq);
            maxSeq = new List<int>(seq);

            while (minSeq.Count < elements)
            {
                int min = 100000;
                int max = -100000;
                for (int i = -range; i < range; i++)
                {
                    if (Check(minSeq, i))
                        if (i < min)
                            min = i;
                    if (Check(maxSeq, i))
                        if (i > max)
                            max = i;
                }

                minSeq.Add(min);
                maxSeq.Add(max);

                Console.WriteLine(min + "\t\t" + max);
            }
        }

        static void Main(string[] args)
        {
            while (true)
            {
                //string input = Console.ReadLine();
                List<int> seq = new List<int> { 2, 3, 5, 7, 11, 13 }; // input.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToList();
                Calculate(seq, out List<int> minSeq, out List<int> maxSeq);

                //Console.WriteLine("min\t\tmax");
                //for (int i = 0; i < maxSeq.Count; i++)
                //    Console.WriteLine(minSeq[i] + "\t\t" + maxSeq[i]);

                Console.WriteLine("-------------");
            }
        }
    }
}