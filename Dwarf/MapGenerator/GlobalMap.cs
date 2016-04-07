using MapGenerator.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator
{
    public class GlobalMap
    {
        public readonly int Height;
        public readonly int Width;
        public Player Player { get; private set; }
        public readonly Random rand;
        private static int[] dx = { 1, 0, -1, 0 };
        private static int[] dy = { 0, -1, 0, 1 };
        public MapObject[,] Map { get; set; }
        private bool[,] TouchMap { get; set; }

        public MapObject this[int x, int y] {
            get {
                return Map[x, y];
            }
            set {
                Map[x, y] = value;
                TouchMap[x, y] = true;
            }
        }

        public bool Retouch(int x, int y)
        {
            bool old = TouchMap[x, y];
            TouchMap[x, y] = false;
            if (old == true)
                return old; // Не индус, нужно было поставить дебаг только на True-значение.
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
            this[newPos.X, newPos.Y] = Player;
        }

        public GlobalMap(int width, int height)
        {
            rand = new Random();
            Height = height;
            Width = width;
            Map = new MapObject[Width, Height];
            TouchMap = new bool[Width, Height];
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    Map[x, y] = new Rock();
        }
        ////////////////////////////////////////////////////////////////////////////////
        public void AddWater(int count, int minSize, int maxSize)
        {
            for (int counter = 0; counter < count; counter++)
            {
                var pos = GetFreeCell();
                this[pos.X, pos.Y] = new Water();
                int length = rand.Next(minSize, maxSize);
                int x = pos.X;
                int y = pos.Y;
                for (int k = 0; k < length; k++)
                {
                    Bolk(x, y);
                    int d = rand.Next(4);
                    x += dx[d];
                    y += dy[d];
                }
            }
        }

        public void Bolk(int x, int y)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (ValidX(x + i) && ValidY(y + j))
                    {
                        if (this[x + i, y + j] is Gold)
                            continue;
                        if (this[x + i, y + j] is IFree || this[x + i, y + j] is Gravel)
                            this[x + i, y + j] = new Water();
                    }
                }
            }
        }

        public void Flood(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var cell = GetFreeCell();
                if (this[cell.X, cell.Y] is Water)
                {
                    Bolk(cell.X, cell.Y);
                }
            }
        }

        public void AddCaves(int count, int length) {
            var rand = new Random();
            for (int i = 0; i < count; i++)
                AddCave(rand.Next(Width), rand.Next(Height), length);
        }

        public void AddCave(int x, int y, int length) {
            for (int k = 0; k < length; k++)
            {
                if (ValidX(x) && ValidY(y))
                {
                    Map[x, y] = new Earth();
                }
                int d = rand.Next(4);
                x += dx[d];
                y += dy[d];
            }
        }
        public void AddGold(int value, int count)
        {
            for (int i = 0; i < count; i++) {
                var point = GetFreeCell();
                Map[point.X, point.Y] = new Gold(value);
            }
        }

        public void AddPlayer(Player player)
        {
            var point = GetFreeCell();
            Map[point.X, point.Y] = player;
            Player = player;
        }

        public void AddExit()
        {
            var point = GetFreeSq();
            for (int i = 0; i <= 1; i++)
                for (int j = 0; j <= 1; j++)
                    Map[point.X + i, point.Y + j] = new Exit();

        }

        public void Collapse(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var cell = GetFreeCell();
                this[cell.X, cell.Y] = new Gravel();
            }
        }

        ////////////////////////////////////////////////////////////////////

        public Point GetFreeCell() {
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
