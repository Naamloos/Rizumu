﻿/*
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

        public static void Draw(SpriteBatch spriteBatch)
        {
            background = new Background(spriteBatch, GameResources.background_menu);
            background.draw();

            logo = new Sprite(spriteBatch, Game1.graphics.PreferredBackBufferWidth - (530 * (Game1.graphics.PreferredBackBufferHeight / 720)), 100, GameResources.Logo, GameResources.basecolor)
            {
                scale = 1f * (Game1.graphics.PreferredBackBufferHeight / 720)
            };
            //logo.draw();
            MouseState omstate = mstate;
            mstate = Mouse.GetState();
            if (playbtn.hitbox.Intersects(Game1.cursorbox))
            {
                if (mstate.LeftButton == ButtonState.Released && omstate.LeftButton == ButtonState.Pressed)
                {
                    GameResources.GameScreen = 1;
                }
                playbtn = new Sprite(spriteBatch, 40, 70, GameResources.ButtonSelected, GameResources.basecolor);
            }
            else
            {
                playbtn = new Sprite(spriteBatch, 40, 70, GameResources.Button, GameResources.basecolor);
            }
            playbtn.draw();
            Text.draw(GameResources.font, "Play", 70, 100, spriteBatch);

            if (optionsbtn.hitbox.Intersects(Game1.cursorbox))
            {
                if (mstate.LeftButton == ButtonState.Released && omstate.LeftButton == ButtonState.Pressed)
                {
                    GameResources.GameScreen = 4;
                }
                optionsbtn = new Sprite(spriteBatch, 40, 180, GameResources.ButtonSelected, GameResources.basecolor);
            }
            else
            {
                optionsbtn = new Sprite(spriteBatch, 40, 180, GameResources.Button, GameResources.basecolor);
            }
            optionsbtn.draw();
            Text.draw(GameResources.font, "Options", 70, 210, spriteBatch);

            if (loginbtn.hitbox.Intersects(Game1.cursorbox))
            {
                if (mstate.LeftButton == ButtonState.Released && omstate.LeftButton == ButtonState.Pressed)
                {
                    IngamePopup.SetPopup("Oopsie!", "Online play and score uploading aren't\nimplemented yet.\n\nSorry!");
                }
                loginbtn = new Sprite(spriteBatch, 40, 290, GameResources.ButtonSelected, GameResources.basecolor);
            }
            else
            {
                loginbtn = new Sprite(spriteBatch, 40, 290, GameResources.Button, GameResources.basecolor);
            }
            loginbtn.draw();
            Text.draw(GameResources.font, "Log in", 70, 320, spriteBatch);

            if (exitbtn.hitbox.Intersects(Game1.cursorbox))
            {
                exitbtn = new Sprite(spriteBatch, 40, 400, GameResources.ButtonSelected, GameResources.basecolor);
                if (mstate.LeftButton == ButtonState.Released && omstate.LeftButton == ButtonState.Pressed)
                {
                    Game1.exiting = true;
                }
            }
            else
            {
                exitbtn = new Sprite(spriteBatch, 40, 400, GameResources.Button, GameResources.basecolor);
            }
            exitbtn.draw();
            Text.draw(GameResources.font, "Exit", 70, 430, spriteBatch);

            Text.draw(GameResources.font, GameResources.Maps[GameResources.selected].Name, scrolltextx, 0, spriteBatch);
            if (scrolltextx < (300 + (GameResources.Maps[GameResources.selected].Name.Length * 20)) * -1)
            {
                scrolltextx = Game1.graphics.PreferredBackBufferWidth + (GameResources.Maps[GameResources.selected].Name.Length * 10);
            }
            else
            {
                scrolltextx = scrolltextx - 5;
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.F11))
            {
                GameResources.GameScreen = 6;
            }
        }

        public static void Update()
        {
            Konami();
        }

        public static int konamii = 0;
        public static KeyboardState oks;

        public static void Konami()
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Up) && konamii == 0)
                konamii = 1;
            else if (ks.IsKeyDown(Keys.Up) && konamii == 1)
                konamii = 2;
            else if (ks.IsKeyDown(Keys.Down) && konamii == 2)
                konamii = 3;
            else if (ks.IsKeyDown(Keys.Down) && konamii == 3)
                konamii = 4;
            else if (ks.IsKeyDown(Keys.Left) && konamii == 4)
                konamii = 5;
            else if (ks.IsKeyDown(Keys.Right) && konamii == 5)
                konamii = 6;
            else if (ks.IsKeyDown(Keys.Left) && konamii == 6)
                konamii = 7;
            else if (ks.IsKeyDown(Keys.Right) && konamii == 7)
                konamii = 8;
            else if (ks.IsKeyDown(Keys.B) && konamii == 8)
                konamii = 9;
            else if (ks.IsKeyDown(Keys.A) && konamii == 9)
            {
                if(GameResources.autoplay == true)
                {
                    IngamePopup.SetPopup("Konami code disabled", "You entered the konami code.\nAutoplay has been deactivated again!");
                    GameResources.autoplay = false;
                }
                else
                {
                    IngamePopup.SetPopup("Konami code enabled", "You entered the konami code.\nAutoplay has been activated!");
                    GameResources.autoplay = true;
                }
                konamii = 0;
            }
            oks = ks;
        }
    }
}
