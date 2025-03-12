using System;

namespace ExitControl
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isEnable = true;
            string exitСommand = "exit";
            string userСommand;

            while (isEnable)
            {
                Console.Write($"\nДля выхода введите {exitСommand}. \nВведите Вашу команду: ");
                userСommand = Console.ReadLine();

                if (userСommand == exitСommand)
                    isEnable = false;
                else
                    Console.WriteLine(userСommand);
            }
        }
    }
}
