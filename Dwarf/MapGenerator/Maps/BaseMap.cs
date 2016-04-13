using MapGenerator.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator.Maps
{
    public abstract class BaseMap
    {
        public static List<String> after = new List<String>() {
            "GAME OVER!", "Leave me alone...", "Just DEAD!", "Boooo!", "Where is my graveyard...", "I... whant... sleep", "Helo from Zombie!", "Braaaains!",
            "Hey. It is REALLY end of the game. Without joke", "42", "Мозгииииии...", "(@_@)", "Упс", "Бульк об стену..."
        };
        public readonly int Height;
        public readonly int Width;
        public Player Player { get; protected set; }
        public readonly Random rand;
        protected static int[] dx = { 1, 0, -1, 0 };
        protected static int[] dy = { 0, -1, 0, 1 };
        public MapObject[,] Map { get; set; }
        public bool[,] TouchMap { get; set; }

        public MapObject this[int x, int y]
        {
            get
            {
                return Map[x, y];
            }
            set
            {
                Map[x, y] = value;
                TouchMap[x, y] = true;
            }
        }

        public bool Retouch(int x, int y)
        {
            bool old = TouchMap[x, y];
            TouchMap[x, y] = false;
            return old;
        }

        public bool SoftRetouch(int x, int y)
        {
            return TouchMap[x, y];
        }

        public void PlayerMove(Point newPos)
        {
            var oldPos = PlayerCell;
            this[oldPos.X, oldPos.Y] = Player.Ground;
            Player.Ground = this[newPos.X, newPos.Y];
            this[newPos.X, newPos.Y] = Player.Check();
            if (Player.Check() is Gravestone)
            {
                GraphicModule.PrintOnMap(new Point(newPos.X + 1, newPos.Y), after[rand.Next(after.Count)]);
            }
        }

        public BaseMap(int width, int height)
        {
            rand = new Random();
            Height = height;
            Width = width;
            Map = new MapObject[Width, Height];
            TouchMap = new bool[Width, Height];
        }
     

        public void AddPlayer(Player player, int x, int y)
        {
            player.Ground = this[x, y];
            this[x, y] = player;
            Player = player;
        }


        public Point GetFreeCell()
        {
            int MAX_ITERATIONS = (int)1e8;
            for (int i = 0; i < MAX_ITERATIONS; i++)
            {
                int x = rand.Next(Width);
                int y = rand.Next(Height);
                if (Map[x, y] is IFree)
                {
                    return new Point(x, y);
                }
            }
            return new Point(0, 0);
        }

        public Point GetFreeSq()
        {
            int MAX_ITERATIONS = (int)1e8;
            for (int i = 0; i < MAX_ITERATIONS; i++)
            {
                int x = rand.Next(Width - 1);
                int y = rand.Next(Height - 1);
                if (Map[x, y] is IFree && Map[x + 1, y] is IFree && Map[x, y + 1] is IFree && Map[x + 1, y + 1] is IFree)
                {
                    return new Point(x, y);
                }
            }
            return new Point(0, 0);
        }

        public bool ValidX(int x)
        {
            return (x >= 0 && x < Width);
        }

        public bool ValidY(int y)
        {
            return (y >= 0 && y < Height);
        }

        public bool ValidP(Point p)
        {
            return (ValidX(p.X) && ValidY(p.Y));
        }

        public Point PlayerCell
        {
            get
            {
                for (int y = 0; y < Height; y++)
                    for (int x = 0; x < Width; x++)
                        if (Map[x, y] == Player)
                            return new Point(x, y);
                throw new IndexOutOfRangeException("Игрок не найден");
            }
        }

        ///////////////////////////////////////////////////////////////////


    }
}
