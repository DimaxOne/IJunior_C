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

            if (TryAddPosition(userPosition, personnelData))
                AddPosition(personnelData, userPosition);

            AddFullName(personnelData, userPosition, GetUserInput("Введите полное имя сотрудника: "));
        }

        private static void RemoveEmployee(Dictionary<string, List<string>> personnelData)
        {
            string userFullName = GetUserInput("Введите полное имя увольняемого сотрудника: ");
            string userPosition = GetUserInput("Введите должность увольняемого сотрудника: ");

            if (personnelData.TryGetValue(userPosition, out List<string> fullNames))
            {
                if (TryRemoveEmployee(fullNames, userFullName))
                {
                    fullNames.Remove(userFullName);
                    TryRemovePosition(personnelData, userPosition);
                }
                else
                {
                    Console.WriteLine("Такого сотрудника нет.");
                }
            }
            else
            {
                Console.WriteLine("Введена некорректная должность");
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

        private static void AddPosition(Dictionary<string, List<string>> personnelData, string position)
        {
            personnelData.Add(position, new List<string>());
        }

        private static void AddFullName(Dictionary<string, List<string>> personnelData, string position, string fullName)
        {
            personnelData[position].Add(fullName);
        }

        private static bool TryRemoveEmployee(List<string> fullNames, string userFullName)
        {
            bool canRemove = false;

            foreach (string name in fullNames)
            {
                if (name == userFullName)
                    canRemove = true;
            }

            return canRemove;
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

        private static bool TryAddPosition(string position, Dictionary<string, List<string>> personnelData)
        {
            bool isAdd = true;

            foreach (string key in personnelData.Keys)
            {
                if (key == position)
                {
                    isAdd = false;
                }
            }

            return isAdd;
        }
    }
}