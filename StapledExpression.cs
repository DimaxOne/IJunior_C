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
            int openSymbolCount = 0;
            int closeSymbolCount = 0;
            int maximumDepth = 0;
            bool isCorrectExpression = true;
            int currentDepth;

            foreach (char symbol in symbols)
            {
                if (symbol == openSymbol)
                    openSymbolCount++;
                else if (symbol == closeSymbol)
                    closeSymbolCount++;

                if (openSymbolCount >= closeSymbolCount)
                {
                    currentDepth = openSymbolCount - closeSymbolCount;

                    if (currentDepth > maximumDepth)
                        maximumDepth = currentDepth;
                }
                else
                {
                    isCorrectExpression = false;
                }
            }

            if (openSymbolCount != closeSymbolCount)
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