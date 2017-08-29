using Rizumu.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rizumu.GuiObjects;

namespace Rizumu.GameObjects.Screens
{
    class Results : IScreen
    {
        public string Name { get => "results"; }

        Background bg;
        Text MapResults;
        Button Back;
        public bool ResultsPreloaded = false;

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Rectangle cursor, bool clicked)
        {
            bg.Draw();
            Back.Draw(cursor, clicked);
            MapResults.Draw();
        }

        public void Preload(SpriteBatch spriteBatch, GraphicsDeviceManager Graphics)
        {
            bg = new Background(spriteBatch, GameData.Instance.CurrentSkin.MenuBackground, Color.White, Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight);
            Back = new Button(spriteBatch, 25, Graphics.PreferredBackBufferHeight - GameData.Instance.CurrentSkin.Button.Height - 25,
                GameData.Instance.CurrentSkin.Button, GameData.Instance.CurrentSkin.ButtonHover, "Back");
            Back.OnClick += (sender, e) =>
            {
                GameData.Instance.CurrentScreen = "select";
                ((InGame)GameData.Instance.Screens.Find(x => x.Name == "ingame")).MapLoaded = false;
                ResultsPreloaded = false;
                GameData.MusicManager.Restart();
            };
            MapResults = new Text(spriteBatch, GameData.Instance.CurrentSkin.Font, "if you read this u suk", 5, 5, Color.Thistle);
        }

        public void Update(GameTime gameTime, Rectangle cursor, bool clicked)
        {
            if (!ResultsPreloaded)
            {
                var ig = (InGame)GameData.Instance.Screens.Find(x => x.Name == "ingame");
                MapResults.Content = "Results: (I'm done so no full result screen yet)\n";
                MapResults.Content += "Hits Left: " + ig.NotesLeft.FindAll(x => x.Hit == true).Count() + " Miss Left: " + ig.NotesLeft.FindAll(x => x.Miss == true).Count() + "\n";
                MapResults.Content += "Hits Up: " + ig.NotesUp.FindAll(x => x.Hit == true).Count() + " Miss Up: " + ig.NotesUp.FindAll(x => x.Miss == true).Count() + "\n";
                MapResults.Content += "Hits Right: " + ig.NotesRight.FindAll(x => x.Hit == true).Count() + " Miss Right: " + ig.NotesRight.FindAll(x => x.Miss == true).Count() + "\n";
                MapResults.Content += "Hits Down: " + ig.NotesDown.FindAll(x => x.Hit == true).Count() + " Miss Down: " + ig.NotesDown.FindAll(x => x.Miss == true).Count() + "\n";
                bg.Texture = ig.Background.Texture;
                ResultsPreloaded = true;
            }
        }
    }
}
