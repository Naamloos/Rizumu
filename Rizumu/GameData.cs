using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Rizumu.Engine;
using Rizumu.GameObjects;
using Rizumu.GameObjects.Screens;
using Rizumu.Helpers;
using Rizumu.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rizumu.Enums;

namespace Rizumu
{
    class GameData
    {
        public static GameData Instance;
        public static MapManager MapManager;
        public static MusicManager MusicManager;

        // Objects
        public Options Options;
        public Skin DefaultSkin;
        public Skin CurrentSkin;

        // GameScreens
        public List<IScreen> Screens;
        public Screen CurrentScreen;

        // Public vars
        public bool Exiting; //im lazy
        public ModCollection Mods = new ModCollection();

        public static int globalwidth;
        public static int globalheight;
        public static int realwidth;
        public static int realheight;

        public GameData(ContentManager content)
        {
            DefaultSkin = Skin.LoadDefault(content);
            CurrentScreen = Screen.Main;
        }

        public void LoadScreens(SpriteBatch spriteBatch, GraphicsDeviceManager Graphics)
        {
            Screens = new List<IScreen>();
            Screens.Add(new MainMenu());
            Screens.Add(new SongSelect());
            Screens.Add(new OptionScreen());
            Screens.Add(new InGame());
            Screens.Add(new Editor());
            Screens.Add(new Results());
            foreach (IScreen s in Screens)
            {
                s.Preload(spriteBatch, Graphics);
            }
        }
    }
}
