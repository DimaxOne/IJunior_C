using System;
using System.Collections.Generic;

namespace Aquarium
{
    class Program
    {
        static void Main(string[] args)
        {
            Aquarist aquarist = new Aquarist(5, 15);

            aquarist.Work();
        }
    }

    class UserUtils
    {
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

    class Aquarist
    {
        private FishCreator _fishCreator;
        private Aquarium _aquarium;

        public Aquarist(int initialCountOfFish, int maximumFishCount)
        {
            _fishCreator = new FishCreator();
            _aquarium = new Aquarium(maximumFishCount);

            _aquarium.CreateInitialFish(_fishCreator, initialCountOfFish);
        }
        
        public void Work()
        {
            while(_aquarium.FishCount > 0)
            {
                const string CommandAddFish = "1";
                const string CommandRemove = "2";
                const string CommandEndYear = "3";

                _aquarium.ShowFishes();
                Console.WriteLine();

                Console.WriteLine($"Вы можете:" +
                    $"\n{CommandAddFish} - Добавить рыбку;" +
                    $"\n{CommandRemove} - Убрать рыбку;" +
                    $"\n{CommandEndYear} - Прожить год.");

                switch (UserUtils.GetUserInput("Ваша команда"))
                {
                    case CommandAddFish:
                        _aquarium.TryAddFish(_fishCreator);
                        break;

                    case CommandRemove:
                        _aquarium.TryRemoveFish();
                        break;

                    case CommandEndYear:
                        _aquarium.BecomeYearOlder();
                        break;

                    default:
                        Console.WriteLine("Некорректный ввод.");
                        break;
                }

                UserUtils.CleanConsole();
            }
        }
    }

    class Aquarium
    {
        private List<Fish> _fishes;

        public Aquarium(int maximumFishCount)
        {
            MaximumFishCount = maximumFishCount;
            _fishes = new List<Fish>();
        }

        public int MaximumFishCount { get; private set; }
        public int FishCount => _fishes.Count;

        public void TryRemoveFish()
        {
            bool isRemove = false;

            if (int.TryParse(UserUtils.GetUserInput("Введите номер рыбки для удаления из аквариума"), out int fishIndex))
            {
                fishIndex--;

                if (fishIndex >= 0 && fishIndex < _fishes.Count)
                {
                    _fishes.RemoveAt(fishIndex);
                    isRemove = true;
                }
            }

            if (isRemove)
                Console.WriteLine("Рыбка удалена.");
            else
                Console.WriteLine("Не удалось убрать рыбку.");
        }

        public void TryAddFish(FishCreator fishCreator)
        {
            if (_fishes.Count < MaximumFishCount)
            {
                if (int.TryParse(UserUtils.GetUserInput("Введите возраст рыбы"), out int userInput))
                {
                    if (TryCreateFish(fishCreator, userInput))
                        Console.WriteLine("Рыбка добавлена");
                    else
                        Console.WriteLine("Не удалось добавить рыбку.");
                }
                else
                {
                    Console.WriteLine("Введены некорректные данные возраста рыбы.");
                }
            }
            else
            {
                Console.WriteLine("В аквариуме нет места для новой рыбы.");
            }
        }

        public void ShowFishes()
        {
            for (int i = 0; i < _fishes.Count; i++)
            {
                Console.Write(i + 1 + " - ");
                _fishes[i].ShowInfo();
            }
        }

        public void CreateInitialFish(FishCreator fishCreator ,int count)
        {
            int ageInitialFish = 0;
            int maximumAgeInitialFish = 15;

            for (int i = 0; i < count; i++)
            {
                _fishes.Add(fishCreator.Create(ageInitialFish, maximumAgeInitialFish));
            }
        }

        public void BecomeYearOlder()
        {
            foreach (Fish fish in _fishes)
            {
                if (fish.IsLive)
                    fish.GrowOlder();
            }
        }

        private bool TryCreateFish(FishCreator fishCreator, int age)
        {
            bool isCreate = false;

            if (age < 0)
            {
                Console.WriteLine("Указан отрицательный возраст");
            }
            else
            {
                if (int.TryParse(UserUtils.GetUserInput("Какой максимальный возраст рыбы"), out int maximumFishAge))
                {
                    if (maximumFishAge > age && maximumFishAge >= 0)
                    {
                        _fishes.Add(fishCreator.Create(age, maximumFishAge));
                        isCreate = true;
                    }
                }
            }

            return isCreate;
        }
    }

    class Fish
    {
        private int _age;
        private int _maximumAge;

        public Fish(int age, int maximumAge)
        {
            _age = age;
            _maximumAge = maximumAge;
            IsLive = true;
        }

        public bool IsLive { get; private set; }

        public void GrowOlder()
        {
            if (IsLive && _age < _maximumAge)
                _age++;
            else
                IsLive = false;
        }

        public void ShowInfo()
        {
            if (IsLive)
                Console.WriteLine($"рыбке сейчас {_age} лет. Обычные такие рыбки доживают до {_maximumAge} лет.");
            else
                Console.WriteLine("рыбка покинула нас.");
        }
    }

    class FishCreator
    {
        public Fish Create(int age, int maximumAge)
        {
            return new Fish(age, maximumAge);
        }
    }
}