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
        private static Random s_random = new Random();

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
        private FishCreator _fishFactory;
        private List<Fish> _fishes;
        private Aquarium _aquarium;

        public Aquarist(int initialCountOfFish, int maximumFishCount)
        {
            _fishes = new List<Fish>();
            _fishFactory = new FishCreator();
            _aquarium = new Aquarium(maximumFishCount);

            CreateInitialFish(initialCountOfFish);
        }

        public void Work()
        {
            const string CommandAddFish = "1";
            const string CommandRemove = "2";
            const string CommandEndYear = "3";

            while (_fishes.Count > 0)
            {
                ShowFishes();
                Console.WriteLine();

                Console.WriteLine($"Вы можете:" +
                    $"\n{CommandAddFish} - Добавить рыбку;" +
                    $"\n{CommandRemove} - Убрать рыбку;" +
                    $"\n{CommandEndYear} - Прожить год.");

                switch (UserUtils.GetUserInput("Ваша команда"))
                {
                    case CommandAddFish:
                        if (TryAddFish())
                            Console.WriteLine("Рыбка добавлена");
                        else
                            Console.WriteLine("Не удалось добавить рыбку.");
                            break;

                    case CommandRemove:
                        if (TryRemoveFish())
                            Console.WriteLine("Рыбка удалена.");
                        else
                            Console.WriteLine("Не удалось убрать рыбку.");
                            break;

                    case CommandEndYear:
                        BecomeYearOlder();
                        break;

                    default:
                        Console.WriteLine("Некорректный ввод.");
                        break;
                }

                UserUtils.CleanConsole();
            }
        }

        private bool TryRemoveFish()
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

            return isRemove;
        }

        private bool TryCreateFish(int age)
        {
            bool isCreate = false;

            if (int.TryParse(UserUtils.GetUserInput("Какой максимальный возраст рыбы"), out int maximumFishAge))
            {
                if (maximumFishAge > age && maximumFishAge >= 0)
                {
                    _fishes.Add(_fishFactory.Create(age, maximumFishAge));
                    isCreate = true;
                }
            }

            return isCreate;
        }

        private bool TryAddFish()
        {
            if (_fishes.Count < _aquarium.MaximumFishCount)
            {
                if (int.TryParse(UserUtils.GetUserInput("Введите возраст рыбы"), out int userInput))
                {
                    return TryCreateFish(userInput);
                }
                else
                {
                    Console.WriteLine("Введены некорректные данные возраста рыбы.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("В аквариуме нет места для новой рыбы.");
                return false;
            }
        }

        private void ShowFishes()
        {
            for (int i = 0; i < _fishes.Count; i++)
            {
                Console.Write(i + 1 + " - ");
                _fishes[i].ShowInfo();
            }
        }

        private void CreateInitialFish(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _fishes.Add(_fishFactory.Create(0, 15));
            }
        }

        private void BecomeYearOlder()
        {
            foreach (Fish fish in _fishes)
            {
                if (fish.IsLive)
                    fish.GrowingOld();
            }
        }
    }

    class Aquarium
    {

        public Aquarium(int maximumFishCount)
        {   
            MaximumFishCount = maximumFishCount;
        }

        public int MaximumFishCount { get; private set; }
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

        public void GrowingOld()
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