using System;
using System.Collections.Generic;

namespace ExplanatoryDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> numbers = new Dictionary<string, int>() { { "One", 1 }, { "Two" , 2 }, { "Three", 3 },
                {"Four", 4 }, {"Five", 5 }, {"Six",6 }, {"Seven", 7 },{"Eight", 8 },{"Nine", 9 },{"Ten", 10 } };

            ShowNumber(GetUserInput("Напишите словом какое число вам нужно: "), numbers);
        }

        private static void ShowNumber(string userInput, Dictionary<string, int> numbers)
        {
            if (numbers.TryGetValue(userInput, out int value))
                Console.WriteLine(value);
            else
                Console.WriteLine("Такого слова нет.");
        }

        private static string GetUserInput(string message)
        {
            Console.Write(message);

            return Console.ReadLine();
        }
    }
}