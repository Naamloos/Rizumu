/*
 * Listing of songs. Feel free to improve, make smoother, etc
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rizumu.Objects;
using System;
using System.IO;

namespace Rizumu.GameScreens
{
    class SongList
    {
        public static Background background;

        public static Sprite songbar;
        public static int oldscrollval;
        public static int scrolled = 1 + (GameResources.startint * -1);
        public static Sprite backbtn;
        public static Sprite playbtn;
        public static MouseState mstate;
        public static string oldsong = "";
        public static bool firstsong = true;

        public static void Draw(SpriteBatch spriteBatch)
        {
            MouseState omstate = mstate;
            if (Keyboard.GetState().IsKeyDown(Keys.F2))
            {
                Random r = new Random();
                scrolled = r.Next(1, GameResources.Maps.Count) * -1;
            }
            mstate = Mouse.GetState();
            try
            {
                background = new Background(spriteBatch, GameResources.songbg);
            }
            catch (Exception)
            {
                background = new Background(spriteBatch, GameResources.background_menu);
            }
            songbar = new Sprite(spriteBatch, 0, 0, GameResources.Songbar, GameResources.basecolor);
            songbar.draw();
            try
            {
                background.draw();
            }
            catch (Exception)
            {

            }
            int limit = (GameResources.Maps.Count * -1) - 1;
            int txty = 0;

            if (oldscrollval < Mouse.GetState().ScrollWheelValue && scrolled != 1)
            {
                scrolled++;
            }
            else if (oldscrollval > Mouse.GetState().ScrollWheelValue && scrolled > (limit + 3))
            {
                scrolled--;
            }
            oldscrollval = Mouse.GetState().ScrollWheelValue;

            foreach (var map in GameResources.Maps)
            {
                string song = map.Key;
                int myx = Game1.graphics.PreferredBackBufferWidth - 450;
                if (txty + (110 * scrolled) == 110)
                {
                    myx = Game1.graphics.PreferredBackBufferWidth - 500;
                    GameResources.selected = song;
                    if (GameResources.selected != oldsong)
                    {
                        try
                        {
                            if (File.Exists(Path.Combine(song, map.Value.BackgroundFile)))
                            {
                                System.IO.Stream stream4 = TitleContainer.OpenStream(Path.Combine(song, map.Value.BackgroundFile));
                                GameResources.songbg = Texture2D.FromStream(Game1.graphics.GraphicsDevice, stream4);
                            }
                            else
                            {
                                GameResources.songbg = GameResources.background_menu;
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                    if (oldsong != song)
                    {
                        oldsong = song;
                        if (firstsong == false)
                        {
                            Music.play(song, 0);
                        }
                    }
                    firstsong = false;
                }
                songbar = new Sprite(spriteBatch, myx, txty + (110 * scrolled), GameResources.Songbar, GameResources.basecolor);
                songbar.draw();
                Text.draw(GameResources.font, map.Value.Name, myx + 40, txty + (110 * scrolled) + 10, spriteBatch);
                txty = txty + 110;
            }

            if (playbtn.hitbox.Intersects(Game1.cursorbox))
            {
                if (mstate.LeftButton == ButtonState.Released && omstate.LeftButton == ButtonState.Pressed && GameResources.scorebackmouse.LeftButton != ButtonState.Pressed)
                {
                    GameResources.GameScreen = 2;
                }
                playbtn = new Sprite(spriteBatch, 20, Game1.graphics.PreferredBackBufferHeight - 240, GameResources.ButtonSelected, GameResources.basecolor);
            }
            else
            {
                playbtn = new Sprite(spriteBatch, 20, Game1.graphics.PreferredBackBufferHeight - 240, GameResources.Button, GameResources.basecolor);
            }
            playbtn.draw();
            Text.draw(GameResources.font, "Play map", 50, Game1.graphics.PreferredBackBufferHeight - 210, spriteBatch);

            if (backbtn.hitbox.Intersects(Game1.cursorbox))
            {
                if (mstate.LeftButton == ButtonState.Released && omstate.LeftButton == ButtonState.Pressed && GameResources.scorebackmouse.LeftButton != ButtonState.Pressed)
                {
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
            string info = "Folder name: " + GameResources.selected.Substring(14) + "\n";
            info += "Map creator: " + GameResources.Maps[GameResources.selected].Creator + "\n\n";
            info += GameResources.Maps[GameResources.selected].Description;
            Text.draw(GameResources.font, info, 10, 10, spriteBatch);
            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                GameResources.scorebackmouse = Mouse.GetState();
            }
        }

        public static void Update(SpriteBatch spriteBatch)
        {

        }
    }
}