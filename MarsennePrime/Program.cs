using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsennePrime
{
    class Program
    {
        //static bool IsPrime(StringMath exp, StringMath number)
        //{
        //    StringMath s = new StringMath("4");
        //    StringMath index = new StringMath("0");
        //    while (index != exp)
        //    {
        //        s = (s ^ new StringMath("2") - new StringMath("2"));
        //    }
        //}

        static void Main(string[] args)
        {
            StringMath a = new StringMath("2");
            StringMath b = new StringMath("4564578");

            int blocks = 10000;
            int actualExp = Convert.ToInt32(b.ToString());
            int singleExp = actualExp / blocks;
            List<StringMath> exp = new StringMath[singleExp].Select(x => new StringMath(blocks.ToString())).ToList();
            exp.Add(new StringMath((actualExp - singleExp * blocks).ToString()));

            List<StringMath> partial = new StringMath[exp.Count].Select(x => new StringMath("1")).ToList();
            Parallel.For(0, partial.Count, i =>
            {
                partial[i] = a ^ exp[i];
            });

            int lastPerc = 0;
            StringMath c = partial[0];
            for (int i = 1; i < partial.Count; i++)
            {
                c *= partial[i];

                int perc = i * 100 / partial.Count;
                if (perc > lastPerc)
                {
                    lastPerc = perc;
                    Console.WriteLine(perc);
                }
            }

            //Console.WriteLine(c.ToString());

            //if (IsPrime(c))
            //{
            //    Console.WriteLine("E' primo");
            //    Console.WriteLine("La lunghezza è: " + c.ToString().Length);
            //}
            //else
            //    Console.WriteLine("Non è primo");

            Console.ReadLine();
        }
    }
}