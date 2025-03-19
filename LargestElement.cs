using System;

namespace LargestElement
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int[,] numbers = new int[10, 10];
            int maximumRandomValue = 99;
            int minimumRandomValue = 10;
            int replacementNumber = 0;
            int largestElement;

            largestElement = minimumRandomValue;

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    numbers[i, j] = random.Next(minimumRandomValue, maximumRandomValue + 1);

                    if (numbers[i, j] > largestElement)
                        largestElement = numbers[i, j];
                }
            }

            Console.WriteLine($"Наибольший элемент матрицы: {largestElement}");

            Console.WriteLine("Исходная матрица: ");

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    Console.Write(numbers[i, j] + " ");
                }

                Console.WriteLine();
            }

            Console.WriteLine("Полученная матрица: ");

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    if (numbers[i, j] == largestElement)
                        numbers[i, j] = replacementNumber;

                    Console.Write(numbers[i, j] + " ");
                }

                Console.WriteLine();
            }
        }
    }
}
