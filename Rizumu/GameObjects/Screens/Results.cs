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
    class Results : IScreen
    {
        public Screen Name => Screen.Results;

        Background bg;
        Text MapResults;
        Button Back;
        public bool ResultsPreloaded = false;

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Rectangle cursor, bool clicked, GraphicsDevice g)
        {
            bg.Draw(spriteBatch);
            Back.Draw(spriteBatch, cursor, clicked);
            MapResults.Draw(spriteBatch);
        }

        public void Preload(SpriteBatch spriteBatch, GraphicsDeviceManager Graphics)
        {
            bg = new Background(GameData.Instance.CurrentSkin.MenuBackground, Color.White, GameData.globalwidth, GameData.globalheight);
            Back = new Button(25, GameData.globalheight - GameData.Instance.CurrentSkin.Button.Height - 25,
                GameData.Instance.CurrentSkin.Button, GameData.Instance.CurrentSkin.ButtonHover, "Back");
            Back.OnClick += (sender, e) =>
            {
                GameData.Instance.CurrentScreen = Screen.Select;
                ((InGame)GameData.Instance.Screens.Find(x => x.Name == Screen.Ingame)).MapLoaded = false;
                ResultsPreloaded = false;
                GameData.MusicManager.Restart();
            };
            MapResults = new Text(GameData.Instance.CurrentSkin.Font, "if you read this u suk", 5, 5, Color.Thistle);
        }

        public void Update(GameTime gameTime, Rectangle cursor, bool clicked)
        {
            if (!ResultsPreloaded)
            {
                var ig = (InGame)GameData.Instance.Screens.Find(x => x.Name == Screen.Ingame);
                MapResults.Content = "Results: (I'm done so no full result screen yet)\n";
                MapResults.Content += $"Hits Left: {ig.NotesLeft.FindAll(x => x.Hit).Count} Miss Left: {ig.NotesLeft.FindAll(x => x.Miss).Count} \n";
                MapResults.Content += $"Hits Up: {ig.NotesUp.FindAll(x => x.Hit).Count} Miss Up: {ig.NotesUp.FindAll(x => x.Miss).Count}\n";
                MapResults.Content += $"Hits Right: {ig.NotesRight.FindAll(x => x.Hit).Count} Miss Right: {ig.NotesRight.FindAll(x => x.Miss).Count}\n";
                MapResults.Content += $"Hits Down: {ig.NotesDown.FindAll(x => x.Hit).Count} Miss Down: {ig.NotesDown.FindAll(x => x.Miss).Count}\n";
                bg.Texture = ig.Background.Texture;
                ResultsPreloaded = true;
            }
        }
    }
}
