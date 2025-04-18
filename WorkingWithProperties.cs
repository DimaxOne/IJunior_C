using System;

namespace WorkingWithProperties
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player(50, 10, '@');
            Renderer renderer = new Renderer();

            renderer.Drow(player.PositionX, player.PositionY, player.Symbol);
            renderer.HideUnnecessary();
        }
    }

    class Player
    {
        public int PositionX { get; private set; }
        public int PositionY { get; private set; }
        public char Symbol { get; private set; }

        public Player(int positionX, int positionY, char symbol)
        {
            PositionX = positionX;
            PositionY = positionY;
            Symbol = symbol;
        }
    }

    class Renderer
    {
        public void Drow(int positionX, int positionY, char symbol)
        {
            Console.SetCursorPosition(positionX, positionY);
            Console.Write(symbol);
        }

        public void HideUnnecessary()
        {
            Console.CursorVisible = false;
            Console.ReadKey(true);
        }
    }
}
