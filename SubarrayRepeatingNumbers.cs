using System;
using System.Collections.Generic;

namespace SubarrayRepeatingNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int[] numbers = new int[30];
            int maximumRandomValue = 3;
            int minimumRandomValue = 1;
            int defaultRepetition = 1;
            int maximumRepetition = 0;
            int maximumNumberRepeated = 0;
            int matchesOnFirstRepetition = 2;
            bool isRepetition = false;
            int currentRepetition;

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(minimumRandomValue, maximumRandomValue + 1);
            }

            currentRepetition = defaultRepetition;

            for (int i = 0; i < numbers.Length - 1; i++)
            {
                if (numbers[i] == numbers[i + 1])
                {
                    isRepetition = true;
                    currentRepetition++;

                    if (maximumRepetition < currentRepetition)
                    {
                        maximumRepetition = currentRepetition;
                        maximumNumberRepeated = numbers[i];
                    }
                }
                else
                {
                    currentRepetition = defaultRepetition;
                }
            }

            if(isRepetition)
            {
                Console.WriteLine("{" + string.Join(", ", numbers) + 
                    $"}} - число {maximumNumberRepeated} повторяется {maximumRepetition} раза подряд.");
            }
            else
            {
                Console.WriteLine("Повторений нет.");
            }
        }
    }
}