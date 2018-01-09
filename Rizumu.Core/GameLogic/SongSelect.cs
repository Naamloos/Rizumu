using Rizumu.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rizumu.Engine.GUI;
using Rizumu.Engine.Entities;

namespace Rizumu.GameLogic
{
    class SongSelect : IGameScreen
    {
        private Gui _select;

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, MouseValues mouseValues)
        {
            _select.Draw(spriteBatch, mouseValues);
        }

        public void Initialize(GameScreenReturns values, RizumuGame game)
        {
            this._select = new GuiBuilder()
                .AddBackground("menu")
                .AddSprite(300, 300, "logo", "logo", Origin: GuiOrigin.TopLeft)
                .Build();
        }

        public GameScreenReturns Unload(GameScreenType NewScreen)
        {
            return new GameScreenReturns()
            {
                PreviousScreen = GameScreenType.SongSelect
            };
        }

        public void Update(GameTime gameTime, MouseValues mouseValues)
        {
            
        }
    }
}
