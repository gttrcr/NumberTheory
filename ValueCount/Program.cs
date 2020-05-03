using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValueCount
{
    class Program
    {
        static void Main(string[] args)
        {
            int cnt = 0;
            for (double i = 0; i < 10; i += (double)1 / (double)10000)
            {
                Console.WriteLine(++cnt + " " + i);
            }

            Console.ReadLine();
        }
    }
}