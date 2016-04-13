using MapGenerator.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapGenerator.Menu
{
    public class CaveMenu : BaseMenu
    {
        private CaveController caveController;
        public CaveMenu(CaveController caveController) {
            this.caveController = caveController;
        }

        public override void Reregister(BaseController controller)
        {
            var cc = (CaveController)controller;
            this.caveController = cc;
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
            PrintXp(AfterMapX, 3, caveController.Map.Player);
        }

        public void PrintHp()
        {
            PrintHp(AfterMapX, 1, caveController.Map.Player);
        }

        public void PrintMp()
        {
            PrintMp(AfterMapX, 2, caveController.Map.Player);
        }

        public void PrintGold()
        {
            PrintGold(AfterMapX, ConsoleSettings.HEIGHT - 3, caveController.Map.Player);
        }

        public void PrintInventory()
        {
            PrintInventory(caveController.Map.Player);
        }

        public void PrintGroundInfo()
        {
            PrintGroundInfo(caveController.Map.Player);
        }
    }
}
