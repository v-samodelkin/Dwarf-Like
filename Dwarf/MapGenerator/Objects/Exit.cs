using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator.Objects
{
    public class Exit : MapObject, IInteractive
    {
        public Exit() : base('E', ConsoleColor.DarkYellow, ConsoleColor.DarkMagenta) { }
        public override MapObject Check()
        {
            return this;
        }
    }
}
