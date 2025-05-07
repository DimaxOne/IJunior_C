using System;
using System.Collections.Generic;

namespace War
{
    class Program
    {
        static void Main(string[] args)
        {
            War war = new War();

            war.Fight();
        }
    }

    class UserUtils
    {
        private static Random s_random = new Random();

        public static int GenerateRandomNumber(int mininimumValue, int maximumValue)
        {
            return s_random.Next(mininimumValue, maximumValue + 1);
        }
    }

    class War
    {
        private PlatoonFactory _platoonFactory = new PlatoonFactory();

        private Platoon _firstPlatoon;
        private Platoon _secondPlatoon;

        public War()
        {
            _firstPlatoon = _platoonFactory.Create();
            _secondPlatoon = _platoonFactory.Create();
        }

        public void Fight()
        {
            string firstPlatoonName = "Wolfs";
            string secondPlatoonName = "Snakes";

            while (_firstPlatoon.SoldiersCount > 0 && _secondPlatoon.SoldiersCount > 0)
            {
                ShowInfo(firstPlatoonName, _firstPlatoon);
                Console.WriteLine();
                ShowInfo(secondPlatoonName, _secondPlatoon);

                _firstPlatoon.Attack(_secondPlatoon.GetSoldiers());
                _secondPlatoon.Attack(_firstPlatoon.GetSoldiers());
                _firstPlatoon.RemoveDefeated();
                _secondPlatoon.RemoveDefeated();

                Console.WriteLine("\nНажмите любую кнопку для продолжения.");
                Console.ReadLine();
                Console.Clear();
            }

            ShowWinners(firstPlatoonName, secondPlatoonName);
        }

        private void ShowInfo(string platoonNumber, Platoon platoon)
        {
            Console.WriteLine($"В взводе {platoonNumber} готовы воевать:");
            platoon.ShowSoldiers();
        }

        private void ShowWinners(string firstPlatoonName, string secondPlatoonName)
        {
            if (_firstPlatoon.SoldiersCount <= 0 && _secondPlatoon.SoldiersCount <= 0)
                Console.WriteLine("Ничья!");
            else if (_firstPlatoon.SoldiersCount <= 0)
                Console.WriteLine($"Победил взвод {secondPlatoonName}!");
            else
                Console.WriteLine($"Победил взвод {firstPlatoonName}!");
        }
    }

    class Platoon
    {
        private List<Soldier> _soldiers;

        public Platoon(List<Soldier> soldiers)
        {
            _soldiers = soldiers;
        }

        public int SoldiersCount => _soldiers.Count;

        public void ShowSoldiers()
        {
            foreach (Soldier soldier in _soldiers)
            {
                soldier.ShowInfo();
            }
        }

        public void Attack(List<Soldier> soldiers)
        {
            foreach (Soldier soldier in _soldiers)
            {
                soldier.Attack(soldiers);
            }
        }

        public void RemoveDefeated()
        {
            for (int i = _soldiers.Count - 1; i >= 0; i--)
            {
                if (_soldiers[i].Health <= 0)
                    _soldiers.RemoveAt(i);
            }
        }

        public List<Soldier> GetSoldiers()
        {
            return new List<Soldier>(_soldiers);
        }
    }

    class PlatoonFactory
    {
        public Platoon Create()
        {
            return new Platoon(CreateSoldiers());
        }

