/*
 * Listing of songs. Feel free to improve, make smoother, etc
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        public static void draw(SpriteBatch spriteBatch)
        {
            MouseState omstate = mstate;
            if (Keyboard.GetState().IsKeyDown(Keys.F2))
            {
                Random r = new Random();
                scrolled = r.Next(1, GameResources.songs.Length) * -1;
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
            int limit = (GameResources.songs.Length * -1) - 1;
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

            foreach (string song in GameResources.songs)
            {
                int myx = Game1.graphics.PreferredBackBufferWidth - 430;
                if (txty + (100 * scrolled) == 100)
                {
                    myx = Game1.graphics.PreferredBackBufferWidth - 500;
                    GameResources.selected = song;
                    if (GameResources.selected != oldsong)
                    {
                        try
                        {
                            if (File.Exists(song + "/back.png"))
                            {
                                System.IO.Stream stream4 = TitleContainer.OpenStream(GameResources.selected + "/back.png");
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
                songbar = new Sprite(spriteBatch, myx, txty + (100 * scrolled), GameResources.Songbar, GameResources.basecolor);
                songbar.draw();
                Text.draw(GameResources.font, song.Substring(14), myx + 20, txty + (100 * scrolled), spriteBatch);
                txty = txty + 100;
            }

            if (playbtn.hitbox.Intersects(Game1.cursorbox))
            {
                if (mstate.LeftButton == ButtonState.Released && omstate.LeftButton == ButtonState.Pressed && GameResources.scorebackmouse.LeftButton != ButtonState.Pressed)
                {
                    GameResources.GameScreen = 2;
                }
                playbtn = new Sprite(spriteBatch, 0, Game1.graphics.PreferredBackBufferHeight - 200, GameResources.ButtonSelected, GameResources.basecolor);
            }
            else
            {
                playbtn = new Sprite(spriteBatch, 0, Game1.graphics.PreferredBackBufferHeight - 200, GameResources.Button, GameResources.basecolor);
            }
            playbtn.draw();
            Text.draw(GameResources.font, "Play map", 10, Game1.graphics.PreferredBackBufferHeight - 170, spriteBatch);

            if (backbtn.hitbox.Intersects(Game1.cursorbox))
            {
                if (mstate.LeftButton == ButtonState.Released && omstate.LeftButton == ButtonState.Pressed && GameResources.scorebackmouse.LeftButton != ButtonState.Pressed)
                {
                    GameResources.GameScreen = 0;
                }
                backbtn = new Sprite(spriteBatch, 0, Game1.graphics.PreferredBackBufferHeight - 100, GameResources.ButtonSelected, GameResources.basecolor);
            }
            else
            {
                backbtn = new Sprite(spriteBatch, 0, Game1.graphics.PreferredBackBufferHeight - 100, GameResources.Button, GameResources.basecolor);
            }
            backbtn.draw();
            Text.draw(GameResources.font, "Back", 10, Game1.graphics.PreferredBackBufferHeight - 70, spriteBatch);
            string info = GameResources.selected.Substring(14) + "\n";
            if (File.Exists(GameResources.selected + "/info.rizum"))
            {
                string text = File.ReadAllText(GameResources.selected + "/info.rizum");
                info += text;
            }
            Text.draw(GameResources.font, info, 10, 10, spriteBatch);
            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                GameResources.scorebackmouse = Mouse.GetState();
            }
        }

        public static void update(SpriteBatch spriteBatch)
        {

        }
    }
}