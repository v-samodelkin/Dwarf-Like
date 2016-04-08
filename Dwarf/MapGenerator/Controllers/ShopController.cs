using MapGenerator.MapObjects;
using MapGenerator.Maps;
using MapGenerator.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator.Controllers
{
    public class ShopController : BaseController
    {
        public ShopMap Map { get; set; }

        public ShopController(ShopMap map)
        {
            Map = map;
        }

        public override void HandleInput(ConsoleKeyInfo key)
        {
            if (HandleMovement(key))
                return;
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
            return new ShopMenu(this);
        }

        public override BaseMap BaseMap
        {
            get {
                return (BaseMap)Map;
            }
        }

        public override void Turn()
        {
            return;
        }
    }
}
