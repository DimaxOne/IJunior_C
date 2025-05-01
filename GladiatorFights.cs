using System;
using System.Collections.Generic;

namespace GladiatorFights
{
    class Program
    {
        static void Main(string[] args)
        {
            Arena arena = new Arena();

            arena.Work();
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

        public static int GenerateRandomNumber(int mininimumValue, int maximumValue)
        {
            return s_random.Next(mininimumValue, maximumValue + 1);
        }
    }

    class Arena
    {
        private List<Warrior> _warriors = new List<Warrior>();
        private bool _isWork = true;

        public Arena()
        {
            _warriors.Add(new Assassin("Убийца", 25, 10, 160, 50, 2));
            _warriors.Add(new Barbarian("Варвар", 30, 5, 200, 2, 2));
            _warriors.Add(new Paladin("Паладин", 15, 15, 180, 120, 35));
            _warriors.Add(new Mage("Маг", 15, 5, 130, 200, 40, 40));
            _warriors.Add(new Monk("Монах", 30, 5, 170, 50));
        }

        public void Work()
        {
            const string CommandShowWelcomeMessage = "1";
            const string CommandWatchFight = "2";
            const string CommandExit = "3";

            ShowWelcomeMessage();

            while (_isWork)
            {
                Console.WriteLine($"Вам доступно:" +
                    $"\n{CommandShowWelcomeMessage} - Приветственное сообщение;" +
                    $"\n{CommandWatchFight} - Посмотреть бой;" +
                    $"\n{CommandExit} - Выход из программы.");

                switch (UserUtils.GetUserInput("Что вы хотите"))
                {
                    case CommandShowWelcomeMessage:
                        ShowWelcomeMessage();
                        break;

                    case CommandWatchFight:
                        Fight();
                        break;

                    case CommandExit:
                        _isWork = false;
                        break;

                    default:
                        Console.WriteLine("Неверный ввод. Введите номер команды.");
                        break;
                }

                ClearConsole();
            }
        }

        private void Fight()
        {
            Warrior firstWarrior = CreateWarrior("Первый");
            Warrior secondWarrior = CreateWarrior("Второй");

            while (firstWarrior.Health > 0 && secondWarrior.Health > 0)
            {
                firstWarrior.Attack(secondWarrior);
                secondWarrior.Attack(firstWarrior);
                Console.WriteLine();
                firstWarrior.ShowStats();
                firstWarrior.ShowAdditionalStats();
                secondWarrior.ShowStats();
                secondWarrior.ShowAdditionalStats();
                Console.WriteLine("\n" + new string('*', 50));
            }

            if (firstWarrior.Health <= 0 && secondWarrior.Health <= 0)
                Console.WriteLine("Бой завершился ничьей!");
            else if (firstWarrior.Health <= 0)
                Console.WriteLine("Победил второй боец!");
            else
                Console.WriteLine("Победил первый боец!");
        }

        private Warrior CreateWarrior(string serialNumber)
        {
            Warrior warrior = null;

            Console.Clear();

            while (warrior == null)
            {
                if (TryCreateFighter(out warrior))
                    Console.WriteLine($"{serialNumber} боец готов к бою!");

                ClearConsole();
            }

            return warrior;
        }

        private bool TryCreateFighter(out Warrior warrior)
        {
            bool isWarrior = false;
            Warrior userWarrior = null;

            ShowWarriors();

            if (int.TryParse(UserUtils.GetUserInput("Выберите номер бойца"), out int userIndex))
            {
                userIndex--;

                if (userIndex >= 0 && userIndex < _warriors.Count)
                {
                    isWarrior = true;
                    userWarrior = _warriors[userIndex].Clone();
                }
                else
                {
                    Console.WriteLine("Бойца с таким индексом нет.");
                }
            }
            else
            {
                Console.WriteLine("Введены некорректные данные.");
            }

            warrior = userWarrior;
            return isWarrior;
        }

