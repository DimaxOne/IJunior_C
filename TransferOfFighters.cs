using System;
using System.Collections.Generic;
using System.Linq;

namespace TransferOfFighters
{
    class Program
    {
        static void Main(string[] args)
        {
            SoldierDatabase soldierDatabase = new SoldierDatabase();

            soldierDatabase.ShowSoldiers();
            soldierDatabase.RegroupSoldiers();
            soldierDatabase.ShowSoldiers();
        }
    }

    class SoldierDatabase
    {
        private List<Soldier> _firstPlatoon;
        private List<Soldier> _secondPlatoon;

        public SoldierDatabase()
        {
            _firstPlatoon = new List<Soldier>();
            _secondPlatoon = new List<Soldier>();

            FillSoldiers();
        }

        public void ShowSoldiers()
        {
            Console.WriteLine("Первый взвод:");
            ShowSoldiersInfo(_firstPlatoon);
            Console.WriteLine();
            Console.WriteLine("Второй взвод:");
            ShowSoldiersInfo(_secondPlatoon);
        }

        public void RegroupSoldiers()
        {
            string separator = "Б";

            Console.WriteLine("\nПроизведена перегруппировка солдат.\n");
            var filteredSoldiers = _firstPlatoon.Where(soldier => soldier.FullName.ToUpper().StartsWith(separator)).ToList();

            _firstPlatoon = _firstPlatoon.Where(soldier => !soldier.FullName.ToUpper().StartsWith(separator)).ToList();
            _secondPlatoon = _secondPlatoon.Union(filteredSoldiers).ToList();
        }

        private void ShowSoldiersInfo(List<Soldier> soldiers)
        {
            foreach (Soldier soldier in soldiers)
            {
                soldier.ShowInfo();
            }
        }

        private void FillSoldiers()
        {
            _firstPlatoon.Add(new Soldier("Беляков Климент Тихонович"));
            _firstPlatoon.Add(new Soldier("Гурьев Ермак Митрофанович"));
            _firstPlatoon.Add(new Soldier("Шарапов Игнатий Вадимович"));
            _firstPlatoon.Add(new Soldier("Калинин Владлен Геннадиевич"));
            _firstPlatoon.Add(new Soldier("Колесников Тимур Пётрович"));
            _firstPlatoon.Add(new Soldier("Федотов Любомир Даниилович"));
            _firstPlatoon.Add(new Soldier("Беляев Леонид Станиславович"));
            _firstPlatoon.Add(new Soldier("Баранов Лев Макарович"));

            _secondPlatoon.Add(new Soldier("Ильин Мечеслав Валерьянович"));
            _secondPlatoon.Add(new Soldier("Иванов Венедикт Мэлсович"));
            _secondPlatoon.Add(new Soldier("Ильин Мартын Яковович"));
            _secondPlatoon.Add(new Soldier("Зайцев Павел Евсеевич"));
            _secondPlatoon.Add(new Soldier("Доронин Вилли Тихонович"));
            _secondPlatoon.Add(new Soldier("Харитонов Августин Тимофеевич"));
            _secondPlatoon.Add(new Soldier("Марков Лаврентий Мартынович"));
        }
    }

    class Soldier
    {
        public Soldier(string fullName)
        {
            FullName = fullName;
        }

        public string FullName { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя солдата - {FullName}.");
        }
    }
}