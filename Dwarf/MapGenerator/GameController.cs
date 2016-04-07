using MapGenerator.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator
{
    public class GameController
    {
        public static int TurnNumber;
        public static List<String> after = new List<String>() {
            "GAME OVER!", "LEAVE ME ALONE!", "I ad DEAD!", "Boooo!", "Where is my graveyard...", "I... whant... sleep", "Helo from Zombie!", "Braaaains!",
            "Hey. It is REALLY end of the game. Not joke. Just give me some more time for scoreboard and other things"
        };
        public GlobalMap Map { get; set; }
        public static Dictionary<ConsoleKey, Point> movement = new Dictionary<ConsoleKey,Point>() {
            {ConsoleKey.W, new Point(0, -1) },
            {ConsoleKey.A, new Point(-1, 0) },
            {ConsoleKey.S, new Point(0, 1) },
            {ConsoleKey.D, new Point(1, 0) },

            {ConsoleKey.UpArrow, new Point(0, -1) },
            {ConsoleKey.LeftArrow, new Point(-1, 0) },
            {ConsoleKey.DownArrow, new Point(0, 1) },
            {ConsoleKey.RightArrow, new Point(1, 0) },
        };

        public GameController(GlobalMap map)
        {
            Map = map;
            map.Player.Changed += Player_Changed;
        }

        void Player_Changed(object sender, EventArgs e)
        {
            dynamic ch = Map.Player.Check();
            if (ch is Gravestone)
            {
                Console.WriteLine(after[Map.rand.Next(after.Count)]);
            }
        }

        public void HandleInput(ConsoleKeyInfo key)
        {
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
                    break;
                case ConsoleKey.P:
                    TrySavePlayer();
                    break;
                case ConsoleKey.O:
                    TrySaveSettings();
                    break;
            }
        }

        public void TrySavePlayer(bool forced = false)
        {
            if (forced)
            {
                DataLoader.WritePlayer(new Player());
                return;
            }
            if (Map.Player.Home())
                if (Map.Player.Gold >= DataLoader.settings.SAVE_COST) {
                    Map.Player.Gold -= DataLoader.settings.SAVE_COST;
                    bool afterall = DataLoader.WritePlayer(Map.Player);
                    Program.Save("персонажа", afterall);
                    if (afterall)
                        Map.Player.SpendGold(0);
                    else
                        Map.Player.Gold += DataLoader.settings.SAVE_COST;
                }
                else
                {
                    Program.ConsoleMessage(String.Format("Недосотаточно денег! Нужно {0} монет, а у вас {1}.", DataLoader.settings.SAVE_COST, Map.Player.Gold));
                }
            else
                Program.ConsoleMessage(String.Format("Здесь сохранения нет, ищите выход на карте [а ещё нужно {0} монет]", DataLoader.settings.SAVE_COST));
        }

        public void TrySaveSettings()
        {
            if (Map.Player.Home())
                Program.Save("настроек", DataLoader.WriteSettings());
            else
                Program.ConsoleMessage("Здесь сохранения нет, ищите выход на карте");
        }

        public void Turn()
        {
            TurnNumber++;
            Map.Collapse(Math.Min(TurnNumber/50, 3));
            if (DataLoader.settings.FLOOD)
                Map.Flood(TurnNumber / 10);
        }

        public void MovePlayer(Point delta) {
            var pos = Map.PlayerCell;
            var newPos = pos + delta;
            if (!Map.ValidP(newPos))
                return;
            dynamic obj = Map[newPos.X, newPos.Y];
            var result = Map.Player.ActWith(obj);
            if (result != Map[newPos.X, newPos.Y])
                Map[newPos.X, newPos.Y] = result;
            if (result is IFree || result is IInteractive)
            {
               Map.PlayerMove(newPos);
            } 
        }
    }
}
