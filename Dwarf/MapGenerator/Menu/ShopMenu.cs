using MapGenerator.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MapGenerator.Menu
{
    public class ShopMenu : BaseMenu
    {
        private ShopController shopController;
        public ShopMenu(ShopController caveController)
        {
            this.shopController = caveController;
        }

        public override void Reregister(BaseController controller)
        {
            this.shopController = (ShopController)controller;
        }

        public override void FullRefresh()
        {
            PrintHp();
            PrintMp();
            PrintGold();
            PrintXp();
            PrintInventory();
        }

        public override void Player_Changed(object sender, EventArgs e)
        {
            FullRefresh();
        }

        public void PrintXp()
        {
            PrintXp(3, 4, shopController.Map.Player);
        }

        public void PrintHp()
        {
            PrintHp(3, 1, shopController.Map.Player);
        }

        public void PrintMp()
        {
            PrintMp(3, 2, shopController.Map.Player);
        }

        public void PrintGold()
        {
            PrintGold(3, 3, shopController.Map.Player);
        }

        public void PrintInventory()
        {
            PrintInventory(shopController.Map.Player);
        }
    }
}
