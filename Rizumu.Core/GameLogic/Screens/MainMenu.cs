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
using Microsoft.Xna.Framework.Input;

namespace Rizumu.GameLogic
{
    internal class MainMenu : IGameScreen
    {
        private GameScreenReturns _startvalues { get; set; }
        private RizumuGame _game { get; set; }
        private Gui _menu;
		private GameScreenReturns values;

        public void Initialize(GameScreenReturns values, RizumuGame game)
        {
            this._startvalues = values;
            this._game = game;
            // Build GUI
            this._menu = new GuiBuilder()
                .AddBackground("menu")
                .AddButton(15, 125, "play", "button", "buttonhover", GuiOrigin.TopRight, "Play", GuiOrigin.BottomLeft, new Vector2(30, 0))
                .AddButton(115, 275, "settings", "button", "buttonhover", GuiOrigin.TopRight, "Settings", GuiOrigin.BottomLeft, new Vector2(30, 0))
                .AddButton(15, 425, "exit", "button", "buttonhover", GuiOrigin.TopRight, "Exit", GuiOrigin.BottomLeft, new Vector2(30, 0))
                .AddSprite(-100, -100, "logo", "logo", Origin: GuiOrigin.BottomRight, widthoverride: 300, heightoverride: 300)
                .Build();

            // Set GUI events to handler methods
            this._menu.OnClick += Menu_OnClick;
			this.values = values;
        }

        private void Menu_OnClick(object sender, GuiEventArgs e)
        {
            /*
             * The GUI events are just generic events that relay events sent by GUI Items.
             * The ID value held by the GuiEventArgs tells us what item we've received events from.
             */

            switch (e.Id)
            {
                case "exit":
                    this._game.ExitUnload();
                    break;
                case "play":
                    GameScreenManager.ChangeScreen(GameScreenType.SongSelect, this._game);
                    break;
                case "settings":
                    GameScreenManager.ChangeScreen(GameScreenType.Options, this._game);
                    break;
            }
        }

        public GameScreenReturns Unload(GameScreenType NewScreen)
        {
			values.PreviousScreen = GameScreenType.MainMenu;
			return values;
        }

        public void Update(GameTime gameTime, MouseValues mouseValues)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Pause))
                GameScreenManager.CatchError("slob on my know like corn on da cob", this._game);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, MouseValues mouseValues)
        {
            this._menu.Draw(spriteBatch, mouseValues);
        }
    }
}
