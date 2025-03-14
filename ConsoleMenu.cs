using System;

namespace ConsoleMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CommandShowPlayerHealth = "1";
            const string CommandShowEnemyHealth = "2";
            const string CommandShowRandomNumber = "3";
            const string CommandClearConsole = "4";
            const string CommandExit = "5";

            Random random = new Random();
            bool isWork = true;
            int maxRandomNumber = 99;
            string userInput;

            while (isWork)
            {
                Console.Write($"Добро пожаловать!\nВы можете выбрать:" +
                    $"\n{CommandShowPlayerHealth} - показать здоровье игрока;" +
                    $"\n{CommandShowEnemyHealth} - показать здоровье врага;" +
                    $"\n{CommandShowRandomNumber} - показать случайное число;" +
                    $"\n{CommandClearConsole} - очистить консоль;" +
                    $"\n{CommandExit} - выход." +
                    $"\nВаша команда: ");

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandShowPlayerHealth:
                        Console.WriteLine("Здоровье игрока равно 124 XP.");
                        break;
                    case CommandShowEnemyHealth:
                        Console.WriteLine("Здоровье врага равно 32 XP.");
                        break;
                    case CommandShowRandomNumber:
                        Console.WriteLine(random.Next(0, maxRandomNumber + 1));
                        break;
                    case CommandClearConsole:
                        Console.Clear();
                        break;
                    case CommandExit:
                        isWork = false;
                        break;
                    default:
                        Console.WriteLine("Введена неверная команда.");
                        break;
                }

                Console.WriteLine();
            }
        }
    }
}
