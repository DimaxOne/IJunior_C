using System;

namespace Sequence
{
    class Program
    {
        static void Main(string[] args)
        {
            int initialNumber = 5;
            int maxNumber = 103;
            int step = 7;

            for (int i = initialNumber; i <= maxNumber;)
            {
                Console.Write(i + " ");
                i += step;
            }
        }
    }
}