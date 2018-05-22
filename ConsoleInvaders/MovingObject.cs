using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleInvaders
{
    class MovingObject : GameObject
    {
        //keep the last coordinates so that the screen can be cleared at the previous position
        public int LastX { get; protected set; }
        public int LastY { get; protected set; }

        public MovingObject(Game game, String chars, int x, int y) : base(game, chars, x, y)
        {
            this.LastX = x;
            this.LastY = y;
        }

        public override void Redraw()
        {
            if (X != LastX || Y != LastY)
            {
                Draw();
                LastX = X;
                LastY = Y;
            }
        }

        public override void Clear()
        {
            if (X != LastX || Y != LastY)
            {
                Console.SetCursorPosition(LastX, LastY);
                Console.Write(new String(' ', Length));
            }
        }

        public void Delete()
        {
            Console.SetCursorPosition(LastX, LastY);
            Console.Write(new String(' ', Length));
        }

        public virtual void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    X--;
                    break;
                case Direction.Right:
                    X++;
                    break;
                case Direction.Up:
                    Y--;
                    break;
                case Direction.Down:
                    Y++;
                    break;
            }
        }

        public bool WasAtCoordinates(int x, int y)
        {
            return y == LastY && x >= LastX && x < LastX + Length;
        }
    }
}
