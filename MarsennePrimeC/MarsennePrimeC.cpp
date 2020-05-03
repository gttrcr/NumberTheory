#include"bigint.h"

#include<algorithm>
#include<ppl.h>
#include <atomic>
#include <cmath>
#include <cstdlib>

template <class Integral>
bool is_prime(const Integral& n) {
    if (n < 2)
        return false;
    if (n == 2 || n == 3)
        return true;
    if (n % 2 == 0 || n % 3 == 0)
        return false;

    auto const top = (Integral)std::sqrt(n) + 1;
    std::atomic<bool> is_prime = true;

#pragma omp parallel for
    for (Integral i = 3; i < top; i += 6) {
        if (!is_prime)
            continue;
        if (n % (i + 2) == 0)
            is_prime = false;
        if (n % (i + 4) == 0)
            is_prime = false;
    }
    return is_prime;
}

bool is_marsenne_prime(bigint m, unsigned int p)
{
    bigint s = 4;
    for (unsigned int i = 0; i < p - 2; i++)
        s = ((s ^ 2) - 2) % m;
    if (s == 0)
        return true;

    return false;
}

int main()
{
    unsigned int blocks = 10000;
    bigint a = bigint(2);

    concurrency::parallel_for(2, 1000000, [&](unsigned int prime)
        //for (unsigned int prime = 2; prime < 1000000; prime++)
    {
        if (is_prime(prime))
        {
            //unsigned int expP = 13466917;
            unsigned int expP = prime;
            bigint b = bigint(expP);

            std::vector<bigint> exp;
            for (unsigned int i = 0; i < expP / blocks; i++)
                exp.push_back(blocks);
            exp.push_back(expP - exp.size() * blocks);

            std::vector<bigint> partial;
            for (unsigned int i = 0; i < exp.size(); i++)
                partial.push_back(1);

            bigint expOfa;
            if (partial.size() > 1)
                expOfa = a ^ blocks;
            for (unsigned int i = 0; i < partial.size(); i++)
            {
                if (exp[i] == blocks)
                    partial[i] = expOfa;
                else
                    partial[i] = a ^ exp[i];
            }

            bigint res = 1;
            for (unsigned int i = 0; i < partial.size(); i++)
                res *= partial[i];
            res -= 1;

            if (is_marsenne_prime(res, prime))
                std::cout << prime << std::endl;
        }
    });

    system("pause");
}