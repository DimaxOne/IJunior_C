using System;

namespace SimpleCycle
{
    class Program
    {
        static void Main(string[] args)
        {
            string message;
            int repeats;

            Console.Write("Введите сообщение для повтора: ");
            message = Console.ReadLine();
            Console.Write("Введите количество повторов: ");
            repeats = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < repeats; i++)
            {
                Console.WriteLine(message);
            }
        }
    }
}
