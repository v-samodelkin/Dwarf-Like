using MapGenerator.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MapGenerator.Menu
{
    public class CityMenu : BaseMenu
    {
        private CityController shopController;
        public CityMenu(CityController caveController)
        {
            this.shopController = caveController;
        }

        public override void Reregister(BaseController controller)
        {
            var cc = (CityController)controller;
            this.shopController = cc;
            cc.Menu = this;
        }

        public override void FullRefresh()
        {
            PrintHp();
            PrintMp();
            PrintGold();
            PrintXp();
            PrintInventory();
            PrintGroundInfo();
        }

        public override void Player_Changed(object sender, EventArgs e)
        {
            FullRefresh();
        }

        public void PrintXp()
        {
            PrintXp(AfterMapX, 3, shopController.Map.Player);
        }

        public void PrintHp()
        {
            PrintHp(AfterMapX, 1, shopController.Map.Player);
        }

        public void PrintMp()
        {
            PrintMp(AfterMapX, 2, shopController.Map.Player);
        }

        public void PrintGold()
        {
            PrintGold(AfterMapX, ConsoleSettings.HEIGHT - 3, shopController.Map.Player);
        }

        public void PrintInventory()
        {
            PrintInventory(shopController.Map.Player);
        }

        public void PrintGroundInfo()
        {
            PrintGroundInfo(shopController.Map.Player);
        }
    }
}
