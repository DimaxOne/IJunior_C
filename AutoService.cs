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
        private Queue<Client> _clients;
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
            _clients = new Queue<Client>();

            FillQueue();
        }

        public void Work()
        {
            while (_clients.Count > 0)
            {
                Console.WriteLine($"Сейчас в кассе {_money} рублей.\n");
                Car car = _clients.Dequeue().Car;
                car.ShowParts();

                Console.WriteLine("Машина принята в ремонт. Неисправные детали:");

                List<Part> _brokenParts = GetBrokenParts(car);
                ShowBrokenParts(_brokenParts);

                if (TryStartRepairs())
                {
                    for (int i = _brokenParts.Count; i > 0; i--)
                    {
                        Console.WriteLine($"Следующая деталь для замены {_brokenParts[i - 1].Name}.");

                        if (TryRepair(_brokenParts, car) == false)
                        {
                            PayPenalty(_brokenParts.Count);
                            break;
                        }
                    }
                }
                else
                {
                    _money += _fixedPenalty;
                    Console.WriteLine("Вы отказались от начала ремонта и заплатили фиксированный штраф.");
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
                _clients.Enqueue(new Client());
            }
        }

        private List<Part> GetBrokenParts(Car car)
        {
            List<Part> _parts = car.GetParts();
            List<Part> _brokenParts = new List<Part>();

            for (int i = 0; i < _parts.Count; i++)
            {
                if (_parts[i].IsBroken)
                    _brokenParts.Add(_parts[i]);
            }

            return _brokenParts;
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

            Console.WriteLine($"Согласны ли вы начать ремонт автомобиля?" +
                $"\n{CommandRepair} - Да;" +
                $"\n{CommandRefuseRepairs} - Отказаться (с уплатой штрафа).");

            if (GetUserCommand(CommandRepair, CommandRefuseRepairs) == CommandRepair)
                return true;

            return false;
        }

        private bool TryRepair(List<Part> brokenParts, Car car)
        {
            const string CommandAgree = "1";
            const string CommandRefuse = "2";

            List<Part> warehouseParts = _partsWarehouse.GetParts();
            Part part = brokenParts[brokenParts.Count - 1];

            int indexPartOnWarehouse;

            if (TryGetIndex(out indexPartOnWarehouse, warehouseParts, part) == false)
            {
                Console.WriteLine("Сожалеем, но эта деталь закончились на складе.");
                return true;
            }

            Console.WriteLine($"Деталь есть в наличии. Вы согласны сделать ремонт этой детали?" +
                $"\n{CommandAgree} - Да;" +
                $"\n{CommandRefuse} - Нет (с оплатой штрафа за каждую непочиненную деталь).");

            string userCommand = GetUserCommand(CommandAgree, CommandRefuse);

            if (userCommand == CommandAgree)
            {
                car.AddPart(part);
                _partsWarehouse.RemovePart(indexPartOnWarehouse);
                brokenParts.RemoveAt(brokenParts.Count - 1);
                _money += part.Price + _repairCost;
                Console.WriteLine($"Деталь заменена. Вы заплатили {part.Price} за новую деталь и {_repairCost} за ремонт.");
                return true;
            }
            else
            {
                Console.WriteLine("Вы оплачиваете штраф за каждую не отремонтированную делать.");
                return false;
            }
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

        private void PayPenalty(int partsCount)
        {
            for (int i = 0; i < partsCount; i++)
            {
                _money += _penaltyNonCompliantPart;
            }
        }
    }

    class Client
    {
        private CarFactory _carFactory;
        private Car _car;

        public Client()
        {
            _carFactory = new CarFactory();
            _car = _carFactory.Create();
        }

        public Car Car => _car;
    }

    class PartsWarehouse
    {
        private List<Part> _parts;
        private PartFactory _partFactory;
        private int _partsCount;

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

            _partsCount = UserUtils.GenerateRandomNumber(minimumPartsCount, maximumPartsCount);

            List<Part> parts = new List<Part>()
            {
                _partFactory.Create("Мотор", 10000),
                _partFactory.Create("Передняя дверь", 6000),
                _partFactory.Create("Задняя дверь", 5500),
                _partFactory.Create("Колесо", 5000),
                _partFactory.Create("Фара", 1500),
                _partFactory.Create("Стекло", 7000),
                _partFactory.Create("Зеркало", 1000),
                _partFactory.Create("Капот", 3500),
            };

            for (int i = 0; i < _partsCount; i++)
            {
                _parts.Add(parts[UserUtils.GenerateRandomNumber(0, parts.Count - 1)]);
            }
        }
    }

    class Car
    {
        private List<Part> _parts;

        public Car(List<Part> parts)
        {
            _parts = parts;

            BreakParts();
        }

        public List<Part> GetParts()
        {
            return new List<Part>(_parts);
        }

        public void AddPart(Part part)
        {
            _parts.Add(part);
        }

        public void ShowParts()
        {
            for (int i = 0; i < _parts.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {_parts[i].Name} стоимостью {_parts[i].Price} нужен ремонт - {_parts[i].IsBroken}.");
            }
        }

        private void BreakParts()
        {
            int minimumBrokenCount = 1;
            int brokenPartsCount = UserUtils.GenerateRandomNumber(minimumBrokenCount, _parts.Count);
            List<Part> _allParts = new List<Part>(_parts);

            for (int i = 0; i < brokenPartsCount; i++)
            {
                if (brokenPartsCount > 1)
                {
                    int index = UserUtils.GenerateRandomNumber(0, _allParts.Count - 1);

                    _allParts[index].Break();
                }
                else
                {
                    _allParts[0].Break();
                }
            }
        }
    }

    class Part
    {
        public Part(string name, int price, bool isBroken = false)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; private set; }
        public int Price { get; private set; }
        public bool IsBroken { get; private set; }

        public void Break()
        {
            if (IsBroken)
                return;
            else
                IsBroken = true;
        }
    }

    class PartFactory
    {
        public Part Create(string name, int price)
        {
            return new Part(name, price);
        }
    }

    class CarFactory
    {
        private PartFactory _partFactory;

        public Car Create()
        {
            return new Car(GetParts());
        }

        private List<Part> GetParts()
        {
            _partFactory = new PartFactory();

            return new List<Part>()
            {
                _partFactory.Create("Мотор", 10000),
                _partFactory.Create("Передняя дверь", 6000),
                _partFactory.Create("Задняя дверь", 5500),
                _partFactory.Create("Колесо", 5000),
                _partFactory.Create("Фара", 1500),
                _partFactory.Create("Стекло", 7000),
                _partFactory.Create("Зеркало", 1000),
                _partFactory.Create("Капот", 3500),
            };
        }
    }
}