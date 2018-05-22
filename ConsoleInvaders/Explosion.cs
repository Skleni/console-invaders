using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleInvaders
{
    class Explosion : GameObject
    {
        private const int UPDATE_INTERVAL = 150;
        private const String CHARS = ".+*";
        private static ConsoleColor[] COLORS = new [] { ConsoleColor.Yellow, ConsoleColor.DarkYellow, ConsoleColor.Red };

        private DateTime lastUpdate;
        private int stage;

        public bool IsFinished
        {
            get { return stage >= Chars.Length; }
        }

        public Explosion(Game game, int x, int y, int delay = 0) : base(game, CHARS, x, y)
        {
            stage = -1;
            lastUpdate = DateTime.Now.AddMilliseconds(delay - UPDATE_INTERVAL);
        }

        public override void Draw()
        {
            Redraw();
        }

        public override void Redraw()
        {
            if (stage >= 0 && !IsFinished)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = COLORS[stage];

                Console.SetCursorPosition(X, Y);
                Console.Write(Chars[stage]);

                Console.ForegroundColor = color;
            }
        }

        public override void Update()
        {
            if ((DateTime.Now - lastUpdate).TotalMilliseconds >= UPDATE_INTERVAL) 
            {
                stage++;
                lastUpdate = DateTime.Now;
            }
        }
    }
}
