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
using Microsoft.Xna.Framework.Media;
using Rizumu.GameLogic.Entities;
using Microsoft.Xna.Framework.Input;

namespace Rizumu.GameLogic
{
    class SongSelect : IGameScreen
    {
        private Gui _select;
        private RizumuGame _game { get; set; }
        private GuiScrollable _scroller;
        private Gui _songs;
        private Sprite Thumbnail;
        private int _selectedmapid = 1;
        private int _previoustickmap = 0;
        private GameScreenReturns values;
        private Sprite _selectoverlay;
        private RizumuMap _selectedmap = null;

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, MouseValues mouseValues)
        {
            this._select.Draw(spriteBatch, mouseValues);
            this._scroller.Draw(_songs, spriteBatch, mouseValues);
            //spriteBatch.DrawString(RizumuGame.Font, $"Current selected map: {MapManager.LoadedMaps[_selectedmapid].ArtistName} - {MapManager.LoadedMaps[_selectedmapid].SongName}",
            //	new Vector2(3, 3), Color.Crimson);

            foreach (var s in _songs.Items)
            {
                if (s.ItemId == $"map{_selectedmapid}")
                {
                    s.Texture.Scale = 1.15f;
                    s.TextureHover.Scale = 1.15f;
                }
                else
                {
                    s.Texture.Scale = 1.0f;
                    s.TextureHover.Scale = 1.0f;
                }
            }

            if (_previoustickmap != _selectedmapid)
            {
                Thumbnail.Texture2D = MapManager.LoadedMaps[_selectedmapid]?.Thumbnail;
                Thumbnail.Empty = Thumbnail.Texture2D == null;
                values.firstload = false;
                MusicManager.Play(MapManager.LoadedMaps[_selectedmapid].MapSong);
            }

            Thumbnail.Draw(spriteBatch, Width: 400, Height: 225);

            _selectoverlay.Draw(spriteBatch, 0, 0);

            var height = RizumuGame.Font.MeasureString("Mate I only need the height").Y + 2;

            if (_selectedmap.Enabled)
            {
                spriteBatch.DrawString(RizumuGame.Font, $"Artist:", new Vector2(30, 300), Color.Gray);
                spriteBatch.DrawString(RizumuGame.MetaFont, $"{(_selectedmap.ArtistName.Length > 0 ? _selectedmap.ArtistName : "Unknown")}", new Vector2(30, 305 + height), Color.White);
                spriteBatch.DrawString(RizumuGame.Font, $"Song name:", new Vector2(30, 300 + height * 2), Color.Gray);
                spriteBatch.DrawString(RizumuGame.MetaFont, $"{_selectedmap.SongName}", new Vector2(30, 305 + height * 3), Color.White);
                spriteBatch.DrawString(RizumuGame.Font, $"Creator:", new Vector2(30, 300 + height * 4), Color.Gray);
                spriteBatch.DrawString(RizumuGame.MetaFont, $"{_selectedmap.Author}", new Vector2(30, 305 + height * 5), Color.White);
                spriteBatch.DrawString(RizumuGame.Font, $"ID in memory:", new Vector2(30, 300 + height * 6), Color.Gray);
                spriteBatch.DrawString(RizumuGame.MetaFont, $"{_selectedmapid}", new Vector2(30, 305 + height * 7), Color.White);
            }

            _previoustickmap = _selectedmapid;

            if (Keyboard.GetState().IsKeyDown(Keys.F2))
            {
                GameScreenManager.ChangeScreen(GameScreenType.InGame, this._game);
                Logger.Log("Entered map screen in debug mode!");
            }
        }

        public void Initialize(GameScreenReturns values, RizumuGame game)
        {
            this._game = game;
            this._select = new GuiBuilder()
                .AddBackground("menu")
                .AddButton(-25, 65, "back", "button", "buttonhover", GuiOrigin.BottomLeft, "Back", GuiOrigin.BottomRight, new Vector2(30, 10))
                .AddButton(-25, 65, "play", "button", "buttonhover", GuiOrigin.BottomRight, "Play", GuiOrigin.BottomLeft, new Vector2(30, 10))
                .Build();

            var sngs = new GuiBuilder();
            int i = 0;
            foreach (var m in MapManager.LoadedMaps)
            {
                if (m.Value.Enabled)
                {
                    string artist = m.Value.ArtistName.Length > 0 ? $"by {m.Value.ArtistName}" : "Unknown Artist";
                    sngs.AddSongButton(0, i * 150, $"map{m.Key}", "button", "buttonhover", GuiOrigin.TopLeft, $"{m.Value.SongName}", artist,
                        GuiOrigin.TopLeft, new Vector2(55, 25));
                    i++;
                }
            }
            values.SelectedMap = MapManager.LoadedMaps.First(x => x.Value.MapSong == MusicManager.Song).Key;

            this._songs = sngs.Build();

            this._scroller = new GuiScrollable(660, 0, 600, 1080, ScrollDirection.Vertical);

            _select.OnClick += _select_OnClick;
            _songs.OnClick += _songs_OnClick;

            Thumbnail = "";
            Thumbnail.Location = new Point(25, 50);
            Thumbnail.Texture2D = MapManager.LoadedMaps[values.SelectedMap]?.Thumbnail;
            Thumbnail.Empty = Thumbnail.Texture2D == null;
            _selectoverlay = "selectoverlay";
            _selectedmapid = values.SelectedMap;
            _selectedmap = MapManager.LoadedMaps[values.SelectedMap];
            _previoustickmap = _selectedmapid;
            this.values = values;
        }

        private void _select_OnClick(object sender, GuiEventArgs e)
        {
            if (e.Id == "back")
                GameScreenManager.ChangeScreen(GameScreenType.MainMenu, this._game);
            else if(e.Id == "play")
                GameScreenManager.ChangeScreen(GameScreenType.InGame, this._game);

        }

        private void _songs_OnClick(object sender, GuiEventArgs e)
        {
            if (e.Id.StartsWith("map"))
            {
                var idstr = e.Id.Substring(3);
                _selectedmapid = int.Parse(idstr);
                // bloop new map selected.
            }
        }

        public GameScreenReturns Unload(GameScreenType NewScreen)
        {
            values.PreviousScreen = GameScreenType.SongSelect;
            values.SelectedMap = _selectedmapid;
            return values;
        }

        public void Update(GameTime gameTime, MouseValues mouseValues, InputManager input)
        {

        }
    }
}
