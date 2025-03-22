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
            int indexIteration = 0;
            int templateNumber;

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(minimumRandomValue, maximumRandomValue + 1);
            }

            Console.WriteLine(string.Join(" ", numbers));

            while (indexIteration < numbers.Length - 1)
            {
                if (numbers[indexIteration] > numbers[indexIteration + 1])
                {
                    templateNumber = numbers[indexIteration + 1];
                    numbers[indexIteration + 1] = numbers[indexIteration];
                    numbers[indexIteration] = templateNumber;
                    indexIteration = 0;
                    continue;
                }

                indexIteration++;
            }

            Console.WriteLine(string.Join(" ", numbers));
        }
    }
}