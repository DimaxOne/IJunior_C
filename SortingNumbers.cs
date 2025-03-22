using System;

namespace SortingNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int[] numbers = new int[15];
            int maximumRandomValue = 15;
            int minimumRandomValue = 0;
            int templateNumber;

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(minimumRandomValue, maximumRandomValue + 1);
            }

            Console.WriteLine(string.Join(" ", numbers));

            for (int i = 0; i < numbers.Length - 1; i++)
            {
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if (numbers[i] > numbers[j])
                    {
                        templateNumber = numbers[i];
                        numbers[i] = numbers[j];
                        numbers[j] = templateNumber;
                    }
                }
            }

            Console.WriteLine(string.Join(" ", numbers));
        }
    }
}