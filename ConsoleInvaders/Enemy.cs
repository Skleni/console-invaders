using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleInvaders
{
    class Enemy : Ship
    {
        private const int MOVE_INTERVAL = 500;

        private DateTime lastMove;

        public Enemy(Game game, int x, int y, int strength) : base(game, Ship.ENEMY, x, y, strength)
        {
            lastMove = DateTime.Now;
        }

        public override void Update()
        {
            if ((DateTime.Now - lastMove).TotalMilliseconds >= MOVE_INTERVAL)
            {
                Move(Direction.Down);
                base.Update();
                lastMove = DateTime.Now;
            }
        }
    }
}
