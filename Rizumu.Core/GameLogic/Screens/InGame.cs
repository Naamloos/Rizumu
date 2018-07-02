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

namespace Rizumu.GameLogic.Screens
{
	class InGame : IGameScreen
	{
		GameScreenReturns _data;
		RizumuMap _loadedmap;

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime, MouseValues mouseValues)
		{
			// Draw shit here
		}

		public void Initialize(GameScreenReturns values, RizumuGame game)
		{
			// Init
			_data = values;
		}

		public GameScreenReturns Unload(GameScreenType NewScreen)
		{
			// TODO: DON'T FILL WITH CONSTANTS YOU COCK
			var score = new RizumuScoreData()
			{
				LeftHits = 5,
				RightHits = 5,
				UpHits = 5,
				DownHits = 5,

				LeftMisses = 5,
				RightMisses = 5,
				UpMisses = 5,
				DownMisses = 5,

				MapData = this._loadedmap,
				Player = null
			};

			return _data;
		}

		public void Update(GameTime gameTime, MouseValues mouseValues)
		{
			// Update values
		}
	}
}
