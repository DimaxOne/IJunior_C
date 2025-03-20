using System;

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
            int currentRepetition = 0;
            int maximumRepetition = 0;
            int maximumNumberRepeated = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(minimumRandomValue, maximumRandomValue + 1);
            }

            for (int i = 0; i < numbers.Length - 1; i++)
            {
                if (numbers[i] == numbers[i + 1])
                {
                    currentRepetition++;
                }
                else
                {
                    if (maximumRepetition < currentRepetition)
                    {
                        maximumRepetition = currentRepetition;
                        maximumNumberRepeated = numbers[i];
                    }

                    currentRepetition = 0;
                }
            }

            Console.Write("{");

            for (int i = 0; i < numbers.Length - 1; i++)
            {
                Console.Write(numbers[i] + ", ");
            }

            Console.Write($"{numbers[numbers.Length - 1]}}} - число {maximumNumberRepeated} повторяется {maximumRepetition + 1} раза подряд.");
        }
    }
}