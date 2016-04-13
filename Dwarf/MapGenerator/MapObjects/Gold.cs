using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator.MapObjects
{
    public class Gold : MapObject, IFree
    {
        public int Value { get; set; }
        public Gold(int value) : base('$', ConsoleColor.Black, ConsoleColor.Yellow) {
            Value = value;
        }

        public override MapObject Check()
        {
            return this;
        }

        public override string[] Info
        {
            get
            {
                return String.Format(" Кусок золота. \n Стоимость: {0} монет", Value).Split('\n');
            }
        }
    }
}
