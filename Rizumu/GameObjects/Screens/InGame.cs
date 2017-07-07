using Rizumu.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rizumu.Objects;
using Microsoft.Xna.Framework.Input;
using Rizumu.GuiObjects;

namespace Rizumu.GameObjects.Screens
{
    class InGame : IScreen
    {
        public Map Playing;
        public List<Note> NotesLeft;
        public List<Note> NotesUp;
        public List<Note> NotesRight;
        public List<Note> NotesDown;
        public bool MapLoaded = false;
        public bool Paused = false;
        public int Timer;
        public int lastnote = 0;
        public Background Background;
        public Background PauseOverlay;

        public KeyboardState OldState;

        public Sprite LeftNote;
        public Sprite UpNote;
        public Sprite RightNote;
        public Sprite DownNote;

        public Button ExitButton;
        public Button ResumeButton;

        public Text TimerTex;

        public string Name { get => "ingame"; }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Rectangle cursor, bool clicked)
        {
            var NewState = Keyboard.GetState();
            bool LeftPress = false;
            bool UpPress = false;
            bool RightPress = false;
            bool DownPress = false;

            if (NewState.IsKeyDown(Keys.NumPad4) && !OldState.IsKeyDown(Keys.NumPad4))
                LeftPress = true;
            if (NewState.IsKeyDown(Keys.NumPad8) && !OldState.IsKeyDown(Keys.NumPad8))
                UpPress = true;
            if (NewState.IsKeyDown(Keys.NumPad6) && !OldState.IsKeyDown(Keys.NumPad6))
                RightPress = true;
            if (NewState.IsKeyDown(Keys.NumPad2) && !OldState.IsKeyDown(Keys.NumPad2))
                DownPress = true;

            if (NewState.IsKeyDown(Keys.Escape) && !OldState.IsKeyDown(Keys.Escape))
            {
                if (Paused)
                    Paused = false;
                else
                    Paused = true;
            }

            OldState = NewState;

            if (!MapLoaded)
            {
                Playing = GameData.MapManager.Current;
                Timer = Playing.Offset;
                #region Preloading notes
                NotesLeft = new List<Note>();
                NotesUp = new List<Note>();
                NotesRight = new List<Note>();
                NotesDown = new List<Note>();
                foreach (int n in Playing.NotesLeft)
                {
                    NotesLeft.Add(new Note(spriteBatch, NoteMode.left, Background.Width, Background.Height)
                    {
                        Hit = false,
                        Position = 0,
                        Time = n,
                        Accuracy = 0
                    });
                }

                foreach (int n in Playing.NotesUp)
                {
                    NotesUp.Add(new Note(spriteBatch, NoteMode.up, Background.Width, Background.Height)
                    {
                        Hit = false,
                        Position = 0,
                        Time = n,
                        Accuracy = 0
                    });
                }

                foreach (int n in Playing.NotesRight)
                {
                    NotesRight.Add(new Note(spriteBatch, NoteMode.right, Background.Width, Background.Height)
                    {
                        Hit = false,
                        Position = 0,
                        Time = n,
                        Accuracy = 0
                    });
                }

                foreach (int n in Playing.NotesDown)
                {
                    NotesDown.Add(new Note(spriteBatch, NoteMode.down, Background.Width, Background.Height)
                    {
                        Hit = false,
                        Position = 0,
                        Time = n,
                        Accuracy = 0
                    });
                }
                #endregion
                Background.Texture = Playing.Background;
                lastnote = GetLastNote();
                GameData.MusicManager.Restart();
                MapLoaded = true;
            }
            else
            {
                Background.Draw();
                if (NewState.IsKeyDown(Keys.NumPad4))
                    LeftNote.Color = Color.DarkGray;
                else
                    LeftNote.Color = Color.White;

                if (NewState.IsKeyDown(Keys.NumPad8))
                    UpNote.Color = Color.DarkGray;
                else
                    UpNote.Color = Color.White;

                if (NewState.IsKeyDown(Keys.NumPad6))
                    RightNote.Color = Color.DarkGray;
                else
                    RightNote.Color = Color.White;

                if (NewState.IsKeyDown(Keys.NumPad2))
                    DownNote.Color = Color.DarkGray;
                else
                    DownNote.Color = Color.White;


                LeftNote.Draw();
                UpNote.Draw();
                RightNote.Draw();
                DownNote.Draw();

                if (Paused)
                {
                    PauseOverlay.Draw();
                    ResumeButton.Draw(cursor, clicked);
                    ExitButton.Draw(cursor, clicked);
                }
                #region Gameplay (yay!)
                foreach (Note n in NotesLeft)
                {
                    if (n.Time - ((Background.Width / 2) + n.NoteSprite.Texture.Width) < Timer)
                        n.Draw(ref LeftPress, Paused);
                }
                foreach (Note n in NotesUp)
                {
                    if (n.Time - ((Background.Height / 2) + n.NoteSprite.Texture.Height) < Timer)
                        n.Draw(ref UpPress, Paused);
                }
                foreach (Note n in NotesRight)
                {
                    if (n.Time - ((Background.Width / 2) + (n.NoteSprite.Texture.Width * 2)) < Timer)
                        n.Draw(ref RightPress, Paused);
                }
                foreach (Note n in NotesDown)
                {
                    if (n.Time - ((Background.Height / 2) + (n.NoteSprite.Texture.Height * 2)) < Timer)
                        n.Draw(ref DownPress, Paused);
                }
                #endregion
                TimerTex.Content = "" + Timer;
                TimerTex.Draw();
                if (NewState.IsKeyDown(Keys.OemTilde))
                    MapLoaded = false;
            }
        }

