using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Client
{
    public enum Gameview
    {
        Main,
        Songselect,
        Options,
        Ingame,
        Results
    }

    public class StaticStuff
    {
        /*
         * All static stuff here heh
         */
        public static Gameview View;
        public static View Main;
    }
}
