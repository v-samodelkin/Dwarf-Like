using MapGenerator.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MapGenerator.MapObjects
{
    [XmlInclude(typeof(SmallHealthPotion))]
    public class Player : MapObject, IActor
    {
        public delegate void ChangedEventHandler(object sender, EventArgs e);
        public event ChangedEventHandler Changed;
        [XmlIgnore]
        public MapObject Ground { get; set; }
        public int Gold { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int InventorySize { get; set; }
        public Attribute Hp { get; set; }
        public Attribute Mp { get; set; }
        public Attribute Vitality { get; set; }
        public Attribute Starve { get; set; }
        public Attribute Thirst { get; set; }
        public Attribute Xp { get; set; }
        public List<Item> Inventory { get; set; }
        public Player() : base('@', ConsoleColor.Black, ConsoleColor.Green) {
            Ground = new Earth();
            Hp = new Attribute(40, 50);
            Mp = new Attribute(20, 30);
            Vitality = new Attribute(90, 100);
            Xp = new Attribute(0, 100);
            Thirst = new Attribute(0, 100);
            Starve = new Attribute(0, 100);
            Name = "Antoto";
            InventorySize = 10;
            Inventory = new List<Item>();
        }

        public void Activate()
        {
            var interactive = Ground as IInteractive;
            if (interactive == null)
                return;
            interactive.OnActivated(EventArgs.Empty);
        }

        public void SpendGold(int count)
        {
            if (count > Gold)
                throw new ArgumentOutOfRangeException("Нету столько золота");
            Gold -= count;
            OnChanged(EventArgs.Empty);
        }

        protected virtual void OnChanged(EventArgs e)
        {
            if (Changed != null)
                Changed(this, e);
        }

        public void UseItem(int num)
        {
            num--;
            if (num < 0 || num >= Inventory.Count)
                return;
            Inventory[num].Activate(this);
            if (Inventory[num].Breaked())
                Inventory.RemoveAt(num);
            Changed(this, EventArgs.Empty);
        }

        public void GetItem(Item item)
        {
            Inventory.Add(item);
            Changed(this, EventArgs.Empty);
        }

        public bool Home()
        {
            return (Ground is Exit);
        }


        ///////////////////////////////////////////////////////////////////

        public MapObject ActWith(Gold mo)
        {
            Gold += mo.Value;
            Xp.Current += mo.Value;
            OnChanged(EventArgs.Empty);
            return new Earth();
        }

        public MapObject ActWith(Player mo)
        {
            return mo.Check();
        }

        public MapObject ActWith(Rock mo)
        {
            mo.Hp--;
            if (mo.Hp < 5)
            {
                Hp.Current--;
                if (mo.Hp == 0)
                    Xp.Current++;
                OnChanged(EventArgs.Empty);
            }

            
            return mo.Check();
        }

        public MapObject ActWith(Earth mo)
        {
            return mo.Check();
        }

        public override MapObject Check()
        {
            if (Hp.Current > 0)
                return this;
            return new Gravestone(this);
        }


        public MapObject ActWith(Gravel mo)
        {
            mo.Hp--;
            if (mo.Hp < 3)
            {
                Hp.Current--;
                if (mo.Hp == 0)
                    Xp.Current++;
                OnChanged(EventArgs.Empty);
            }


            return mo.Check();
        }


        public MapObject ActWith(Exit mo)
        {
            return mo.Check();
        }


        public MapObject ActWith(Water mo)
        {
            Hp.Current--;
            OnChanged(EventArgs.Empty);
            return mo.Check();
        }


        public MapObject ActWith(Road mo)
        {
            return mo.Check();
        }


        public MapObject ActWith(ShopCell mo)
        {
            mo.Activate();
            return mo;
        }

        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        public override string[] Info
        {
            get
            {
                return String.Format(" Игрок {0}", Name).Split('\n');
            }
        }
    }
}
