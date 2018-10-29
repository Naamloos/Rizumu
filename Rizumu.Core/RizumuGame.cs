using DiscordRPC;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Rizumu.Engine;
using Rizumu.Engine.Entities;
using Rizumu.GameLogic;
using Rizumu.GameLogic.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Rizumu
{
    public class Entry
    {
        static void Main()
        {
            Logger.EnableLoggerDump();
            Logger.Log("Checking folder/file prerequisites");
            // Check folder prerequisites
            if (!Directory.Exists("songs"))
                Directory.CreateDirectory("songs");
            if (!File.Exists("settings.json"))
            {
                var fs = File.Create("settings.json");
                byte[] emptysettings = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Settings()));
                fs.Write(emptysettings, 0, emptysettings.Length);
                fs.Close();
            }
#if DEBUG
            if (!Directory.Exists("songs/mock"))
            {
                Directory.CreateDirectory("songs/mock");
                File.Create("songs/mock/map.json").Close();
                var m = new RizumuMap();
                m.Difficulties.Add(new RizumuDifficulty()
                {
                    Name = "pritty-hard",
                    NotesDown = new List<int>() { 20 },
                    NotesLeft = new List<int>() { 20 },
                    NotesRight = new List<int>() { 20 },
                    NotesUp = new List<int>() { 20 },
                    Offset = 0
                });
                m.Author = "Debug";
                File.WriteAllText("songs/mock/map.json", JsonConvert.SerializeObject(m, Formatting.Indented));
            }
#endif
            Console.CursorVisible = false;
            Logger.Log("Starting game");
            using (var game = new RizumuGame(Environment.OSVersion.Platform.ToString()))
            {
                game.Run();
            }

#if DEBUG
            Console.ReadKey();
#endif
        }
    }

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
        public static DiscordRpcClient DiscordRpc;
        public static Settings Settings;
        SpriteBatch SpriteBatch;
        MouseValues MouseValues = new MouseValues();

        public RizumuGame(string platform)
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            DiscordRpc = new DiscordRpcClient("493799075528048641");
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
            Logger.Log("Initializing Discord RPC");
            DiscordRpc.OnReady += Rpc_OnReady;
            DiscordRpc.Initialize();

            Logger.Log($"Setting Rich Presence");
            DiscordRpc.SetPresence(new RichPresence()
            {
                Assets = new Assets()
                {
                    LargeImageKey = "logo",
                    LargeImageText = "Rizumu"
                },
                Details = "Main Menu",
            });
        }

        private void Rpc_OnReady(object sender, DiscordRPC.Message.ReadyMessage args)
        {
            Logger.Log($"Initialized Discord RPC with user {args.User.Username}#{args.User.Discriminator} ({args.User.ID})");
        }

        protected override void LoadContent()
        {
            Logger.Log("Loading various contents");
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Font = Content.Load<SpriteFont>("fonts/default");
            MetaFont = Content.Load<SpriteFont>("fonts/Metadata");
            Hit = Content.Load<SoundEffect>("soundfx/hit");

            Logger.Log("Loading textures");
            TextureManager.LoadTexture(Content, "testing/texture", "test");
            TextureManager.LoadTexture(Content, "backgrounds/main_bg", "menu");
            TextureManager.LoadTexture(Content, "gui/button", "button");
            TextureManager.LoadTexture(Content, "gui/buttonhover", "buttonhover");
            TextureManager.LoadTexture(Content, "gui/logo", "logo");
            TextureManager.LoadTexture(Content, "gui/sad", "sad");
            TextureManager.LoadTexture(Content, "gui/selectorbox", "selectorbox");
            TextureManager.LoadTexture(Content, "gui/songselect_overlay", "selectoverlay");
            TextureManager.LoadTexture(Content, "ingame/note", "note");

            Logger.Log("Loading maps");
            MapManager.LoadMaps(GraphicsDevice);

            GameScreenManager.ChangeScreen(GameScreenType.MainMenu, this);
        }

        double _oldms = 0;
        protected override void Update(GameTime gameTime)
        {
            MouseValues.Update(Mouse.GetState(Window), this.Window.ClientBounds.Width, this.Window.ClientBounds.Height, this.IsActive);
            if (_oldms < gameTime.ElapsedGameTime.TotalMilliseconds)
            {
                // millisecond based updates here
                AnimationManager.UpdateValues();
            }
            _oldms = gameTime.ElapsedGameTime.TotalMilliseconds;

            base.Update(gameTime);
            GameScreenManager.UpdateCurrent(gameTime, MouseValues);
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
            DiscordRpc.Dispose();
            Logger.Log("Bye bye!");
            Logger.UnloadAndDispose();
            this.Exit();
        }
    }
}
