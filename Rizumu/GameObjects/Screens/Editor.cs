using Rizumu.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rizumu.Objects;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Rizumu.GameObjects.Screens
{
    class Editor : IScreen
    {
        public string Name { get => "editor"; }

        public bool Recording = false;
        public int Timer = 0;
        public List<int> Left = new List<int>();
        public List<int> Up = new List<int>();
        public List<int> Right = new List<int>();
        public List<int> Down = new List<int>();

        public Text Instructions;

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Rectangle cursor, bool clicked)
        {
            Instructions.Draw();
        }

        public void Preload(SpriteBatch spriteBatch, GraphicsDeviceManager Graphics)
        {
            OldState = Keyboard.GetState();
            Instructions = new Text(spriteBatch, GameData.Instance.CurrentSkin.FontSmall, "Press R to record and press keys to the music.\rBetter editor to be made soon-ish\nS to save.",
                0, 15, Color.Chocolate);
            Instructions.X = (Graphics.PreferredBackBufferWidth / 2) - (Instructions.Width / 2);
        }

        KeyboardState OldState;
        public void Update(GameTime gameTime, Rectangle cursor, bool clicked)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.R) && !Recording)
            {
                GameData.MusicManager.Change(new Map()
                {
                    Path = "editor/",
                    FileName = "song.mp3",
                    Name = "editorsong",
                    Creator = "me"
                });

                Recording = true;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.S) && Recording)
            {
                SaveMap();
                GameData.Instance.Exiting = true;
            }

            if (Recording)
            {
                var NewState = Keyboard.GetState();
                if(NewState.IsKeyDown(Keys.NumPad4) && !OldState.IsKeyDown(Keys.NumPad4))
                {
                    Left.Add(Timer);
                }
                if (NewState.IsKeyDown(Keys.NumPad8) && !OldState.IsKeyDown(Keys.NumPad8))
                {
                    Up.Add(Timer);
                }
                if (NewState.IsKeyDown(Keys.NumPad6) && !OldState.IsKeyDown(Keys.NumPad6))
                {
                    Right.Add(Timer);
                }
                if (NewState.IsKeyDown(Keys.NumPad2) && !OldState.IsKeyDown(Keys.NumPad2))
                {
                    Down.Add(Timer);
                }
                OldState = NewState;
                Timer++;
            }
            else
            {
                GameData.MusicManager.Pause();
            }
        }

        public void SaveMap()
        {
            Map m = new Map()
            {
                Name = "Song",
                BackgroundFile = "bg.png",
                Creator = "User",
                Description = "Made by you\nyes yes it is",
                FileName = "song.mp3",
                NotesDown = Down,
                NotesLeft = Left,
                NotesRight = Right,
                NotesUp = Up,
            };

            JObject map = JObject.FromObject(m);
            File.Create("editor/map.json").Close();
            File.WriteAllText("editor/map.json", map.ToString());
        }
    }
}
