﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator.MapObjects
{
    public class Gravestone : MapObject
    {
        public Gravestone(Player player) : base('T', ConsoleColor.DarkGray, ConsoleColor.DarkRed) { }

        public override MapObject Check()
        {
            return this;
        }

        public override string[] Info
        {
            get
            {
                return String.Format(" Могильный камень игрока").Split('\n');
            }
        }
    }
}
