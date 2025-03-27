using System;

namespace UIElement
{
    class Program
    {
        static void Main(string[] args)
        {
            DrowBarsGraph("Health", 40, 10, '#', 0, ConsoleColor.Red);
            DrowBarsGraph("Mana", 70, 16, 'I', 1, ConsoleColor.Blue, emptyBar: '/');
        }

        private static void DrowBarsGraph(string name, int currentPercentValue, int maximumValue, char filledSymbol, int positionY, ConsoleColor color, char emptyBar = '_')
        {
            ConsoleColor defaultColor = Console.BackgroundColor;
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
            DrowBars(valueInBars, true, filledSymbol);
            Console.BackgroundColor = defaultColor;
            DrowBars(emptyBars, false, emptyBar);
        }

        private static void DrowBars(int barsCount, bool isFirstPart, char symbol)
        {
            char openBarSymbol = '[';
            char closeBarSymbol = ']';

            if (isFirstPart)
                Console.Write(openBarSymbol);

            for (int i = 0; i < barsCount; i++)
            {
                Console.Write(symbol);
            }

            if (isFirstPart == false)
                Console.Write(closeBarSymbol);
        }
    }
}