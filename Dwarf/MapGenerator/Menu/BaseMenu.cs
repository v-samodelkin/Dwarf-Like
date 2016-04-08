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

        public int AfterMapX
        {
            get
            {
                return DataProvider.settings.MAP_WIDTH + ConsoleSettings.RIGHT_MENU_WIDTH + 2;
            }
        }

        private int prevItems;
        public void PrintInventory(int dx, int dy, Player player)
        {
            int height = player.InventorySize + 1;
            int width = 20;
            
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
            PrintAttribute(dx, dy, ConsoleColor.Green, ConsoleColor.DarkGreen, player.Xp);
        }

        public void PrintHp(int dx, int dy, Player player)
        {
            PrintAttribute(dx, dy, ConsoleColor.Red, ConsoleColor.DarkRed, player.Hp);
        }

        public void PrintMp(int dx, int dy, Player player)
        {
            PrintAttribute(dx, dy, ConsoleColor.Blue, ConsoleColor.DarkBlue, player.Mp);
        }

        public static int prevGold;
        public void PrintGold(int dx, int dy, Player player, bool clear = true)
        {
            int counter = ConsoleSettings.BAR_WIDTH;
            Action act = () =>
            {
                if (counter <= 0)
                {
                    Console.SetCursorPosition(dx, dy - 1);
                    dy--;
                    counter = ConsoleSettings.BAR_WIDTH;
                }
            };
            
            Console.SetCursorPosition(dx, dy);
            int goldTarget = DataProvider.settings.GOLD_TARGET;
            int gold = player.Gold;
            int saveCost = DataProvider.settings.SAVE_COST;
            using (new Colorife(ConsoleColor.Yellow))
            {
                for (int i = 0; i < gold; i++)
                {
                    Console.Write(" ");
                    counter--; act();
                }
            }
            int dg = saveCost - gold;
            using (new Colorife(ConsoleColor.DarkGray))
            {
                for (int i = 0; i < dg; i++)
                {
                    Console.Write(" ");
                    counter--; act();
                }
            }
            int dary = goldTarget + saveCost - Math.Max(gold, saveCost);
            using (new Colorife(ConsoleColor.DarkYellow))
            {
                for (int i = 0; i < dary; i++)
                {
                    Console.Write(" ");
                    counter--; act();
                }
            }
            int b = prevGold - Math.Max(gold, goldTarget + saveCost) + 3;
            if (clear)
                using (new Colorife(ConsoleColor.Black))
                {
                    for (int i = 0; i <= b; i++)
                    {
                        Console.Write(" ");
                        counter--; act();
                    }
                }
            prevGold = player.Gold;
        }

        public void PrintInventory(Player player)
        {
            int dx = DataProvider.settings.MAP_WIDTH + ConsoleSettings.RIGHT_MENU_WIDTH + 3;
            int dy = 7;
            PrintInventory(dx, dy, player);
        }

        public void PrintAttribute(int dx, int dy, ConsoleColor Current, ConsoleColor Max, Attribute attribute, ConsoleColor Text = ConsoleColor.White)
        {
            Console.SetCursorPosition(dx, dy);
            var att = DivideAttribute(attribute);
            int center = ConsoleSettings.BAR_WIDTH / 2;
            var currentT = String.Format("{0} / ", attribute.Current);
            var maxT = attribute.Max.ToString();
            int start = center - currentT.Length;
            var message = currentT + maxT;
            using (new Colorife(Current, Text))
            {
                for (int i = 0; i < att.X; i++)
                    if (i >= start && (i - start) < message.Length)
                        Console.Write(message[i - start]);
                    else
                        Console.Write(" ");
                    
            }
            using (new Colorife(Max, Text))
            {
                for (int i = att.X; i < att.X + att.Y; i++)
                    if (i >= start && (i - start) < message.Length)
                        Console.Write(message[i - start]);
                    else
                        Console.Write(" ");
            }
        }

        public Point DivideAttribute(Attribute att)
        {
            int able = (int)Math.Min(att.Current * ConsoleSettings.BAR_WIDTH / att.Max, att.Max);
            return new Point(able, ConsoleSettings.BAR_WIDTH - able);
        }
    }
}
