using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rizumu.GameLogic;
using Rizumu.GameLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Engine
{
    internal class GameScreenManager
    {
        private static IGameScreen _screen = null;
        private static RizumuGame _game;
        private static float _fade = 0.0f;
        public static RenderTarget2D _fadeTarget;
        private static DateTime _fadeStart = DateTime.Now;
        private static int fadeTime = 3000;
        private static bool firstFade = true;

        public static void ChangeScreen(GameScreenType screen, RizumuGame game)
        {
            if(_fadeTarget == null)
            {
                _fadeTarget = new RenderTarget2D(game.GraphicsDevice, 1920, 1080);
            }
            var returns = GameScreenReturns.Empty();
            if (_screen != null)
                returns = _screen.Unload(screen);

            switch (screen)
            {
                default:
                case GameScreenType.None:
                    // unknown screen, throw error
                    _screen = new ErrorScreen();
                    returns.Message = "Sorry Mario, but the princess is in another castle!\nOr, err.. I mean this game screen isn't here..";
                    break;
                case GameScreenType.MainMenu:
                    // do main menu
                    _screen = new MainMenu();
                    break;
                case GameScreenType.SongSelect:
                    _screen = new SongSelect();
                    break;
                case GameScreenType.InGame:
                    _screen = new InGame();
                    break;
            }
            Logger.Log($"Switched to gamescreen with type {_screen.GetType().ToString()}");

            _fade = 1.0f;
            _fadeStart = DateTime.Now;
            _screen.Initialize(returns, game);
        }

        public static void CatchError(string error, RizumuGame game)
        {
            if (_screen != null)
                _screen.Unload(GameScreenType.None);
            _screen = new ErrorScreen();
            var returns = GameScreenReturns.Empty();
            returns.Message = error;
            Logger.Log(error);
            _screen.Initialize(returns, game);
        }

        public static void UnloadCurrent()
        {
            _screen.Unload(GameScreenType.None);
        }

        public static void UpdateCurrent(GameTime gt, MouseValues mv, InputManager input)
        {
            if (_screen != null)
                _screen.Update(gt, mv, input);
        }

        public static void DrawCurrent(SpriteBatch sb, GameTime gt, MouseValues mv)
        {
            if (_screen != null)
                _screen.Draw(sb, gt, mv);

            if (_fade >= 1.0f && !firstFade)
            {
                // Render our rendertarget to a rendertarget So we can render our new render target back to the rendertarget and fade it out
                sb.GraphicsDevice.SetRenderTarget(_fadeTarget);
                sb.Draw(RizumuGame.RT, new Rectangle(0, 0, 1920, 1080), null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0f);
                sb.GraphicsDevice.SetRenderTarget(RizumuGame.RT);
                fadeTime = 500;
            }
            if (_fade > 0.0f)
            {
                var col = new Color(_fade, _fade, _fade, _fade);
                sb.Draw(_fadeTarget, new Rectangle(0, 0, 1920, 1080), null, col, 0.0f, Vector2.Zero, SpriteEffects.None, 0f);
                _fade = Easings.QuadraticEaseOut(1.0f - (float)(DateTime.Now.Subtract(_fadeStart).TotalMilliseconds / fadeTime));
            }

            if(firstFade)
            {
                firstFade = false;
            }
        }
    }

    internal class GameScreenReturns
    {
        public GameScreenType PreviousScreen;
        public string Message = "";
        public int SelectedMap = 1;
        public bool firstload = true;
        public RizumuScoreData Score;
        public string LoadedDifficulty;
        public UserData Player;

        public static GameScreenReturns Empty()
        {
            return new GameScreenReturns();
        }
    }

    internal enum GameScreenType
    {
        MainMenu,
        SongSelect,
        Options,
        InGame,
        Results,
        Multi,
        None
    }

    internal interface IGameScreen
    {
        void Draw(SpriteBatch spriteBatch, GameTime gameTime, MouseValues mouseValues);
        void Update(GameTime gameTime, MouseValues mouseValues, InputManager input);
        void Initialize(GameScreenReturns values, RizumuGame game);
        GameScreenReturns Unload(GameScreenType NewScreen);
    }
}
