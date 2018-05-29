using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Rizumu.Engine;
using Rizumu.Engine.Entities;
using Rizumu.GameLogic;
using Rizumu.GameLogic.Entities;
using System;
using System.Collections.Generic;
using System.IO;

namespace Rizumu
{
	public class Entry
	{
		static void Main()
		{
			// Check folder prerequisites
			if (!Directory.Exists("songs"))
			{
				Directory.CreateDirectory("songs");
			}
#if DEBUG
			if (!Directory.Exists("songs/mock"))
			{
				Directory.CreateDirectory("songs/mock");
				File.Create("songs/mock/map.json").Close();
				var m = new RizumuMap();
				m.Difficulties.Add(new RizumuDifficulty()
				{
					Name = "pritty-hard",
					NotesDown = new List<int>() { 20 },
					NotesLeft = new List<int>() { 20 },
					NotesRight = new List<int>() { 20 },
					NotesUp = new List<int>() { 20 },
					Offset = 0
				});
				m.Author = "Debug";
				File.WriteAllText("songs/mock/map.json", JsonConvert.SerializeObject(m, Formatting.Indented));
			}
#endif
			using (var game = new RizumuGame(Environment.OSVersion.Platform.ToString()))
			{
				game.Run();
			}
		}
	}

	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class RizumuGame : Game
	{
		public static SpriteFont Font;
		GraphicsDeviceManager Graphics;
		public static RenderTarget2D RT;
		SpriteBatch SpriteBatch;
		MouseValues MouseValues = new MouseValues();

		public RizumuGame(string platform)
		{
			Graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			base.Initialize();
			this.IsMouseVisible = true;
			this.Graphics.PreferredBackBufferHeight = 720;
			this.Graphics.PreferredBackBufferWidth = 1280;
			this.Graphics.ApplyChanges();
			RT = new RenderTarget2D(SpriteBatch.GraphicsDevice, 1920, 1080);
		}

		protected override void LoadContent()
		{
			SpriteBatch = new SpriteBatch(GraphicsDevice);
			Font = Content.Load<SpriteFont>("fonts/default");

			TextureManager.LoadTexture(Content, "testing/texture", "test");
			TextureManager.LoadTexture(Content, "backgrounds/main_bg", "menu");
			TextureManager.LoadTexture(Content, "gui/button", "button");
			TextureManager.LoadTexture(Content, "gui/buttonhover", "buttonhover");
			TextureManager.LoadTexture(Content, "gui/logo", "logo");
			TextureManager.LoadTexture(Content, "gui/sad", "sad");
			TextureManager.LoadTexture(Content, "gui/selectorbox", "selectorbox");
			MapManager.LoadMaps(GraphicsDevice);
			GameScreenManager.ChangeScreen(GameScreenType.MainMenu, this);
		}

		double _oldms = 0;
		protected override void Update(GameTime gameTime)
		{
			MouseValues.Update(Mouse.GetState(Window), this.Window.ClientBounds.Width, this.Window.ClientBounds.Height, this.IsActive);
			if (_oldms < gameTime.ElapsedGameTime.TotalMilliseconds)
			{
				// millisecond based updates here
				AnimationManager.UpdateValues();
			}
			_oldms = gameTime.ElapsedGameTime.TotalMilliseconds;

			base.Update(gameTime);
			GameScreenManager.UpdateCurrent(gameTime, MouseValues);
		}

		protected override void Draw(GameTime gameTime)
		{
			SpriteBatch.GraphicsDevice.SetRenderTarget(RT);
			SpriteBatch.Begin(SpriteSortMode.Immediate);

			GraphicsDevice.Clear(Color.Black);
			GameScreenManager.DrawCurrent(SpriteBatch, gameTime, MouseValues);
			SpriteBatch.End();

			SpriteBatch.GraphicsDevice.SetRenderTarget(null);
			SpriteBatch.Begin();
			SpriteBatch.Draw(RT, new Rectangle(0, 0, this.Window.ClientBounds.Width, this.Window.ClientBounds.Height), Color.White);
			SpriteBatch.End();

			base.Draw(gameTime);
		}

		public void ExitUnload()
		{
			GameScreenManager.UnloadCurrent();
			TextureManager.UnloadAll();
			this.Exit();
		}
	}
}
