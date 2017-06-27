using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rizumu;
using System.IO;
using Microsoft.Xna.Framework.Media;
using System;

namespace RizumuMapEditor
{
    public class Game1 : Game
    {
        #region Game shit
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        EditorTools tools = new EditorTools();
        #endregion

        #region Editor shits
        Background bg;
        public static string backgroundpath = "";
        public static string newbackgroundpath = "";
        public static string mp3path = "";
        public static string newmp3path = "";
        public static event EventHandler<MusicEventArgs> PlayerUpdate;
        #endregion

        #region renderable shit
        Texture2D menu_bg;
        Song music;
        #endregion

        public Game1()
        {
            if (!Directory.Exists("map"))
            {
                Directory.CreateDirectory("map");
            }
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 500;
            graphics.PreferredBackBufferWidth = 888;
            this.IsMouseVisible = true;
            tools.Show();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            menu_bg = Content.Load<Texture2D>("menubg");
            bg = new Background(spriteBatch, menu_bg);
            music = Content.Load<Song>("default");
            PlayerUpdate += (sender, e) =>
            {
                if (e.Action == MusicPlayerAction.Play)
                    MediaPlayer.Play(music);
                if (e.Action == MusicPlayerAction.Pause)
                    MediaPlayer.Pause();
            };
            MediaPlayer.Play(music);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            SharedEvents.InvokePlaybar((int)MediaPlayer.PlayPosition.TotalSeconds);
            if(backgroundpath != newbackgroundpath)
            {
                bg.texture = Texture2D.FromStream(GraphicsDevice, new FileStream(newbackgroundpath, FileMode.Open));
                backgroundpath = newbackgroundpath;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            bg.draw(888, 500);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
