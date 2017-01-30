using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

    public enum GameOverlay
    {
        Login,
        Chat,
        Loading
    }

    public class StaticStuff
    {
        /*
         * All static stuff here heh
         */
        public static Gameview View;
        public static GameOverlay Overlay;
        public static bool OverlayEnable;
        public static View Main;
        public static Texture2D Background;
        public static MouseState oldMouseState;
        public static MouseState mouseState;
        public static float Volume;
    }
}
