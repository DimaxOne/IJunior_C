using System;

namespace UIElement
{
    class Program
    {
        static void Main(string[] args)
        {
            DrowBar("Health", 4, 10, '#', 0, ConsoleColor.Red);
            DrowBar("Mana", 9, 16, 'I', 2, ConsoleColor.Blue, emptyBar: '/');
        }

        private static void DrowBar(string name, int currentValue, int maximumValue, char filledSymbol, int positionY, ConsoleColor color, char emptyBar = '_')
        {
            char openBarSymbol = '[';
            char closeBarSymbol = ']';

            if (positionY >= 0)
                Console.SetCursorPosition(0, positionY);
            else
                Console.WriteLine(" - Ось Y указана некорректно.");

            Console.Write($"{name}: ");
            ConsoleColor defaultColor = Console.BackgroundColor;
            Console.BackgroundColor = color;
            Console.Write(openBarSymbol);

            for (int i = 0; i < currentValue; i++)
            {
                Console.Write(filledSymbol);
            }

            Console.BackgroundColor = defaultColor;

            for (int i = currentValue; i < maximumValue; i++)
            {
                Console.Write(emptyBar);
            }

            Console.Write(closeBarSymbol);
        }
    }
}