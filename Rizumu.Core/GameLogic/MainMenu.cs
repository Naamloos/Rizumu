using Rizumu.Core.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rizumu.Core.Engine.GUI;
using Rizumu.Core.Engine.Entities;

namespace Rizumu.Core.GameLogic
{
    internal class MainMenu : IGameScreen
    {
        private GameScreenReturns _startvalues { get; set; }

        public void Initialize(SpriteBatch spriteBatch, GraphicsDeviceManager Graphics, GameScreenReturns values)
        {
            this._startvalues = values;
        }

        public GameScreenReturns Unload(GameScreenType NewScreen)
        {
            return new GameScreenReturns()
            {
                PreviousScreen = GameScreenType.MainMenu
            };
        }

        public void Update(GameTime gameTime, MouseValues mouseValues)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, MouseValues mouseValues)
        {

        }
    }
}
