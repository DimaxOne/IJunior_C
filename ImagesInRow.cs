using System;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            int images = 52;
            int imagesInRow = 3;

            int filledRow = 52 / 3;
            int remainingImage = 52 % 3;

            Console.WriteLine($"Полностью заполненных рядов - {filledRow}, картинки сверх меры - {remainingImage}.");
        }
    }
}
