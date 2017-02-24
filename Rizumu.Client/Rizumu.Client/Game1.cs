using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rizumu.Client
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            // Set new GraphicsDeviceManager
            graphics = new GraphicsDeviceManager(this);
            // Set Content root, Donut change
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            // Initialize Static Stuff
            IsMouseVisible = true;
            // Set current View to Main
            Assets.Views.Current = Assets.Gameview.Main;
            StaticStuff.Background = Content.Load<Texture2D>("naamloos");
            Assets.Textures.Button = Content.Load<Texture2D>("button");
            Assets.Textures.ButtonSelected = Content.Load<Texture2D>("button_selected");
            #region Main view
            // This initializes the Main View.
            // Create new View for Main
            Assets.Views.Initialize();
            #endregion
        }

        protected override void LoadContent()
        {
            // Load SpriteBatch
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            // Run Update for current View

            // Update mouse state
            StaticStuff.oldMouseState = StaticStuff.mouseState;
            StaticStuff.mouseState = Mouse.GetState();

            #region Update switch
            UpdateEventArgs eventArgs = new UpdateEventArgs()
            {
                gameTime = gameTime
            };

            switch (Assets.Views.Current)
            {
                case Assets.Gameview.Main:
                    Assets.Views.Main.Update(eventArgs);
                    break;
                case Assets.Gameview.Songselect:
                    break;
                case Assets.Gameview.Options:
                    break;
                case Assets.Gameview.Ingame:
                    break;
                case Assets.Gameview.Results:
                    break;
                default:
                    break;
            }
            #endregion

            // Run a new Update
            base.Update(gameTime);
        }

        /*
         * I'm planning to Draw the game into 3 layers:
         * Layer 0: Background Layer (Continuously draws the Background Texture2D, to be changed by views)
         * Layer 1: View Layer (All main view controls and textures should be drawn here)
         * Layer 2: Overlay Layer (Has a black transparent background. If active, Layer 1 will not respond to input)
         * layer 3: Maybe a border? unsure.
         */

        protected override void Draw(GameTime gameTime)
        {
            // Clear the GraphicsDevice for a new draw
            GraphicsDevice.Clear(Color.WhiteSmoke);

            spriteBatch.Begin();
            // Layer 0:
            spriteBatch.Draw(StaticStuff.Background, new Rectangle(0, 0, (int)graphics.PreferredBackBufferWidth, (int)graphics.PreferredBackBufferHeight), Color.White);

            // Layer 1:
            #region Draw switch
            DrawEventArgs eventArgs = new DrawEventArgs()
            {
                gameTime = gameTime,
                spriteBatch = spriteBatch
            };

            switch (Assets.Views.Current)
            {
                case Assets.Gameview.Main:
                    Assets.Views.Main.Draw(eventArgs);
                    break;
                case Assets.Gameview.Songselect:
                    break;
                case Assets.Gameview.Options:
                    break;
                case Assets.Gameview.Ingame:
                    break;
                case Assets.Gameview.Results:
                    break;
                default:
                    Assets.Views.Main.Draw(eventArgs);
                    break;
            }
            #endregion

            // Layer 2:
            #region Overlay switch
            if (StaticStuff.OverlayEnable)
            {
                switch (StaticStuff.Overlay)
                {
                    case GameOverlay.Chat:
                        break;
                    case GameOverlay.Login:
                        break;
                    case GameOverlay.Loading:
                        break;
                    default:
                        break;
                }
            }
            #endregion

            // Layer 3: 

            spriteBatch.DrawString(Content.Load<SpriteFont>("debug"), $"{StaticStuff.mouseState.X}, {StaticStuff.mouseState.Y}", new Vector2(0, 0), Color.Gray);
            spriteBatch.End();
            // Run a new Draw
            base.Draw(gameTime);
        }
    }
}
