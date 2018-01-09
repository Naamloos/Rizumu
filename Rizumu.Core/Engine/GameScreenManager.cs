using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rizumu.GameLogic;
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

        public static void ChangeScreen(GameScreenType screen, RizumuGame game)
        {
            var returns = GameScreenReturns.Empty();
            if(_screen != null)
                returns = _screen.Unload(screen);

            switch (screen)
            {
                default:
                case GameScreenType.None:
                    // unknown screen, throw error
                    _screen = new ErrorScreen();
                    returns.Message = "You came across a game screen that does not (yet) exist! You silly goose!";
                    break;
                case GameScreenType.MainMenu:
                    // do main menu
                    _screen = new MainMenu();
                    break;
                case GameScreenType.SongSelect:
                    _screen = new SongSelect();
                    break;
            }
            _screen.Initialize(returns, game);
        }

        public static void CatchError(string error, RizumuGame game)
        {
            if (_screen != null)
                _screen.Unload(GameScreenType.None);
            _screen = new ErrorScreen();
            var returns = GameScreenReturns.Empty();
            returns.Message = error;
            _screen.Initialize(returns, game);
        }

        public static void UnloadCurrent()
        {
            _screen.Unload(GameScreenType.None);
        }

        public static void UpdateCurrent(GameTime gt, MouseValues mv)
        {
            if(_screen != null)
                _screen.Update(gt, mv);
        }

        public static void DrawCurrent(SpriteBatch sb, GameTime gt, MouseValues mv)
        {
            if (_screen != null)
                _screen.Draw(sb, gt, mv);
        }
    }

    internal class GameScreenReturns
    {
        public GameScreenType PreviousScreen;
        public string Message = "";

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
        None
    }

    internal interface IGameScreen
    {
        void Draw(SpriteBatch spriteBatch, GameTime gameTime, MouseValues mouseValues);
        void Update(GameTime gameTime, MouseValues mouseValues);
        void Initialize(GameScreenReturns values, RizumuGame game);
        GameScreenReturns Unload(GameScreenType NewScreen);
    }
}
