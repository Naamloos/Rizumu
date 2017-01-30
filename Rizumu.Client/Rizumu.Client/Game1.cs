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
            // Set current View to Main
            StaticStuff.View = Gameview.Main;
            StaticStuff.Background = Content.Load<Texture2D>("naamloos");

            #region Main view
            // This initializes the Main View.
            // Create new View for Main
            StaticStuff.Main = new View();

            // Set UpdateEvent
            StaticStuff.Main.UpdateEvent += (sender, e) =>
            {
            };

            // Set DrawEvent
            StaticStuff.Main.DrawEvent += (sender, e) =>
            {
                Texture2D naam = Content.Load<Texture2D>("naamloos");
                e.spriteBatch.Draw(naam, new Rectangle(0, 0, 100, 100), Color.White);
            };
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
            #region Update switch
            UpdateEventArgs eventArgs = new UpdateEventArgs()
            {
                gameTime = gameTime
            };

            switch (StaticStuff.View)
            {
                case Gameview.Main:
                    StaticStuff.Main.Update(eventArgs);
                    break;
                case Gameview.Songselect:
                    break;
                case Gameview.Options:
                    break;
                case Gameview.Ingame:
                    break;
                case Gameview.Results:
                    break;
                default:
                    break;
            }
            #endregion

            // Update mouse state
            StaticStuff.oldMouseState = StaticStuff.mouseState;
            StaticStuff.mouseState = Mouse.GetState();

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

            switch (StaticStuff.View)
            {
                case Gameview.Main:
                    StaticStuff.Main.Draw(eventArgs);
                    break;
                case Gameview.Songselect:
                    break;
                case Gameview.Options:
                    break;
                case Gameview.Ingame:
                    break;
                case Gameview.Results:
                    break;
                default:
                    StaticStuff.Main.Draw(eventArgs);
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

            spriteBatch.End();
            // Run a new Draw
            base.Draw(gameTime);
        }
    }
}
