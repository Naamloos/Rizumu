using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json.Linq;
using Rizumu.Engine;
using Rizumu.Objects;
using System;
using System.IO;
using System.Windows.Forms;

namespace Rizumu
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle CursorLocation;
        MouseState OldMouseState;
        TimeSpan OGTimeSpan;
        bool Click;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.Position = new Point(50, 50);
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

            if(GameData.Instance.Options.Fullscreen == true)
            {
                // hacky shit stolen from stackoverflow
                graphics.PreferredBackBufferHeight = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
                graphics.PreferredBackBufferWidth = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
                graphics.IsFullScreen = true;
                Window.IsBorderless = true;

                graphics.ApplyChanges();
            }

            if(GameData.Instance.Options.SkinName == "default")
                GameData.Instance.CurrentSkin = GameData.Instance.DefaultSkin;
            else
            {
                if(Directory.Exists("skins/" + GameData.Instance.Options.SkinName))
                    GameData.Instance.CurrentSkin = Skin.LoadFromPath(GraphicsDevice, Content, "skins/" + GameData.Instance.Options.SkinName);
                else
                    GameData.Instance.CurrentSkin = GameData.Instance.DefaultSkin;
            }

            GameData.MapManager = new Helpers.MapManager();
            bool hasmaps = GameData.MapManager.Preload();
            GameData.MapManager.PreloadSongs();

            if (!hasmaps)
            {
                MessageBox.Show("Please add some maps to '/songs' before playing!");
                Environment.Exit(-1);
            }
            GameData.Instance.CurrentSkin.Hello.Play();
            GameData.MusicManager = new Helpers.MusicManager();
            GameData.MapManager.PreloadBackgrounds(spriteBatch);
            GameData.Instance.LoadScreens(spriteBatch, graphics);
            graphics.GraphicsDevice.PresentationParameters.PresentationInterval = PresentInterval.Immediate;
            graphics.GraphicsDevice.PresentationParameters.MultiSampleCount = 500;
            Framerate = new Text(spriteBatch, GameData.Instance.CurrentSkin.Font, "0", 0, 0, Color.White);
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
                if(!GameData.Instance.Options.Fullscreen)
                Window.IsBorderless = false;
            }

            CursorLocation = new Rectangle(Mouse.GetState(Window).Position, new Point(1, 1));

            Click = false;
            MouseState current = Mouse.GetState();
            if (current.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed 
                && OldMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released
                && IsActive)
                Click = true;
            OldMouseState = current;

            GameData.Instance.Screens.Find(x => x.Name == GameData.Instance.CurrentScreen).Update(gameTime, CursorLocation, Click);
            base.Update(gameTime);
            if(GameData.Instance.Exiting)
                Exit();
        }

        Text Framerate;
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
            GameData.Instance.Screens.Find(x => x.Name == GameData.Instance.CurrentScreen).Draw(spriteBatch, gameTime, CursorLocation, Click);
            Framerate.Content = $"{Math.Round(1 / gameTime.ElapsedGameTime.TotalSeconds)} FPS";
            Framerate.X = graphics.PreferredBackBufferWidth - Framerate.Width;
            Framerate.Y = graphics.PreferredBackBufferHeight - Framerate.Height;
            Framerate.Draw();
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
