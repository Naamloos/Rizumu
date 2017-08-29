using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rizumu.Engine;
using Rizumu.GuiObjects;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Rizumu.GameObjects.Screens
{
    class MainMenu : IScreen
    {
        // Buttons + Text
        public Button PlayButton;
        public Button OptionsButton;
        public Button ExitButton;

        // Background
        public Background Background;
        public Background MenuOverlay;

        public string Name { get => "main"; }

        #region Preloading
        public void Preload(SpriteBatch spriteBatch, GraphicsDeviceManager Graphics)
        {
            int buttonHeight = GameData.Instance.CurrentSkin.Button.Height;

            #region PlayBTN
            PlayButton = new Button(spriteBatch, 50, 50, GameData.Instance.CurrentSkin.Button, GameData.Instance.CurrentSkin.ButtonHover, "Play");
            PlayButton.OnClick += (sender, e) =>
            {
                GameData.Instance.CurrentScreen = "select";
            };
            #endregion

            #region OptionsBTN
            OptionsButton = new Button(spriteBatch, 50, (buttonHeight * 1) + 75, GameData.Instance.CurrentSkin.Button, GameData.Instance.CurrentSkin.ButtonHover, "Options");
            OptionsButton.OnClick += (sender, e) =>
            {
                GameData.Instance.CurrentScreen = "options";
            };
            #endregion

            #region ExitBTN
            ExitButton = new Button(spriteBatch, 50, (buttonHeight * 2) + 100, GameData.Instance.CurrentSkin.Button, GameData.Instance.CurrentSkin.ButtonHover, "Exit");
            ExitButton.OnClick += (sender, e) =>
            {
                GameData.Instance.Exiting = true;
            };
            #endregion

            Background = new Background(spriteBatch, GameData.Instance.CurrentSkin.MenuBackground, Color.White, Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight);
            MenuOverlay = new Background(spriteBatch, GameData.Instance.CurrentSkin.FunctionOverlay, Color.White, Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight);
        }
        #endregion

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Rectangle cursor, bool clicked)
        {
            Background.Draw();

            PlayButton.Draw(cursor, clicked);
            OptionsButton.Draw(cursor, clicked);
            ExitButton.Draw(cursor, clicked);

            MenuOverlay.Draw();
        }

        public void Update(GameTime gameTime, Rectangle cursor, bool clicked)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.F1))
                GameData.Instance.CurrentScreen = "editor";
            if (Keyboard.GetState().IsKeyDown(Keys.F2))
            {
                var ofd = new System.Windows.Forms.OpenFileDialog();
                ofd.ShowDialog();
                var replay = JObject.Parse(File.ReadAllText(ofd.FileName)).ToObject<Objects.Replay>();
                ((InGame)GameData.Instance.Screens.Find(x => x.Name == "ingame")).Replay = replay;
                ((InGame)GameData.Instance.Screens.Find(x => x.Name == "ingame")).Replaying = true;
                GameData.MapManager.Current = GameData.MapManager.Maps.Find(x => x.MD5 == replay.Md5);
                GameData.Instance.CurrentScreen = "ingame";
                GameData.MusicManager.Change(GameData.MapManager.Current);
            }
        }
    }
}