        private void ShowWelcomeMessage()
        {
            Console.WriteLine("Приветствуем на арене Колизея! Выбирайте бойцов и смотрите сраженине!");
        }

        private void ShowWarriors()
        {
            for (int i = 0; i < _warriors.Count; i++)
            {
                Console.Write(i + 1 + " - ");
                _warriors[i].ShowStats();
                _warriors[i].ShowSkill();
                Console.WriteLine();
            }
        }

        private void ClearConsole()
        {
            Console.WriteLine("Нажмите любую кнопку для продолжения.");
            Console.ReadLine();
            Console.Clear();
        }
    }

    abstract class Warrior : IDamageable
    {
        public Warrior(string name, int damage, int armor, int health)
        {
            Name = name;
            Damage = damage;
            Armor = armor;
            Health = health;
        }

        public int Health { get; protected set; }

        public string Name { get; private set; }
        public int Damage { get; private set; }
        public int Armor { get; private set; }

        public virtual void TakeDamage(int damage)
        {
            if (Armor > damage)
                damage = Armor;

            Health -= damage - Armor;
        }

        public virtual void Attack(IDamageable warrior)
        {
            ShowSimpleAttackMessage();
            warrior.TakeDamage(Damage);
        }

        public virtual void ShowStats()
        {
            Console.WriteLine($"Боец {Name} имеет {Health} здоровья, {Armor} брони и наносит {Damage} урона.");
        }

        public virtual void ShowAdditionalStats() { }

        public abstract void ShowSkill();

        public abstract Warrior Clone();


        protected void ShowSimpleAttackMessage()
        {
            Console.WriteLine($"Боец {Name} совершил простую атаку с уроном {Damage}.");
        }
    }

    class Assassin : Warrior
    {
        private int _сriticalHitChance;
        private int _damageMultiplier;

        public Assassin(string name, int damage, int armor, int health, int сriticalHitChance, int damageMultiplier) : base(name, damage, armor, health)
        {
            _сriticalHitChance = сriticalHitChance;
            _damageMultiplier = damageMultiplier;
        }

        public override void Attack(IDamageable warrior)
        {
            int maximumRandomValue = 100;
            int minimumRandomValue = 0;
            int randomNumber = UserUtils.GenerateRandomNumber(minimumRandomValue, maximumRandomValue + 1);

            int damage;

            if (randomNumber <= _сriticalHitChance)
            {
                damage = Damage * _damageMultiplier;

                Console.WriteLine($"Боец {Name} наносит критический урон - {damage}!");
                warrior.TakeDamage(damage);
            }
            else
            {
                warrior.TakeDamage(Damage);
            }
        }

        public override void ShowSkill()
        {
            Console.WriteLine("Боец имеет некий шанс нанести удвоенный урон.");
        }

        public override Warrior Clone()
        {
            return new Assassin(Name, Damage, Armor, Health, _сriticalHitChance, _damageMultiplier);
        }
    }

    class Barbarian : Warrior
    {
        private int _counterDoubleDamage;
        private int _currentCounterDoubleDamage;
        private int _countAttacksInAbility;

        public Barbarian(string name, int damage, int armor, int health, int counterDoubleDamage, int countAttacksInAbility) : base(name, damage, armor, health)
        {
            _counterDoubleDamage = counterDoubleDamage;
            _currentCounterDoubleDamage = 0;
            _countAttacksInAbility = countAttacksInAbility;
        }

        public override void Attack(IDamageable warrior)
        {
            if (_counterDoubleDamage == _currentCounterDoubleDamage)
            {
                Console.Write($"Боец {Name} совершает двойную атаку! ");

                for (int i = 0; i < _countAttacksInAbility; i++)
                {
                    Console.WriteLine($"Удар {i + 1} наносит {Damage} урона. ");
                    warrior.TakeDamage(Damage);
                }

                _currentCounterDoubleDamage = 0;
            }
            else
            {
                warrior.TakeDamage(Damage);
                _currentCounterDoubleDamage++;
            }
        }

