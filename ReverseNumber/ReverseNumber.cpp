#include <iostream>

int main()
{
    double x = 1011000;
    double b = 2;
    double gnX = floor(log(x) / log(b));
    double sum;
    
    sum = 0;
    for (int n = 0; n <= gnX; n++)
    {
        double p = pow(b, -n - 1);
        double pp = x / pow(b, n);
        double f = (int)floor(pp) % (int)b;
        sum += p * f;
    }
    sum *= pow(b, gnX + 1);
    std::cout << sum << std::endl;

    sum = 0;
    for (int n = 0; n <= gnX; n++)
    {
        double p = pow(b, -n - 1);
        double f = floor(x / pow(b, n)) - b * floor(x / pow(b, n + 1));
        sum += p * f;
    }
    sum *= pow(b, gnX + 1);
    std::cout << sum << std::endl;

    double sum1 = 0;
    double sum2 = 0;
    for (int n = 0; n <= gnX; n++)
    {
        sum1 += 1 / (pow(b, n + 1)) * floor(x / pow(b, n));
        sum2 += 1 / pow(b, n) * floor(x / pow(b, n + 1));
    }
    sum = (sum1 - sum2) * pow(b, gnX + 1);
    std::cout << sum << std::endl;

    x = sum;
    gnX = floor(log(x) / log(b));
    sum1 = 0;
    sum2 = 0;
    for (int n = 0; n <= gnX; n++)
    {
        sum1 += 1 / (pow(b, n + 1)) * floor(x / pow(b, n));
        sum2 += 1 / pow(b, n) * floor(x / pow(b, n + 1));
    }
    sum = (sum1 - sum2) * pow(b, gnX + 1);
    std::cout << sum << std::endl;

    system("pause");
}