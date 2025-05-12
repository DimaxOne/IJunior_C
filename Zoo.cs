using System;
using System.Collections.Generic;

namespace Zoo
{
    class Program
    {
        static void Main(string[] args)
        {
            Zoo zoo = new Zoo();

            zoo.Work();
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

    class Zoo
    {
        private ZooKeeper _zooKeeper;
        private List<Aviary> _aviaries;

        public Zoo()
        {
            _zooKeeper = new ZooKeeper();
            _aviaries = new List<Aviary>();

            GetAviary("Вольер с обезьянами", "Обезьяна", "Крик");
            GetAviary("Вольер с медведями", "Медведь", "Рык");
            GetAviary("Вольер со змеями", "Змея", "Шипение");
            GetAviary("Вольер с воронами", "Ворона", "Карканье");
            GetAviary("Вольер с носорогами", "Носорог", "Молчаливый киллер");
        }

        public void Work()
        {
            while (_aviaries.Count > 0)
            {
                Console.WriteLine($"Добро пожаловать в зоопарк! У нас есть:");
                ShowAllAviaryDescription();

                TryShowAviary(UserUtils.GetUserInput("Куда вы хотите пройти"));

                UserUtils.CleanConsole();
            }
        }

        private void GetAviary(string description, string name, string sound)
        {
            _aviaries.Add(_zooKeeper.CreateAviary(description));
            _zooKeeper.FillAviary(_aviaries[_aviaries.Count - 1], name, sound);
        }

        private void ShowAllAviaryDescription()
        {
            for (int i = 0; i < _aviaries.Count; i++)
            {
                Console.WriteLine(i + 1 + $" - {_aviaries[i].Description}");
            }
        }

        private void TryShowAviary(string userInput)
        {
            int index;

            if (int.TryParse(userInput, out index) == false)
            {
                Console.WriteLine("Некорректный ввод номера.");
                return;
            }

            if(index < 0 || index > _aviaries.Count)
            {
                Console.WriteLine("Такого вольера нет.");
                return;
            }

            index--;

            _aviaries[index].ShowInfo();
        }
    }

    class ZooKeeper
    {
        private AnimalCreator _animalCreator;

        public ZooKeeper()
        {
            _animalCreator = new AnimalCreator();
        }

        public void FillAviary(Aviary _aviary, string name, string sound)
        {
            int maximumAnimalCount = 10;
            int minimumAnimalCount = 3;
            int animalCount = UserUtils.GenerateRandomNumber(minimumAnimalCount, maximumAnimalCount);

            for (int i = 0; i < animalCount; i++)
            {
                _aviary.AddAnimal(CreateAnimal(name, sound));
            }
        }

        public Aviary CreateAviary(string description)
        {
            return new Aviary(description);
        }

        private Animal CreateAnimal(string name, string sound)
        {
            return _animalCreator.Create(name, sound);
        }
    }

    class Aviary
    {
        private List<Animal> _animals = new List<Animal>();

        public Aviary(string description)
        {
            Description = description;
        }

        public string Description { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine(Description);
            Console.WriteLine($"Количество животных: {_animals.Count}");

            foreach (Animal animal in _animals)
            {
                animal.ShowInfo();
            }
        }

        public void AddAnimal(Animal animal)
        {
            _animals.Add(animal);
        }
    }

    class AnimalCreator
    {
        public Animal Create(string name, string sound)
        {
            return new Animal(name, sound);
        }
    }

    class Animal
    {
        private string _name;
        private string _gender;
        private string _sound;

        public Animal(string name, string sound)
        {
            _name = name;
            _sound = sound;
            _gender = GetGender();
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{_name}, {_gender} пол, издающее звук {_sound}");
        }

        private string GetGender()
        {
            string[] genders = { "мужской", "женский" };

            return genders[UserUtils.GenerateRandomNumber(0, genders.Length - 1)];
        }
    }
}