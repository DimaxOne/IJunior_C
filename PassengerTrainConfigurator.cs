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

    class Dispatcher
    {
        private Dictionary<Direction, Train> _completeTrains = new Dictionary<Direction, Train>();
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

                userInput = GetUserInput("Ваша команда");

                if(userInput == CommandCreateTrain)
                {
                    CreateTrain();
                }
                else if(userInput == CommandExit)
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

        private void CreateTrain()
        {
            Random random = new Random();
            string departurePoint = GetUserInput("Введите пункт отправления поезда");
            string arrivalPoint = GetUserInput("Введите пункт прибытия поезда");

            Direction direction = new Direction(departurePoint, arrivalPoint, random);

            _completeTrains.Add(direction, new Train(direction.Tickets));
        }

        private string GetUserInput(string message)
        {
            Console.Write(message + ": ");

            return Console.ReadLine();
        }

        private void ShowInfo()
        {
            foreach (var completeTrain in _completeTrains)
            {
                completeTrain.Key.ShowWay();
                Console.Write("Общее количество пассажиров: " + completeTrain.Key.Tickets + ". ");
                completeTrain.Value.ShowInfo();
                Console.WriteLine();
            }
        }
    }

    class Train
    {
        private Random _random = new Random();
        private List<Carriage> _carriages = new List<Carriage>();

        public Train(int tickets)
        {
            while (tickets > 0)
            {
                _carriages.Add(new Carriage(_random));

                int remainingPassengers = tickets - _carriages[_carriages.Count - 1].MaximumPassengers;

                if (remainingPassengers <= 0)
                    _carriages[_carriages.Count - 1].IndicateCurrentPassengers(tickets);
                else
                    _carriages[_carriages.Count - 1].IndicateCurrentPassengers(_carriages[_carriages.Count - 1].MaximumPassengers);

                tickets -= _carriages[_carriages.Count - 1].MaximumPassengers;
            }
        }

        public void ShowInfo()
        {
            for (int i = 0; i < _carriages.Count; i++)
            {
                Console.Write($"Вагон №{i + 1} количество пассажиров {_carriages[i].CurrentPassengers} из {_carriages[i].MaximumPassengers}| ");
            }

            Console.WriteLine();
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

        public Direction(string departurePoint, string arrivalPoint, Random random)
        {
            _departurePoint = departurePoint;
            _arrivalPoint = arrivalPoint;
            Tickets = GetRandomTicketsCount(random);
        }

        public int Tickets { get; private set; }

        public void ShowWay()
        {
            Console.Write($"{_departurePoint} - {_arrivalPoint} : ");
        }

        private int GetRandomTicketsCount(Random random)
        {
            int maximumRandomValue = 150;
            int minimumRandomValue = 80;

            return random.Next(minimumRandomValue, maximumRandomValue + 1);
        }
    }
}