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

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime, MouseValues mouseValues)
		{
			this._select.Draw(spriteBatch, mouseValues);
			this._scroller.Draw(_songs, spriteBatch, mouseValues);
			spriteBatch.DrawString(RizumuGame.Font, $"Current selected map: {MapManager.LoadedMaps[_selectedmapid].ArtistName} - {MapManager.LoadedMaps[_selectedmapid].SongName}",
				new Vector2(3, 3), Color.Crimson);

			if (_previoustickmap != _selectedmapid)
			{
				Thumbnail.Texture2D = MapManager.LoadedMaps[_selectedmapid]?.Thumbnail;
				Thumbnail.Empty = Thumbnail.Texture2D == null;
				MediaPlayer.Stop();
				values.firstload = false;
				MediaPlayer.Volume = 0.2f;
				MediaPlayer.Play(MapManager.LoadedMaps[_selectedmapid].MapSong);
			}

			Thumbnail.Draw(spriteBatch, Width: 400, Height: 225);
			_previoustickmap = _selectedmapid;
		}

		public void Initialize(GameScreenReturns values, RizumuGame game)
		{
			this._game = game;
			this._select = new GuiBuilder()
				.AddBackground("menu")
				.AddButton(15, 25, "back", "button", "buttonhover", GuiOrigin.BottomLeft, "Back", GuiOrigin.BottomRight, new Vector2(55, 0))
				.AddButton(15, 135, "mods", "button", "buttonhover", GuiOrigin.BottomLeft, "Mods", GuiOrigin.BottomRight, new Vector2(55, 0))
				.Build();

			var sngs = new GuiBuilder();
			int i = 0;
			foreach (var m in MapManager.LoadedMaps)
			{
				if (m.Value.Enabled)
				{
					sngs.AddButton(0, i * 110, $"map{m.Key}", "button", "buttonhover", GuiOrigin.TopLeft, $"{m.Value.SongName}\n{m.Value.ArtistName}",
						GuiOrigin.BottomLeft, new Vector2(55, 0));
					i++;
				}
			}

			this._songs = sngs.Build();

			this._scroller = new GuiScrollable(1320, 0, 600, 1080, ScrollDirection.Vertical);

			_select.OnClick += _select_OnClick;
			_songs.OnClick += _songs_OnClick;

			Thumbnail = "";
			Thumbnail.Location = new Point(25, 50);
			_selectedmapid = values.SelectedMap;
			this.values = values;
		}

		private void _select_OnClick(object sender, GuiEventArgs e)
		{
			if (e.Id == "back")
				GameScreenManager.ChangeScreen(GameScreenType.MainMenu, this._game);
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

		public void Update(GameTime gameTime, MouseValues mouseValues)
		{

		}
	}
}
