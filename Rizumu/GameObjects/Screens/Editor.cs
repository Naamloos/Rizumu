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
using Microsoft.Xna.Framework.Media;
using Rizumu.Enums;

namespace Rizumu.GameObjects.Screens
{
    class Editor : IScreen
    {
        public Screen Name => Screen.Editor;

        public bool Recording = false;
        public int Timer = 0;
        public List<int> Left = new List<int>();
        public List<int> Up = new List<int>();
        public List<int> Right = new List<int>();
        public List<int> Down = new List<int>();

        public List<int> SLeft = new List<int>();
        public List<int> SUp = new List<int>();
        public List<int> SRight = new List<int>();
        public List<int> SDown = new List<int>();

        public int HLeft = 0;
        public int HUp = 0;
        public int HRight = 0;
        public int HDown = 0;

        public Text Instructions;

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Rectangle cursor, bool clicked, GraphicsDevice g)
        {
            Instructions.Draw(spriteBatch);
        }

        public void Preload(SpriteBatch spriteBatch, GraphicsDeviceManager Graphics)
        {
            OldState = Keyboard.GetState();
            Instructions = new Text(GameData.Instance.CurrentSkin.FontSmall, "Press R to record and press keys to the music.\rBetter editor to be made soon-ish\nS to save.",
                0, 15, Color.Chocolate);
            Instructions.X = (GameData.globalwidth / 2) - (Instructions.Width / 2);
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
                Timer = (int)((MediaPlayer.PlayPosition.TotalMilliseconds * 500) / 1000);
                var NewState = Keyboard.GetState();
                if (NewState.IsKeyDown(Keys.NumPad4))
                {
                    if(!OldState.IsKeyDown(Keys.NumPad4))
                        Left.Add(Timer);
                }
                else
                    HLeft = Timer;

                if (NewState.IsKeyDown(Keys.NumPad8))
                {
                    if (!OldState.IsKeyDown(Keys.NumPad8))
                        Up.Add(Timer);
                }
                else
                    HUp = Timer;
                if (NewState.IsKeyDown(Keys.NumPad6))
                {
                    if (!OldState.IsKeyDown(Keys.NumPad6))
                        Right.Add(Timer);
                }
                else
                    HRight = Timer;
                if (NewState.IsKeyDown(Keys.NumPad2))
                {
                    if (!OldState.IsKeyDown(Keys.NumPad2))
                        Down.Add(Timer);
                }
                else
                    HDown = Timer;

                if (HLeft < Timer - 250 && !SLeft.Contains(Timer))
                {
                    if (HLeft == Timer - 250)
                        Left.Remove(Left.Last());
                    SLeft.Add(Timer);
                }
                if (HUp < Timer - 250 && !SUp.Contains(Timer))
                {
                    if(HUp == Timer - 250)
                        Up.Remove(Up.Last());
                    SUp.Add(Timer);
                }
                if (HRight < Timer - 250 && !SRight.Contains(Timer))
                {
                    if (HRight == Timer - 250)
                        Right.Remove(Right.Last());
                    SRight.Add(Timer);
                }
                if (HDown < Timer - 250 && !SDown.Contains(Timer))
                {
                    if (HDown == Timer - 250)
                        Down.Remove(Down.Last());
                    SDown.Add(Timer);
                }

                OldState = NewState;
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
                SlidesDown = SDown,
                SlidesLeft = SLeft,
                SlidesRight = SRight,
                SlidesUp = SUp,
                Offset = 0
            };

            JObject map = JObject.FromObject(m);
            File.Create("editor/map.json").Close();
            File.WriteAllText("editor/map.json", map.ToString());
        }
    }
}
