using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator.MapObjects
{
    public abstract class MapObject
    {
        public readonly ConsoleColor BackgroundColor;
        public readonly ConsoleColor ForegroundColor;
        public readonly char Char;
        public MapObject(char Ch, ConsoleColor Back = ConsoleColor.Black, ConsoleColor Front = ConsoleColor.DarkGray)
        {
            Char = Ch;
            BackgroundColor = Back;
            ForegroundColor = Front;
        }

        public abstract MapObject Check();
    }
}
