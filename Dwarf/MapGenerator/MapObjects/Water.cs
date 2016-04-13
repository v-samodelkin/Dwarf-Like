using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator.MapObjects
{
   public class Water : MapObject, IFree
    {
       public Water() : base('~', ConsoleColor.Blue, ConsoleColor.DarkBlue) { }

       public override MapObject Check()
       {
           return this;
       }

       public override string[] Info
       {
           get
           {
               return String.Format(" Вода. \n Холодная и мокрая. \n Брр.").Split('\n');
           }
       }
    }
}
