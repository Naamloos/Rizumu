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
using Microsoft.Xna.Framework.Media;

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
                .AddButton(-25, 240, "play", "button", "buttonhover", GuiOrigin.BottomLeft, "Singleplayer", GuiOrigin.BottomRight, new Vector2(30, 10))
                .AddButton(-25, 65, "multi", "button", "buttonhover", GuiOrigin.BottomLeft, "Multiplayer", GuiOrigin.BottomRight, new Vector2(30, 10))
                .AddButton(-25, 240, "settings", "button", "buttonhover", GuiOrigin.BottomRight, "Options", GuiOrigin.BottomLeft, new Vector2(30, 10))
                .AddButton(-25, 65, "exit", "button", "buttonhover", GuiOrigin.BottomRight, "Exit", GuiOrigin.BottomLeft, new Vector2(30, 10))
                .AddSprite(710, 200, "logo", "logo", Origin: GuiOrigin.TopLeft)
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
                case "multi":
                    GameScreenManager.ChangeScreen(GameScreenType.Multi, this._game);
                    break;
            }
        }

        public GameScreenReturns Unload(GameScreenType NewScreen)
        {
            values.PreviousScreen = GameScreenType.MainMenu;
            return values;
        }

        public void Update(GameTime gameTime, MouseValues mouseValues, InputManager input)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Pause))
                GameScreenManager.CatchError("shlob on my knob like corn on da cob", this._game);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, MouseValues mouseValues)
        {
            this._menu.Draw(spriteBatch, mouseValues);
        }
    }
}
