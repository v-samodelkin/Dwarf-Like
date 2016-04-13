using MapGenerator.Maps;
using MapGenerator.MapObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using MapGenerator.Controllers;
using MapGenerator.Menu;
namespace MapGenerator
{
    public static class DataProvider
    {
        public static Settings settings { get; set; }

        static DataProvider()
        {
            settings = LoadSettings();
        }

        public static CavelMap GenerateCaveMap(Player player = null)
        {
            //Base
            var map = new CavelMap(settings.MAP_WIDTH, settings.MAP_HEIGHT);
            //Caves
            if (settings.CAVES)
                map.AddCaves(settings.CAVES_COUNT, settings.CAVES_LENGTH);
            //Clear
            if (settings.CAVES_CLEAR)
                map.ClearFromSmallRocks(settings.CAVES_CLEAR_SIZE);
            //Water
            if (settings.WATER)
                map.AddWater(settings.WATER_COUNT, settings.WATER_MIN_SIZE, settings.WATER_MAX_SIZE);
            //Gold
            if (settings.GOLD)
                map.AddGold(settings.GOLD_VALUE, settings.GOLD_COUNT);
            //Player
            if (settings.PLAYER)
                if (player == null)
                    map.AddPlayer(LoadPlayer());
            // Exit
            if (settings.EXIT)
                for (int i = 0; i < settings.EXIT_COUNT; i++ )
                    map.AddExit();
            return map;
        }

        public static CityMap GenerateShopMap(Player player = null)
        {
            var map = new CityMap(settings.MAP_WIDTH, settings.MAP_HEIGHT);




            //Road
            map.AddRoad(settings.CITY_ROAD_WIDTH, settings.CITY_ROAD_BRAKES, settings.CITY_ROAD_LINES);

            //Shops
            map.PlaceShop("PotionShop");

            //Player
            if (settings.PLAYER)
                if (player == null)
                    map.AddPlayer(LoadPlayer());
            return map;
        }


        public static BaseController GetController()
        {
            int controllers = 0;
            if (settings.CAVES)
                controllers++;
            if (settings.CITY)
                controllers++;

            if (controllers == 0)
                throw new ArgumentException("Не выбран режим игры. Пожалуйста, включите один в настройках: CAVES, CITY");
            if (controllers > 1)
                throw new ArgumentException("Выбрано сразу несколько режимов игры. Пожалуйста, включите только один: CAVES, CITY");

            if (settings.CAVES)
                return new CaveController(GenerateCaveMap());
            return new CityController(GenerateShopMap());
        }

        public static bool WritePlayer(Player player)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Player));
                File.Delete("Players.XML");
                using (var st = File.OpenWrite("Players.XML"))
                {
                    serializer.Serialize(st, player);
                }
                return true;
            }
            catch (Exception e)
            {
                string text = e.Message;
                return false;
            }
        }

        public static bool WriteSettings()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                using (var st = File.OpenWrite("Settings.XML"))
                {
                    serializer.Serialize(st, settings);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Settings LoadSettings()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                using (var st = File.OpenRead("Settings.XML"))
                {
                    return  (Settings)serializer.Deserialize(st);
                }
            }
            catch
            {
                return new Settings();
            }
        }

        public static Player LoadPlayer()
        {

            try {
                XmlSerializer serializer = new XmlSerializer(typeof(Player));
                using(var st =  File.OpenRead("Players.XML")) {
                    var player = serializer.Deserialize(st);
                    return (Player)player;
                }
            } 
            catch (Exception e) {
                string text = e.Message;
                return new Player();
            }
        }
    }
}
