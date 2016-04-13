using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator
{
    [Serializable()]
    public class Settings
    {
        // Base
        public int MAP_WIDTH = 180;
        public int MAP_HEIGHT = 49;
        public int SAVE_COST = 7;

        //Location
        public bool CITY = true;
        public bool CAVES = false;

        // Caves
        public int CAVES_COUNT = 28;
        public int CAVES_LENGTH = 900;
        public bool CAVES_CLEAR = true;
        public int CAVES_CLEAR_SIZE = 5;

        // Gold
        public bool GOLD = true;
        public int GOLD_VALUE = 1;
        public int GOLD_COUNT = 30;
        public int GOLD_TARGET = 20;

        // Player
        public bool PLAYER = true;

        // Water
        public bool WATER = true;
        public int WATER_COUNT = 15;
        public int WATER_MIN_SIZE = 1;
        public int WATER_MAX_SIZE = 120;
        public bool WATER_FLOOD = true;

        //Exit
        public bool EXIT = true;
        public int EXIT_COUNT = 2;


        //City
        
        public int CITY_ROAD_WIDTH = 5;
        public int CITY_ROAD_BRAKES = 2;
        public int CITY_ROAD_LINES = 3;

        public Settings() { }
    }
}
