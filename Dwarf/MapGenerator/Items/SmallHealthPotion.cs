using MapGenerator.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator.Items
{
    public class SmallHealthPotion : Item 
    {
        public const int Effect = 3;
        private bool used;
        public override int Uid
        {
            get {
                return "SmallHealthPotion".GetHashCode();
            }
        }

        public override bool Breaked()
        {
            return used;
        }

        public override void Activate(MapObject mo = null)
        {
            if (!used)
            {
                var obj = mo as Player;
                if (obj == null)
                    throw new ArgumentException("На этот объект нельзя использовать зелье");
                obj.Hp.Current += Effect;
            }
            used = true;
        }

        public override string Name
        {
            get { return "Слабое зелье жизни"; }
        }

        public override int Cost
        {
            get 
            {
                return 10;
            }
        }
    }
}
