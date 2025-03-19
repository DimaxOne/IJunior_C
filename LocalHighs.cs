﻿using System;

namespace LocalHighs
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int[] numbers = new int[30];
            int maximumRandomValue = 99;
            int minimumRandomValue = 1;

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(minimumRandomValue, maximumRandomValue + 1);
            }

            for (int i = 0; i < numbers.Length; i++)
            {
                if (i == 0)
                {
                    if (numbers[i] > numbers[i + 1])
                        Console.Write(numbers[i] + " ");
                }
                else if (i == numbers.Length - 1)
                {
                    if (numbers[numbers.Length - 1] > numbers[numbers.Length - 2])
                        Console.Write(numbers[numbers.Length - 1] + " ");
                }
                else if (numbers[i] > numbers[i - 1] && numbers[i] > numbers[i + 1])
                {
                        Console.Write(numbers[i] + " ");
                }
            }
        }
    }
}
