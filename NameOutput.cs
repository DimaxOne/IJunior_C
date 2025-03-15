using System;

namespace NameOutput
{
    class Program
    {
        static void Main(string[] args)
        {
            string topAndBottomRow = "";
            string name;
            string middleRow;
            char symbol;

            Console.Write("Введите Ваше имя: ");
            name = Console.ReadLine();
            Console.Write("Введите символ: ");
            symbol = Convert.ToChar(Console.ReadLine());

            middleRow = symbol + name + symbol;

            for (int i = 0; i < middleRow.Length; i++)
            {
                topAndBottomRow += symbol;
            }

            Console.WriteLine($"{topAndBottomRow}\n{middleRow}\n{topAndBottomRow}");
        }
    }
}
