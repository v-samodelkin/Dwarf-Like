﻿using System;
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
        public int MAP_WIDTH = 200;
        public int MAP_HEIGHT = 60;
        // Caves
        public bool CAVES = true;
        public int CAVES_COUNT = 33;
        public int CAVES_LENGTH = 800;

        // Gold
        public bool GOLD = true;
        public int GOLD_VALUE = 1;
        public int GOLD_COUNT = 30;
        public int GOLD_TARGET = 20;
        public int SAVE_COST = 7;

        // Player
        public bool PLAYER = true;

        // Water
        public bool WATER = true;
        public int WATER_COUNT = 15;
        public int WATER_MIN_SIZE = 1;
        public int WATER_MAX_SIZE = 120;
        public bool FLOOD = true;

        //Exit
        public bool EXIT = true;

        public Settings() { }
    }
}