        public override void ShowSkill()
        {
            Console.WriteLine("Боец каждую третью атаку наносит дважды урон врагу.");
        }

        public override Warrior Clone()
        {
            return new Barbarian(Name, Damage, Armor, Health, _counterDoubleDamage, _countAttacksInAbility);
        }
    }

    class Paladin : Warrior
    {
        private int _currentRage;
        private int _maximumRage;
        private int _powerOfHeal;

        public Paladin(string name, int damage, int armor, int health, int maximumRage, int healPower) : base(name, damage, armor, health)
        {
            _currentRage = 0;
            _maximumRage = maximumRage;
            _powerOfHeal = healPower;
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);

            _currentRage += damage;

            if (_currentRage >= _maximumRage)
            {
                Console.WriteLine($"Боец {Name} восстанавливает здоровье в размере {_powerOfHeal}!");
                Health += _powerOfHeal;
                _currentRage = 0;
            }
        }

        public override Warrior Clone()
        {
            return new Paladin(Name, Damage, Armor, Health, _maximumRage, _powerOfHeal);
        }

        public override void ShowSkill()
        {
            Console.WriteLine("Боец накапливает ярость и может восстановить здоровье при её максимуме.");
        }

        public override void ShowAdditionalStats()
        {
            Console.WriteLine($"Имеет {_currentRage} ярости из {_maximumRage}.");
        }
    }

    class Mage : Warrior
    {
        private int _maximumMana;
        private int _currentMana;
        private int _fireBallManaCost;
        private int _fireBallDamage;

        public Mage(string name, int damage, int armor, int health, int maximumMana, int fireBallManaCost, int fireBallDamage) : base(name, damage, armor, health)
        {
            _maximumMana = maximumMana;
            _currentMana = maximumMana;
            _fireBallManaCost = fireBallManaCost;
            _fireBallDamage = fireBallDamage;
        }

        public override void Attack(IDamageable warrior)
        {
            if (_currentMana - _fireBallManaCost > 0)
            {
                _currentMana -= _fireBallManaCost;
                warrior.TakeDamage(_fireBallDamage);
                Console.WriteLine($"Боец {Name} атаковал Огненным шаром и нанес {_fireBallDamage} урона!");
            }
            else
            {
                base.Attack(warrior);
            }
        }

        public override Warrior Clone()
        {
            return new Mage(Name, Damage, Armor, Health, _maximumMana, _fireBallManaCost, _fireBallDamage);
        }

        public override void ShowSkill()
        {
            Console.WriteLine("У бойца есть мана и пока её достаточно он применяет заклинание “Огненный шар”.");
        }

        public override void ShowAdditionalStats()
        {
            Console.WriteLine($"Имеет {_currentMana} маны из {_maximumMana}.");
        }
    }

    class Monk : Warrior
    {
        private int _dodgeСhance;

        public Monk(string name, int damage, int armor, int health, int dodgeСhance) : base(name, damage, armor, health)
        {
            _dodgeСhance = dodgeСhance;
        }

        public override void Attack(IDamageable warrior)
        {
            base.Attack(warrior);
        }

        public override void TakeDamage(int damage)
        {
            int maximumRandomValue = 100;
            int minimumRandomValue = 0;

            if (UserUtils.GenerateRandomNumber(minimumRandomValue, maximumRandomValue + 1) <= _dodgeСhance)
            {
                Console.WriteLine($"Боец {Name} уклонился от атаки и не получил урон.");
            }
            else
            {
                base.TakeDamage(damage);
            }
        }

        public override Warrior Clone()
        {
            return new Monk(Name, Damage, Armor, Health, _dodgeСhance);
        }

        public override void ShowSkill()
        {
            Console.WriteLine("Боец имеет шанс уклониться, когда по нему наносят урон.");
        }
    }

    interface IDamageable
    {
        void TakeDamage(int damage);
    }
}