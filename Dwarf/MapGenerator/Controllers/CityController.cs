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
    public class CityController : BaseController
    {
        public CityMap Map { get; set; }
        public CityMenu Menu { get; set; }
        public CityController(CityMap map)
        {
            Map = map;
            Map.Player.Changed += Player_Changed;
            foreach (var cell in map.ShopCells)
                cell.Activated += cell_Activated;
        }

        void cell_Activated(object sender, EventArgs e)
        {
            var shopCell = sender as ShopCell;
            if (shopCell != null)
            {
                ModalForm = shopCell.GetModal(Player);
                GraphicModule.DrawModal(Map, ModalForm);
            }
        }

        public override void HandleInput(ConsoleKeyInfo key)
        {
            if (ModalForm != null)
            {
                if (key.Key == ConsoleKey.Escape) {
                    GraphicModule.RemoveModal(Map, ModalForm);
                    ModalForm = null;
                }
                else
                {
                    ModalForm.HandleInput(key);
                    return;
                }

            }
            if (HandleMovement(key))
                return;
            if (HandleInventory(key))
                return;
            switch (key.Key)
            {
                case ConsoleKey.P:
                    TrySavePlayer();
                    break;
                case ConsoleKey.Q:
                    bool save = TrySavePlayer(true);
                    if (save)
                    {
                        DataProvider.settings.CAVES = true;
                        DataProvider.settings.CITY = false;
                        Program.Restart();
                    }
                    else
                    {
                        GraphicModule.ConsoleMessage("Не удалось сохраниться! Пожалуйста, попробуйте сохраниться через Р, там будет подсказка.");
                    }
                    break;

            }
        }

        public bool TrySavePlayer(bool silence = false)
        {
            bool afterall = DataProvider.WritePlayer(Map.Player);
            if (!silence)
                Program.Save("персонажа", afterall);
            return afterall;
        }

        public override void Player_Changed(object sender, EventArgs e)
        {
            return;
        }

        public override Player Player
        {
            get
            {
                return Map.Player;
            }
        }

        public override BaseMenu GenerateMenu()
        {
            Menu = new CityMenu(this);
            return Menu;
        }

        public override BaseMap BaseMap
        {
            get {
                return (BaseMap)Map;
            }
        }

        public override void Turn()
        {
            Menu.Turn(Player);
        }
    }
}
