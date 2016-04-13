using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator
{
    public struct Point : IComparable
    {
        public readonly int X, Y;
        public Point(int x, int y) {
            X = x;
            Y = y;
        }

        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }

        public int CompareTo(object obj)
        {
            var p2 = (Point)obj;
            if (X < p2.X)
                return -1;
            if (X > p2.X)
                return 1;
            if (Y < p2.Y)
                return -1;
            if (Y > p2.Y)
                return 1;
            return 0;
        }
    }
}
