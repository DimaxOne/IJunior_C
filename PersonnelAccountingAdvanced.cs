using System;
using System.Collections.Generic;

namespace PersonnelAccountingAdvanced
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CommandAddEmployee = "1";
            const string CommandRemoveEmployee = "2";
            const string CommandShowFullInfomation = "3";
            const string CommandExit = "4";

            Dictionary<string, List<string>> personnelData = new Dictionary<string, List<string>>();
            bool isWork = true;

            while (isWork)
            {
                Console.Write($"База данных сотрудников. Вам доступно:" +
                    $"\n {CommandAddEmployee} - Добавление сотрудника;" +
                    $"\n {CommandRemoveEmployee} - Удаление сотрудника;" +
                    $"\n {CommandShowFullInfomation} - Показ полной информации;" +
                    $"\n {CommandExit} - Выход.\n");

                switch (GetUserInput("Введите команду: "))
                {
                    case CommandAddEmployee:
                        AddEmployee(personnelData);
                        break;

                    case CommandRemoveEmployee:
                        RemoveEmployee(personnelData);
                        break;

                    case CommandShowFullInfomation:
                        ShowFullInfomation(personnelData);
                        break;

                    case CommandExit:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Введена некорректная команда.");
                        break;
                }

                Console.WriteLine("Нажмите любую кнопку для продолжения.");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void AddEmployee(Dictionary<string, List<string>> personnelData)
        {
            string userPosition = GetUserInput("Введите должность сотрудника: ");

            if (personnelData.ContainsKey(userPosition) == false)
                personnelData.Add(userPosition, new List<string>());

            personnelData[userPosition].Add(GetUserInput("Введите полное имя сотрудника: "));
        }

        private static void RemoveEmployee(Dictionary<string, List<string>> personnelData)
        {
            string userPosition = GetUserInput("Введите должность увольняемого сотрудника: ");

            if (personnelData.TryGetValue(userPosition, out List<string> fullNames))
            {
                Console.WriteLine("Полный список сотрудников: ");

                for (int i = 0; i < fullNames.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - {fullNames[i]}");
                }

                string userIndex = GetUserInput("Введите номер удаляемого сотрудника: ");

                if (int.TryParse(userIndex, out int index))
                {
                    index--;

                    if (index >= 0 && index < fullNames.Count)
                    {
                        fullNames.RemoveAt(index);
                        TryRemovePosition(personnelData, userPosition);
                        Console.WriteLine("Сотрудник удален.");
                    }
                    else
                    {
                        Console.WriteLine("Такого сотрудника нет.");
                    }
                }
                else
                {
                    Console.WriteLine("Введен некорректный индекс.");
                }
            }
            else
            {
                Console.WriteLine("Введена некорректная должность.");
            }
        }

        private static void ShowFullInfomation(Dictionary<string, List<string>> personnelData)
        {
            foreach (string position in personnelData.Keys)
            {
                Console.Write($"На позиции: {position} - ");

                foreach (string fullName in personnelData[position])
                {
                    Console.Write($"{fullName} | ");
                }

                Console.WriteLine();
            }
        }

        private static void TryRemovePosition(Dictionary<string, List<string>> personnelData, string userPosition)
        {
            if (personnelData[userPosition].Count == 0)
                personnelData.Remove(userPosition);
        }

        private static string GetUserInput(string message)
        {
            Console.Write(message);

            return Console.ReadLine();
        }
    }
}