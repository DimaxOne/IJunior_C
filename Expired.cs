using System;
using System.Collections.Generic;
using System.Linq;

namespace Expired
{
    class Program
    {
        static void Main(string[] args)
        {
            Warehouse warehouse = new Warehouse();

            warehouse.ShowStews();
            warehouse.ShowExpiredStews();
        }
    }

    class Warehouse
    {
        private List<Stew> _stews;

        public Warehouse()
        {
            _stews = new List<Stew>();

            FillStews();
        }

        public void ShowStews()
        {
            ShowStews(_stews);
        }

        public void ShowExpiredStews()
        {
            var expiredProducts = _stews.Where(stew => stew.YearOfManufacture + stew.ShelfLife < DateTime.Now.Year).ToList();

            Console.Write("\nПросроченная тушенка:");
            ShowStews(expiredProducts);
        }

        private void ShowStews(List<Stew> stews)
        {
            Console.WriteLine();

            foreach (Stew stew in stews)
            {
                stew.ShowInfo();
            }
        }

        private void FillStews()
        {
            _stews.Add(new Stew("Микоян", 2020, 4));
            _stews.Add(new Stew("Честный продукт", 2024, 10));
            _stews.Add(new Stew("Гродфуд", 2020, 6));
            _stews.Add(new Stew("Совок", 2020, 12));
            _stews.Add(new Stew("Барс", 2019, 3));
            _stews.Add(new Stew("Атрус", 2025, 1));
            _stews.Add(new Stew("Йошкар-Олинский мясокомбинат", 2017, 15));
            _stews.Add(new Stew("Симбирский мясоперерабатывающий комбинат", 2023, 8));
            _stews.Add(new Stew("МясоКонсервный завод АРГО", 2018, 6));
            _stews.Add(new Stew("Мясной Дом Олония", 2017, 7));
        }
    }

    class Stew
    {
        public Stew(string title, int yearOfManufacture, int shelfLife)
        {
            Title = title;
            YearOfManufacture = yearOfManufacture;
            ShelfLife = shelfLife;
        }

        public string Title { get; private set; }
        public int YearOfManufacture { get; private set; }
        public int ShelfLife { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Название {Title}, год производства {YearOfManufacture}, " +
                $"срок годности {ShelfLife}.");
        }
    }
}