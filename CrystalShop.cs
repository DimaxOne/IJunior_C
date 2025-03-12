using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            int crystalsInBag = 0;
            int crystalPrice = 10;
            int goldsInBag;
            int crystalsForPurchase;

            Console.Write("Сколько у Вас золота? ");
            goldsInBag = Convert.ToInt32(Console.ReadLine());
            Console.Write("Скалько кристаллов Вам нужно? ");
            crystalsForPurchase = Convert.ToInt32(Console.ReadLine());

            goldsInBag -= crystalsForPurchase * crystalPrice;
            crystalsInBag += crystalsForPurchase;

            Console.WriteLine($"У Вас в сумке {goldsInBag} золота и {crystalsInBag} кристаллов.");
        }
    }
}