using System;

namespace PasswordProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            string password = "pass123";
            int availableNumberOfAttempts = 3;
            int currentNumberOfAttempts;
            string userInput;

            for (int i = 0; i < availableNumberOfAttempts; i++)
            {
                Console.Write("Введите пароль: ");
                userInput = Console.ReadLine();

                if (userInput == password)
                {
                    Console.WriteLine("Тайное сообщение.");
                    break;
                }

                currentNumberOfAttempts = availableNumberOfAttempts - i - 1;

                if (currentNumberOfAttempts > 0)
                    Console.WriteLine($"Неверный пароль. Оставшиеся попытки: {currentNumberOfAttempts}.");
                else
                    Console.WriteLine("Доступ заблокирован.");
            }
        }
    }
}