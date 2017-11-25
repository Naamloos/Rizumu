using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rizumu.Core.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Core.Engine
{
    internal class GameScreenManager
    {
        private static IGameScreen _screen = null;

        public static void ChangeScreen(GameScreenType screen, SpriteBatch sb, GraphicsDeviceManager gdm)
        {
            var returns = GameScreenReturns.Empty();
            if(_screen != null)
                returns = _screen.Unload(screen);

            switch (screen)
            {
                default:
                case GameScreenType.MainMenu:
                    // do main menu
                    _screen = new MainMenu();
                    break;
            }
            _screen.Initialize(sb, gdm, returns);
        }

        public static void UpdateCurrent(GameTime gt)
        {
            _screen.Update(gt);
        }

        public static void DrawCurrent(SpriteBatch sb, GameTime gt)
        {
            _screen.Draw(sb, gt);
        }
    }

    internal class GameScreenReturns
    {
        public GameScreenType PreviousScreen;
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
        Results
    }

    internal interface IGameScreen
    {
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
        void Update(GameTime gameTime);
        void Initialize(SpriteBatch spriteBatch, GraphicsDeviceManager Graphics, GameScreenReturns values);
        GameScreenReturns Unload(GameScreenType NewScreen);
    }
}
