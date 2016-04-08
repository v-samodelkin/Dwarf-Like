using MapGenerator.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator
{
    public class Colorife : IDisposable
    {
        public readonly ConsoleColor PrevBack;
        public readonly ConsoleColor PrevFront;
        public Colorife(MapObject mo)
        {
            PrevBack = Console.BackgroundColor;
            PrevFront = Console.ForegroundColor;
            if (Console.BackgroundColor != mo.BackgroundColor)
                Console.BackgroundColor = mo.BackgroundColor;
            if (Console.ForegroundColor != mo.ForegroundColor)
                Console.ForegroundColor = mo.ForegroundColor;
        }
        public Colorife(ConsoleColor Back, ConsoleColor Front)
        {
            PrevBack = Console.BackgroundColor;
            PrevFront = Console.ForegroundColor;
            if (Console.BackgroundColor != Back)
                Console.BackgroundColor = Back;
            if (Console.ForegroundColor != Front)
                Console.ForegroundColor = Front;
        }

        public Colorife(ConsoleColor Back)
        {
            PrevBack = Console.BackgroundColor;
            PrevFront = Console.ForegroundColor;
            if (Console.BackgroundColor != Back)
                Console.BackgroundColor = Back;
        }

        public void Dispose()
        {
            if (Console.BackgroundColor != PrevBack)
                Console.BackgroundColor = PrevBack;
            if (Console.ForegroundColor != PrevFront)
                Console.ForegroundColor = PrevFront;
        }
    }
}
