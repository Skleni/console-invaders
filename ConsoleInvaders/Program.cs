using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleInvaders
{
    class Program
    {
        private static Game game;

        public static void Main(string[] args)
        {
            while (true)
            {
                Init();

                Write("Press ENTER to start!", 5);
                Console.ReadLine();
                Console.Clear();

                game.Draw();
                while (game.IsPlaying)
                {
                    ReadInput();

                    game.Update();

                    Console.SetCursorPosition(0, 2 * game.Map.Offset + game.Map.Height);
                    Console.Write("Score: {0}", game.Score);
                    Thread.Sleep(30);
                }

                Console.Clear();
                Write("G A M E   O V E R", -3);
                Write("Score: " + game.Score, -1);
            }
        }

        private static void Write(string text, int verticalOffset)
        {
            Console.SetCursorPosition((game.Map.Width - text.Length) / 2, (game.Map.Height / 2) + verticalOffset);
            Console.Write(text);
        }

        private static void ReadInput()
        {
            while (game.Ship != null && Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        game.Ship.Move(Direction.Left);
                        break;
                    case ConsoleKey.RightArrow:
                        game.Ship.Move(Direction.Right);
                        break;
                    case ConsoleKey.UpArrow:
                        game.Ship.Move(Direction.Up);
                        break;
                    case ConsoleKey.DownArrow:
                        game.Ship.Move(Direction.Down);
                        break;
                    case ConsoleKey.Spacebar:
                         game.Ship.Fire(Direction.Up);
                        break;
                }
            }
        }

        private static void Init()
        {
            int width = 50;
            int height = 25;

            game = new Game(width, height);

            Console.CursorVisible = false;
            Console.SetWindowSize(2 * game.Map.Offset + width, 2 * game.Map.Offset + game.Map.Height + 1);
            Console.SetBufferSize(2 * game.Map.Offset + width, 2 * game.Map.Offset + game.Map.Height + 1);
        }
    }
}
