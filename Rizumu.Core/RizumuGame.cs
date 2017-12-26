using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rizumu.Engine;
using Rizumu.Engine.Entities;
using System;
using MyGameEngine.Engine.Input;

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
        
        public RizumuGame(string platform)
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.IsMouseVisible = true;
            this.Graphics.PreferredBackBufferHeight = 540;
            this.Graphics.PreferredBackBufferWidth = 960;
            RT = new RenderTarget2D(SpriteBatch.GraphicsDevice, 1920, 1080);
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Font = Content.Load<SpriteFont>("fonts/default");

            TextureManager.LoadTexture(Content, "testing/texture", "test");
            TextureManager.LoadTexture(Content, "backgrounds/menuscreen", "menu");
            TextureManager.LoadTexture(Content, "gui/button", "button");
            TextureManager.LoadTexture(Content, "gui/buttonhover", "buttonhover");
            GameScreenManager.ChangeScreen(GameScreenType.MainMenu, SpriteBatch, Graphics);
        }

        protected override void UnloadContent()
        {
        }

        double _oldms = 0;
        protected override void Update(GameTime gameTime)
        {
            KeyboardInput.Update();
            MouseInput.Update(this.Window.ClientBounds.Width, this.Window.ClientBounds.Height);
            if(_oldms < gameTime.ElapsedGameTime.TotalMilliseconds)
            {
                // millisecond based updates here
                AnimationManager.UpdateValues();
            }
            _oldms = gameTime.ElapsedGameTime.TotalMilliseconds;

            base.Update(gameTime);
            GameScreenManager.UpdateCurrent(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            SpriteBatch.GraphicsDevice.SetRenderTarget(RT);
            SpriteBatch.Begin();
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GameScreenManager.DrawCurrent(SpriteBatch, gameTime);
            SpriteBatch.End();

            SpriteBatch.GraphicsDevice.SetRenderTarget(null);
            SpriteBatch.Begin();
            SpriteBatch.Draw(RT, new Rectangle(0, 0, this.Window.ClientBounds.Width, this.Window.ClientBounds.Height), Color.White);
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
