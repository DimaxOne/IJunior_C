using System;

namespace WorkingWithRowsColumns
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int[,] arrow = new int[10, 10];
            int maximumRandomValue = 9;
            int minimumRandomValue = 1;
            int amountRowIndex = 1;
            int multiplicationСolumnIndex = 0;
            int sumResult = 0;
            int multiplicationResult = 1;

            for (int i = 0; i < arrow.GetLength(0); i++)
            {
                for (int j = 0; j < arrow.GetLength(1); j++)
                {
                    arrow[i, j] = random.Next(minimumRandomValue, maximumRandomValue + 1);
                }
            }

            for (int i = 0; i < arrow.GetLength(0); i++)
            {
                for (int j = 0; j < arrow.GetLength(1); j++)
                {
                    if (i == amountRowIndex)
                        sumResult += arrow[i, j];

                    if (j == multiplicationСolumnIndex)
                        multiplicationResult *= arrow[i, j];
                }
            }

            for (int i = 0; i < arrow.GetLength(0); i++)
            {
                for (int j = 0; j < arrow.GetLength(1); j++)
                {
                    Console.Write(arrow[i, j] + " ");
                }

                Console.WriteLine();
            }

            Console.WriteLine($"Сумма {amountRowIndex + 1} строки равна {sumResult}." +
                $"\nПроизведение {multiplicationСolumnIndex + 1} столбца равна {multiplicationResult}");
        }
    }
}
