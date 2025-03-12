using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            string name;
            string specialty;
            int age;
            int salary;

            Console.Write("Как Вас зовут? ");
            name = Console.ReadLine();
            Console.Write("Сколько Вам лет? ");
            age = Convert.ToInt32(Console.ReadLine());
            Console.Write("Ваша специальность? ");
            specialty = Console.ReadLine();
            Console.Write("Ожидаемая заработная плата? ");
            salary = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"Вас зовут {name}, Ваш возраст - {age}. Предпочитаемая заработная плата - {salary}" +
                $" за деятельность по специальности: {specialty}.");
        }
    }
}
