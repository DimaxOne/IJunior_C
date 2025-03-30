using System;

namespace PersonnelRecords
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CommandAddDossier = "1";
            const string CommandPullAllDossiers = "2";
            const string CommandRemoveDossier = "3";
            const string CommandSearchSurnames = "4";
            const string CommandExit = "5";

            string[] fullNamesOfEmployees = { "Богатьков Павел Николаевич", "Гейб Виктор Франкович", "Петров Василий Иванович" };
            string[] positions = { "HR менеджер", "IQ менеджер", "Слесарь" };
            bool isWork = true;
            string userInput;

            while (isWork)
            {
                Console.Write($"База данных сотрудников. Вам доступно:" +
                    $"\n {CommandAddDossier} - добавить досье;" +
                    $"\n {CommandPullAllDossiers} - вывести все досье;" +
                    $"\n {CommandRemoveDossier} - удалить досье;" +
                    $"\n {CommandSearchSurnames} - поиск по фамилии;" +
                    $"\n {CommandExit} - выход.\n");

                userInput = GetUserInput("Введите команду: ");

                switch (userInput)
                {
                    case CommandAddDossier:
                        AddDossier(ref fullNamesOfEmployees, ref positions);
                        break;

                    case CommandPullAllDossiers:
                        ShowAllDossiers(fullNamesOfEmployees, positions);
                        break;

                    case CommandRemoveDossier:
                        RemoveDossier(ref fullNamesOfEmployees, ref positions);
                        break;

                    case CommandSearchSurnames:
                        SearchSurnames(fullNamesOfEmployees);
                        break;

                    case CommandExit:
                        isWork = false;
                        break;
                }

                Console.WriteLine("Нажмите любую кнопку для продолжения.");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void AddDossier(ref string[] fullNames, ref string[] positions)
        {
            fullNames = Expand(fullNames, GetUserInput("Введите полное имя сотрудника (Ф.И.О): "));
            positions = Expand(positions, GetUserInput("Введите должность сотрудника: "));
        }

        private static string[] Expand(string[] array, string employeeData)
        {
            string[] templateArray = new string[array.Length + 1];

            if (array.Length > 0)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    templateArray[i] = array[i];
                }
            }

            templateArray[templateArray.Length - 1] = employeeData;

            return templateArray;
        }

        private static void RemoveDossier(ref string[] fullNames, ref string[] positions)
        {
            int index = 0;

            if (GetIndex(ref index) && index >= 0 && fullNames.Length > index)
            {
                fullNames = ReduceArray(fullNames, index);
                positions = ReduceArray(positions, index);
            }
            else
            {
                Console.WriteLine("Некорректный порядковый номер для удаления");
            }
        }

        private static bool GetIndex(ref int index)
        {
            if (int.TryParse(GetUserInput("Введите порядковый номер досье для удаления: "), out int userNumber))
            {
                index = userNumber - 1;
                return true;
            }
            else
            {
                return false;
            }
        }

        private static string[] ReduceArray(string[] array, int index)
        {
            string[] templateArray = new string[array.Length - 1];

            for (int i = 0; i < index; i++)
            {
                if (i < index)
                    templateArray[i] = array[i];
            }

            for (int i = index + 1; i < array.Length; i++)
            {
                templateArray[i - 1] = array[i];
            }

            return templateArray;
        }

        private static void SearchSurnames(string[] array)
        {
            string surname = GetUserInput("Введите фамилию для поиска: ");
            bool isSuccessfulSearch = false;

            foreach (string fullName in array)
            {
                if (GetSurname(fullName) == surname)
                {
                    Console.WriteLine(fullName);
                    isSuccessfulSearch = true;
                } 
            }

            if(isSuccessfulSearch == false)
                Console.WriteLine("Данной фамилии в базе нет.");
        }

        private static string GetSurname(string fullName)
        {
            string[] separatedName;
            char separationSymbol = ' ';

            separatedName = fullName.Split(separationSymbol);

            return separatedName[0];
        }

        private static void ShowAllDossiers(string[] fullNames, string[] positions)
        {
            for (int i = 0; i < fullNames.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {fullNames[i]} - {positions[i]}");
            }
        }

        private static string GetUserInput(string message)
        {
            Console.Write(message);

            return Console.ReadLine();
        }
    }
}