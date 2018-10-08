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
using Rizumu.GameLogic.Entities;
using Microsoft.Xna.Framework.Media;

namespace Rizumu.GameLogic
{
	class InGame : IGameScreen
	{
		Gui _ingamegui;
		GameScreenReturns _data;
		RizumuMap _loadedmap;
		RizumuDifficulty _loadeddifficulty;
		int _traveltime = 200;
        RizumuGame gamu;

		List<RizumuLeftNote> LeftNotes = new List<RizumuLeftNote>();
		List<RizumuUpNote> UpNotes = new List<RizumuUpNote>();
		List<RizumuRightNote> RightNotes = new List<RizumuRightNote>();
		List<RizumuDownNote> DownNotes = new List<RizumuDownNote>();

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime, MouseValues mouseValues)
		{
			// Draw shit here
			_ingamegui.Draw(spriteBatch, mouseValues);

			foreach (var n in LeftNotes)
				n.Render(spriteBatch);

			foreach (var n in UpNotes)
				n.Render(spriteBatch);

			foreach (var n in RightNotes)
				n.Render(spriteBatch);

			foreach (var n in DownNotes)
				n.Render(spriteBatch);
		}

		public void Initialize(GameScreenReturns values, RizumuGame game)
		{
			Logger.Log("Initializing ingame...");
			// Init
			_data = values;
			_loadedmap = MapManager.LoadedMaps[_data.SelectedMap];
			_loadeddifficulty = _loadedmap.Difficulties.First(/*x => x.Name == _data.LoadedDifficulty*/);
            gamu = game;
			Logger.Log("Loaded map / difficulty without issues!");
            RizumuGame.DiscordRpc.UpdateState($"{_loadedmap.ArtistName} - {_loadedmap.SongName} [{_loadeddifficulty.Name}]");

			var nspr = TextureManager.GetTexture("note");

			_ingamegui = new GuiBuilder()
				.AddBackground("menu")
				.AddSprite((1920 / 2) - (int)(nspr.Width * 1.5), (1080 / 2) - (nspr.Height / 2), "leftsp", "note")
				.AddSprite((1920 / 2) - (nspr.Width / 2), (1080 / 2) - (int)(nspr.Height * 1.5), "upsp", "note")
				.AddSprite((1920 / 2) + (int)(nspr.Width * 0.5), (1080 / 2) - (nspr.Height / 2), "rightsp", "note")
				.AddSprite((1920 / 2) - (nspr.Width / 2), (1080 / 2) + (int)(nspr.Height * 0.5), "downsp", "note")
				.Build();

			// hacky swapping of background, meh.
			_ingamegui.Items.First(x => x.Type == GuiItemType.Background).Texture.Texture2D = _loadedmap.Background;

			foreach(var n in _loadeddifficulty.NotesLeft)
				LeftNotes.Add(new RizumuLeftNote(n, "note", _traveltime));
			Logger.Log("Loaded left notes");

			foreach (var n in _loadeddifficulty.NotesRight)
				RightNotes.Add(new RizumuRightNote(n, "note", _traveltime));
			Logger.Log("Loaded right notes");

			foreach (var n in _loadeddifficulty.NotesUp)
				UpNotes.Add(new RizumuUpNote(n, "note", _traveltime));
			Logger.Log("Loaded up notes");

			foreach (var n in _loadeddifficulty.NotesDown)
				DownNotes.Add(new RizumuDownNote(n, "note", _traveltime));
			Logger.Log("Loaded down notes");

			// Done initializing, start playing music or smth
			// Stop old tune
			MediaPlayer.Stop();
			Logger.Log("Stopped song");
			// Start new tune
			MediaPlayer.Play(_loadedmap.MapSong);
			Logger.Log("Started song for map");
		}

		public GameScreenReturns Unload(GameScreenType NewScreen)
		{
			// TODO: DON'T FILL WITH CONSTANTS YOU COCK
			var score = new RizumuScoreData()
			{
				LeftHits = LeftNotes.Count(x => x.Hit = true),
				RightHits = RightNotes.Count(x => x.Hit = true),
				UpHits = UpNotes.Count(x => x.Hit = true),
				DownHits = DownNotes.Count(x => x.Hit = true),

				LeftMisses = LeftNotes.Count(x => x.Miss = true),
				RightMisses = RightNotes.Count(x => x.Miss = true),
				UpMisses = UpNotes.Count(x => x.Miss = true),
				DownMisses = DownNotes.Count(x => x.Miss = true),

				MapData = this._loadedmap,
				Player = this._data.Player
			};

            RizumuGame.DiscordRpc.UpdateState("");

			_data.Score = score;

			return _data;
		}

        KeyboardState _previousState;
        float lscale = 1f;
        float uscale = 1f;
        float rscale = 1f;
        float dscale = 1f;
		public void Update(GameTime gameTime, MouseValues mouseValues)
		{
            if (_previousState == null)
                _previousState = new KeyboardState();

            var ks = Keyboard.GetState();

            var leftpress = ks.IsKeyDown(RizumuGame.Settings.LeftKey) && _previousState.IsKeyUp(RizumuGame.Settings.LeftKey);
            var rightpress = ks.IsKeyDown(RizumuGame.Settings.RightKey) && _previousState.IsKeyUp(RizumuGame.Settings.RightKey);
            var uppress = ks.IsKeyDown(RizumuGame.Settings.UpKey) && _previousState.IsKeyUp(RizumuGame.Settings.UpKey);
            var downpress = ks.IsKeyDown(RizumuGame.Settings.DownKey) && _previousState.IsKeyUp(RizumuGame.Settings.DownKey);

            if (leftpress)
                lscale = 1.5f;
            if (uppress)
                uscale = 1.5f;
            if (rightpress)
                rscale = 1.5f;
            if (downpress)
                dscale = 1.5f;

            if (lscale > 1f)
                lscale -= 0.1f;
            if (rscale > 1f)
                rscale -= 0.1f;
            if (uscale > 1f)
                uscale -= 0.1f;
            if (dscale > 1f)
                dscale -= 0.1f;

            this._ingamegui.Items.First(x => x.ItemId == "leftsp").Texture.Scale = lscale;
            this._ingamegui.Items.First(x => x.ItemId == "upsp").Texture.Scale = uscale;
            this._ingamegui.Items.First(x => x.ItemId == "rightsp").Texture.Scale = rscale;
            this._ingamegui.Items.First(x => x.ItemId == "downsp").Texture.Scale = dscale;

            _previousState = ks;


            int time = (int)((MediaPlayer.PlayPosition.TotalMilliseconds * 500) / 1000) + _loadeddifficulty.Offset;

            foreach (var n in LeftNotes)
				n.Update(ref leftpress, time);

			foreach (var n in UpNotes)
				n.Update(ref uppress, time);

			foreach (var n in RightNotes)
				n.Update(ref rightpress, time);

			foreach (var n in DownNotes)
				n.Update(ref downpress, time);

            if (Keyboard.GetState().IsKeyDown(Keys.F3))
            {
                GameScreenManager.ChangeScreen(GameScreenType.MainMenu, gamu);
                Logger.Log("Force-quit ingame!");
            }
        }
	}
}
