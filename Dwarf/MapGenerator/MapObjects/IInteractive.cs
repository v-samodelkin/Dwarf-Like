using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator.MapObjects
{
    public delegate void ActivatedEventHandler(object sender, EventArgs e);
    public interface IInteractive
    {
        
        event ActivatedEventHandler Activated;
        void OnActivated(EventArgs e);
    }
}
