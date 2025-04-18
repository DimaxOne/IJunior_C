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
        const string CommandAddPlayer = "1";
        const string CommandBanPlayer = "2";
        const string CommandUnbanPlayer = "3";
        const string CommandRemovePlayer = "4";
        const string CommandExit = "5";

        private Dictionary<int, Player> players = new Dictionary<int, Player>();
        private int _playersId = 1;
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
                players.Add(_playersId, new Player(_playersId, name, level));
                _playersId++;
            }
            else
            {
                Console.WriteLine("Указан некорректный уровень.");
            }
        }

        private void BanPlayer()
        {
            bool isPlayer = false;
            
            if(int.TryParse(GetUserInput("Введите id игрока, которого нужно забанить"), out int userInput))
            {
                foreach (Player player in players.Values)
                {
                    if(player.Id == userInput)
                    {
                        isPlayer = true;
                        player.Ban();
                        Console.WriteLine("Игрок забанен.");
                    }    
                }

                if(isPlayer == false)
                    Console.WriteLine("Такого id нет.");
            }
            else
            {
                Console.WriteLine("Некорректный ввод.");
            }
        }

        private void UnbanPlayer()
        {
            bool isPlayer = false;

            if (int.TryParse(GetUserInput("Введите id игрока, которого нужно разбанить"), out int userInput))
            {
                foreach (Player player in players.Values)
                {
                    if (player.Id == userInput)
                    {
                        isPlayer = true;
                        player.Unban();
                        Console.WriteLine("Игрок разбанен.");
                    }
                }

                if (isPlayer == false)
                    Console.WriteLine("Такого id нет.");
            }
            else
            {
                Console.WriteLine("Некорректный ввод.");
            }
        }

        private void RemovePlayer()
        {
            bool isPlayer = false;

            if (int.TryParse(GetUserInput("Введите id игрока для удаления"), out int userInput))
            {
                foreach (var player in players)
                {
                    if (player.Value.Id == userInput)
                    {
                        isPlayer = true;
                        players.Remove(player.Key);
                        Console.WriteLine("Угрок удален.");
                        break;
                    }    
                }

                if (isPlayer == false)
                    Console.WriteLine("Такого id нет.");
            }
            else
            {
                Console.WriteLine("Некорректный ввод.");
            }
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
            foreach (Player player in players.Values)
            {
                Console.WriteLine($"{player.Id} - {player.NickName}, уровень {player.Level}, статус бана {player.IsBanned}.");
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