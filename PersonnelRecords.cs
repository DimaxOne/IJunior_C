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

            string[] fullNamesOfEmployees = new string[0];
            string[] positions = new string[0];
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
                        userInput = GetUserInput("Введите полное имя сотрудника (Ф.И.О): ");
                        fullNamesOfEmployees = Expand(fullNamesOfEmployees);
                        FillEmployeeData(fullNamesOfEmployees, userInput);
                        userInput = GetUserInput("Введите должность сотрудника: ");
                        positions = Expand(positions);
                        FillEmployeeData(positions, userInput);
                        break;

                    case CommandPullAllDossiers:
                        ShowAllDossiers(fullNamesOfEmployees, positions);
                        break;

                    case CommandRemoveDossier:
                        userInput = GetUserInput("Введите порядковый номер досье для удаления: ");

                        if (int.TryParse(userInput, out int index))
                        {
                            if (fullNamesOfEmployees.Length < index || index < 0)
                            {
                                Console.WriteLine("Некорректный порядковый номер досье.");
                            }
                            else
                            {
                                fullNamesOfEmployees = RemoveDossier(fullNamesOfEmployees, index - 1);
                                positions = RemoveDossier(positions, index - 1);
                            }
                        }

                        break;

                    case CommandSearchSurnames:
                        userInput = GetUserInput("Введите фамилию для поиска: ");
                        SearchSurnames(fullNamesOfEmployees, userInput);
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

        private static void FillEmployeeData(string[] array, string employeeInformation)
        {
            array[array.Length - 1] = employeeInformation;
        }

        private static string[] Expand(string[] array)
        {
            string[] templateArray = new string[array.Length + 1];

            if (array.Length > 0)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    templateArray[i] = array[i];
                }
            }

            return templateArray;
        }

        private static string[] RemoveDossier(string[] array, int index)
        {
            string[] templateArray = new string[array.Length - 1];

            for (int i = 0; i < array.Length; i++)
            {
                if (i < index)
                    templateArray[i] = array[i];
                else if (i == index)
                    continue;
                else
                    templateArray[i - 1] = array[i];
            }

            return templateArray;
        }

        private static void SearchSurnames(string[] array, string surname)
        {
            foreach (string fullName in array)
            {
                if (GetSurname(fullName) == surname)
                    Console.WriteLine(fullName);
            }
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