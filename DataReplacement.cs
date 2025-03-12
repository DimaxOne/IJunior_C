using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = "Смирнов";
            string surname = "Евгений";
            string firstCap = "кофе";
            string secondCap = "чай";
            string template;

            Console.WriteLine($"Ваша фамилия - {surname}, Вас зовут - {name}.");
            template = name;
            name = surname;
            surname = template;
            Console.WriteLine($"Произведены корректировки. Ваша фамилия - {surname}, Вас зовут - {name}.");

            Console.WriteLine($"В первой чашке сейчас {firstCap}, во второй чашке {secondCap}.");
            template = firstCap;
            firstCap = secondCap;
            secondCap = template;
            Console.WriteLine($"Произведен перелив содержимого. В первой чашке сейчас {firstCap}, во второй чашке {secondCap}.");
        }
    }
}