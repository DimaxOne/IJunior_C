using System;
using System.Collections.Generic;

namespace Aquarium
{
    class Program
    {
        static void Main(string[] args)
        {
            Aquarium aquarium = new Aquarium(25, 5);

            aquarium.Work();
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

    class Aquarium
    {
        private FishCreator _fishFactory;
        private List<Fish> _fishes;
        private int _maximumFishCount;

        public Aquarium(int maximumFishCount, int initialCountOfFish)
        {
            _fishes = new List<Fish>();
            _fishFactory = new FishCreator();
            _maximumFishCount = maximumFishCount;

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
                    $"\n{CommandEndYear} - прожить год.");

                switch (UserUtils.GetUserInput("Ваша команда"))
                {
                    case CommandAddFish:
                        if(TryAddFish())
                            Console.WriteLine("Рыбка добавлена");
                        break;

                    case CommandRemove:
                        if(TryRemoveFish())
                            Console.WriteLine("Рыбка удалена.");
                        break;

                    case CommandEndYear:
                        EndYear();
                        break;

                    default:
                        Console.WriteLine("Некорректный ввод.");
                        break;
                }

                UserUtils.CleanConsole();
            }
        }

        private bool TryAddFish()
        {
            if (_fishes.Count < _maximumFishCount)
            {
                int userInput;

                if (TryGetNumber(out userInput, "Введите возраст рыбы"))
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

        private bool TryRemoveFish()
        {
            int fishIndex;

            if (TryGetNumber(out fishIndex, "Введите номер рыбки для удаления из аквариума"))
            {
                fishIndex--;

                if (fishIndex >= 0 && fishIndex < _fishes.Count)
                {
                    _fishes.RemoveAt(fishIndex);
                    return true;
                }
                else
                {
                    Console.WriteLine("Некорректный индекс рыбки.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод данных.");
                return false;
            }
        }

        private bool TryGetNumber(out int number, string message)
        {
            return int.TryParse(UserUtils.GetUserInput(message), out number);
        }

        private bool TryCreateFish(int age)
        {
            int maximumFishAge;

            if (TryGetNumber(out maximumFishAge, "Какой максимальный возраст рыбы"))
            {
                if (maximumFishAge > age && maximumFishAge >= 0)
                {
                    _fishes.Add(_fishFactory.Create(age, maximumFishAge));
                    return true;
                }
                else
                {
                    Console.WriteLine("Некорректный возраст рыбы.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Введены некорректные данные максимального возраста рыбы.");
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

        private void EndYear()
        {
            foreach (Fish fish in _fishes)
            {
                fish.TryGrowingOld();
            }
        }
    }

    class Fish
    {
        private int _age;
        private int _maximumAge;
        private bool _isLive;

        public Fish(int age, int maximumAge)
        {
            _age = age;
            _maximumAge = maximumAge;
            _isLive = true;
        }

        public bool TryGrowingOld()
        {
            if (_isLive && _age < _maximumAge)
            {
                _age++;
                return true;
            }
            else
            {
                _isLive = false;
                return false;
            }
        }

        public void ShowInfo()
        {
            if (_isLive)
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