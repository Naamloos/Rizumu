/*
 * Main menu logic
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;

namespace Rizumu.GameScreens
{
    class MainMenu
    {
        public static Sprite playbtn;
        public static Sprite optionsbtn;
        public static Sprite loginbtn;
        public static Sprite exitbtn;
        public static Sprite logo;

        public static Background background;
        public static MouseState mstate;
        public static int scrolltextx = 0;

        public static void draw(SpriteBatch spriteBatch)
        {
            background = new Background(spriteBatch, GameResources.background_menu);
            background.draw();

            logo = new Sprite(spriteBatch, Game1.graphics.PreferredBackBufferWidth - (530 * (Game1.graphics.PreferredBackBufferHeight / 720)), 100, GameResources.Logo, GameResources.basecolor);
            logo.scale = 1f * (Game1.graphics.PreferredBackBufferHeight / 720);
            //logo.draw();
            MouseState omstate = mstate;
            mstate = Mouse.GetState();
            if (playbtn.hitbox.Intersects(Game1.cursorbox))
            {
                if (mstate.LeftButton == ButtonState.Released && omstate.LeftButton == ButtonState.Pressed)
                {
                    GameResources.GameScreen = 1;
                }
                playbtn = new Sprite(spriteBatch, 0, 50, GameResources.ButtonSelected, GameResources.basecolor);
            }
            else
            {
                playbtn = new Sprite(spriteBatch, 0, 50, GameResources.Button, GameResources.basecolor);
            }
            playbtn.draw();
            Text.draw(GameResources.font, "Play", 10, 80, spriteBatch);

            if (optionsbtn.hitbox.Intersects(Game1.cursorbox))
            {
                if (mstate.LeftButton == ButtonState.Released && omstate.LeftButton == ButtonState.Pressed)
                {
                    GameResources.GameScreen = 4;
                }
                optionsbtn = new Sprite(spriteBatch, 0, 160, GameResources.ButtonSelected, GameResources.basecolor);
            }
            else
            {
                optionsbtn = new Sprite(spriteBatch, 0, 160, GameResources.Button, GameResources.basecolor);
            }
            optionsbtn.draw();
            Text.draw(GameResources.font, "Options", 10, 190, spriteBatch);

            if (loginbtn.hitbox.Intersects(Game1.cursorbox))
            {
                loginbtn = new Sprite(spriteBatch, 0, 270, GameResources.ButtonSelected, GameResources.basecolor);
            }
            else
            {
                loginbtn = new Sprite(spriteBatch, 0, 270, GameResources.Button, GameResources.basecolor);
            }
            loginbtn.draw();
            Text.draw(GameResources.font, "Log in", 10, 300, spriteBatch);

            if (exitbtn.hitbox.Intersects(Game1.cursorbox))
            {
                exitbtn = new Sprite(spriteBatch, 0, 380, GameResources.ButtonSelected, GameResources.basecolor);
                if (mstate.LeftButton == ButtonState.Released && omstate.LeftButton == ButtonState.Pressed)
                {
                    Game1.exiting = true;
                }
            }
            else
            {
                exitbtn = new Sprite(spriteBatch, 0, 380, GameResources.Button, GameResources.basecolor);
            }
            exitbtn.draw();
            Text.draw(GameResources.font, "Exit", 10, 410, spriteBatch);

            Text.draw(GameResources.font, "Currently playing: " + GameResources.selected.Substring(14), scrolltextx, 0, spriteBatch);
            if (scrolltextx < (300 + (GameResources.selected.Substring(14).Length * 20)) * -1)
            {
                scrolltextx = Game1.graphics.PreferredBackBufferWidth + (GameResources.selected.Substring(14).Length * 10);
            }
            else
            {
                scrolltextx = scrolltextx - 5;
            }
            Sprite mascotte = new Sprite(spriteBatch, Game1.graphics.PreferredBackBufferWidth - GameResources.Mascotte.Width - 70, Game1.graphics.PreferredBackBufferHeight - GameResources.Mascotte.Height + 80, GameResources.Mascotte, GameResources.basecolor);
            mascotte.scale = 1f;
            if (Music.beat)
            {
                mascotte.scale = 2f;
            }

            mascotte.draw();
            if (Keyboard.GetState().IsKeyDown(Keys.F11))
            {
                GameResources.GameScreen = 6;
            }
        }

        public static void update()
        {

        }
    }
}
