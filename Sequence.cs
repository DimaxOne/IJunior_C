using System;

namespace Sequence
{
    class Program
    {
        static void Main(string[] args)
        {
            int initialNumber = 5;
            int maxNumber = 103;
            int cycleStep = 7;

            for (int i = initialNumber; i <= maxNumber; i += cycleStep)
            {
                Console.Write(i + " ");
            }
        }
    }
}