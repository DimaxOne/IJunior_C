using System;

namespace Shuffle
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[CreateRandomLenght()];

            Populate(array);
            Show(array);
            Shuffle(array);
            Console.WriteLine("\n");
            Show(array);
        }

        private static int CreateRandomLenght()
        {
            Random random = new Random();
            int maximumRandomValue = 30;
            int minimumRandomValue = 5;

            return random.Next(minimumRandomValue, maximumRandomValue + 1);
        }

        private static void Populate(int[] array)
        {
            Random random = new Random();
            int maximumRandomValue = 99;
            int minimumRandomValue = 10;

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(minimumRandomValue, maximumRandomValue + 1);
            }
        }

        private static void Shuffle(int[] array)
        {
            Random random = new Random();
            int templateNumber;
            int randomIndex;

            for (int i = 0; i < array.Length; i++)
            {
                randomIndex = random.Next(array.Length);
                templateNumber = array[i];
                array[i] = array[randomIndex];
                array[randomIndex] = templateNumber;
            }
        }

        private static void Show(int[] array)
        {
            foreach (int number in array)
            {
                Console.Write(number + " ");
            }
        }
    }
}