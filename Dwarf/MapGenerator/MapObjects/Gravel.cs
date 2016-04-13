using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator.MapObjects
{
    public class Gravel : MapObject
    {
        public int Hp { get; set; }
        public Gravel(int hp = 10) : base('#', ConsoleColor.Gray, ConsoleColor.Gray) {
            Hp = hp;
        }

        public override MapObject Check()
        {
            if (Hp > 0)
                return this;
            return new Earth();
        }

        public override string[] Info
        {
            get
            {
                return String.Format(" Кусок гравия").Split('\n');
            }
        }
    }
}
