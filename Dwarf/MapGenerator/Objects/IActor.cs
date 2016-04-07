using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator.Objects
{
    public interface IActor
    {
        MapObject ActWith(Earth mo);
        MapObject ActWith(Gold mo);
        MapObject ActWith(Player mo);
        MapObject ActWith(Rock mo);
        MapObject ActWith(Gravel mo);
        MapObject ActWith(Exit mo);
        MapObject ActWith(Water mo);
    }
}
