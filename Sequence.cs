using System;

namespace Sequence
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxNumber = 103;
            int step = 7;

            for (int i = 5; i <= maxNumber;)
            {
                Console.Write(i + " ");
                i += step;
            }
        }
    }
}
