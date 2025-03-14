using System;

namespace CurrencyConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CommandExchangeRublesToDollars = "1";
            const string CommandExchangeRublesToDirhams = "2";
            const string CommandExchangeDollarsToRubles = "3";
            const string CommandExchangeDollarsToDirhams = "4";
            const string CommandExchangeDirhamsToRubles = "5";
            const string CommandExchangeDirhamsToDollars = "6";
            const string CommandExit = "7";

            float rublesInAccount = 1000000;
            float dollarsInAccount = 10000;
            float dirhamsInAccount = 100000;
            float rublesToDollarsExchangeRate = 85.35f;
            float rublesToDirhamsExchangeRate = 23.24f;
            float dollarsToRublesExchangeRate = 0.012f;
            float dollarsToDirhamsExchangeRate = 0.27f;
            float dirhamsToRublesExchangeRate = 0.043f;
            float dirhamsToDollarsExchangeRate = 3.67f;
            float amountСurrencyExchange;
            string userCommand;
            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine($"Ваш баланс {rublesInAccount} рублей, {dollarsInAccount} долларов, " +
                    $"{dirhamsInAccount} дирхам.");
                Console.SetCursorPosition(0, 3);
                Console.Write($"Добро пожаловать в пункт обмена валют!\nВы можете:" +
                    $"\n{CommandExchangeRublesToDollars} - обмен рублей на доллары;" +
                    $"\n{CommandExchangeRublesToDirhams} - обмен рублей на дирхамы;" +
                    $"\n{CommandExchangeDollarsToRubles} - обмен долларов на рубли;" +
                    $"\n{CommandExchangeDollarsToDirhams} - обмен долларов на дирхамы;" +
                    $"\n{CommandExchangeDirhamsToRubles} - обмен дирхам на рубли;" +
                    $"\n{CommandExchangeDirhamsToDollars} - обмен дирхам на доллары;" +
                    $"\n{CommandExit} - завершить обслуживание." +
                    $"\nВыберите необходимую операцию: ");

                userCommand = Console.ReadLine();

                switch (userCommand)
                {
                    case CommandExchangeRublesToDollars:
                        Console.Write($"Обмен рублей на доллары.\nВам доступно {rublesInAccount} рублей. Сколько Вы хотите обменять? ");
                        amountСurrencyExchange = Convert.ToSingle(Console.ReadLine());

                        if (rublesInAccount >= amountСurrencyExchange && amountСurrencyExchange > 0)
                        {
                            rublesInAccount -= amountСurrencyExchange;
                            dollarsInAccount += amountСurrencyExchange / rublesToDollarsExchangeRate;
                        }
                        else
                        {
                            Console.WriteLine("Недопустимое количество валюты.");
                        }

                        break;

                    case CommandExchangeRublesToDirhams:
                        Console.Write($"Обмен рублей на дирхамы.\nВам доступно {rublesInAccount} рублей. Сколько Вы хотите обменять? ");
                        amountСurrencyExchange = Convert.ToSingle(Console.ReadLine());

                        if (rublesInAccount >= amountСurrencyExchange && amountСurrencyExchange > 0)
                        {
                            rublesInAccount -= amountСurrencyExchange;
                            dirhamsInAccount += amountСurrencyExchange / rublesToDirhamsExchangeRate;
                        }
                        else
                        {
                            Console.WriteLine("Недопустимое количество валюты.");
                        }

                        break;

                    case CommandExchangeDollarsToRubles:
                        Console.Write($"Обмен долларов на рубли.\nВам доступно {dollarsInAccount} долларов. Сколько Вы хотите обменять? ");
                        amountСurrencyExchange = Convert.ToSingle(Console.ReadLine());

                        if (dollarsInAccount >= amountСurrencyExchange && amountСurrencyExchange > 0)
                        {
                            dollarsInAccount -= amountСurrencyExchange;
                            rublesInAccount += amountСurrencyExchange / dollarsToRublesExchangeRate;
                        }
                        else
                        {
                            Console.WriteLine("Недопустимое количество валюты.");
                        }

                        break;

                    case CommandExchangeDollarsToDirhams:
                        Console.Write($"Обмен долларов на дирхамы.\nВам доступно {dollarsInAccount} долларов. Сколько Вы хотите обменять? ");
                        amountСurrencyExchange = Convert.ToSingle(Console.ReadLine());

                        if (dollarsInAccount >= amountСurrencyExchange && amountСurrencyExchange > 0)
                        {
                            dollarsInAccount -= amountСurrencyExchange;
                            dirhamsInAccount += amountСurrencyExchange / dollarsToDirhamsExchangeRate;
                        }
                        else
                        {
                            Console.WriteLine("Недопустимое количество валюты.");
                        }

                        break;

                    case CommandExchangeDirhamsToRubles:
                        Console.Write($"Обмен дирхам на рубли.\nВам доступно {dirhamsInAccount} дирхам. Сколько Вы хотите обменять? ");
                        amountСurrencyExchange = Convert.ToSingle(Console.ReadLine());

                        if (dirhamsInAccount >= amountСurrencyExchange && amountСurrencyExchange > 0)
                        {
                            dirhamsInAccount -= amountСurrencyExchange;
                            rublesInAccount += amountСurrencyExchange / dirhamsToRublesExchangeRate;
                        }
                        else
                        {
                            Console.WriteLine("Недопустимое количество валюты.");
                        }

                        break;

                    case CommandExchangeDirhamsToDollars:
                        Console.Write($"Обмен дирхам на доллары.\nВам доступно {dirhamsInAccount} дирхам. Сколько Вы хотите обменять? ");
                        amountСurrencyExchange = Convert.ToSingle(Console.ReadLine());

                        if (dirhamsInAccount >= amountСurrencyExchange && amountСurrencyExchange > 0)
                        {
                            dirhamsInAccount -= amountСurrencyExchange;
                            dollarsInAccount += amountСurrencyExchange / dirhamsToDollarsExchangeRate;
                        }
                        else
                        {
                            Console.WriteLine("Недопустимое количество валюты.");
                        }
                        break;

                    case CommandExit:
                        Console.WriteLine("Приходите к нам ещё!");
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Введена неверная команда.");
                        break;
                }

                Console.WriteLine("Для продолжения нажмите любую клавишу.");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
