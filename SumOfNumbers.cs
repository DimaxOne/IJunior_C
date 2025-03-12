using System;

namespace SumOfNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            int firstDivisor = 3;
            int secondDivisor = 5;
            int maxRandomNumber = 100;
            int sum = 0;
            int number;
            bool canDivideWithoutRemainder;

            number = rand.Next(0, maxRandomNumber);

            for (int i = 0; i <= number; i++)
            {
                canDivideWithoutRemainder = i % firstDivisor == 0 || i % secondDivisor == 0;

                if (canDivideWithoutRemainder)
                    sum += i;
            }

            Console.WriteLine($"Для числа {number} сумма чисел равна: {sum}.");
        }
    }
}
