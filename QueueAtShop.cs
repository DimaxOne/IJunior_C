using System;
using System.Collections.Generic;

namespace QueueAtShop
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> customers = new Queue<int>();
            int money = 0;

            FillQueue(customers);

            while(customers.Count > 0)
            {
                money += ServeCustomer(customers);
                ShowCashInformation(money);
            }

            Console.WriteLine("Клиенты закончились.");
        }

        private static void FillQueue(Queue<int> purchaseAmount)
        {
            Random random = new Random();
            int maximumQueueLenght = 30;
            int minimumQueueLenght = 5;
            int maximumRandomValue = 1000;
            int minimumRandomValue = 10;
            int queueLenght;

            queueLenght = random.Next(minimumQueueLenght, maximumQueueLenght + 1);

            for (int i = 0; i < queueLenght; i++)
            {
                purchaseAmount.Enqueue(random.Next(minimumRandomValue, maximumRandomValue + 1));
            }
        }

        private static int ServeCustomer(Queue<int> purchaseAmount)
        {
            Console.WriteLine($"Сумма операции равна: {purchaseAmount.Peek()}");

            return purchaseAmount.Dequeue();
        }

        private static void ShowCashInformation(int money)
        {
            Console.WriteLine($"Сумма в кассе после обслуживания клиента: {money}.\nДля продолжения нажмите любую кнопку.");
            Console.ReadKey();
            Console.Clear();
        }
    }
}