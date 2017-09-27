using Rizumu.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rizumu.Enums;
using Rizumu.GuiObjects;

namespace Rizumu.GameObjects.Screens
{
    class OptionScreen : IScreen
    {
        public Text DebugTextThing;
        public Background Background;
        public Button BackButton;

        public Screen Name => Screen.Options;

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Rectangle cursor, bool clicked, GraphicsDevice g)
        {
            Background.Draw();
            DebugTextThing.Draw();
            BackButton.Draw(cursor, clicked);
        }

        public void Preload(SpriteBatch spriteBatch, GraphicsDeviceManager Graphics)
        {
            DebugTextThing = new Text(spriteBatch, GameData.Instance.CurrentSkin.Font, "This will be the options screen... someday..", 69, 69, Color.Green);
            Background = new Background(spriteBatch, GameData.Instance.CurrentSkin.MenuBackground, Color.White, GameData.globalwidth, GameData.globalheight);
            BackButton = new Button(spriteBatch, 25, GameData.globalheight - GameData.Instance.CurrentSkin.Button.Height - 25,
                GameData.Instance.CurrentSkin.Button, GameData.Instance.CurrentSkin.ButtonHover, "Back");
            BackButton.OnClick += (sender, e) =>
            {
                GameData.Instance.CurrentScreen = Screen.Main;
            };
        }

        public void Update(GameTime gameTime, Rectangle cursor, bool clicked)
        {
        }
    }
}
