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
            bool isWork = true;
            int[] playerPosition = { 1, 1 };
            int[] scorePosition = { 0, map.GetLength(1) };
            int nextPositionX;
            int nextPositionY;
            int score = 0;
            int[] direction;
            ConsoleKeyInfo pressedKey;

            Console.CursorVisible = false;

            while (isWork)
            {
                Console.Clear();

                DrawMap(map);
                DrawPlayer(playerPosition[0], playerPosition[1]);
                ShowScore(score, scorePosition);
                pressedKey = Console.ReadKey();

                if(TryExit(pressedKey))
                {
                    isWork = false;
                }
                else
                {
                    direction = GetDirection(pressedKey);
                    GetNextPosition(out nextPositionX, out nextPositionY, playerPosition, direction);

                    if (TryFindWall(map, nextPositionX, nextPositionY) == false)
                    {
                        MovePlayer(playerPosition, nextPositionX, nextPositionY);
                        score = TakeСandy(score, playerPosition, map);
                    }
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

        private static void DrawPlayer(int positionX, int positionY)
        {
            char player = '@';

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
            char playerMoveUp = 'w';
            char playerMoveDown = 's';
            char playerMoveLeft = 'a';
            char playerMoveRight = 'd';

            if (pressedKey.KeyChar == playerMoveUp)
                direction[1] = -1;
            else if (pressedKey.KeyChar == playerMoveDown)
                direction[1] = 1;
            else if (pressedKey.KeyChar == playerMoveLeft)
                direction[0] = -1;
            else if (pressedKey.KeyChar == playerMoveRight)
                direction[0] = 1;

            return direction;
        }

        private static bool TryExit(ConsoleKeyInfo pressedKey)
        {
            char exitGame = 'p';

            return pressedKey.KeyChar == exitGame;
        }

        private static void MovePlayer(int[] playerPosition, int nextPositionX, int nextPositionY)
        {
            playerPosition[0] = nextPositionX;
            playerPosition[1] = nextPositionY;
        }

        private static void GetNextPosition(out int nextPositionX, out int nextPositionY, int[] playerPosition, int[] direction)
        {
            nextPositionX = playerPosition[0] + direction[0];
            nextPositionY = playerPosition[1] + direction[1];
        }

        private static bool TryFindWall(char[,] map, int nextPositionX, int nextPositionY)
        {
            char wall = '#';

            return map[nextPositionY, nextPositionX] == wall;
        }

        private static int TakeСandy(int score, int[] playerPosition, char[,] map)
        {
            char empty = ' ';
            char candy = '$';

            if (map[playerPosition[1], playerPosition[0]] == candy)
            {
                map[playerPosition[1], playerPosition[0]] = empty;
                score++;
            }

            return score;
        }
    }
}