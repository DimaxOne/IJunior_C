using System;

namespace UIElement
{
    class Program
    {
        static void Main(string[] args)
        {
            DrowBarGraph("Health", 40, 10, '#', 0, ConsoleColor.Red);
            DrowBarGraph("Mana", 70, 16, 'I', 1, ConsoleColor.Blue, emptyBar: '/');
        }

        private static void DrowBarGraph(string name, int currentPercentValue, int maximumValue, char filledSymbol, int positionY, ConsoleColor color, char emptyBar = '_')
        {
            ConsoleColor defaultColor = Console.BackgroundColor;
            char openBarSymbol = '[';
            char closeBarSymbol = ']';
            int maximumPercent = 100;
            int positionX = 0;
            int valueInBars;
            int emptyBars;

            if (positionY >= 0)
                Console.SetCursorPosition(positionX, positionY);
            else
                Console.WriteLine(" - Ось Y указана некорректно.");

            valueInBars = currentPercentValue * maximumValue / maximumPercent;
            emptyBars = maximumValue - valueInBars;

            Console.Write($"{name}: ");
            Console.BackgroundColor = color;
            Console.Write(openBarSymbol);
            DrowBar(valueInBars, filledSymbol);
            Console.BackgroundColor = defaultColor;
            DrowBar(emptyBars, emptyBar);
            Console.Write(closeBarSymbol);
        }

        private static void DrowBar(int barsCount, char symbol)
        {
            for (int i = 0; i < barsCount; i++)
            {
                Console.Write(symbol);
            }
        }
    }
}