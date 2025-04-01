using System;

namespace BraveNewWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] map =
            {
                {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#' },
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#' },
                {'#', ' ', '#', '#', ' ', '#', '$', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', ' ', ' ', '#', ' ', '#' },
                {'#', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#' },
                {'#', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#' },
                {'#', ' ', '#', ' ', '#', '#', ' ', '#', '#', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#' },
                {'#', ' ', '#', ' ', '#', '#', ' ', ' ', '#', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', '#', ' ', '#' },
                {'#', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', '#' },
                {'#', ' ', '#', '#', '#', '#', '#', '#', '#', ' ', '#', ' ', '$', '#', '#', '#', '#', ' ', '#', ' ', ' ', '#' },
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#', ' ', ' ', '#', ' ', ' ', '#', ' ', '#', '#', ' ', '#' },
                {'#', ' ', ' ', ' ', ' ', '#', '#', ' ', '#', '#', '#', ' ', ' ', '#', ' ', ' ', '#', ' ', ' ', '#', ' ', '#' },
                {'#', ' ', ' ', '#', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', '#', '#', ' ', ' ', '#', ' ', ' ', '#', ' ', '#' },
                {'#', ' ', ' ', '#', ' ', '#', '#', '#', '#', ' ', ' ', ' ', '#', '#', ' ', ' ', '#', ' ', ' ', '#', ' ', '#' },
                {'#', ' ', ' ', '#', ' ', '#', ' ', ' ', ' ', ' ', '#', ' ', ' ', '#', ' ', ' ', '#', '#', '#', '#', ' ', '#' },
                {'#', ' ', ' ', '#', ' ', '#', '#', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
                {'#', ' ', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', '#', '#', '#', ' ', ' ', ' ', ' ', ' ', '#' },
                {'#', ' ', ' ', '#', '#', '#', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#' },
                {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
                {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
            };
            char player = '@';
            char wall = '#';
            char candy = '$';
            char empty = ' ';
            bool isWork = true;
            int[] playerPosition = { 1, 1 };
            int[] scorePosition = { 0, map.GetLength(1) };
            int score = 0;
            int[] direction;

            Console.CursorVisible = false;

            while (isWork)
            {
                Console.Clear();

                DrawMap(map);
                DrawPlayer(playerPosition[0], playerPosition[1], player);
                ShowScore(score, scorePosition);
                direction = GetDirection(Console.ReadKey());

                if (CheckWall(map, playerPosition, direction, wall) == false)
                {
                    ChangePosition(playerPosition, direction);
                    CheckCandy(ref score, playerPosition, map, candy, empty);
                }
            }
        }

        private static void DrawMap(char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }
                Console.Write("\n");
            }
        }

        private static void DrawPlayer(int positionX, int positionY, char player)
        {
            Console.SetCursorPosition(positionX, positionY);
            Console.Write(player);
        }

        private static void ShowScore(int score, int[] position)
        {
            Console.SetCursorPosition(position[0], position[1]);
            Console.WriteLine($"Счет: {score}");
        }

        private static int[] GetDirection(ConsoleKeyInfo pressedKey)
        {
            int[] direction = { 0, 0 };

            if (pressedKey.Key == ConsoleKey.W)
            {
                direction[1] = -1;
            }
            else if (pressedKey.Key == ConsoleKey.S)
            {
                direction[1] = 1;
            }
            else if (pressedKey.Key == ConsoleKey.A)
            {
                direction[0] = -1;
            }
            else if (pressedKey.Key == ConsoleKey.D)
            {
                direction[0] = 1;
            }

            return direction;
        }

        private static void ChangePosition(int[] playerPosition, int[] direction)
        {
            for (int i = 0; i < playerPosition.Length; i++)
            {
                playerPosition[i] += direction[i];
            }
        }

        private static bool CheckWall(char[,] map, int[] playerPosition, int[] direction, char wall)
        {
            if (map[playerPosition[1] + direction[1], playerPosition[0] + direction[0]] == wall)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void CheckCandy(ref int score, int[] playerPosition, char[,] map, char candy, char empty)
        {
            if (map[playerPosition[1], playerPosition[0]] == candy)
            {
                map[playerPosition[1], playerPosition[0]] = empty;
                score++;
            }
        }
    }
}