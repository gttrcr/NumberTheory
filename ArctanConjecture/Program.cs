using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArctanConjecture
{
    class Program
    {
        static void Main(string[] args)
        {
            double lookingFor = 0;
            double max = Math.Pow(10, 9);
            for (double n = 1; n < max; n++)
            {
                double left = Math.Log10(n / Math.PI);
                double right = -Math.Log10(Math.Tan(Math.PI / (n + 1)));
                double avg = (left + right) / 2;

                if (left > lookingFor)
                {
                    Console.WriteLine("The conjecture is false");
                    break;
                }

                if (left <= lookingFor && right > lookingFor)
                {
                    bool up = lookingFor > avg;
                    Console.WriteLine("LookingFor: " + lookingFor + " n: " + n + " left: " + left + " right: " + right + " the number is " + (up ? "up" : "down") + " the avg");
                    lookingFor++;
                }
            }

            Console.ReadLine();
        }
    }
}