using System;
using System.Collections.Generic;

namespace ExplanatoryDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> numbers = new Dictionary<string, int>();
            string[] keyNumbers = { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten" };
            int[] valueNumbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            FillDictionary(numbers, keyNumbers, valueNumbers);
            ShowNumber(GetUserInput("Напишите словом какое число вам нужно: "), numbers);
        }

        private static void FillDictionary(Dictionary<string, int> dictionary, string[] keyNumbers, int[] valueNumbers)
        {
            for (int i = 0; i < keyNumbers.Length; i++)
            {
                dictionary.Add(keyNumbers[i].ToLower(), valueNumbers[i]);
            }
        }

        private static void ShowNumber(string userInput, Dictionary<string, int> numbers)
        {
            if (numbers.TryGetValue(userInput.ToLower(), out int value))
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