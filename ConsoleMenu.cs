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

            Random rand = new Random();
            bool isWork = true;
            int maxRandomNumber = 100;
            string userInput;

            while(isWork)
            {
                Console.Write("Добро пожаловать!\nВы можете выбрать действия:\n1 - показать здоровье игрока;\n" +
                    "2 - показать здоровье врага;\n3 - показать случайное число;\n4 - очистить консоль;\n" +
                    "5 - выход.\nВаша команда: ");

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
                        Console.WriteLine(rand.Next(0, maxRandomNumber));
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
