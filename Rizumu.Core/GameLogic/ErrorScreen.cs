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
    internal class ErrorScreen : IGameScreen
    {
        private Gui _data;
        private RizumuGame _game { get; set; }
        private GameScreenReturns _startvalues { get; set; }
        private TimeSpan _errorwait;
        private DateTimeOffset _errorstart;

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, MouseValues mouseValues)
        {
            this._data.Draw(spriteBatch, mouseValues);
        }

        public void Initialize(GameScreenReturns values, RizumuGame game)
        {
            _errorwait = TimeSpan.FromSeconds(10);
            _errorstart = DateTimeOffset.Now;
            this._startvalues = values;
            this._game = game;
            _data = new GuiBuilder()
                .AddSprite(200, 200, "message", "", text: $"Whope something went wrong!\n\nError: {values.Message}\n\nExiting in 10 seconds...")
                .Build();
        }

        public GameScreenReturns Unload(GameScreenType NewScreen)
        {
            return GameScreenReturns.Empty();
        }

        public void Update(GameTime gameTime, MouseValues mouseValues)
        {
            if(DateTimeOffset.Now.Subtract(_errorstart) > _errorwait)
                this._game.ExitUnload();
        }
    }
}
