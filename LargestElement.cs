using System;

namespace LargestElement
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int[,] arrow = new int[10, 10];
            int maximumRandomValue = 99;
            int minimumRandomValue = 10;
            int replacementNumber = 0;
            int largestElement;

            largestElement = minimumRandomValue;

            for (int i = 0; i < arrow.GetLength(0); i++)
            {
                for (int j = 0; j < arrow.GetLength(1); j++)
                {
                    arrow[i, j] = random.Next(minimumRandomValue, maximumRandomValue + 1);

                    if (arrow[i, j] > largestElement)
                        largestElement = arrow[i, j];
                }
            }

            Console.WriteLine($"Наибольший элемент матрицы: {largestElement}");

            Console.WriteLine("Исходная матрица: ");

            for (int i = 0; i < arrow.GetLength(0); i++)
            {
                for (int j = 0; j < arrow.GetLength(1); j++)
                {
                    Console.Write(arrow[i, j] + " ");
                }

                Console.WriteLine();
            }

            Console.WriteLine("Полученная матрица: ");

            for (int i = 0; i < arrow.GetLength(0); i++)
            {
                for (int j = 0; j < arrow.GetLength(1); j++)
                {
                    if (arrow[i, j] == largestElement)
                        Console.Write(replacementNumber + " ");
                    else
                        Console.Write(arrow[i, j] + " ");
                }

                Console.WriteLine();
            }
        }
    }
}
