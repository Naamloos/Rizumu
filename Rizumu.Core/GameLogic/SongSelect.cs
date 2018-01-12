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
        private SongSelector _selector;

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, MouseValues mouseValues)
        {
            this._select.Draw(spriteBatch, mouseValues);
            this._selector.Draw(spriteBatch, gameTime, mouseValues);
        }

        public void Initialize(GameScreenReturns values, RizumuGame game)
        {
            this._selector = new SongSelector(game.GraphicsDevice);
            this._select = new GuiBuilder()
                .AddBackground("menu")
                .AddButton(15, 25, "back", "button", "buttonhover", GuiOrigin.BottomLeft, "Back", GuiOrigin.BottomRight)
                .AddButton(15, 25, "mods", "button", "buttonhover", GuiOrigin.BottomRight, "Mods", GuiOrigin.BottomLeft)
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
