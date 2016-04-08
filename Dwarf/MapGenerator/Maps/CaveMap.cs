using MapGenerator.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator.Maps
{
    public class CavelMap : BaseMap
    {

        public CavelMap(int width, int height) : base(width, height)
        {
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
                        if (this[x + i, y + j] is Gold || this[x + i, y + j] is Water)
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
    }
}
