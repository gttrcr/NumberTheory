using System;
using System.Collections.Generic;

namespace CollatzPattern
{
    class Program
    {
        static bool CollatzOk(Int64 a, Int64 b, Int64 number)
        {
            List<Int64> sequence = new List<Int64>();
            try
            {
                while (number != 1 && sequence.IndexOf(number) == -1)
                {
                    sequence.Add(number);
                    if (number % 2 == 0)
                        number /= 2;
                    else
                        number = a * number + b;
                }
            }
            catch
            {
                return false;
            }

            return number == 1;
        }

        static void Main(string[] args)
        {
            for (int a = 0; a < 1000; a++)
            {
                for (int b = -1000; b < 1000; b++)
                {
                    if (Math.Abs(a) % 2 == 1 && Math.Abs(b) % 2 == 1)
                    {
                        bool collatzOk = true;
                        Int64 i;
                        for (i = 1; i < 1000000; i++)
                            if (!CollatzOk(a, b, i))
                            {
                                collatzOk = false;
                                break;
                            }

                        if (collatzOk)
                            Console.WriteLine(a + " " + b);
                    }
                }
            }

            Console.WriteLine("STOP");
            Console.ReadLine();
        }
    }
}