using MapGenerator.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MapGenerator
{
    class Program
    {
        // Base
        public const int WIDTH = 240;
        public const int HEIGHT = 70;
        // Menu
        public const int UP_MENU_HEIGHT = 7;
        public const int RIGHT_MENU_WIDTH = 4;

        private static Menu menu;

        private static bool quit = false;
        static void Main(string[] args)
        {
            // Base Console
            Console.CursorVisible = false;
            Console.SetWindowSize(WIDTH, HEIGHT);
            Console.BufferWidth = WIDTH;
            Console.BufferHeight = HEIGHT;

            var gameController = new GameController(DataLoader.GenerateMap());
            // Menu
            //PrintSample();
            menu = new Menu(gameController);
            Player_Changed(new object(), EventArgs.Empty);
            gameController.Map.Player.Changed += Player_Changed;
            DrawAround(gameController.Map);

            //DataLoader.WritePlayer(gameController.Map.Player);

            //Draw Map
            HardDrawMap(gameController.Map);

            gameController.TrySavePlayer(true);
            //Mainloop
            MainLoop(gameController);
        }

        static void Player_Changed(object sender, EventArgs e)
        {
            menu.PrintHp();
            menu.PrintMp();
            menu.PrintGold();
            menu.PrintXp();
        }

        static void MainLoop(GameController gc)
        {
            do
            {
                gc.HandleInput(Console.ReadKey(true));
                SoftDrawMap(gc.Map);
            }
            while (!quit);
        }

        // Отрисовка карты
        public static void HardDrawMap(GlobalMap map) {
            Console.SetCursorPosition(RIGHT_MENU_WIDTH, UP_MENU_HEIGHT);
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    using (new Colorife(map[x, y]))
                    {
                        Console.Write(map[x, y].Char);
                    }
                }
                Console.SetCursorPosition(RIGHT_MENU_WIDTH, UP_MENU_HEIGHT + y + 1);
            }
        }

        // Отрисовка зафиксированных изменений
        public static void SoftDrawMap(GlobalMap map)
        {
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    if (map.Retouch(x, y))
                    {
                        Console.SetCursorPosition(RIGHT_MENU_WIDTH + x, UP_MENU_HEIGHT + y);
                        using (new Colorife(map[x, y]))
                        {
                            Console.Write(map[x, y].Char);
                        }
                    }
                }
            }
        }
        // Примеры цветов
        static void PrintSample()
        {
            for (int l = 0; l < 4; l++)
            {
                for (int c = 0; c < 16; c++)
                {
                    using (new Colorife((ConsoleColor)c))
                    {
                        for (int w = 0; w < 5; w++)
                        {
                            Console.Write(".");
                        }
                    }
                }
                Console.WriteLine();
            }
            for (int c = 0; c < 16; c++)
            {
                var color = (ConsoleColor)c;
                Console.WriteLine(color.ToString());
            }
        }

        // Рамка
        static void DrawAround(GlobalMap map)
        {
            using (new Colorife(ConsoleColor.DarkYellow, ConsoleColor.DarkYellow))
            {
                Console.SetCursorPosition(RIGHT_MENU_WIDTH - 1, UP_MENU_HEIGHT - 1);
                for (int i = 0; i < map.Width + 2; i++)
                    Console.Write("~");
                Console.SetCursorPosition(RIGHT_MENU_WIDTH - 1, UP_MENU_HEIGHT + map.Height);
                for (int i = 0; i < map.Width + 2; i++)
                    Console.Write("~");
                for (int i = 0; i < map.Height; i++)
                {
                    Console.SetCursorPosition(RIGHT_MENU_WIDTH - 1, UP_MENU_HEIGHT + i);
                    Console.Write("~");
                    Console.SetCursorPosition(RIGHT_MENU_WIDTH + map.Width, UP_MENU_HEIGHT + i);
                    Console.Write("~");
                }
            }
        }

        public static void ConsoleMessage(string message) {
            using (new Colorife(ConsoleColor.Black, ConsoleColor.Gray)) {
                Console.SetCursorPosition(RIGHT_MENU_WIDTH, HEIGHT - 2);
                Console.Write(message);
                for (int i = 0; i < WIDTH - RIGHT_MENU_WIDTH - message.Length; i++)
                    Console.Write(" ");
            }
        }

        public static void Save(string obj, bool flag)
        {
            ConsoleMessage(
                flag ?
                String.Format("Сохранение {0} прошло успешно! Ура!", obj) :
                String.Format("СОХРАНЕНИЕ {0} НЕ УДАЛОСЬ! Возможно, не хватило прав на запись файла. Попробуйте перенести игру в другую папку или запустить от администратора (если не боитесь)", obj.ToUpper())
            );
        }
    }
}
