using System;

namespace StapledExpression
{
    class Program
    {
        static void Main(string[] args)
        {
            string symbols = "((()))((((()))))";
            char openSymbol = '(';
            char closeSymbol = ')';
            bool isCorrectExpression = true;
            int currentDepth = 0;
            int maximumDepth = 0;

            foreach (char symbol in symbols)
            {
                if (symbol == openSymbol)
                    currentDepth++;
                else if (symbol == closeSymbol)
                    currentDepth--;

                if (currentDepth < 0)
                {
                    isCorrectExpression = false;
                    break;
                }
                    
                if (currentDepth > maximumDepth)
                    maximumDepth = currentDepth;
            }

            if (currentDepth != 0)
                isCorrectExpression = false;

            if (isCorrectExpression)
            {
                Console.WriteLine("Это корректное скобочное выражение.");
                Console.WriteLine($"Максимальная глубина вложенности скобок - {maximumDepth}.");
            }
            else
            {
                Console.WriteLine("Выражение некорректно.");
            }
        }
    }
}