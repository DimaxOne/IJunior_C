using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchForTheCriminal
{
    class Program
    {
        static void Main(string[] args)
        {
            Detective detective = new Detective();

            detective.Work();
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

    class Detective
    {
        private CriminalDatabase _criminalDatabase;
        private List<Criminal> _criminals;

        public Detective()
        {
            _criminalDatabase = new CriminalDatabase();
            _criminals = new List<Criminal>();

            _criminals = _criminalDatabase.GetData();
        }

        public void Work()
        {
            while (_criminals.Count > 0)
            {
                int growth;
                int weight;
                string nationality;

                Console.WriteLine("Для поиска подозреваемых введите следующие данные.");

                if (GetNumber(out growth, "Рост: ") == false)
                {
                    Console.WriteLine("Введены некорректные данные роста");
                    UserUtils.CleanConsole();
                    continue;
                }

                if (GetNumber(out weight, "Вес: ") == false)
                {
                    Console.WriteLine("Введены некорректные данные веса");
                    UserUtils.CleanConsole();
                    continue;
                }

                Console.Write("Введите национальность: ");
                nationality = Console.ReadLine();

                var filteredCriminal = _criminals.Where(criminal => criminal.Growth == growth && criminal.Weight == weight &&
                criminal.Nationality.ToString().ToUpper() == nationality.ToUpper() && criminal.IsСustody == false).ToList();

                if (filteredCriminal.Count <= 0)
                {
                    Console.WriteLine("Никто не подходит под описание.");
                }
                else
                {
                    foreach (var criminal in filteredCriminal)
                    {
                        criminal.ShowInfo();
                    }
                }

                UserUtils.CleanConsole();
            }
        }

        private bool GetNumber(out int number, string message)
        {
            Console.Write(message);
            string userInput = Console.ReadLine();

            return (int.TryParse(userInput, out number));
        }
    }

    class CriminalDatabase
    {
        private List<Criminal> _criminals;

        public CriminalDatabase()
        {
            _criminals = new List<Criminal>();

            FillData();
        }

        public List<Criminal> GetData()
        {
            return new List<Criminal>(_criminals);
        }

        private void FillData()
        {
            _criminals.Add(new Criminal("Alex Petrov", false, 183, 86, Nationality.Russia));
            _criminals.Add(new Criminal("Daimon Iare", true, 188, 115, Nationality.USA));
            _criminals.Add(new Criminal("Axel Mell", false, 169, 72, Nationality.Canada));
            _criminals.Add(new Criminal("Simon Green", false, 181, 88, Nationality.USA));
            _criminals.Add(new Criminal("Michel Archi", false, 181, 88, Nationality.Canada));
            _criminals.Add(new Criminal("Sergey Gas", false, 176, 76, Nationality.Russia));
            _criminals.Add(new Criminal("Fernando Rodriges", true, 180, 81, Nationality.USA));
            _criminals.Add(new Criminal("Chu Van Vinh", false, 165, 55, Nationality.China));
            _criminals.Add(new Criminal("Rally Retkof", false, 192, 107, Nationality.USA));
            _criminals.Add(new Criminal("Bill Petters", false, 175, 89, Nationality.France));
            _criminals.Add(new Criminal("Pit Simpson", true, 183, 86, Nationality.USA));
            _criminals.Add(new Criminal("Mo Ku Chi", false, 171, 63, Nationality.China));
            _criminals.Add(new Criminal("Sub Zero", false, 183, 86, Nationality.France));
        }
    }

    class Criminal
    {
        public Criminal(string name, bool isСustody, int growth, int weight, Nationality nationality)
        {
            Name = name;
            IsСustody = isСustody;
            Growth = growth;
            Weight = weight;
            Nationality = nationality;
        }

        public string Name { get; private set; }
        public bool IsСustody { get; private set; }
        public int Growth { get; private set; }
        public int Weight { get; private set; }
        public Nationality Nationality { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя {Name}, рост {Growth}, вес {Weight}, национальность {Nationality.ToString()}, " +
                $"находится под стражей {IsСustody}.");
        }
    }

    public enum Nationality
    {
        Russia,
        USA,
        Canada,
        France,
        China
    }
}