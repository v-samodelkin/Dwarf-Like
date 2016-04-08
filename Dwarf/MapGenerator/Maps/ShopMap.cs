using MapGenerator.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator.Maps
{
    public class ShopMap : BaseMap
    {
        public ShopMap(int width, int height) : base(width, height)
        {
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    Map[x, y] = new Earth();
        }
    }
}
