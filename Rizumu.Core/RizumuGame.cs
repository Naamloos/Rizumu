using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rizumu.Core.Engine;
using Rizumu.Core.Engine.Entities;
using System;

namespace Rizumu.Core
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class RizumuGame : Game
    {
        GraphicsDeviceManager Graphics;
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
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            TextureManager.LoadTexture(Content, "testing/texture", "test");
            GameScreenManager.ChangeScreen(GameScreenType.MainMenu, SpriteBatch, Graphics);
        }

        protected override void UnloadContent()
        {
        }

        double _oldms = 0;
        protected override void Update(GameTime gameTime)
        {
            MouseValues.Update(Mouse.GetState());
            if(_oldms < gameTime.ElapsedGameTime.TotalMilliseconds)
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
            GraphicsDevice.Clear(Color.CornflowerBlue);
            SpriteBatch.Begin();
            GameScreenManager.DrawCurrent(SpriteBatch, gameTime, MouseValues);
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
