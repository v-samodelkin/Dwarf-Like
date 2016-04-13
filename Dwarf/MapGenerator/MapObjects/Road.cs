using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator.MapObjects
{
    public class Road : MapObject, IFree
    {
        public Road() : base('░', ConsoleColor.Black, ConsoleColor.White) { }

        public override MapObject Check()
        {
            return this;
        }

        public override string[] Info
        {
            get
            {
                return String.Format(" Дорога. \n Ускоряет движение.").Split('\n');
            }
        }
    }
}
