using System;

namespace DynamicArray
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CommandSum = "sum";
            const string CommandExit = "exit";

            int[] numbers = new int[0];
            int sumNumbers = 0;
            bool isWork = true;
            string userInput;

            while (isWork)
            {
                Console.Write("Имеющиеся числа: ");

                foreach (int number in numbers)
                {
                    Console.Write(number + " ");
                }

                Console.Write($"\nВы можете:" +
                    $"\n{CommandSum} - вывести сумму имеющихся чисел;" +
                    $"\n{CommandExit} - выйти из программы;" +
                    $"\nИли ввести число для добавления." +
                    $"\nВведите команду: ");

                userInput = Console.ReadLine();

                if (userInput == CommandSum)
                {
                    foreach (int number in numbers)
                    {
                        sumNumbers += number;
                    }

                    Console.WriteLine($"Сумма всех чисел равна {sumNumbers}.");

                    sumNumbers = 0;
                }
                else if (userInput == CommandExit)
                {
                    isWork = false;
                }
                else
                {
                    int[] tempArray = new int[numbers.Length + 1];

                    for (int i = 0; i < numbers.Length; i++)
                    {
                        tempArray[i] = numbers[i];
                    }

                    tempArray[tempArray.Length - 1] = Convert.ToInt32(userInput);
                    numbers = tempArray;
                }

                Console.WriteLine("Для продолжения нажмите любую кнопку...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}