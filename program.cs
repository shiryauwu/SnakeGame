using System;
using System.Diagnostics;
using static System.Console;
namespace SnakeGame
{
    class Program
    {
        private const int MapWidth = 30;
        private const int MapHeight = 20;
        private const ConsoleColor BorderColor = ConsoleColor.Red;
        private const ConsoleColor HeadColor = ConsoleColor.Cyan;
        private const ConsoleColor BodyColor = ConsoleColor.Gray;
        private const ConsoleColor FoodColor = ConsoleColor.Green;

        private static readonly Random random = new Random();
        private const int Frames = 300;
        public static void Main()
        {
            SetWindowSize(MapWidth, MapHeight);
            SetBufferSize(MapWidth, MapHeight);
            CursorVisible = false;
            while (true)
            {
                StartGame();
                Thread.Sleep(3000);
                ReadKey();
            }
            
            
            
        
        }
        static void StartGame()
        {
            Clear();
            DrawBorder();
            

            Direction currentMovement = Direction.Right;

            var snake = new Snake(10, 10, HeadColor, BodyColor);
            Pixel food = GenFood(snake);
            food.Draw();

            var stopwatch = new Stopwatch();
            while (true)
            {
                stopwatch.Restart();
                Direction oldMovement = currentMovement;
                while (stopwatch.ElapsedMilliseconds <= Frames)
                {
                    if (currentMovement == oldMovement)
                    {
                        currentMovement = ReadMovement(currentMovement);

                    }
                }

                if(snake.Head.X == food.X && snake.Head.Y == food.Y)
                {
                    snake.Move(currentMovement, true);
                    food = GenFood(snake);
                    food.Draw();
                    
                }
                else
                {
                    snake.Move(currentMovement);
                }
                snake.Move(currentMovement);

                if (snake.Head.X == MapWidth - 1
                    || snake.Head.X == 0
                    || snake.Head.Y == MapHeight - 1
                    || snake.Head.Y == 0
                    || snake.Body.Any(b => b.X == snake.Head.X && b.Y == snake.Head.Y))
                    break;
            }
            snake.Clear();
            SetCursorPosition(MapWidth / 3, MapHeight / 2);
            WriteLine($"GAME OVER!!");
        }

        static Pixel GenFood(Snake snake)
        {
            Pixel food;
            do
            {
                food = new Pixel(random.Next(1, MapWidth - 2), random.Next(1, MapHeight - 2), FoodColor);
            } while (snake.Head.X == food.X && snake.Head.Y == food.Y
            || snake.Body.Any(b => b.X == food.X && b.Y == food.Y));
            return food;
        }

        static Direction ReadMovement(Direction currentDirection)
        {
            if (!KeyAvailable)
                return currentDirection;

            ConsoleKey key = ReadKey(true).Key;
            currentDirection = key switch
            {
                ConsoleKey.UpArrow when currentDirection != Direction.Down => Direction.Up,
                ConsoleKey.DownArrow when currentDirection != Direction.Up => Direction.Down,
                ConsoleKey.LeftArrow when currentDirection != Direction.Right => Direction.Left,
                ConsoleKey.RightArrow when currentDirection != Direction.Left => Direction.Right,

                _ => currentDirection
            };
            return currentDirection;
        }
        static void DrawBorder()
        {
            for (int i = 0; i < MapWidth; i++)
            {
                new Pixel(i, 0, BorderColor).Draw();
                new Pixel(i, MapHeight - 1, BorderColor).Draw();

            }

            for (int i = 0; i < MapHeight; i++)
            {
                new Pixel(0, i, BorderColor).Draw();
                new Pixel(MapWidth - 1, i, BorderColor).Draw();

            }
        }
    }
}
