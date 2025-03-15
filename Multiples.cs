using System;

namespace Multiples
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int minimumRandomValue = 10;
            int maximumRandomValue = 25;
            int minimumValue = 50;
            int maximumValue = 150;
            int sum = 0;
            int number;

            number = random.Next(minimumRandomValue, maximumRandomValue + 1);

            for (int i = number; i <= maximumValue; i += number)
            {
                if (i >= minimumValue)
                    sum++;
            }

            Console.WriteLine($"Количество числел кратных {number} в диапазоне от {minimumValue} " +
                $"до {maximumValue} составляет {sum}.");
        }
    }
}