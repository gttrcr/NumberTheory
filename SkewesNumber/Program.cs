using System;
using WolframLinkNamespace;

namespace SkewesNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            WolframLink wolframLink = new WolframLink();
            wolframLink.Evaluate("prime=10^14; primepi:=PrimePi[prime];");
            string skewes = "False";
            UInt64 a = 0;

            while (skewes != "True")
            {
                skewes = wolframLink.Evaluate("primepi-LogIntegral[N[prime]]>0");
                wolframLink.Evaluate("prime=NextPrime[prime]; ++primepi;");

                a++;
                if (a % 5000 == 0)
                    Console.WriteLine(wolframLink.Evaluate("N[Log10[prime], 5]"));
            }

            string prime = wolframLink.Evaluate("prime");
            string primepi = wolframLink.Evaluate("primepi");
            Console.WriteLine("SKEWES IS TRUE! Prime: " + prime + " primepi: " + primepi);

            Console.ReadLine();
        }
    }
}