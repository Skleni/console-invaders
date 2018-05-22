using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace ConsoleInvaders
{
    class GameObject
    {
        public GameObject(Game game, String chars, int x, int y)
        {
            this.Game = game;
            this.Chars = chars;

            this.X = x; 
            this.Y = y;
        }

        public Game Game { get; set; }

        public String Chars { get; set; }

        public int Length { get { return Chars.Length; } }

        public int X { get; set; }
        public int Y { get; set; }

        public IEnumerable<int> GetXValues()
        {
            for (int x = X; x < X + Length; x++)
                yield return x;
        }
        
        public bool IsInMap
        {
            get { return Y >= Game.Map.Offset && Y <= Game.Map.Height - Game.Map.Offset; }
        }

        public virtual void Update()
        {
            
        }

        public virtual void Draw() 
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(Chars);
        }

        public virtual void Redraw()
        {
        }

        public virtual void Clear()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(new String(' ', Length));
        }

        public bool IsAtCoordinates(int x, int y) 
        {
            return y == Y && x >= X && x < X + Length;
        }
    }
}
