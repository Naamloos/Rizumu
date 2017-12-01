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
    internal class MainMenu : IGameScreen
    {
        private GameScreenReturns _startvalues { get; set; }
        private Gui Menu;

        public void Initialize(SpriteBatch spriteBatch, GraphicsDeviceManager Graphics, GameScreenReturns values)
        {
            this._startvalues = values;
            this.Menu = new GuiBuilder()
                .AddBackground("menu")
                .AddButton(50, 80, "exit", "button", "buttonhover", "Exit", GuiTextOrigin.BottomRight)
                .Build();
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
            Menu.Draw(spriteBatch, mouseValues);
        }
    }
}
