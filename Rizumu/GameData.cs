using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Rizumu.Engine;
using Rizumu.GameObjects.Screens;
using Rizumu.Helpers;
using Rizumu.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string CurrentScreen;

        // Public vars
        public bool Exiting; //im lazy

        public GameData(ContentManager content)
        {
            DefaultSkin = Skin.LoadDefault(content);
            CurrentScreen = "main";
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
