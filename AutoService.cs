using System;
using System.Collections.Generic;

namespace AutoService
{
    class Program
    {
        static void Main(string[] args)
        {
            AutoService autoService = new AutoService();

            autoService.Work();
        }
    }

    class UserUtils
    {
        private static Random s_random = new Random();

        public static int GenerateRandomNumber(int mininimumValue, int maximumValue)
        {
            return s_random.Next(mininimumValue, maximumValue + 1);
        }

        public static string GetUserInput(string message)
        {
            Console.Write(message + ": ");

            return Console.ReadLine();
        }

        public static void CleanConsole()
        {
            Console.WriteLine("Нажмите любую кнопку для продолжения.");
            Console.ReadLine();
            Console.Clear();
        }
    }

    class AutoService
    {
        private int _money;
        private PartsWarehouse _partsWarehouse;
        private CarFactory _carFactory;
        private Queue<Car> _cars;
        private int _repairCost;
        private int _fixedPenalty;
        private int _penaltyNonCompliantPart;

        public AutoService()
        {
            _money = 0;
            _repairCost = 1000;
            _fixedPenalty = 2000;
            _penaltyNonCompliantPart = 500;
            _partsWarehouse = new PartsWarehouse();
            _cars = new Queue<Car>();
            _carFactory = new CarFactory();

            FillQueue();
        }

        public void Work()
        {
            while (_cars.Count > 0)
            {
                Console.WriteLine($"Сейчас в кассе {_money} рублей.\n");
                Car car = _cars.Dequeue();
                car.ShowParts();

                Console.WriteLine("Машина принята в ремонт. Неисправные детали:");

                List<Part> brokenParts = GetBrokenParts(car);
                ShowBrokenParts(brokenParts);

                if (TryStartRepairs() == false)
                {
                    PayPenalty(_fixedPenalty);
                    Console.WriteLine("Вы отказались от начала ремонта и заплатили фиксированный штраф.");
                    UserUtils.CleanConsole();
                    continue;
                }

                bool isRepair = true;
                const string CommandAgree = "1";
                const string CommandExit = "2";

                while (isRepair)
                {
                    Console.WriteLine($"Сейчас в кассе {_money} рублей.\n");
                    car.ShowParts();

                    Console.WriteLine($"\nВы можете выбрать деталь для замены или закончить ремонт." +
                        $"\n{CommandAgree} - Выбрать деталь для замены;" +
                        $"\n{CommandExit} - Отказаться от ремонта.");

                    string userInput = GetUserCommand(CommandAgree, CommandExit);

                    if (userInput == CommandExit)
                    {
                        List<Part> remainingBrokenParts = GetBrokenParts(car);
                        Console.WriteLine("\nВы отказались от ремонта и заплатили штраф за каждую неподчиненную деталь.");

                        for (int i = 0; i < remainingBrokenParts.Count; i++)
                        {
                            PayPenalty(_penaltyNonCompliantPart);
                        }

                        isRepair = false;
                    }
                    else
                    {
                        TryRepair(car);
                    }
                }


                UserUtils.CleanConsole();
            }

            Console.WriteLine("На сегодня машин больше нет. Автосервис закрывается.");
        }

        private void FillQueue()
        {
            int maximumClientsCount = 20;
            int minimumClientsCount = 7;

            int clientsCount = UserUtils.GenerateRandomNumber(minimumClientsCount, maximumClientsCount);

            for (int i = 0; i < clientsCount; i++)
            {
                _cars.Enqueue(_carFactory.Create());
            }
        }

        private List<Part> GetBrokenParts(Car car)
        {
            List<Part> parts = car.GetParts();
            List<Part> brokenParts = new List<Part>();

            for (int i = 0; i < parts.Count; i++)
            {
                if (parts[i].IsBroken)
                    brokenParts.Add(parts[i]);
            }

            return brokenParts;
        }

        private void ShowBrokenParts(List<Part> parts)
        {
            for (int i = 0; i < parts.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {parts[i].Name} стоимостью {parts[i].Price}.");
            }
        }

        private bool TryStartRepairs()
        {
            const string CommandRepair = "1";
            const string CommandRefuseRepairs = "2";

            Console.WriteLine($"\nСогласны ли вы начать ремонт автомобиля?" +
                $"\n{CommandRepair} - Да;" +
                $"\n{CommandRefuseRepairs} - Отказаться (с уплатой штрафа).");

            return (GetUserCommand(CommandRepair, CommandRefuseRepairs) == CommandRepair);
        }

