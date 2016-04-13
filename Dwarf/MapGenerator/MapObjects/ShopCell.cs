using MapGenerator.Items;
using MapGenerator.ModalForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator.MapObjects
{
    public class ShopCell : MapObject, IInteractive
    {
        public ShopCell(char ch = 'S', ConsoleColor Back = ConsoleColor.Black, ConsoleColor Front = ConsoleColor.White)
            : base(ch, Back, Front)
        {
        }

        public void Activate()
        {

        }

        public List<Item> GetItems()
        {
            return new List<Item>() {
                new SmallHealthPotion(),
                new SmallHealthPotion(),
                new SmallHealthPotion(),
                new SmallHealthPotion(),
                new SmallHealthPotion(),
                new SmallHealthPotion(),
                new SmallHealthPotion(),
                new SmallHealthPotion(),
                new SmallHealthPotion(),
            };
        }

        public override MapObject Check()
        {
            return this;
        }

        public event ActivatedEventHandler Activated;

        public void OnActivated(EventArgs e)
        {
            if (Activated != null)
                Activated(this, e);
        }

        public override string[] Info
        {
            get
            {
                return String.Format(" Магазин. \n {0} - клетка.", Char).Split('\n');
            }
        }

        public ModalForm GetModal(Player player)
        {
            return new ShopModal(this, player);
        }
    }
}
