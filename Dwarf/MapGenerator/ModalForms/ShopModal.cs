using MapGenerator.Items;
using MapGenerator.MapObjects;
using MapGenerator.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator.ModalForms
{
    public class ShopModal : ModalForm
    {
        public List<Item> Items { get; set; }
        public ShopModal(ShopCell shopCell, Player player) : base(40, 20, player)
        {
            Items = shopCell.GetItems();
        }

        public override void Action(int option)
        {
            if (option >= 0 && option < Items.Count) {
                if (Player.Gold >= Items[option].Cost)
                {
                    Player.SpendGold(Items[option].Cost);
                    Player.GetItem(Items[option]);
                    Items.RemoveAt(option);
                    Draw(Dx, Dy);
                }
            }
            
        }

        ////////////////// Drawing /////////////////////////////

        public override void Draw(int dx, int dy)
        {
            base.Draw(dx, dy);
            GraphicModule.ModalWriteCenter(this, 1, "Магазин Зелий");
            GraphicModule.ModalWriteCenter(this, 6, String.Concat(Enumerable.Repeat("_", Width)));
            GraphicModule.ModalWriteCenter(this, 7, "Товар:");
            for (int i = 0; i < Items.Count; i++)
            {
                GraphicModule.ModalWriteCenter(this, 8 + i, String.Format("{0}){1}", i + 1, Items[i].Name));
            }
        }
    }
}
