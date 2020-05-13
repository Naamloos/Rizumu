using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Newtonsoft.Json;
using Rizumu.Engine;
using Rizumu.Engine.Entities;
using Rizumu.GameLogic;
using Rizumu.GameLogic.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Rizumu
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class RizumuGame : Game
    {
        public static SpriteFont Font;
        public static SpriteFont MetaFont;
        public static SoundEffect Hit;
        GraphicsDeviceManager Graphics;
        public static RenderTarget2D RT;
        public static Settings Settings;
        SpriteBatch SpriteBatch;
        MouseValues MouseValues = new MouseValues();
        public static SoundEffect menumusic;
        public static SoundEffect welcome;
        public InputManager input;

        public RizumuGame(string platform)
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Logger.Log("Initializing game");
            base.Initialize();
            this.IsMouseVisible = true;
            this.Graphics.PreferredBackBufferHeight = 844;
            this.Graphics.PreferredBackBufferWidth = 1500;
            this.Graphics.ApplyChanges();
            RT = new RenderTarget2D(SpriteBatch.GraphicsDevice, 1920, 1080);
            Logger.Log("Loading Settings");
            Settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText("settings.json"));
            input = new InputManager(Settings);
        }

        protected override void LoadContent()
        {
            Logger.Log("Loading various contents");
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Font = Content.Load<SpriteFont>("fonts/default");
            MetaFont = Content.Load<SpriteFont>("fonts/Metadata");
            Hit = Content.Load<SoundEffect>("soundfx/hit");
            menumusic = Content.Load<SoundEffect>("soundfx/menu");
            welcome = Content.Load<SoundEffect>("soundfx/welcome");

            Logger.Log("Loading textures");
            TextureManager.LoadTexture(Content, "testing/texture", "test");
            TextureManager.LoadTexture(Content, "backgrounds/paint_small", "menu");
            TextureManager.LoadTexture(Content, "gui/button_idle", "button");
            TextureManager.LoadTexture(Content, "gui/button_hover", "buttonhover");
            TextureManager.LoadTexture(Content, "gui/flatlogo", "logo");
            TextureManager.LoadTexture(Content, "gui/sad", "sad");
            TextureManager.LoadTexture(Content, "gui/selectorbox", "selectorbox");
            TextureManager.LoadTexture(Content, "gui/songselect_overlay", "selectoverlay");
            TextureManager.LoadTexture(Content, "ingame/note", "note");

            Logger.Log("Loading maps");
            MapManager.LoadMaps(GraphicsDevice);
            welcome.Play(0.5f, 0.0f, 0.0f);
            MusicManager.Play(MapManager.LoadedMaps.Values.ToList()[new Random().Next(0, MapManager.LoadedMaps.Count)].MapSong);

            GameScreenManager.ChangeScreen(GameScreenType.MainMenu, this);
            GraphicsDevice.SetRenderTarget(GameScreenManager._fadeTarget);
            GraphicsDevice.Clear(Color.Black);
        }

        double _oldms = 0;
        protected override void Update(GameTime gameTime)
        {
            MusicManager.CheckMapComplete();
            MouseValues.Update(Mouse.GetState(Window), this.Window.ClientBounds.Width, this.Window.ClientBounds.Height, this.IsActive);
            if (_oldms < gameTime.ElapsedGameTime.TotalMilliseconds)
            {
                // millisecond based updates here
                AnimationManager.UpdateValues();
            }
            _oldms = gameTime.ElapsedGameTime.TotalMilliseconds;

            base.Update(gameTime);
            GameScreenManager.UpdateCurrent(gameTime, MouseValues, input);
            if(input.FullscreenToggle)
            {
                if(!Graphics.IsFullScreen)
                {
                    this.Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                    this.Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                    Graphics.IsFullScreen = true;
                    Window.IsBorderless = true;
                    this.Graphics.ApplyChanges();
                }
                else
                {
                    this.Graphics.PreferredBackBufferHeight = 844;
                    this.Graphics.PreferredBackBufferWidth = 1500;
                    Graphics.IsFullScreen = false;
                    Window.IsBorderless = false;
                    this.Graphics.ApplyChanges();
                }
            }
            input.Update();
            MediaPlayer.Volume = 0.5f;
        }

        protected override void Draw(GameTime gameTime)
        {
            SpriteBatch.GraphicsDevice.SetRenderTarget(RT);
            SpriteBatch.Begin(SpriteSortMode.Immediate);

            GraphicsDevice.Clear(Color.Black);
            GameScreenManager.DrawCurrent(SpriteBatch, gameTime, MouseValues);
            SpriteBatch.End();

            SpriteBatch.GraphicsDevice.SetRenderTarget(null);
            SpriteBatch.Begin();
            SpriteBatch.Draw(RT, new Rectangle(0, 0, this.Window.ClientBounds.Width, this.Window.ClientBounds.Height), Color.White);
            SpriteBatch.End();

            base.Draw(gameTime);
        }

        public void ExitUnload()
        {
            Logger.Log("Exiting and unloading game");
            GameScreenManager.UnloadCurrent();
            TextureManager.UnloadAll();
            Logger.Log("Bye bye!");
            Logger.UnloadAndDispose();
            this.Exit();
        }
    }
}
