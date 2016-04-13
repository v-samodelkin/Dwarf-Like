using MapGenerator.MapObjects;
using MapGenerator.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator.ModalForms
{
    public abstract class ModalForm
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public Player Player { get; set; }

        public virtual char FrameChar
        {
            get
            {
                return 'X';
            }
        }

        public virtual ConsoleColor FrameCharFront
        {
            get
            {
                return ConsoleColor.DarkMagenta;
            }
        }

        public virtual ConsoleColor FrameCharBack
        {
            get
            {
                return ConsoleColor.DarkBlue;
            }
        }

        public ModalForm(int width, int height, Player player)
        {
            Width = width;
            Height = height;
            Player = player;
        }
        public virtual bool HandleInput(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.D0:
                case ConsoleKey.D1:
                case ConsoleKey.D2:
                case ConsoleKey.D3:
                case ConsoleKey.D4:
                case ConsoleKey.D5:
                case ConsoleKey.D6:
                case ConsoleKey.D7:
                case ConsoleKey.D8:
                case ConsoleKey.D9:
                    Action(int.Parse(key.KeyChar.ToString()));
                    return true;
            }
            return false;
        }

        public abstract void Action(int option);

        /////////////////////// Drawing//////////////////////////////
        public int Dx { get; set; }
        public int Dy { get; set; }
        public virtual void Draw(int dx, int dy)
        {
            Dx = dx;
            Dy = dy;
            for (int y = dy; y < dy + Height; y++)
            {
                Console.SetCursorPosition(dx, y);
                for (int x = dx; x < dx + Width; x++)
                {
                    Console.Write(" ");
                }
            }
        }
    }
}
