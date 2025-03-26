using System;

namespace UIElement
{
    class Program
    {
        static void Main(string[] args)
        {
            DrowBar("Health", 40, 10, '#', 0, ConsoleColor.Red);
            DrowBar("Mana", 70, 16, 'I', 1, ConsoleColor.Blue, emptyBar: '/');
        }

        private static void DrowBar(string name, int currentPercentValue, int maximumValue, char filledSymbol, int positionY, ConsoleColor color, char emptyBar = '_')
        {
            char openBarSymbol = '[';
            char closeBarSymbol = ']';
            int maximumPercent = 100;
            int positionX = 0;
            int valueInBar;

            valueInBar = currentPercentValue * maximumValue / maximumPercent;

            if (positionY >= 0)
                Console.SetCursorPosition(positionX, positionY);
            else
                Console.WriteLine(" - Ось Y указана некорректно.");

            Console.Write($"{name}: ");
            ConsoleColor defaultColor = Console.BackgroundColor;
            Console.BackgroundColor = color;
            Console.Write(openBarSymbol);

            for (int i = 0; i < valueInBar; i++)
            {
                Console.Write(filledSymbol);
            }

            Console.BackgroundColor = defaultColor;

            for (int i = valueInBar; i < maximumValue; i++)
            {
                Console.Write(emptyBar);
            }

            Console.Write(closeBarSymbol);
        }
    }
}