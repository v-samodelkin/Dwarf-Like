using MapGenerator.MapObjects;
using MapGenerator.Maps;
using MapGenerator.Menu;
using MapGenerator.ModalForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator.Controllers
{
    public abstract class BaseController
    {
        public abstract void HandleInput(ConsoleKeyInfo key);
        public abstract void Player_Changed(object sender, EventArgs e);
        public abstract Player Player {get; }
        public abstract BaseMenu GenerateMenu();
        public abstract BaseMap BaseMap { get;  }
        public abstract void Turn();
        public ModalForm ModalForm { get; set; }

        public static Dictionary<ConsoleKey, Point> movement = new Dictionary<ConsoleKey, Point>() {
            {ConsoleKey.W, new Point(0, -1) },
            {ConsoleKey.A, new Point(-1, 0) },
            {ConsoleKey.S, new Point(0, 1) },
            {ConsoleKey.D, new Point(1, 0) },

            {ConsoleKey.UpArrow, new Point(0, -1) },
            {ConsoleKey.LeftArrow, new Point(-1, 0) },
            {ConsoleKey.DownArrow, new Point(0, 1) },
            {ConsoleKey.RightArrow, new Point(1, 0) },
        };
        public void MovePlayer(Point delta)
        {
            Point pos;
            try
            {
                pos = BaseMap.PlayerCell;
            }
            catch (IndexOutOfRangeException e)
            {
                string text = e.Message;
                return;
            }
            
            var newPos = pos + delta;
            if (!BaseMap.ValidP(newPos))
                return;
            dynamic obj = BaseMap[newPos.X, newPos.Y];
            var result = BaseMap.Player.ActWith(obj);
            if (result != BaseMap[newPos.X, newPos.Y])
                BaseMap[newPos.X, newPos.Y] = result;
            if (result is IFree || result is IInteractive)
            {
                BaseMap.PlayerMove(newPos);
            }
        }

        public bool HandleMovement(ConsoleKeyInfo key) {
            switch (key.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.A:
                case ConsoleKey.S:
                case ConsoleKey.D:
                case ConsoleKey.UpArrow:
                case ConsoleKey.DownArrow:
                case ConsoleKey.LeftArrow:
                case ConsoleKey.RightArrow:
                    MovePlayer(movement[key.Key]);
                    Turn();
                    return true;
                case ConsoleKey.R:
                    DataProvider.settings.CAVES = false;
                    DataProvider.settings.CITY = true;
                    Program.Restart();
                    return true;
                case ConsoleKey.E:
                    Player.Activate();
                    return true;
                default:
                    return false;
            }
        }

        public bool HandleInventory(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.D0:
                case ConsoleKey.D1:
                case ConsoleKey.D2:
                case ConsoleKey.D3:
                case ConsoleKey.D4:
                case ConsoleKey.D5:
                case ConsoleKey.D6:
                case ConsoleKey.D7:
                case ConsoleKey.D8:
                case ConsoleKey.D9:
                    Player.UseItem(int.Parse(key.KeyChar.ToString()));
                    return true;
                case ConsoleKey.G:
                    Player.SpendGold(-10);
                    return true;
            }
            return false;
        }

    }
}
