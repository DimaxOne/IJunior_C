using System;
using System.Collections.Generic;
using System.Linq;

namespace ArmsReport
{
    class Program
    {
        static void Main(string[] args)
        {
            SoldierDatabase soldierDatabase = new SoldierDatabase();

            soldierDatabase.ShowSoldiers();
            soldierDatabase.ShowNameAndRankSoldiers();
        }
    }

    class SoldierDatabase
    {
        private List<Soldier> _soldiers;

        public SoldierDatabase()
        {
            _soldiers = new List<Soldier>();

            FillSoldiers();
        }

        public void ShowSoldiers()
        {
            foreach (var soldier in _soldiers)
            {
                soldier.ShowInfo();
            }
        }

        public void ShowNameAndRankSoldiers()
        {
            var shortInfoSoldiers = _soldiers.Select(soldier => new { soldier.FullName, soldier.Rank }).ToList();

            Console.WriteLine();

            foreach (var soldier in shortInfoSoldiers)
            {
                Console.WriteLine($"Имя {soldier.FullName}, звание {soldier.Rank}.");
            }
        }

        private void FillSoldiers()
        {
            _soldiers.Add(new Soldier("Беляков Климент Тихонович", "Пулемет", "Сержант", 5));
            _soldiers.Add(new Soldier("Гурьев Ермак Митрофанович", "Автомат", "Рядовой", 5));
            _soldiers.Add(new Soldier("Шарапов Игнатий Вадимович", "Автомат", "Прапорщик", 5));
            _soldiers.Add(new Soldier("Калинин Владлен Геннадиевич", "Снайперская винтовка", "Рядовой", 5));
            _soldiers.Add(new Soldier("Колесников Тимур Пётрович", "Дробовик", "Старшина", 5));
            _soldiers.Add(new Soldier("Ильин Мечеслав Валерьянович", "Автомат", "Рядовой", 5));
            _soldiers.Add(new Soldier("Иванов Венедикт Мэлсович", "Пулемет", "Ефрейтор", 5));
            _soldiers.Add(new Soldier("Ильин Мартын Яковович", "Дробовик", "Рядовой", 5));
            _soldiers.Add(new Soldier("Зайцев Павел Евсеевич", "Автомат", "Майор", 5));
            _soldiers.Add(new Soldier("Доронин Вилли Тихонович", "Снайперская винтовка", "Сержант", 5));
        }
    }

    class Soldier
    {
        private string _weaponry;
        private int _termOfServiceInMonth;

        public Soldier(string fullName, string weaponry, string rank, int termOfServiceInMonth)
        {
            FullName = fullName;
            Rank = rank;
            _weaponry = weaponry;
            _termOfServiceInMonth = termOfServiceInMonth;
        }

        public string FullName { get; private set; }
        public string Rank { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя {FullName}, вооружение - {_weaponry}, звание {Rank}, срок службы " +
                $"{_termOfServiceInMonth} месяца.");
        }
    }
}