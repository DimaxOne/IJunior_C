using System;

namespace WorkWithClasses
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player("Voin", 120, 26, 19);

            player.ShowStats();
        }
    }

    class Player
    {
        private string _name;
        private int _health;
        private int _armor;
        private int _damage;

        public Player(string name, int health, int armor, int damage)
        {
            _name = name;
            _health = health;
            _armor = armor;
            _damage = damage;
        }

        public void ShowStats()
        {
            Console.WriteLine($"У игрока {_name} имеется: {_health} здоровья, {_armor} брони и {_damage} урона.");
        }
    }
}