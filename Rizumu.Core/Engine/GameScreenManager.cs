﻿using Microsoft.Xna.Framework;
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

        public static void ChangeScreen(GameScreenType screen, RizumuGame game)
        {
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
                    RizumuGame.DiscordRpc.UpdateDetails("oof");
                    break;
                case GameScreenType.MainMenu:
                    // do main menu
                    _screen = new MainMenu();
                    if(RizumuGame.DiscordRpc.IsInitialized)
                        RizumuGame.DiscordRpc.UpdateDetails("Main Menu");
                    break;
                case GameScreenType.SongSelect:
                    _screen = new SongSelect();
                    if (RizumuGame.DiscordRpc.IsInitialized)
                        RizumuGame.DiscordRpc.UpdateDetails("Song Select");
                    break;
                case GameScreenType.InGame:
                    _screen = new InGame();
                    if (RizumuGame.DiscordRpc.IsInitialized)
                        RizumuGame.DiscordRpc.UpdateDetails("In Game");
                    break;
            }
            Logger.Log($"Switched to gamescreen with type {_screen.GetType().ToString()}");

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