        public void Preload(SpriteBatch spriteBatch, GraphicsDeviceManager Graphics)
        {
            Background = new Background(spriteBatch, GameData.Instance.CurrentSkin.MenuBackground, Color.White, Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight);
            PauseOverlay = new Background(spriteBatch, GameData.Instance.CurrentSkin.PauseOverlay, Color.White, Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight);
            var notetex = GameData.Instance.CurrentSkin.Note;
            LeftNote = new Sprite(spriteBatch, (int)(Background.Width / 2 - notetex.Width * 1.5), (int)(Background.Height / 2 - notetex.Width * 0.5), notetex, Color.White);
            UpNote = new Sprite(spriteBatch, (int)(Background.Width / 2 - notetex.Width * 0.5), (int)(Background.Height / 2 - notetex.Width * 1.5), notetex, Color.White);
            RightNote = new Sprite(spriteBatch, (int)(Background.Width / 2 + notetex.Width * 0.5), (int)(Background.Height / 2 - notetex.Width * 0.5), notetex, Color.White);
            DownNote = new Sprite(spriteBatch, (int)(Background.Width / 2 - notetex.Width * 0.5), (int)(Background.Height / 2 + notetex.Width * 0.5), notetex, Color.White);
            TimerTex = new Text(spriteBatch, GameData.Instance.CurrentSkin.Font, "" + Timer, 0, 0, Color.GreenYellow);
            // Making sure OldState is not null
            OldState = Keyboard.GetState();
            ResumeButton = new Button(spriteBatch, (Graphics.PreferredBackBufferWidth / 2) - (GameData.Instance.CurrentSkin.Button.Width / 2),
                Graphics.PreferredBackBufferHeight / 2 - (GameData.Instance.CurrentSkin.Button.Height) - 25, GameData.Instance.CurrentSkin.Button,
                GameData.Instance.CurrentSkin.ButtonHover, "Resume");
            ExitButton = new Button(spriteBatch, (Graphics.PreferredBackBufferWidth / 2) - (GameData.Instance.CurrentSkin.Button.Width / 2),
                Graphics.PreferredBackBufferHeight / 2 + 25, GameData.Instance.CurrentSkin.Button,
                GameData.Instance.CurrentSkin.ButtonHover, "Exit");

            ResumeButton.OnClick += (sender, e) =>
            {
                Paused = false;
            };

            ExitButton.OnClick += (sender, e) =>
            {
                MapLoaded = false;
                GameData.Instance.CurrentScreen = "select";
                GameData.MusicManager.UnPause();
            };
        }

        public void Update(GameTime gameTime, Rectangle cursor, bool clicked)
        {
            if (MapLoaded && !Paused)
                Timer++;
            if (Paused)
            {
                GameData.MusicManager.Pause();
            }
            else
            {
                GameData.MusicManager.UnPause();
            }
            if(Timer > lastnote + 1000)
            {
                GameData.Instance.CurrentScreen = "results";
                ((Results)GameData.Instance.Screens.Find(x => x.Name == "results")).ResultsPreloaded = false;
                Timer = 0;
            }
        }

        public int GetLastNote()
        {
            int left = 0;
            int up = 0;
            int right = 0;
            int down = 0;
            foreach(Note n in NotesLeft)
            {
                if (n.Time > left)
                    left = n.Time;
            }
            foreach (Note n in NotesUp)
            {
                if (n.Time > up)
                    up = n.Time;
            }
            foreach (Note n in NotesRight)
            {
                if (n.Time > right)
                    right = n.Time;
            }
            foreach (Note n in NotesDown)
            {
                if (n.Time > down)
                    down = n.Time;
            }

            return Math.Max(left, Math.Max(up, Math.Max(right, down)));
        }
    }
}
