using System;
using System.Collections.Generic;

namespace DynamicArrayAdvanced
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CommandGetSum = "sum";
            const string CommandExit = "exit";

            List<int> numbers = new List<int>();
            bool isWork = true;
            string userInput;

            while (isWork)
            {
                Console.Write("Имеющиеся числа: ");
                ShowNumbers(numbers);

                Console.Write($"\n\nВы можете:" +
                    $"\n{CommandGetSum} - вывести сумму имеющихся чисел;" +
                    $"\n{CommandExit} - выйти из программы;" +
                    $"\nИли ввести число для добавления." +
                    $"\nВведите команду: ");

                userInput = Console.ReadLine();

                if (userInput == CommandGetSum)
                    ShowSum(numbers);
                else if (userInput == CommandExit)
                    isWork = false;
                else
                    AddNumber(numbers, userInput);

                Console.WriteLine("Для продолжения нажмите любую кнопку...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void AddNumber(List<int> numbers, string userInput)
        {
            if (int.TryParse(userInput, out int userNumber))
                numbers.Add(userNumber);
            else
                Console.WriteLine("Недопустимый формат.");
        }

        private static void ShowSum(List<int> numbers)
        {
            int sum = 0;

            foreach (int number in numbers)
            {
                sum += number;
            }

            Console.WriteLine("Сумма всех числе равна " + sum);
        }

        private static void ShowNumbers(List<int> numbers)
        {
            foreach (int number in numbers)
            {
                Console.Write(number + " ");
            }
        }
    }
}