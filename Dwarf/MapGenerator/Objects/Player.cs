using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MapGenerator.Objects
{
    public class Player : MapObject, IActor
    {
        public delegate void ChangedEventHandler(object sender, EventArgs e);
        public event ChangedEventHandler Changed;
        [XmlIgnore]
        public MapObject Ground { get; set; }
        public int Gold { get; set; }
        public int MaxHp { get; set; }
        public int CurrentHp { get; set; }
        public int MaxMp { get; set; }
        public int CurrentMp { get; set; }
        public string Name { get; set; }
        public int CurrentVitality { get; set; }
        public int MaxVitality { get; set; }
        public int MaxSatiety { get; set; }
        public int CurrentSatiety { get; set; }
        public int CurrentThirst { get; set; }
        public int MaxThirst { get; set; }
        public int Level { get; set; }
        public int CurrentXp { get; set; }
        public int MaxXp { get; set; }

        public Player() : base('@', ConsoleColor.Black, ConsoleColor.Green) {
            Ground = new Earth();
            MaxHp = 50;
            CurrentHp = 40;
            CurrentMp = 20;
            MaxMp = 30;
            Name = "Antoto";
            MaxVitality = 100;
            CurrentVitality = 90;
            MaxXp = 100;
        }

        public void SpendGold(int count)
        {
            if (count > Gold)
                throw new ArgumentOutOfRangeException("Нету столько золота");
            Gold -= count;
            OnChanged(EventArgs.Empty);
        }

        protected virtual void OnChanged(EventArgs e)
        {
            if (Changed != null)
                Changed(this, e);
        }


        public MapObject ActWith(Gold mo)
        {
            Gold += mo.Value;
            CurrentXp += mo.Value;
            OnChanged(EventArgs.Empty);
            return new Earth();
        }

        public MapObject ActWith(Player mo)
        {
            return mo.Check();
        }

        public MapObject ActWith(Rock mo)
        {
            mo.Hp--;
            if (mo.Hp < 5)
            {
                CurrentHp--;
                if (mo.Hp == 0)
                    CurrentXp++;
                OnChanged(EventArgs.Empty);
            }

            
            return mo.Check();
        }

        public MapObject ActWith(Earth mo)
        {
            return mo.Check();
        }

        public override MapObject Check()
        {
            if (CurrentHp > 0)
                return this;
            return new Gravestone(this);
        }


        public MapObject ActWith(Gravel mo)
        {
            mo.Hp--;
            if (mo.Hp < 3)
            {
                CurrentHp--;
                if (mo.Hp == 0)
                    CurrentXp++;
                OnChanged(EventArgs.Empty);
            }


            return mo.Check();
        }

        public bool Home()
        {
            return (Ground is Exit);
        }


        public MapObject ActWith(Exit mo)
        {
            return mo.Check();
        }


        public MapObject ActWith(Water mo)
        {
            CurrentHp--;
            OnChanged(EventArgs.Empty);
            return mo.Check();
        }
    }
}
