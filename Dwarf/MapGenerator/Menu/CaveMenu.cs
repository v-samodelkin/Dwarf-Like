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
 	        this.caveController = (CaveController)controller;
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
            PrintXp(3, 4, caveController.Map.Player);
        }

        public void PrintHp()
        {
            PrintHp(3, 1, caveController.Map.Player);
        }

        public void PrintMp()
        {
            PrintMp(3, 2, caveController.Map.Player);
        }

        public void PrintGold()
        {
            PrintGold(3, 3, caveController.Map.Player);
        }

        public void PrintInventory()
        {
            PrintInventory(caveController.Map.Player);
        }
    }
}
