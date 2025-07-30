using System;
using System.Collections.Generic;
using System.Linq;

namespace AnarchyInTheHospital
{
    class Program
    {
        static void Main(string[] args)
        {
            Hospital hospital = new Hospital();

            hospital.Work();
        }
    }

    class UserUtils
    {
        public static void CleanConsole()
        {
            Console.WriteLine("Нажмите любую кнопку для продолжения.");
            Console.ReadLine();
            Console.Clear();
        }
    }

    class Hospital
    {
        private List<Patient> _patients;

        public Hospital()
        {
            _patients = new List<Patient>();

            FillPatient();
        }

        public void Work()
        {
            const string CommandSortByName = "1";
            const string CommandSortByAge = "2";
            const string CommandSearchByIllness = "3";

            while (_patients.Count > 0)
            {
                ShowPatients(new List<Patient>(_patients));

                Console.Write($"\n\nПриветствуем в базе пациентов. Вам доступно:" +
                    $"\n{CommandSortByName}) Отсортировать всех больных по фио;" +
                    $"\n{CommandSortByAge}) Отсортировать всех больных по возрасту;" +
                    $"\n{CommandSearchByIllness}) Вывести больных с определенным заболеванием." +
                    $"\nВаша команда: ");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandSortByName:
                        ShowSortByName();
                        break;

                    case CommandSortByAge:
                        ShowSortByAge();
                        break;

                    case CommandSearchByIllness:
                        SearchByIllness();
                        break;

                    default:
                        Console.WriteLine("Введена некорректная команда.");
                        break;
                }

                UserUtils.CleanConsole();
            }
        }

        private void ShowSortByName()
        {
            var sortedByName = _patients.OrderBy(patient => patient.FullName).ToList();

            ShowPatients(new List<Patient>(sortedByName));
        }

        private void ShowSortByAge()
        {
            var sortedByAge = _patients.OrderBy(patient => patient.Age).ToList();

            ShowPatients(new List<Patient>(sortedByAge));
        }

        private void SearchByIllness()
        {
            string userInput;

            Console.Write("Введите название заболевания: ");
            userInput = Console.ReadLine();

            var resultSearchByIllness = _patients.Where(patient => patient.Illness.ToUpper() == userInput.ToUpper())
                .ToList();

            if(resultSearchByIllness.Count > 0)
                ShowPatients(new List<Patient>(resultSearchByIllness));
            else
                Console.WriteLine("Пациентов с таким заболеванием нет.");
        }

        private void ShowPatients(List<Patient> patients)
        {
            Console.WriteLine();

            foreach (Patient patient in patients)
            {
                patient.ShowInfo();
            }
        }

        private void FillPatient()
        {
            _patients.Add(new Patient("Беляков Климент Тихонович", 28, "Аппендицит"));
            _patients.Add(new Patient("Гурьев Ермак Митрофанович", 39, "Гайморит"));
            _patients.Add(new Patient("Шарапов Игнатий Вадимович", 19, "ОРЗ"));
            _patients.Add(new Patient("Калинин Владлен Геннадиевич", 42, "Перелом"));
            _patients.Add(new Patient("Колесников Тимур Пётрович", 55, "Перелом"));
            _patients.Add(new Patient("Ильин Мечеслав Валерьянович", 41, "Неадекватность"));
            _patients.Add(new Patient("Иванов Венедикт Мэлсович", 18, "Перелом"));
            _patients.Add(new Patient("Ильин Мартын Яковович", 33, "Отравление"));
            _patients.Add(new Patient("Зайцев Павел Евсеевич", 42, "Аппендицит"));
            _patients.Add(new Patient("Доронин Вилли Тихонович", 48, "Отравление"));
            _patients.Add(new Patient("Харитонов Августин Тимофеевич", 37, "ОРЗ"));
            _patients.Add(new Patient("Федотов Любомир Даниилович", 37, "Отравление"));
            _patients.Add(new Patient("Беляев Леонид Станиславович", 58, "Гайморит"));
            _patients.Add(new Patient("Марков Лаврентий Мартынович", 44, "ОРЗ"));
            _patients.Add(new Patient("Баранов Лев Макарович", 28, "Перелом"));
        }
    }

    class Patient
    {
        public Patient(string fullName, int age, string illness)
        {
            FullName = fullName;
            Age = age;
            Illness = illness;
        }

        public string FullName { get; private set; }
        public int Age { get; private set; }
        public string Illness { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Пациент {FullName}, возраст {Age}, диагноз - {Illness}.");
        }
    }
}