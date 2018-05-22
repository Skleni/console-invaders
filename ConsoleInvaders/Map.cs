using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace ConsoleInvaders
{
    class Map
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public int Offset { get { return 1; } }

        public void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write('+');
            for (int i = 0; i < Width; i++)
                Console.Write('-');
            Console.Write('+');

            for (int i = Offset; i < Offset + Height; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write('|');
                Console.SetCursorPosition(Offset + Width, i);
                Console.Write('|');
            }

            Console.SetCursorPosition(0, Offset + Height);
            Console.Write('+');
            for (int i = 0; i < Width; i++)
                Console.Write('-');
            Console.Write('+');
        }

        public Map(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
    }
}
