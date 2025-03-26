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

            while(!isNumber)
            {
                Console.Write("Введите число: ");
                userInput = Console.ReadLine();
                
                if(int.TryParse(userInput, out number))
                    isNumber = true;
            }

            return number;
        }
    }
}