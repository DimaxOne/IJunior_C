using System;

namespace GradeOfTwo
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int maximumValue = 100;
            int minimumValue = 0;
            int factorizationNumber = 2;
            int currentDegree = 1;
            int currentNumber;
            int number;

            number = random.Next(minimumValue, maximumValue + 1);
            currentNumber = factorizationNumber;

            while(currentNumber < number)
            {
                currentNumber *= factorizationNumber;
                currentDegree++;
            }

            Console.WriteLine($"Число {number} меньше числа {currentNumber} полученного из {factorizationNumber} " +
                $"в степени {currentDegree}");
        }
    }
}
