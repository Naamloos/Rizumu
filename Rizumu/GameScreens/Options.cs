/*
 * WIP Options screen.
 * Needs a lot of improval
 */

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Rizumu.GameScreens
{
    class Options
    {
        public static MouseState mstate;
        public static void Draw(SpriteBatch spriteBatch)
        {
            MouseState oldstate = mstate;
            mstate = Mouse.GetState();
            Background mainbg = new Background(spriteBatch, GameResources.background_menu);
            mainbg.draw();
            Text.draw(GameResources.font, "Options", 0, 0, spriteBatch);
            Sprite backbtn = new Sprite(spriteBatch, 0, Game1.graphics.PreferredBackBufferHeight - 100, GameResources.Button, GameResources.basecolor);
            if (backbtn.hitbox.Intersects(Game1.cursorbox))
            {
                if (mstate.LeftButton == ButtonState.Pressed)
                {
                    if (GameResources.Optionss.Fullscreen == true)
                    {
                        IngamePopup.SetPopup("Fullscreen enabled!", "Please restart the game for changes to take effect!");
                    }
                    if (GameResources.Optionss.Fullscreen == false)
                    {
                        IngamePopup.SetPopup("Fullscreen disabled!", "Please restart the game for changes to take effect!");
                    }
                    File.WriteAllText("settings.json", JObject.FromObject(GameResources.Optionss).ToString());
                    GameResources.GameScreen = 0;
                }
                backbtn = new Sprite(spriteBatch, 20, Game1.graphics.PreferredBackBufferHeight - 120, GameResources.ButtonSelected, GameResources.basecolor);
            }
            else
            {
                backbtn = new Sprite(spriteBatch, 20, Game1.graphics.PreferredBackBufferHeight - 120, GameResources.Button, GameResources.basecolor);
            }
            backbtn.draw();
            Text.draw(GameResources.font, "Back", 50, Game1.graphics.PreferredBackBufferHeight - 90, spriteBatch);

            Sprite FullscreenCheck;
            if (GameResources.Optionss.Fullscreen == true)
            {
                FullscreenCheck = new Sprite(spriteBatch, 50, 50, GameResources.Checked, GameResources.basecolor);
            }
            else
            {
                FullscreenCheck = new Sprite(spriteBatch, 50, 50, GameResources.Unchecked, GameResources.basecolor);
            }

            if (FullscreenCheck.hitbox.Intersects(Game1.cursorbox))
            {
                if (mstate.LeftButton == ButtonState.Pressed && !(oldstate.LeftButton == ButtonState.Pressed))
                {
                    if (GameResources.Optionss.Fullscreen == false)
                    {
                        GameResources.Optionss.Fullscreen = true;
                    }
                    else
                    {
                        GameResources.Optionss.Fullscreen = false;
                    }
                }
            }
            FullscreenCheck.draw();
            Text.draw(GameResources.font, "Enable Fullscreen", 110, 55, spriteBatch);
        }

        public static void Update()
        {

        }
    }
}
