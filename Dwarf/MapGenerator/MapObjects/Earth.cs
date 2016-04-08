using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MapGenerator.MapObjects
{
    public class Earth : MapObject, IFree
    {
        public Earth(): base('.', ConsoleColor.Black, ConsoleColor.Gray) {}

        public override MapObject Check()
        {
            return this;
        }
    }
}
