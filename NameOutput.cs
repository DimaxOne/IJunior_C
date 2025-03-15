using System;

namespace NameOutput
{
    class Program
    {
        static void Main(string[] args)
        {
            int extraLength = 2;
            int rowCount = 3;
            int middleRow;
            string name;
            char symbol;

            Console.Write("Введите Ваше имя: ");
            name = Console.ReadLine();
            Console.Write("Введите символ: ");
            symbol = Convert.ToChar(Console.ReadLine());

            middleRow = rowCount / 2;

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < name.Length + extraLength; j++)
                {
                    if (i == middleRow)
                    {
                        Console.Write(symbol + name + symbol);
                        break;
                    }

                    Console.Write(symbol);
                }

                Console.WriteLine();
            }
        }
    }
}
