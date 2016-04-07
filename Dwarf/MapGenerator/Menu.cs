using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapGenerator
{
    public class Menu
    {
        private GameController gc;
        public Menu(GameController gc)
        {
            this.gc = gc;
            //using (new Colorife(ConsoleColor.White, ConsoleColor.White)) {
            //    for (int y = 1; y < Program.UP_MENU_HEIGHT; y++)
            //    {
            //        for (int x = Program.RIGHT_MENU_WIDTH; x < Program.RIGHT_MENU_WIDTH + gc.Map.Width; x++ )
            //        {
            //            Console.SetCursorPosition(x, y);
            //            Console.Write(".");
            //        }
            //    }
            //}

        }

        public void PrintXp()
        {
            Console.SetCursorPosition(3, 4);


            using (new Colorife(ConsoleColor.Green))
            {
                for (int i = 0; i < gc.Map.Player.CurrentXp; i++)
                    Console.Write(" ");
            }



            using (new Colorife(ConsoleColor.DarkGreen))
            {
                for (int i = gc.Map.Player.CurrentXp; i < gc.Map.Player.MaxXp; i++)
                    Console.Write(" ");
            }
        }



        public void PrintHp()
        {
            Console.SetCursorPosition(3, 1);
            using (new Colorife(ConsoleColor.Red, ConsoleColor.Red))
            {
                for (int i = 0; i < gc.Map.Player.CurrentHp; i++)
                    Console.Write(" ");
            }
            using (new Colorife(ConsoleColor.DarkRed, ConsoleColor.DarkRed))
            {
                for (int i = gc.Map.Player.CurrentHp; i < gc.Map.Player.MaxHp; i++)
                    Console.Write(" ");
            }
            
        }

        public void PrintMp()
        {
            Console.SetCursorPosition(3, 2);
            using (new Colorife(ConsoleColor.Blue))
            {
                for (int i = 0; i < gc.Map.Player.CurrentMp; i++)
                    Console.Write(" ");
            }
            using (new Colorife(ConsoleColor.DarkBlue))
            {
                for (int i = gc.Map.Player.CurrentMp; i < gc.Map.Player.MaxMp; i++)
                    Console.Write(" ");
            }
        }

        private int prevGold;
        public void PrintGold()
        {
            Console.SetCursorPosition(3, 3);
            using (new Colorife(ConsoleColor.Yellow))
            {
                for (int i = 0; i < gc.Map.Player.Gold; i++)
                    Console.Write(" ");
            }
            using (new Colorife(ConsoleColor.DarkYellow))
            {
                for (int i = 0; i < DataLoader.settings.GOLD_TARGET - gc.Map.Player.Gold; i++)
                    Console.Write(" ");
            }
            using (new Colorife(ConsoleColor.White))
            {
                for (int i = 0; i < DataLoader.settings.GOLD_TARGET + DataLoader.settings.SAVE_COST - Math.Max(DataLoader.settings.GOLD_TARGET, gc.Map.Player.Gold); i++)
                    Console.Write(" ");
            }
            
            using (new Colorife(ConsoleColor.Black))
            {
                for (int i = gc.Map.Player.Gold; i < prevGold; i++)
                    Console.Write(" ");
            }
            prevGold = gc.Map.Player.Gold;
        }
    }
}
