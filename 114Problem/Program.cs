using DecimalMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _114Problem
{
    class Program
    {
        private static double lower = 0;
        private static double number = 1;

        private static void Func(Object obj)
        {
            double upper = ((double[])obj)[0];
            double a = ((double[])obj)[1];
            double aaa;
            double bbb;
            for (double b = lower; b < upper; b++)
            {
                aaa = a * a * a;
                bbb = b * b * b;
                double arg = number - (aaa + bbb);
                double result = Math.Pow(arg, (double)(1 / 3));
                if (result == (int)result)
                    Console.WriteLine(aaa + " " + bbb + " " + result);

                arg = number - (aaa - bbb);
                result = Math.Pow(arg, (double)(1 / 3));
                if (result == (int)result)
                    Console.WriteLine(aaa + " -" + bbb + " " + result);

                arg = number - (-aaa + bbb);
                result = Math.Pow(arg, (double)(1 / 3));
                if (result == (int)result)
                    Console.WriteLine("-" + aaa + " " + bbb + " " + result);

                arg = number - (-aaa - bbb);
                result = Math.Pow(arg, (double)(1 / 3));
                if (result == (int)result)
                    Console.WriteLine("-" + aaa + " -" + bbb + " " + result);
            }
        }

        static int Calculate()
        {
            double upper = 1; // uint.MaxValue;

            for (double a = lower; a < upper; a++)
            {
                Thread thread = new Thread(Func);
                thread.Start(new double[] { 1000000, a });
            }

            return -1;
        }

        static void Main(string[] args)
        {
            Calculate();
        }
    }
}