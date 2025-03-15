using System;

namespace BossFight
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CommandSimpleAttack = "1";
            const string CommandUseFireBall = "2";
            const string CommandUseExplosion = "3";
            const string CommandHealing = "4";

            Random random = new Random();
            int playerHealth = 100;
            int playerDamage = 7;
            int playerMana = 3;
            int maximumPlayerHealth = 110;
            int maximumPlayerMana = 3;
            int fireBallDamage = 15;
            int explosionDamage = 39;
            int healthRecovery = 15;
            int manaRecovery = 1;
            int bossHealth = 150;
            int availableHealingCount = 3;
            int maximumBossDamage = 15;
            int minimumBossDamage = 5;
            bool isFireBallUsed = false;
            int bossDamage;
            string userInput;

            while (playerHealth > 0 && bossHealth > 0)
            {
                Console.Write($"Ваше здоровье {playerHealth}, мана {playerMana}.\nЗдоровье босса {bossHealth}.\nДоступные действия:" +
                    $"\n{CommandSimpleAttack} - обычная атака;" +
                    $"\n{CommandUseFireBall} - удар огненным шаром (расходует 1 ману);" +
                    $"\n{CommandUseExplosion} - взрыв (может применяться после огненного шара);" +
                    $"\n{CommandHealing} - восстановление здоровья и маны (осталось {availableHealingCount} применений)." +
                    $"\nВаш выбор: ");

                userInput = Console.ReadLine();
                bossDamage = random.Next(minimumBossDamage, maximumBossDamage + 1);

                switch (userInput)
                {
                    case CommandSimpleAttack:
                        bossHealth -= playerDamage;
                        break;

                    case CommandUseFireBall:
                        if (playerMana > 0)
                        {
                            bossHealth -= fireBallDamage;
                            isFireBallUsed = true;
                            playerMana--;
                        }
                        else
                        {
                            Console.WriteLine("Недостаточно маны. Атака босса.");
                        }
                        break;

                    case CommandUseExplosion:
                        if (isFireBallUsed)
                        {
                            bossHealth -= explosionDamage;
                            isFireBallUsed = false;
                        }
                        else
                        {
                            Console.WriteLine("Огненый шар не был использован. Атака босса.");
                        }
                        break;

                    case CommandHealing:
                        if (availableHealingCount > 0)
                        {
                            if (playerHealth + healthRecovery <= maximumPlayerHealth)
                                playerHealth += healthRecovery;
                            else
                                playerHealth = maximumPlayerHealth;

                            if (playerMana + manaRecovery <= maximumPlayerMana)
                                playerMana += manaRecovery;
                            else
                                playerMana = maximumPlayerMana;

                            availableHealingCount--;
                            Console.WriteLine("Здоровье и мана пополнены.");
                        }
                        else
                        {
                            Console.WriteLine("Истрачено все доступное восстановление. Атака босса.");
                        }

                        break;

                    default:
                        Console.WriteLine("Ошибочной действие. Атака босса.");
                        break;
                }

                playerHealth -= bossDamage;

                Console.WriteLine("Для следующего хода нажмите любую клавишу.");
                Console.ReadKey();
                Console.Clear();
            }

            if (playerHealth <= 0 && bossHealth <= 0)
                Console.WriteLine("Никто не одержал победу. Ничья.");

            if (playerHealth <= 0)
                Console.WriteLine("Босс победил.");

            if (bossHealth <= 0)
                Console.WriteLine("Победа игрока.");
        }
    }
}
