using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator.Items
{
    public abstract class Item
    {
        public static Dictionary<Type, int> UIds;

        public abstract int Uid { get; }
    }
}
