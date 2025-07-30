using System;
using System.Collections.Generic;
using System.Linq;

namespace TopPlayersServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();

            server.ShowPlayers();
            server.ShowTopPlayers();
        }
    }

    class Server
    {
        private int _countTopPlayers = 3;
        private List<Player> _players;

        public Server()
        {
            _players = new List<Player>();

            FillPlayers();
        }

        public void ShowTopPlayers()
        {
            Console.WriteLine("\nТоп игроков по уровню:");
            ShowPlayersInfo(GetTopPlayersByLevel());

            Console.WriteLine("\nТоп игроков по силе:");
            ShowPlayersInfo(GetTopPlayersByPower());
        }

        public void ShowPlayers()
        {
            ShowPlayersInfo(_players);
        }

        private List<Player> GetTopPlayersByLevel()
        {
            return _players.OrderByDescending(player => player.Level).Take(_countTopPlayers).ToList();
        }

        private List<Player> GetTopPlayersByPower()
        {
            return _players.OrderByDescending(player => player.Power).Take(_countTopPlayers).ToList();
        }

        private void ShowPlayersInfo(List<Player> players)
        {
            foreach (Player player in players)
            {
                player.ShowInfo();
            }
        }

        private void FillPlayers()
        {
            _players.Add(new Player("Viktor", 14, 23));
            _players.Add(new Player("John", 57, 217));
            _players.Add(new Player("Semen", 65, 273));
            _players.Add(new Player("Snikers", 18, 573));
            _players.Add(new Player("Nuts", 74, 435));
            _players.Add(new Player("Seven", 41, 838));
            _players.Add(new Player("Ronny", 80, 3783));
            _players.Add(new Player("Maybah", 78, 272));
            _players.Add(new Player("Inkognito", 15, 836));
            _players.Add(new Player("MyBad", 34, 737));
            _players.Add(new Player("Samurai", 99, 2126));
            _players.Add(new Player("Serega", 77, 357));
            _players.Add(new Player("Vampire", 81, 1057));
            _players.Add(new Player("Wolf", 39, 383));
            _players.Add(new Player("Franklin", 62, 278));
        }
    }

    class Player
    {
        public Player(string name, int level, int power)
        {
            Name = name;
            Level = level;
            Power = power;
        }

        public string Name { get; private set; }
        public int Level { get; private set; }
        public int Power { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Игрок {Name} имеет {Level} уровень и силу {Power}.");
        }
    }
}