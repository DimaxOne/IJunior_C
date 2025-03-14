using System;

namespace SumOfNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int firstDivisor = 3;
            int secondDivisor = 5;
            int maxRandomNumber = 100;
            int sum = 0;
            int number;

            number = random.Next(0, maxRandomNumber + 1);

            for (int i = 0; i <= number; i++)
            {
                if (i % firstDivisor == 0 || i % secondDivisor == 0)
                    sum += i;
            }

            Console.WriteLine($"Для числа {number} сумма чисел равна: {sum}.");
        }
    }
}