        private void TryRepair(Car car)
        {
            List<Part> warehouseParts = _partsWarehouse.GetParts();
            string userIndex = UserUtils.GetUserInput("\nВыберите номер детали для замены");
            List<Part> carParts = car.GetParts();
            int index = -1;
            int indexPartOnWarehouse;

            if (int.TryParse(userIndex, out int userNumber) == false)
                Console.WriteLine("\nНекорректный ввод номера детали. Введите цифры.");
            else
                index = --userNumber;

            if (index < 0 || index > carParts.Count)
            {
                Console.WriteLine("\nНеверный ввод номера детали.");
                return;
            }

            if (TryGetIndex(out indexPartOnWarehouse, warehouseParts, carParts[index]) == false)
            {
                Console.WriteLine("\nСожалеем, но эта деталь закончились на складе.");
                return;
            }

            Part newPart = warehouseParts[indexPartOnWarehouse];
            _partsWarehouse.RemovePart(indexPartOnWarehouse);

            if (carParts[index].IsBroken)
            {
                _money += carParts[index].Price + _repairCost;
                Console.WriteLine($"Деталь заменена. Вы заплатили {carParts[index].Price} за новую деталь и {_repairCost} за ремонт.");
            }
            else
            {
                Console.WriteLine("Вы заменили рабочую деталь и не получили денег.");
            }

            car.ReplacePart(index, newPart);
        }

        private string GetUserCommand(string firstCommand, string secondCommand)
        {
            string userInput = null;

            userInput = UserUtils.GetUserInput("Ваш выбор");

            while (userInput != firstCommand && userInput != secondCommand)
            {
                Console.WriteLine("Некоррректная команда.");
                userInput = UserUtils.GetUserInput("Введите корректную команду");
            }

            return userInput;
        }

        private bool TryGetIndex(out int index, List<Part> parts, Part brokenPart)
        {
            for (int i = 0; i < parts.Count; i++)
            {
                if (parts[i].Name == brokenPart.Name)
                {
                    index = i;
                    return true;
                }
            }

            index = -1;
            return false;
        }

        private void PayPenalty(int penalty)
        {
            if (_money - penalty >= 0)
                _money -= penalty;
            else
                _money = 0;
        }
    }

    class PartsWarehouse
    {
        private List<Part> _parts;
        private PartFactory _partFactory;

        public PartsWarehouse()
        {
            _partFactory = new PartFactory();
            _parts = new List<Part>();

            Fill();
        }

        public List<Part> GetParts()
        {
            return new List<Part>(_parts);
        }

        public void RemovePart(int index)
        {
            if (_parts.Count > 0)
                _parts.RemoveAt(index);
        }

        private void Fill()
        {
            int maximumPartsCount = 60;
            int minimumPartsCount = 30;
            int partsCount = UserUtils.GenerateRandomNumber(minimumPartsCount, maximumPartsCount);

            List<Part> parts = _partFactory.GetParts();

            for (int i = 0; i < partsCount; i++)
            {
                int index = UserUtils.GenerateRandomNumber(0, parts.Count - 1);

                _parts.Add(new Part(parts[index].Name, parts[index].Price));
            }
        }
    }

    class Car
    {
        private List<Part> _parts;

        public Car(List<Part> parts)
        {
            _parts = new List<Part>();

            AddParts(parts);
            BreakParts();
        }

        public List<Part> GetParts()
        {
            return new List<Part>(_parts);
        }

        public void ReplacePart(int index, Part part)
        {
            if (_parts.Count > 0)
            {
                _parts.RemoveAt(index);
                _parts.Insert(index, part);
            }
        }

        public void ShowParts()
        {
            for (int i = 0; i < _parts.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {_parts[i].Name} стоимостью {_parts[i].Price} нужен ремонт - {_parts[i].IsBroken}.");
            }
        }

        private void AddParts(List<Part> parts)
        {
            for (int i = 0; i < parts.Count; i++)
            {
                _parts.Add(new Part(parts[i].Name, parts[i].Price));
            }
        }

        private void BreakParts()
        {
            List<Part> partsToBroke = new List<Part>(_parts);
            int minimumBrokenCount = 1;
            int brokenPartsCount = UserUtils.GenerateRandomNumber(minimumBrokenCount, _parts.Count);

            for (int i = 0; i < brokenPartsCount; i++)
            {
                int index = UserUtils.GenerateRandomNumber(0, partsToBroke.Count - 1);
                partsToBroke[index].Break();
                partsToBroke.RemoveAt(index);
            }
        }
    }

    class Part
    {
        public Part(string name, int price)
        {
            Name = name;
            Price = price;
            IsBroken = false;
        }

        public string Name { get; private set; }
        public int Price { get; private set; }
        public bool IsBroken { get; private set; }

        public void Break()
        {
            if (IsBroken == false)
                IsBroken = true;
        }
    }

    class PartFactory
    {
        private List<Part> _parts;

        public PartFactory()
        {
            _parts = new List<Part>
            {
                new Part("Мотор", 10000),
                new Part("Передняя дверь", 6000),
                new Part("Задняя дверь", 5500),
                new Part("Колесо", 5000),
                new Part("Фара", 1500),
                new Part("Стекло", 7000),
                new Part("Зеркало", 1000),
                new Part("Капот", 3500),
            };
        }

        public List<Part> GetParts()
        {
            return new List<Part>(_parts);
        }
    }

    class CarFactory
    {
        private PartFactory _partFactory;

        public CarFactory()
        {
            _partFactory = new PartFactory();
        }

        public Car Create()
        {
            return new Car(_partFactory.GetParts());
        }
    }
}