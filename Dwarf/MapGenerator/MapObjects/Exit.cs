using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator.MapObjects
{
    public class Exit : MapObject, IInteractive
    {
        public Exit() : base('E', ConsoleColor.DarkYellow, ConsoleColor.DarkMagenta) { }
        public override MapObject Check()
        {
            return this;
        }

        public event ActivatedEventHandler Activated;

        public void OnActivated(EventArgs e)
        {
            if (Activated != null)
                Activated(this, e);
        }

        public override string[] Info
        {
            get
            {
                return " Выход! \n Сохранение персонажа: \n   7 монет \n Сохранение настроек: \n   бесплатно".Split('\n');
            }
        }
    }
}
