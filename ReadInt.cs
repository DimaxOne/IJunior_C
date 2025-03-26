using System;

namespace ReadInt
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetNumber());
        }

        private static int GetNumber()
        {
            bool isNumber = false;
            int number = 0;
            string userInput;

            while(isNumber == false)
            {
                Console.Write("Введите число: ");
                userInput = Console.ReadLine();

                isNumber = int.TryParse(userInput, out number);
            }

            return number;
        }
    }
}