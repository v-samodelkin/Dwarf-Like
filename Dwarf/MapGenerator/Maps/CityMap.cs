using MapGenerator.MapObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator.Maps
{
    public class CityMap : BaseMap
    {
        public int RoadWidth { get; private set; }

        public List<ShopCell> ShopCells;
        public CityMap(int width, int height) : base(width, height)
        {
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    Map[x, y] = new Earth();
            ShopCells = new List<ShopCell>();
        }

        public void AddPlayer(Player player)
        {
            int sx = Width / 2;
            int sy = Height / 2;
            for (int y = 0; y < Height; y++)
            {
                if (this[sx, y] is Road) {
                    AddPlayer(player, sx, y + RoadWidth / 2);
                    return;
                }
            }
            AddPlayer(player, sx, sy);
        }

        public void AddRoad(int roadWidth = 5, int breaksCount = 2, int lines = 3)
        {
            RoadWidth = roadWidth;
            int start = rand.Next(roadWidth / 2 + 1, Height - roadWidth / 2 - 1);
            var breaks = new List<Point>();
            Func<int> randX = () => rand.Next(roadWidth, Width - roadWidth);
            Func<int> randY = () => rand.Next(roadWidth, Height - roadWidth);
            Action<int, int> PlaceX = (x, y) =>
            {
                for (int yy = y - roadWidth / 2; yy <= y + roadWidth / 2; yy++)
                    if (ValidX(x) && ValidY(yy))
                        Map[x, yy] = new Road();
            };
            Action<int, int> PlaceY = (x, y) =>
            {
                for (int xx = x - roadWidth; xx <= x + roadWidth; xx++)
                    if (ValidX(xx) && ValidY(y))
                        Map[xx, y] = new Road();
            };
            Action<int, int, int> Merge = (y1, y2, x) =>
            {
                if (y1 > y2)
                {
                    var y3 = y1;
                    y1 = y2;
                    y2 = y3;
                }
                for (int y = y1; y <= y2; y++)
                    PlaceY(x, y);
            };

            for (int i = 0; i < breaksCount; i++)
                breaks.Add(new Point(rand.Next(Width / breaksCount * i + roadWidth / 2, Width / breaksCount * (i + 1) - roadWidth / 2), randY()));
            breaks = breaks.OrderBy(x => x).ToList();

            int prevX = 0;
            int curY = start;
            for (int i = 0; i < breaks.Count; i++)
            {
                for (int x = prevX; x < breaks[i].X; x++)
                    PlaceX(x, curY);
                Merge(curY, breaks[i].Y, breaks[i].X);
                curY = breaks[i].Y;
                prevX = breaks[i].X;
            }
            for (int x = prevX; x < Width; x++)
                PlaceX(x, curY);
        }

        public void PlaceShop(string shopName)
        {
            var shop = File.ReadAllLines("Resources/" + shopName + ".txt");
            int width = shop[0].Length;
            int height = shop.Length;
            var place = FindPlace(width, height);
            for (int x = place.X; x < place.X + width; x++)
            {
                for (int y = place.Y; y < place.Y + height; y++)
                {
                    var sc = new ShopCell(shop[y - place.Y][x - place.X]);
                    Map[x, y] = sc;
                    ShopCells.Add(sc);
                }
            }

        }

        // Не проверят вертикальные грани
        public Point FindPlace(int placeWidth, int placeHeight)
        {
            Point next = new Point(-1, -1);
            bool bad = true;
            while (bad)
            {
                next = new Point(rand.Next(Width), rand.Next(Height));
                bad = false;
                for (int dx = 0; dx < placeWidth; dx++)
                {
                    for (int dy = 0; dy < placeHeight; dy++)
                    {
                        int nx = next.X + dx;
                        int ny = next.Y + dy;
                        if (ValidX(nx) && ValidY(ny) && this[nx, ny] is Earth)
                            continue;
                        bad = true;
                    }
                }
                if (bad)
                    continue;
                bad = true;
                for (int x = next.X; x < next.X + placeWidth; x++)
                {
                    if (ValidX(x) && ValidY(next.Y - 1) && this[x, next.Y - 1] is Road)
                        bad = false;
                    if (ValidX(x) && ValidY(next.Y + placeHeight) && this[x, next.Y + placeHeight] is Road)
                        bad = false;
                }

                for (int y = next.Y; y < next.Y + placeHeight; y++)
                {
                    if (ValidX(next.X - 1) && ValidY(y) && this[next.X - 1, y] is Road)
                        bad = false;
                    if (ValidX(next.X + placeWidth) && ValidY(y) && this[next.X + placeWidth, y] is Road)
                        bad = false;
                }
            }
            return next;
        }
    }
}
