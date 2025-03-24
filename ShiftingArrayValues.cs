using System;

namespace ShiftingArrayValues
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int[] numbers = new int[15];
            int maximumRandomValue = 30;
            int minimumRandomValue = 0;
            int positionShiftIndex;
            int templateNumber;

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(minimumRandomValue, maximumRandomValue + 1);
            }

            Console.Write("Дан массив: " + string.Join(" ", numbers) +
                "\nНа сколько позиций влево его нужно сдвинуть? ");

            positionShiftIndex = Convert.ToInt32(Console.ReadLine());

            if (positionShiftIndex >= numbers.Length)
                positionShiftIndex %= numbers.Length;

            for (int i = 0; i < positionShiftIndex; i++)
            {
                templateNumber = numbers[0];

                for (int j = 0; j < numbers.Length - 1; j++)
                {
                    numbers[j] = numbers[j + 1];
                }

                numbers[numbers.Length - 1] = templateNumber;
            }

            Console.WriteLine(string.Join(" ", numbers));
        }
    }
}