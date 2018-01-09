using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rizumu.Engine;
using Rizumu.Engine.Entities;
using System;

namespace Rizumu
{
    public class Entry
    {
        static void Main()
        {
            using (var game = new RizumuGame(Environment.OSVersion.Platform.ToString()))
            {
                game.Run();
            }
        }
    }

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class RizumuGame : Game
    {
        public static SpriteFont Font;
        GraphicsDeviceManager Graphics;
        RenderTarget2D RT;
        SpriteBatch SpriteBatch;
        MouseValues MouseValues = new MouseValues();

        public RizumuGame(string platform)
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.IsMouseVisible = true;
            this.Graphics.PreferredBackBufferHeight = 720;
            this.Graphics.PreferredBackBufferWidth = 1280;
            this.Graphics.ApplyChanges();
            RT = new RenderTarget2D(SpriteBatch.GraphicsDevice, 1920, 1080);
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Font = Content.Load<SpriteFont>("fonts/default");

            TextureManager.LoadTexture(Content, "testing/texture", "test");
            TextureManager.LoadTexture(Content, "backgrounds/main_bg", "menu");
            TextureManager.LoadTexture(Content, "gui/button", "button");
            TextureManager.LoadTexture(Content, "gui/buttonhover", "buttonhover");
            TextureManager.LoadTexture(Content, "gui/logo", "logo");
            GameScreenManager.ChangeScreen(GameScreenType.MainMenu, this);
        }

        protected override void UnloadContent()
        {
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
            SpriteBatch.Begin();
            GraphicsDevice.Clear(Color.CornflowerBlue);
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
            GameScreenManager.UnloadCurrent();
            TextureManager.UnloadAll();
            this.Exit();
        }
    }
}
