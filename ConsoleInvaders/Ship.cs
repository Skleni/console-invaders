using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleInvaders
{
    class Ship : MovingObject
    {
        public const String PLAYER = "/^\\";
        public const String ENEMY = "\\v/";
        public const int MAX_EXPLOSION_DELAY = 300;

        public int Strength { get; protected set; }

        public Ship(Game game, String chars, int x, int y, int strength) : base(game, chars, x, y)
        {
            Strength = strength;
        }

        public override void Move(Direction direction)
        {
            //don't let the ship move out of the map
            switch (direction)
            {
                case Direction.Left:
                    X = Math.Max(X - 1, Game.Map.Offset);
                    break;
                case Direction.Right:
                    X = Math.Min(X + 1, Game.Map.Offset + Game.Map.Width - Length);
                    break;
                case Direction.Up:
                    Y = Math.Max(Y - 1, Game.Map.Offset);
                    break;
                case Direction.Down:
                    Y = Math.Min(Y + 1, Game.Map.Offset + Game.Map.Height - 1);
                    break;
            }
        }

        public void Fire(Direction direction) 
        {
            Game.Bullets.Add(new Bullet(Game, X + Length / 2, Y - 1, direction));
        }

        public bool Hit()
        {
            return --Strength <= 0;
        }

        public void Explode() 
        {
            var random = new Random();

            foreach (int x in GetXValues())
            {
                if (random.Next(2) > 0)
                    Game.Explosions.Add(new Explosion(Game, x, Y, random.Next(MAX_EXPLOSION_DELAY)));
            }
        }
    }
}
