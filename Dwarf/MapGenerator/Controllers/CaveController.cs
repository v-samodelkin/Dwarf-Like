using MapGenerator.Maps;
using MapGenerator.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapGenerator.Menu;

namespace MapGenerator.Controllers
{
    public class CaveController : BaseController
    {
        public int TurnNumber;
        public CavelMap Map { get; set; }
        public override BaseMap BaseMap {
            get
            {
                return Map as BaseMap;
            }

        }

        public override Player Player
        {
            get
            {
                return Map.Player;
            }
        }

        public CaveController(CavelMap map)
        {
            Map = map;
            map.Player.Changed += Player_Changed;
        }

        public override void Player_Changed(object sender, EventArgs e)
        {
            dynamic ch = Map.Player.Check();
        }

        public override void HandleInput(ConsoleKeyInfo key)
        {
            if (HandleMovement(key))
                return;
            switch (key.Key)
            {
                case ConsoleKey.R:
                    Program.Restart();
                    break;
                case ConsoleKey.P:
                    TrySavePlayer();
                    break;
                case ConsoleKey.O:
                    TrySaveSettings();
                    break;
                case ConsoleKey.G:
                    Map.Player.SpendGold(-10);
                    break;
            }
        }

        public void TrySavePlayer(bool forced = false)
        {
            if (forced)
            {
                DataProvider.WritePlayer(new Player());
                return;
            }
            if (Map.Player.Home())
                if (Map.Player.Gold >= DataProvider.settings.SAVE_COST) {
                    Map.Player.Gold -= DataProvider.settings.SAVE_COST;
                    bool afterall = DataProvider.WritePlayer(Map.Player);
                    Program.Save("персонажа", afterall);
                    if (afterall)
                        Map.Player.SpendGold(0);
                    else
                        Map.Player.Gold += DataProvider.settings.SAVE_COST;
                }
                else
                {
                    GraphicModule.ConsoleMessage(String.Format("Недостаточно денег! Нужно {0} монет, а у вас {1}.", DataProvider.settings.SAVE_COST, Map.Player.Gold));
                }
            else
                GraphicModule.ConsoleMessage(String.Format("Здесь сохранения нет, ищите выход на карте [а ещё нужно {0} монет]", DataProvider.settings.SAVE_COST));
        }

        public void TrySaveSettings()
        {
            if (Map.Player.Home())
                Program.Save("настроек", DataProvider.WriteSettings());
            else
                GraphicModule.ConsoleMessage("Здесь сохранения нет, ищите выход на карте");
        }

        public override void Turn()
        {
            TurnNumber++;
            Map.Collapse(Math.Min(TurnNumber/50, 3));
            if (DataProvider.settings.WATER_FLOOD)
                Map.Flood(TurnNumber / 10);
        }

        public override BaseMenu GenerateMenu()
        {
            return new CaveMenu(this);
        }
    }
}
