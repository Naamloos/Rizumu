/*
 * WIP Options screen.
 * Needs a lot of improval
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
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
            Text.draw(GameResources.font, "Options menu idk idk", 0, 0, spriteBatch);
            Sprite backbtn = new Sprite(spriteBatch, 0, Game1.graphics.PreferredBackBufferHeight - 100, GameResources.Button, GameResources.basecolor);
            if (backbtn.hitbox.Intersects(Game1.cursorbox))
            {
                if (mstate.LeftButton == ButtonState.Pressed)
                {
                    string[] lines = File.ReadAllLines("settings.ini");
                    int i = 0;
                    while (i < lines.Length)
                    {
                        if (lines[i] == "fullscreen:true" && !GameResources.fullscreen)
                        {
                            lines[i] = "fullscreen:false";
                        }
                        if (lines[i] == "fullscreen:false" && GameResources.fullscreen)
                        {
                            lines[i] = "fullscreen:true";
                        }
                        i++;
                    }
                    File.WriteAllLines("settings.ini", lines);
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
            if (GameResources.fullscreen == true)
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
                    if (GameResources.fullscreen == false)
                    {
                        GameResources.fullscreen = true;
                    }
                    else
                    {
                        GameResources.fullscreen = false;
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
