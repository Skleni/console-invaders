using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleInvaders
{
    class Bullet : MovingObject
    {
        private Direction direction;

        public Bullet(Game game, int x, int y, Direction direction) : base(game, ".", x, y)
        {
            this.direction = direction;
            Draw();
        }

        public override void Update()
        {
            Move(direction);
            if (IsInMap)
                base.Update();
        }

        public override void Draw()
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            base.Draw();
            Console.ForegroundColor = color;
        }
    }
}
