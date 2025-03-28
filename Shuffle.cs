using System;

namespace Shuffle
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array;

            array = CreateRandomLenghtArray();
            FilledArray(array);
            ShowArray(array);
            ShuffleArray(array);
            Console.WriteLine("\n");
            ShowArray(array);
        }

        private static int[] CreateRandomLenghtArray()
        {
            Random random = new Random();
            int maximumRandomValue = 30;
            int minimumRandomValue = 5;

            int[] array = new int[random.Next(minimumRandomValue, maximumRandomValue + 1)];

            return array;
        }

        private static void FilledArray(int[] array)
        {
            Random random = new Random();
            int maximumRandomValue = 99;
            int minimumRandomValue = 10;

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(minimumRandomValue, maximumRandomValue + 1);
            }
        }

        private static void ShuffleArray(int[] array)
        {
            Random random = new Random();
            int maximumRandomValue = array.Length - 1;
            int minimumRandomValue = 0;
            int templateNumber;
            int randomIndex;

            for (int i = 0; i < array.Length; i++)
            {
                randomIndex = random.Next(minimumRandomValue, maximumRandomValue + 1);
                templateNumber = array[i];
                array[i] = array[randomIndex];
                array[randomIndex] = templateNumber;
            }
        }

        private static void ShowArray(int[] array)
        {
            foreach (int number in array)
            {
                Console.Write(number + " ");
            }
        }
    }
}