using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            int images = 52;
            int imagesInRow = 3;

            int filledRow = images / imagesInRow;
            int remainingImage = images % imagesInRow;

            Console.WriteLine($"Полностью заполненных рядов - {filledRow}, картинки сверх меры - {remainingImage}.");
        }
    }
}