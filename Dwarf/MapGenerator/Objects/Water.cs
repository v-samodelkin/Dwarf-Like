using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator.Objects
{
   public class Water : MapObject, IFree
    {
       public Water() : base('~', ConsoleColor.Blue, ConsoleColor.DarkBlue) { }

       public override MapObject Check()
       {
           return this;
       }
    }
}
