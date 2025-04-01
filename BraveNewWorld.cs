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
            char up = 'w';
            char down = 's';
            char left = 'a';
            char right = 'd';
            char exit = 'p';
            bool isExit = false;
            int[] playerPosition = { 1, 1 };
            int[] scorePosition = { 0, map.GetLength(1) };
            int nextPositionX = 0;
            int nextPositionY = 0;
            int score = 0;
            int[] direction;
            ConsoleKeyInfo pressedKey;

            Console.CursorVisible = false;

            while (isExit == false)
            {
                Console.Clear();

                DrawMap(map);
                DrawPlayer(playerPosition[0], playerPosition[1], player);
                ShowScore(score, scorePosition);
                pressedKey = Console.ReadKey();

                isExit = Exit(pressedKey, exit);
                direction = GetDirection(pressedKey, up, down, left, right);
                GetNextPosition(ref nextPositionX, ref nextPositionY, playerPosition, direction);

                if (CanMove(map, nextPositionX, nextPositionY, wall) == false)
                {
                    MovePlayer(playerPosition, nextPositionX, nextPositionY);
                    score = TakeСandy(score, playerPosition, map, candy, empty);
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

        private static int[] GetDirection(ConsoleKeyInfo pressedKey, char up, char down, char left, char right)
        {
            int[] direction = { 0, 0 };

            if (pressedKey.KeyChar == up)
                direction[1] = -1;
            else if (pressedKey.KeyChar == down)
                direction[1] = 1;
            else if (pressedKey.KeyChar == left)
                direction[0] = -1;
            else if (pressedKey.KeyChar == right)
                direction[0] = 1;

            return direction;
        }

        private static bool Exit(ConsoleKeyInfo pressedKey, char exit)
        {
            return pressedKey.KeyChar == exit;
        }

        private static void MovePlayer(int[] playerPosition, int nextPositionX, int nextPositionY)
        {
            playerPosition[0] = nextPositionX;
            playerPosition[1] = nextPositionY;
        }

        private static void GetNextPosition(ref int nextPositionX, ref int nextPositionY, int[] playerPosition, int[] direction)
        {
            nextPositionX = playerPosition[0] + direction[0];
            nextPositionY = playerPosition[1] + direction[1];
        }

        private static bool CanMove(char[,] map, int nextPositionX, int nextPositionY, char wall)
        {
            return map[nextPositionY, nextPositionX] == wall;
        }

        private static int TakeСandy(int score, int[] playerPosition, char[,] map, char candy, char empty)
        {
            if (map[playerPosition[1], playerPosition[0]] == candy)
            {
                map[playerPosition[1], playerPosition[0]] = empty;
                score++;
            }

            return score;
        }
    }
}