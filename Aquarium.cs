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
            while (_aquarium.FishCount > 0)
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
                        TryAddFish();
                        break;

                    case CommandRemove:
                        TryRemoveFish();
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

        private void TryAddFish()
        {
            int age;
            int maximumAge;

            if (_aquarium.FishCount == _aquarium.MaximumFishCount)
            {
                Console.WriteLine("Аквариум полон.");
                return;
            }

            if (TryGetAge("Введите возраст рыбы", out age) == false)
                return;

            if (TryGetAge("Введите максимальный возраст рыбы", out maximumAge) == false)
                return;

            if (maximumAge > age && maximumAge >= 0)
                _aquarium.AddFish(new Fish(age, maximumAge));
            else
                Console.WriteLine("Такие рыбки столько не проживают.");
        }

        private bool TryGetAge(string message, out int age)
        {
            if (int.TryParse(UserUtils.GetUserInput(message), out int userInput))
            {
                if (userInput >= 0)
                {
                    age = userInput;
                    return true;
                }
                else
                {
                    Console.WriteLine("Возраст не может быть отрицательным");
                }
            }
            else
            {
                Console.WriteLine("Введены некорректные данные.");
            }

            age = -1;
            return false;
        }

        private void TryRemoveFish()
        {
            if (int.TryParse(UserUtils.GetUserInput("Введите номер рыбки для удаления из аквариума"), out int fishIndex))
            {
                fishIndex--;

                if (fishIndex >= 0 && fishIndex < _aquarium.FishCount)
                {
                    _aquarium.RemoveFish(fishIndex);
                    Console.WriteLine("Рыбка удалена.");
                    return;
                }
                else
                {
                    Console.WriteLine("Рыбки с таким номером нет.");
                }
            }
            else
            {
                Console.WriteLine("Введены некоректные данные.");
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

        public void AddFish(Fish fish)
        {
            _fishes.Add(fish);
        }

        public void RemoveFish(int index)
        {
            _fishes.RemoveAt(index);
        }

        public void ShowFishes()
        {
            for (int i = 0; i < _fishes.Count; i++)
            {
                Console.Write(i + 1 + " - ");
                _fishes[i].ShowInfo();
            }
        }

        public void CreateInitialFish(FishCreator fishCreator, int count)
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
                if (fish.IsAllive)
                    fish.GrowOlder();
            }
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
        }

        public bool IsAllive => _age <= _maximumAge;

        public void GrowOlder()
        {
            _age++;
        }

        public void ShowInfo()
        {
            if (IsAllive)
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