using System;
using System.Collections.Generic;
using System.Linq;

namespace Amnesty
{
    class Program
    {
        static void Main(string[] args)
        {
            Arstotzka arstotzka = new Arstotzka();

            arstotzka.ShowPrisoners();
            arstotzka.Amnesty();
            arstotzka.ShowPrisoners();
        }
    }

    class Arstotzka
    {
        private List<Prisoner> _prisoners;

        public Arstotzka()
        {
            _prisoners = new List<Prisoner>();

            FillPrisoners();
        }

        public void Amnesty()
        {
            string crimeForAmnesty = "Антиправительственное";
            char separatorSymbol = '*';
            int symbolCount = 100;

            string separator = "\n\n" + new string(separatorSymbol, symbolCount) + "\n\n";

            Console.WriteLine(separator + "В нашей стране произошла амнистия! " +
                "Список заключеных изменен, многие были освобождены." + separator);

            _prisoners = _prisoners.Where(prisioner => prisioner.Crime != crimeForAmnesty).ToList();
        }

        public void ShowPrisoners()
        {
            foreach (Prisoner prisoner in _prisoners)
            {
                prisoner.ShowInfo();
            }
        }

        private void FillPrisoners()
        {
            _prisoners.Add(new Prisoner("Беляков Климент Тихонович", "Мошенничество"));
            _prisoners.Add(new Prisoner("Гурьев Ермак Митрофанович", "Антиправительственное"));
            _prisoners.Add(new Prisoner("Шарапов Игнатий Вадимович", "Хулиганство"));
            _prisoners.Add(new Prisoner("Калинин Владлен Геннадиевич", "Разбой"));
            _prisoners.Add(new Prisoner("Колесников Тимур Пётрович", "Антиправительственное"));
            _prisoners.Add(new Prisoner("Ильин Мечеслав Валерьянович", "Антиправительственное"));
            _prisoners.Add(new Prisoner("Иванов Венедикт Мэлсович", "Разбой"));
            _prisoners.Add(new Prisoner("Ильин Мартын Яковович", "Мошенничество"));
            _prisoners.Add(new Prisoner("Зайцев Павел Евсеевич", "Антиправительственное"));
            _prisoners.Add(new Prisoner("Доронин Вилли Тихонович", "Хулиганство"));
            _prisoners.Add(new Prisoner("Харитонов Августин Тимофеевич", "Антиправительственное"));
            _prisoners.Add(new Prisoner("Федотов Любомир Даниилович", "Мошенничество"));
            _prisoners.Add(new Prisoner("Беляев Леонид Станиславович", "Антиправительственное"));
            _prisoners.Add(new Prisoner("Марков Лаврентий Мартынович", "Разбой"));
            _prisoners.Add(new Prisoner("Баранов Лев Макарович", "Хулиганство"));
        }
    }

    class Prisoner
    {
        private string _fullName;

        public Prisoner(string fullName, string crime)
        {
            _fullName = fullName;
            Crime = crime;
        }

        public string Crime { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя: {_fullName}. Вид преступления - {Crime}.");
        }
    }
}