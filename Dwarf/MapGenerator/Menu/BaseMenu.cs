using MapGenerator.Controllers;
using MapGenerator.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator.Menu
{
    public abstract class BaseMenu
    {
        public abstract void FullRefresh();
        public abstract void Player_Changed(object sender, EventArgs e);

        public abstract void Reregister(BaseController controller);

        private int prevItems;
        public void PrintInventory(int dx, int dy, Player player)
        {
            int height = player.InventorySize + 1;
            int width = 12;
            
            using (new Colorife(ConsoleColor.Black, ConsoleColor.White))
            {
                for (int h = player.Inventory.Count + 2; h < prevItems + 1; h++)
                {
                    Console.SetCursorPosition(dx, dy + h);
                    for (int i = 0; i < width; i++)
                        Console.Write(" ");
                }
                Console.SetCursorPosition(dx, dy);
                Console.Write("Items {0}/{1}     ", player.Inventory.Count, player.InventorySize);
            }
            GraphicModule.DrawRectangle(dx, dy, width, height, ' ', ConsoleColor.Blue);
            prevItems = player.Inventory.Count;
        }

        public void PrintXp(int dx, int dy, Player player)
        {
            Console.SetCursorPosition(dx, dy);


            using (new Colorife(ConsoleColor.Green))
            {
                for (int i = 0; i < player.CurrentXp; i++)
                    Console.Write(" ");
            }



            using (new Colorife(ConsoleColor.DarkGreen))
            {
                for (int i = player.CurrentXp; i < player.MaxXp; i++)
                    Console.Write(" ");
            }
        }

        public void PrintHp(int dx, int dy, Player player)
        {
            Console.SetCursorPosition(dx, dy);
            using (new Colorife(ConsoleColor.Red, ConsoleColor.Red))
            {
                for (int i = 0; i < player.CurrentHp; i++)
                    Console.Write(" ");
            }
            using (new Colorife(ConsoleColor.DarkRed, ConsoleColor.DarkRed))
            {
                for (int i = player.CurrentHp; i < player.MaxHp; i++)
                    Console.Write(" ");
            }

        }

        public void PrintMp(int dx, int dy, Player player)
        {
            Console.SetCursorPosition(dx, dy);
            using (new Colorife(ConsoleColor.Blue))
            {
                for (int i = 0; i < player.CurrentMp; i++)
                    Console.Write(" ");
            }
            using (new Colorife(ConsoleColor.DarkBlue))
            {
                for (int i = player.CurrentMp; i < player.MaxMp; i++)
                    Console.Write(" ");
            }
        }

        public static int prevGold;
        public void PrintGold(int dx, int dy, Player player, bool clear = true)
        {
            Console.SetCursorPosition(dx, dy);
            int goldTarget = DataProvider.settings.GOLD_TARGET;
            int gold = player.Gold;
            int saveCost = DataProvider.settings.SAVE_COST;
            using (new Colorife(ConsoleColor.Yellow))
            {
                for (int i = 0; i < gold; i++)
                    Console.Write(" ");
            }
            int dg = saveCost - gold;
            using (new Colorife(ConsoleColor.DarkGray))
            {
                for (int i = 0; i < dg; i++)
                    Console.Write(" ");
            }
            int dary = goldTarget + saveCost - Math.Max(gold, saveCost);
            using (new Colorife(ConsoleColor.DarkYellow))
            {
                for (int i = 0; i < dary; i++)
                    Console.Write(" ");
            }
            int b = prevGold - Math.Max(gold, goldTarget + saveCost) + 3;
            if (clear)
                using (new Colorife(ConsoleColor.Black))
                {
                    for (int i = 0; i <= b; i++)
                        Console.Write(" ");
                }
            prevGold = player.Gold;
        }

        public void PrintInventory(Player player)
        {
            int dx = DataProvider.settings.MAP_WIDTH + ConsoleSettings.RIGHT_MENU_WIDTH + 3;
            int dy = 7;
            PrintInventory(dx, dy, player);
        }
    }
}
