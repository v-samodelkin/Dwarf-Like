using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator.Objects
{
    public class Rock : MapObject
    {
        public int Hp { get; set; }
        public Rock(int hp = 15) : base('#', ConsoleColor.DarkGray, ConsoleColor.DarkGray) {
            Hp = hp;
        }

        public override MapObject Check()
        {
            if (Hp > 0)
                return this;
            return new Earth();
        }
    }
}
