using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using Newtonsoft.Json.Linq;
using Rizumu.Engine;
using Rizumu.Objects;
using System;
using System.IO;

namespace Rizumu
{
    public class Game1 : Game
    {
        public static EventHandler<EventArgs.RegisterAndroidUriArgs> RegisterAndroidUri;
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle CursorLocation;
        MouseState OldMouseState;
        TimeSpan OGTimeSpan;
        bool Click;
        RenderTarget2D RenderTarget;
        public static bool Windows;
        TouchLocationState oldtls;

        public Game1(int width, int height, bool Windows = false)
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            GameData.globalheight = height;
            GameData.globalwidth = width;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Making sure OldMouseState to prevent errors
            OldMouseState = Mouse.GetState();
            IsFixedTimeStep = false;
            OGTimeSpan = TargetElapsedTime;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameData.Instance = new GameData(Content);
            try
            {
                GameData.Instance.Options = JObject.Parse(File.ReadAllText("Options.json")).ToObject<Options>();
            }
            catch (Exception)
            {
                // Errored, read defaults and delete old Options;
                if (File.Exists("Options.json"))
                    File.Delete("Options.json");
                GameData.Instance.Options = new Options();
            }

            if (GameData.Instance.Options.Fullscreen == true)
            {
                // hacky shit stolen from stackoverflow
                graphics.IsFullScreen = true;
                Window.IsBorderless = false;

                graphics.ApplyChanges();
            }

            if (GameData.Instance.Options.SkinName == "default")
                GameData.Instance.CurrentSkin = GameData.Instance.DefaultSkin;
            else
            {
                if (Directory.Exists("skins/" + GameData.Instance.Options.SkinName))
                    GameData.Instance.CurrentSkin = Skin.LoadFromPath(GraphicsDevice, Content, "skins/" + GameData.Instance.Options.SkinName);
                else
                    GameData.Instance.CurrentSkin = GameData.Instance.DefaultSkin;
            }

            GameData.MapManager = new Helpers.MapManager();
            GameData.realheight = Window.ClientBounds.Height;
            GameData.realwidth = Window.ClientBounds.Width;
            bool hasmaps = GameData.MapManager.Preload();
            GameData.MapManager.PreloadSongs();

            if (!hasmaps)
            {
#if WINDOWS
                System.Windows.Forms.MessageBox.Show("Please add some maps to '/songs' before playing!");
#endif
                Environment.Exit(-1);
            }
            GameData.Instance.CurrentSkin.Hello.Play();
            GameData.MusicManager = new Helpers.MusicManager();
            GameData.MapManager.PreloadBackgrounds(spriteBatch);
            GameData.Instance.LoadScreens(spriteBatch, graphics);
            graphics.GraphicsDevice.PresentationParameters.PresentationInterval = PresentInterval.Immediate;
            graphics.GraphicsDevice.PresentationParameters.MultiSampleCount = 500;
            Framerate = new Text(spriteBatch, GameData.Instance.CurrentSkin.Font, "0", 0, 0, Color.White);

            RenderTarget = new RenderTarget2D(GraphicsDevice, GameData.globalwidth, GameData.globalheight);

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
#if DEBUG
            // SAVE ME KEY
            if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F12))
                Exit();
#endif

            if (GameData.Instance.CurrentScreen == "ingame" || GameData.Instance.CurrentScreen == "editor")
            {
                //TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 500.0f);
            }
            else
            {
                TargetElapsedTime = OGTimeSpan;
#if WINDOWS
                if (!GameData.Instance.Options.Fullscreen)
                    Window.IsBorderless = false;
#endif
            }

            var p = Mouse.GetState(Window).Position;

            CursorLocation = new Rectangle(p, new Point(1, 1));

            if (Windows)
            {
                Click = false;
                MouseState current = Mouse.GetState();
                if (current.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed
                    && OldMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released
                    && IsActive)
                    Click = true;
                OldMouseState = current;
            }
            else
            {
                TouchCollection touchCollection = TouchPanel.GetState();

                var pos = new Point(0, 0);
                Click = false;
                if (touchCollection.Count > 0)
                {
                    pos.X = (int)touchCollection[0].Position.X;
                    pos.Y = (int)touchCollection[0].Position.Y;
                    Click = touchCollection[0].State == TouchLocationState.Pressed && oldtls == (TouchLocationState)0;
                    oldtls = touchCollection[0].State;
                }
                else
                {
                    oldtls = (TouchLocationState)0;
                }

                CursorLocation = new Rectangle(pos, new Point(1, 1));
            }

            GameData.Instance.Screens.Find(x => x.Name == GameData.Instance.CurrentScreen).Update(gameTime, CursorLocation, Click);
            base.Update(gameTime);
            if (GameData.Instance.Exiting)
                Exit();
        }

        Text Framerate;
        protected override void Draw(GameTime gameTime)
        {
            if (MediaPlayer.State == MediaState.Stopped)
                GameData.MusicManager.Restart();
            GraphicsDevice.SetRenderTarget(RenderTarget);
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
            GameData.Instance.Screens.Find(x => x.Name == GameData.Instance.CurrentScreen).Draw(spriteBatch, gameTime, CursorLocation, Click, GraphicsDevice);
            Framerate.Content = $"{Math.Round(1 / gameTime.ElapsedGameTime.TotalSeconds)} FPS";

            Framerate.X = GameData.globalwidth - Framerate.Width;
            Framerate.Y = GameData.globalheight - Framerate.Height;

            Framerate.Draw();
            spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(samplerState: SamplerState.LinearWrap);
            spriteBatch.Draw(RenderTarget, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
