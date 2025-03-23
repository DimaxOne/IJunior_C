using System;

namespace Split
{
    class Program
    {
        static void Main(string[] args)
        {
            string message = "Дана строка с текстом, используя метод строки String.Split() " +
                "получить массив слов, которые разделены пробелом в тексте и вывести массив, " +
                "каждое слово с новой строки.";
            string[] words;

            words = message.Split(' ');

            foreach (string word in words)
            {
                Console.WriteLine(word);
            }
        }
    }
}