using System;
using System.Collections.Generic;

namespace PlayerDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();

            database.Work();
        }
    }

    class Database
    {
        private const string CommandAddPlayer = "1";
        private const string CommandBanPlayer = "2";
        private const string CommandUnbanPlayer = "3";
        private const string CommandRemovePlayer = "4";
        private const string CommandExit = "5";

        private Dictionary<int, Player> _players = new Dictionary<int, Player>();
        private int _lastPlayerId = 1;
        private bool _isWork = true;

        public void Work()
        {
            while (_isWork)
            {
                ShowPlayersInfo();
                Console.WriteLine();
                ShowMenu();

                switch(GetUserInput("Ваша команда"))
                {
                    case CommandAddPlayer:
                        AddPlayer();
                        break;

                    case CommandBanPlayer:
                        BanPlayer();
                        break;

                    case CommandUnbanPlayer:
                        UnbanPlayer();
                        break;

                    case CommandRemovePlayer:
                        RemovePlayer();
                        break;

                    case CommandExit:
                        _isWork = false;
                        break;

                    default:
                        Console.WriteLine("Введена некорректная команда.");
                        break;
                }

                Console.WriteLine("Нажмите любую кнопку для продолжения.");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private void AddPlayer()
        {
            string name = GetUserInput("Введите имя игрока");

            if(int.TryParse(GetUserInput("Введите уровень игрока"), out int level))
            {
                _players.Add(_lastPlayerId, new Player(_lastPlayerId, name, level));
                _lastPlayerId++;
            }
            else
            {
                Console.WriteLine("Указан некорректный уровень.");
            }
        }

        private void BanPlayer()
        {
            string userInput = GetUserInput("Введите id игрока, которого нужно забанить");
            int playerIndex;

            if (TryGetPlayer(_players, userInput, out playerIndex))
                _players[playerIndex].Ban();
        }

        private void UnbanPlayer()
        {
            string userInput = GetUserInput("Введите id игрока, которого нужно разбанить");
            int playerIndex;

            if (TryGetPlayer(_players, userInput, out playerIndex))
                _players[playerIndex].Unban();
        }

        private void RemovePlayer()
        {
            string userInput = GetUserInput("Введите id игрока, которого нужно удалить");
            int playerIndex;

            if (TryGetPlayer(_players, userInput, out playerIndex))
                _players.Remove(playerIndex);
        }

        private bool TryGetPlayer(Dictionary<int, Player> players, string userInput, out int playerIndex)
        {
            bool isPlayer = false;
            int playerKey = 0;

            if (int.TryParse(userInput, out int userNumber))
            {
                foreach (var player in players)
                {
                    if (player.Value.Id == userNumber)
                    {
                        isPlayer = true;
                        playerKey = player.Key;
                    }
                }

                if (isPlayer == false)
                    Console.WriteLine("Такого id нет.");
            }
            else
            {
                Console.WriteLine("Некорректный ввод.");
            }

            playerIndex = playerKey;

            return isPlayer;
        }

        private void ShowMenu()
        {
            Console.WriteLine($"Управление базой игроков сервера:" +
                    $"\n{CommandAddPlayer} - Добавить игрока;" +
                    $"\n{CommandBanPlayer} - Забанить игрока;" +
                    $"\n{CommandUnbanPlayer} - Разбанить игрока;" +
                    $"\n{CommandRemovePlayer} - Удалить игрока;" +
                    $"\n{CommandExit} - Выход.");
        }

        private void ShowPlayersInfo()
        {
            foreach (Player player in _players.Values)
            {
                Console.WriteLine($"Id {player.Id} - {player.NickName}, уровень {player.Level}, статус бана {player.IsBanned}.");
            }
        }

        private string GetUserInput(string message)
        {
            Console.Write(message + ": ");

            return Console.ReadLine();
        }
    }

    class Player
    {
        public Player(int id, string nickName, int level)
        {
            Id = id;
            NickName = nickName;
            Level = level;
            IsBanned = false;
        }

        public bool IsBanned { get; private set; }
        public int Id { get; private set; }
        public int Level { get; private set; }
        public string NickName { get; private set; }

        public void Ban()
        {
            IsBanned = true;
        }

        public void Unban()
        {
            IsBanned = false;
        }
    }
}