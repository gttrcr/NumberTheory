// 114ProblemCpp.cpp : Questo file contiene la funzione 'main', in cui inizia e termina l'esecuzione del programma.
//

#include <iostream>
#include<thread>
#include<math.h>
#include<iomanip>

double lower = 1;
double number =114;

void Func(double upper, double a)
{
    double aaa;
    double bbb;
    for (double b = lower; b < upper; b++)
    {
        aaa = a * a * a;
        bbb = b * b * b;
        double arg = number - (aaa + bbb);
        double result = cbrt(arg);
        if (result == (int)result)
            std::cout << aaa << " " << bbb << " " << pow(result, 3) << std::endl;

        arg = number - (aaa - bbb);
        result = cbrt(arg);
        if (result == (int)result)
            std::cout << aaa << " -" << bbb << " " << pow(result, 3) << std::endl;

        arg = number - (-aaa + bbb);
        result = cbrt(arg);
        if (result == (int)result)
            std::cout << "-" << aaa << " " << bbb << " " << pow(result, 3) << std::endl;

        arg = number - (-aaa - bbb);
        result = cbrt(arg);
        if (result == (int)result)
            std::cout << "-" << aaa << " -" << bbb << " " << pow(result, 3) << std::endl;
    }
}

static int Calculate()
{
    double upper = 2;

    for (double a = lower; a < upper; a++)
    {
        Func(1000000, a);
    }

    return -1;
}

int main()
{
    std::cout << std::setprecision(30) << std::endl;
    Calculate();
}