using System;
using System.Collections.Generic;

namespace PassengerTrainConfigurator
{
    class Program
    {
        static void Main(string[] args)
        {
            Dispatcher dispatcher = new Dispatcher();

            dispatcher.Work();
        }
    }

    static class UserUtils
    {
        public static string GetUserInput(string message)
        {
            Console.Write(message + ": ");

            return Console.ReadLine();
        }
    }

    class Dispatcher
    {
        private List<Train> _trains = new List<Train>();
        private bool _isWork = true;

        public void Work()
        {
            const string CommandCreateTrain = "1";
            const string CommandExit = "2";

            string userInput;

            while (_isWork)
            {
                ShowInfo();

                Console.Write($"Добро пожаловать. Вам доступно:" +
                    $"\n{CommandCreateTrain} - создать поезд;" +
                    $"\n{CommandExit} - выход;\n");

                userInput = UserUtils.GetUserInput("Ваша команда");

                if (userInput == CommandCreateTrain)
                {
                    _trains.Add(new Train());
                }
                else if (userInput == CommandExit)
                {
                    _isWork = false;
                }
                else
                {
                    Console.WriteLine("Введена некорректная команда.");
                }

                Console.WriteLine("Нажмите любую кнопку для продолжения.");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private void ShowInfo()
        {
            foreach (Train train in _trains)
            {
                train.ShowInfo();
            }
        }
    }

    class Train
    {
        private Random _random = new Random();
        private List<Carriage> _carriages = new List<Carriage>();

        private Direction _direction;
        private int _tickets;

        public Train()
        {
            _tickets = GetRandomTicketsCount(_random);
            _direction = CreateDirection();

            AddCarriage(_tickets);
        }

        public void ShowInfo()
        {
            _direction.ShowWay();
            Console.Write($" Общее количество пассажиров: {_tickets}. ");

            for (int i = 0; i < _carriages.Count; i++)
            {
                Console.Write($"Вагон №{i + 1} количество пассажиров {_carriages[i].CurrentPassengers} из {_carriages[i].MaximumPassengers}| ");
            }

            Console.WriteLine("\n");
        }

        private void AddCarriage(int tickets)
        {
            int lastCarriageIndex;
            int remainingPassengers;

            while (tickets > 0)
            {
                _carriages.Add(new Carriage(_random));

                lastCarriageIndex = _carriages.Count - 1;
                remainingPassengers = tickets - _carriages[lastCarriageIndex].MaximumPassengers;

                if (remainingPassengers <= 0)
                    _carriages[lastCarriageIndex].IndicateCurrentPassengers(tickets);
                else
                    _carriages[lastCarriageIndex].IndicateCurrentPassengers(_carriages[lastCarriageIndex].MaximumPassengers);

                tickets -= _carriages[lastCarriageIndex].MaximumPassengers;
            }
        }

        private int GetRandomTicketsCount(Random random)
        {
            int maximumRandomValue = 150;
            int minimumRandomValue = 80;

            return random.Next(minimumRandomValue, maximumRandomValue + 1);
        }

        private Direction CreateDirection()
        {
            string departurePoint = UserUtils.GetUserInput("Введите пункт отправления поезда");
            string arrivalPoint = UserUtils.GetUserInput("Введите пункт прибытия поезда");

            return new Direction(departurePoint, arrivalPoint);
        }
    }

    class Carriage
    {
        public Carriage(Random random)
        {
            MaximumPassengers = GetRandomMaximumPassengers(random);
        }

        public int MaximumPassengers { get; private set; }
        public int CurrentPassengers { get; private set; }

        private int GetRandomMaximumPassengers(Random random)
        {
            int maximumRandomValue = 42;
            int minimumRandomValue = 20;

            return random.Next(minimumRandomValue, maximumRandomValue + 1);
        }

        public void IndicateCurrentPassengers(int passengers)
        {
            if (passengers > 0 && passengers <= MaximumPassengers)
                CurrentPassengers = passengers;
            else
                Console.WriteLine("Недопустимое количество пассажиров.");
        }
    }

    class Direction
    {
        private string _departurePoint;
        private string _arrivalPoint;

        public Direction(string departurePoint, string arrivalPoint)
        {
            _departurePoint = departurePoint;
            _arrivalPoint = arrivalPoint;
        }

        public void ShowWay()
        {
            Console.Write($"{_departurePoint} - {_arrivalPoint} : ");
        }
    }
}