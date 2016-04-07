using MapGenerator.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
namespace MapGenerator
{
    public static class DataLoader
    {
        public static Settings settings { get; set; }

        static DataLoader()
        {
            settings = LoadSettings();
        }

        public static GlobalMap GenerateMap()
        {
            //Base
            var map = new GlobalMap(settings.MAP_WIDTH, settings.MAP_HEIGHT);

            //Caves
            if (settings.CAVES)
                map.AddCaves(settings.CAVES_COUNT, settings.CAVES_LENGTH);

            //Water
            if (settings.WATER)
                map.AddWater(settings.WATER_COUNT, settings.WATER_MIN_SIZE, settings.WATER_MAX_SIZE);

            //Gold
            if (settings.GOLD)
                map.AddGold(settings.GOLD_VALUE, settings.GOLD_COUNT);

            //Player
            if (settings.PLAYER)
                map.AddPlayer(LoadPlayer());



            // Exit
            if (settings.EXIT)
                map.AddExit();

            return map;
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
                    var player = (Player)serializer.Deserialize(st);
                    return player;
                }
            } catch (Exception e) {
                return new Player();
            }
        }
    }
}
