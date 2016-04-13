using MapGenerator.Controllers;
using MapGenerator.MapObjects;
using MapGenerator.Menu;
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


        private static BaseMenu menu;

        private static bool quit = false;
        public static BaseController gameController;
        static void Main(string[] args)
        {
            // Base Console
            Console.CursorVisible = false;
            Console.SetWindowSize(ConsoleSettings.WIDTH, ConsoleSettings.HEIGHT);
            Console.BufferWidth = ConsoleSettings.WIDTH;
            Console.BufferHeight = ConsoleSettings.HEIGHT;

            //Game Controller
            gameController = DataProvider.GetController();

            // Menu
            menu = gameController.GenerateMenu();
            menu.FullRefresh();
            gameController.Player.Changed += menu.Player_Changed;

            //Draw Map
            GraphicModule.DrawRectangle(ConsoleSettings.RIGHT_MENU_WIDTH, ConsoleSettings.UP_MENU_HEIGHT, gameController.BaseMap.Width, gameController.BaseMap.Height, '~', ConsoleColor.DarkYellow, ConsoleColor.DarkYellow);
            GraphicModule.HardDrawMap(gameController.BaseMap);

            // Delete Old Save
            DataProvider.WritePlayer(new Player());

            //Mainloop
            MainLoop();
        }

        public static void Restart()
        {
            gameController = DataProvider.GetController();
            menu = gameController.GenerateMenu();
            gameController.Player.Changed += menu.Player_Changed;
            menu.Player_Changed(new object(), EventArgs.Empty);
            GraphicModule.HardDrawMap(gameController.BaseMap);
            DataProvider.WritePlayer(new Player());
            GraphicModule.ConsoleMessage("");
        }



        static void MainLoop()
        {
            do
            {
                gameController.HandleInput(Console.ReadKey(true));
                GraphicModule.SoftDrawMap(gameController.BaseMap);
            }
            while (!quit);
        }

        public static void Save(string obj, bool flag)
        {
            GraphicModule.ConsoleMessage(
                flag ?
                String.Format("Сохранение {0} прошло успешно! Ура!", obj) :
                String.Format("СОХРАНЕНИЕ {0} НЕ УДАЛОСЬ! Возможно, не хватило прав на запись файла. Попробуйте перенести игру в другую папку или запустить от администратора (если не боитесь)", obj.ToUpper())
            );
        }
    }
}