        private List<Soldier> CreateSoldiers()
        {
            List<Soldier> soldiers = new List<Soldier>();

            string simpleSoldierType = "Simple";
            string rareSoldierType = "Rare";
            string epicSoldierType = "Epic";
            string legendarySoldierType = "Legendary";

            int maximumSimpleSoldiersCount = 20;
            int minimumSimpleSoldiersCount = 14;
            int maximumRareSoldiersCount = 12;
            int minimumRareSoldiersCount = 8;
            int maximumEpicSoldiersCount = 10;
            int minimumEpicSoldiersCount = 6;
            int maximumLegendarySoldiersCount = 5;
            int minimumLegendarySoldiersCount = 3;

            AddSoldiers(minimumSimpleSoldiersCount, maximumSimpleSoldiersCount, new Soldier(70, 16, 5, simpleSoldierType), soldiers);
            AddSoldiers(minimumRareSoldiersCount, maximumRareSoldiersCount, new RareSoldier(100, 16, 5, rareSoldierType), soldiers);
            AddSoldiers(minimumEpicSoldiersCount, maximumEpicSoldiersCount, new EpicSoldier(120, 20, 10, epicSoldierType), soldiers);
            AddSoldiers(minimumLegendarySoldiersCount, maximumLegendarySoldiersCount, new LegendarySoldier(150, 30, 15, legendarySoldierType), soldiers);

            return soldiers;
        }

        private void AddSoldiers(int minimumCount, int maximumCount, Soldier soldier, List<Soldier> soldiers)
        {
            int soldiersCount = UserUtils.GenerateRandomNumber(minimumCount, maximumCount);

            for (int i = 0; i < soldiersCount; i++)
            {
                soldiers.Add(soldier.Clone());
            }
        }
    }

    class Soldier : IDamagable
    {
        public Soldier(int health, int damage, int armor, string type)
        {
            Health = health;
            Damage = damage;
            Armor = armor;
            Type = type;
        }

        public int Health { get; private set; }
        public int Damage { get; private set; }
        public int Armor { get; private set; }
        public string Type { get; private set; }

        public virtual void Attack(List<Soldier> soldiers)
        {
            soldiers[UserUtils.GenerateRandomNumber(0, soldiers.Count - 1)].TakeDamage(Damage);
        }

        public virtual Soldier Clone()
        {
            return new Soldier(Health, Damage, Armor, Type);
        }

        public void TakeDamage(int damage)
        {
            if (damage < Armor)
                damage = Armor;

            Health -= damage - Armor;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"У солдата класса {Type} осталось {Health}.");
        }
    }

    class RareSoldier : Soldier
    {
        private int _damageMultiplier;

        public RareSoldier(int health, int damage, int armor, string type, int damageMultiplier = 2) : base(health, damage, armor, type)
        {
            _damageMultiplier = damageMultiplier;
        }

        public override void Attack(List<Soldier> soldiers)
        {
            soldiers[UserUtils.GenerateRandomNumber(0, soldiers.Count - 1)].TakeDamage(Damage * _damageMultiplier);
        }
    }

    class EpicSoldier : Soldier
    {
        private int _countOfTarget;

        public EpicSoldier(int health, int damage, int armor, string type, int countOfTarget = 5) : base(health, damage, armor, type)
        {
            _countOfTarget = countOfTarget;
        }

        public override void Attack(List<Soldier> soldiers)
        {
            List<Soldier> targetToAttack = new List<Soldier>();

            if (soldiers.Count <= _countOfTarget)
            {
                targetToAttack = soldiers;
            }
            else
            {
                List<Soldier> availableSoldiers = new List<Soldier>(soldiers);

                for (int i = 0; i < _countOfTarget; i++)
                {
                    int indexSoldier = UserUtils.GenerateRandomNumber(0, availableSoldiers.Count - 1);
                    targetToAttack.Add(availableSoldiers[indexSoldier]);
                    availableSoldiers.RemoveAt(indexSoldier);
                }
            }

            foreach (Soldier soldier in targetToAttack)
            {
                soldier.TakeDamage(Damage);
            }
        }
    }

    class LegendarySoldier : Soldier
    {
        private int _countOfTarget;

        public LegendarySoldier(int health, int damage, int armor, string type, int countOfTarget = 8) : base(health, damage, armor, type)
        {
            _countOfTarget = countOfTarget;
        }

        public override void Attack(List<Soldier> soldiers)
        {
            for (int i = 0; i < _countOfTarget; i++)
            {
                soldiers[UserUtils.GenerateRandomNumber(0, soldiers.Count - 1)].TakeDamage(Damage);
            }
        }
    }

    interface IDamagable
    {
        void TakeDamage(int damage);
    }
}