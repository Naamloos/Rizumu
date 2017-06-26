/*
 * Main game logic. Here's the place every game screen gets called etc.
 */

#define WINDOWS_STOREAPP
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.IO;
using System.Threading;

namespace Rizumu
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        public static GraphicsDevice grap;
        SpriteBatch spriteBatch;
        public static MouseState mstate;
        public static Rectangle cursorbox;
        public static bool exiting = false;
        public static Thread discordthread;
        public static Thread twitchthread;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            bool isfull = false;
            isfull = GameResources.Optionss.Fullscreen;
            if (isfull)
            {
                graphics.PreferredBackBufferHeight = System.Windows.Forms.SystemInformation.WorkingArea.Height;
                graphics.PreferredBackBufferWidth = System.Windows.Forms.SystemInformation.WorkingArea.Width;
            }
            else
            {
                graphics.PreferredBackBufferHeight = System.Windows.Forms.SystemInformation.WorkingArea.Height - 200;
                graphics.PreferredBackBufferWidth = System.Windows.Forms.SystemInformation.WorkingArea.Width - 400;
            }
            if (isfull)
            {
                Window.IsBorderless = true;
                graphics.IsFullScreen = true;
            }
            else
            {
                graphics.IsFullScreen = false;
            }
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.IsFixedTimeStep = true;
            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 500.0f);
            Window.Position = new Point(50, 50);
            graphics.SynchronizeWithVerticalRetrace = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameResources.Load(Content);
            IngamePopup.Preload(spriteBatch, GameResources.popup);
        }

        protected override void UnloadContent()
        {
        }

        public static KeyboardState kstate;
        protected override void Update(GameTime gameTime)
        {
            if (exiting)
            {
                Exit();
            }
            if (MediaPlayer.State != MediaState.Playing && GameResources.GameScreen != 2 && GameResources.GameScreen != 6)
            {
                Music.play(GameResources.selected, 0);
            }
            base.Update(gameTime);
            if (GameResources.GameScreen == 0)
            {
                GameScreens.MainMenu.Update();
            }
            if (GameResources.GameScreen == 1)
            {
                // Not needed
            }
            if (GameResources.GameScreen == 2)
            {
                GameScreens.MapScreen.Update(Content, GraphicsDevice);
            }
            if (GameResources.GameScreen == 6)
            {
                GameScreens.Offset.update();
            }
        }

        Sprite cursor;
        public static bool pressed = false;
        public static Stream stream;
        public static RenderTarget2D rt;
        public static bool cycling = false;
        protected override void Draw(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.F2) && Keyboard.GetState().IsKeyDown(Keys.F3) && cycling == false)
            {
                cycling = true;
            }
            if (cycling)
            {
                GameResources.LoopColor();
            }
            Window.Title = "Rizumu: " + GameResources.Maps[GameResources.selected].Name;
            KeyboardState oldstate = kstate;
            kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.F12) && !oldstate.IsKeyDown(Keys.F12))
            {
                rt = new RenderTarget2D(GraphicsDevice, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, false, SurfaceFormat.Color, DepthFormat.Depth24Stencil8);
                GraphicsDevice.SetRenderTarget(rt);
                stream = new FileStream("screenshots/screenshot" + DateTime.Now.Ticks + ".png", FileMode.Create, FileAccess.Write, FileShare.None);
                pressed = true;
                IngamePopup.SetPopup("Screenshot made", "screenshot is saved in /screenshots");
            }
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

            if (GameResources.GameScreen == 0)
            {
                GameScreens.MainMenu.Draw(spriteBatch);
            }
            if (GameResources.GameScreen == 1)
            {
                GameScreens.SongList.Draw(spriteBatch);
            }
            if (GameResources.GameScreen == 2)
            {
                GameScreens.MapScreen.Draw(spriteBatch, gameTime);
            }
            if (GameResources.GameScreen == 3)
            {
                GameScreens.Score.Draw(spriteBatch);
            }
            if (GameResources.GameScreen == 4)
            {
                GameScreens.Options.Draw(spriteBatch);
            }
            if (GameResources.GameScreen == 5)
            {
                GameScreens.Keybinds.draw(spriteBatch);
            }
            if (GameResources.GameScreen == 6)
            {
                GameScreens.Offset.draw(spriteBatch);
            }

            // Cursor!
            mstate = Mouse.GetState();
            cursorbox = new Rectangle(mstate.X + GameResources.Cursor.Width / 2, mstate.Y + GameResources.Cursor.Height / 2, 1, 1);
            cursor = new Sprite(spriteBatch, mstate.X, mstate.Y, GameResources.Cursor, GameResources.basecolor);
            if (GameResources.showcursor)
            {
                cursor.draw();
            }

            Text.draw(GameResources.debug, "Github Build", 0, graphics.PreferredBackBufferHeight - 20, spriteBatch);

            int ci = 0;
            while (ci < GameResources.comments.Count)
            {
                string[] s = GameResources.comments[ci];
                string text = s[0];
                int x = int.Parse(s[1]);
                int y = int.Parse(s[2]);
                Text.draw(GameResources.font, text, x, y, spriteBatch);
                x = x + 10;
                GameResources.comments[ci] = new string[] { text, x.ToString(), y.ToString() };
                ci++;
            }

            IngamePopup.TryDraw();

            spriteBatch.End();
            if (pressed)
            {
                rt.SaveAsPng(stream, 1080, 720);
                stream.Close();
                GraphicsDevice.SetRenderTarget(null);
                pressed = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.F5))
            {
                GameResources.GameScreen = 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.F6) && GameResources.GameScreen == 5)
            {
                GameResources.GameScreen = 0;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.F7))
            {
                IngamePopup.SetPopup("Test", "this is a test popup");
            }

            base.Draw(gameTime);
        }
    }
}
