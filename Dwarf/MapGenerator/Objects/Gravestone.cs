using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator.Objects
{
    public class Gravestone : MapObject
    {
        public Gravestone(Player player) : base('T', ConsoleColor.DarkGray, ConsoleColor.Gray) { }

        public override MapObject Check()
        {
            return this;
        }
    }
}
