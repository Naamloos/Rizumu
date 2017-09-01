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
using System.IO;
using Newtonsoft.Json.Linq;
using Microsoft.Xna.Framework.Media;

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
        public Sprite VisionUp;
        public Sprite VisionDown;
        public Sprite VisionLeft;
        public Sprite VisionRight;
        public Replay Recording;
        public bool Replaying = false;
        public bool ready;
        public Replay Replay;
        public float Rotation = 0f;

        public KeyboardState OldState;

        public Sprite LeftNote;
        public Sprite UpNote;
        public Sprite RightNote;
        public Sprite DownNote;

        public Button ExitButton;
        public Button ResumeButton;

        public Text TimerTex;
        public Text ComboText;
        public Text ComboTextSmall;
        public int CurrentCombo = 0;
        public int HighestCombo = 0;
        bool auto = false;
        bool skippable = false;
        int firstnote = 0;

        public Text ModCollection;
        public int ScreenWidth;

        public string Name { get => "ingame"; }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Rectangle cursor, bool clicked)
        {
            Rotation = Timer * 0.005f;
            if (CurrentCombo > HighestCombo)
                HighestCombo = CurrentCombo;
            if (!MapLoaded)
            {
                ready = false;
                IsRestarted = false;
                GameData.MusicManager.Stop();
                Playing = GameData.MapManager.Current;
                Timer = Playing.Offset;
                #region Preloading notes
                NotesLeft = new List<Note>();
                NotesUp = new List<Note>();
                NotesRight = new List<Note>();
                NotesDown = new List<Note>();
                CurrentCombo = 0;
                HighestCombo = 0;
                auto = GameData.Instance.Mods.Automode;
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

                VisionUp = new Sprite(spriteBatch, 0, 0, GameData.Instance.CurrentSkin.VisionUp, Color.White);
                VisionDown = new Sprite(spriteBatch, 0, 0, GameData.Instance.CurrentSkin.VisionDown, Color.White);
                VisionLeft = new Sprite(spriteBatch, 0, 0, GameData.Instance.CurrentSkin.VisionLeft, Color.White);
                VisionRight = new Sprite(spriteBatch, 0, 0, GameData.Instance.CurrentSkin.VisionRight, Color.White);
                VisionUp.Alpha = 0f;
                VisionDown.Alpha = 0f;
                VisionLeft.Alpha = 0f;
                VisionRight.Alpha = 0f;

                lastnote = GetLastNote();
                firstnote = GetFirstNote();
                if (firstnote > 2500)
                    skippable = true;
                Recording = new Replay();
                Recording.Md5 = Playing.MD5;
                Recording.Player = GameData.Instance.Options.Player;
                MapLoaded = true;
            }
            else
            {
                var NewState = Keyboard.GetState();
                bool LeftPress = false;
                bool UpPress = false;
                bool RightPress = false;
                bool DownPress = false;

                if(skippable && Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    GameData.MusicManager.Restart(TimeSpan.FromMilliseconds((GetFirstNote() - 500) * 2));
                    skippable = false;
                }
                if(Timer > firstnote)
                {
                    skippable = false;
                }

                if (!Replaying)
                {
                    if (!auto)
                    {
                        if (NewState.IsKeyDown((Keys)GameData.Instance.Options.Left) && !OldState.IsKeyDown((Keys)GameData.Instance.Options.Left))
                        {
                            Recording.PressesLeft.Add(Timer);
                            LeftPress = true;
                        }
                        if (NewState.IsKeyDown((Keys)GameData.Instance.Options.Up) && !OldState.IsKeyDown((Keys)GameData.Instance.Options.Up))
                        {
                            Recording.PressesUp.Add(Timer);
                            UpPress = true;
                        }
                        if (NewState.IsKeyDown((Keys)GameData.Instance.Options.Right) && !OldState.IsKeyDown((Keys)GameData.Instance.Options.Right))
                        {
                            Recording.PressesRight.Add(Timer);
                            RightPress = true;
                        }
                        if (NewState.IsKeyDown((Keys)GameData.Instance.Options.Down) && !OldState.IsKeyDown((Keys)GameData.Instance.Options.Down))
                        {
                            Recording.PressesDown.Add(Timer);
                            DownPress = true;
                        }
                    }
                }
                else
                {
                    if (Replay.PressesLeft.Contains(Timer))
                        LeftPress = true;
                    if (Replay.PressesUp.Contains(Timer))
                        UpPress = true;
                    if (Replay.PressesRight.Contains(Timer))
                        RightPress = true;
                    if (Replay.PressesDown.Contains(Timer))
                        DownPress = true;
                }

                if (NewState.IsKeyDown(Keys.Escape) && !OldState.IsKeyDown(Keys.Escape))
                {
                    if (Paused)
                        Paused = false;
                    else
                        Paused = true;
                }

                OldState = NewState;

                if (!auto)
                {
                    if (NewState.IsKeyDown((Keys)GameData.Instance.Options.Left))
                        LeftNote.Color = Color.DarkGray;
                    else
                        LeftNote.Color = Color.White;

                    if (NewState.IsKeyDown((Keys)GameData.Instance.Options.Up))
                        UpNote.Color = Color.DarkGray;
                    else
                        UpNote.Color = Color.White;

                    if (NewState.IsKeyDown((Keys)GameData.Instance.Options.Right))
                        RightNote.Color = Color.DarkGray;
                    else
                        RightNote.Color = Color.White;

                    if (NewState.IsKeyDown((Keys)GameData.Instance.Options.Down))
                        DownNote.Color = Color.DarkGray;
                    else
                        DownNote.Color = Color.White;
                }

                Background.Draw();

                #region Gameplay (yay!)
                float leftdist = 0;
                float updist = 0;
                float rightdist = 0;
                float downdist = 0;
                foreach (Note n in NotesLeft)
                {
                    if (n.Time - ((Background.Width / 2) + n.NoteSprite.Texture.Width) < Timer)
                        n.Draw(ref LeftPress, Paused, ready, Rotation, ref CurrentCombo, ref leftdist, Timer, auto);
                }
                foreach (Note n in NotesUp)
                {
                    if (n.Time - ((Background.Height / 2) + n.NoteSprite.Texture.Height) < Timer)
                        n.Draw(ref UpPress, Paused, ready, Rotation, ref CurrentCombo, ref updist, Timer, auto);
                }
                foreach (Note n in NotesRight)
                {
                    if (n.Time - ((Background.Width / 2) + (n.NoteSprite.Texture.Width * 2)) < Timer)
                        n.Draw(ref RightPress, Paused, ready, Rotation, ref CurrentCombo, ref rightdist, Timer, auto);
                }
                foreach (Note n in NotesDown)
                {
                    if (n.Time - ((Background.Height / 2) + (n.NoteSprite.Texture.Height * 2)) < Timer)
                        n.Draw(ref DownPress, Paused, ready, Rotation, ref CurrentCombo, ref downdist, Timer, auto);
                }
                LeftNote.Rotation = Rotation;
                UpNote.Rotation = Rotation;
                RightNote.Rotation = Rotation;
                DownNote.Rotation = Rotation;

                if (updist != 0)
                    VisionUp.Alpha = (updist / 2);
                else
                    VisionUp.Alpha = VisionUp.Alpha - 0.1f;

                if (downdist != 0)
                    VisionDown.Alpha = (downdist / 2);
                else
                    VisionDown.Alpha = VisionDown.Alpha - 0.1f;

                if (leftdist != 0)
                    VisionLeft.Alpha = (leftdist / 2);
                else
                    VisionLeft.Alpha = VisionLeft.Alpha - 0.1f;

                if (rightdist != 0)
                    VisionRight.Alpha = (rightdist / 2);
                else
                    VisionRight.Alpha = VisionRight.Alpha - 0.1f;

                VisionUp.DrawScaled(Background.Width, Background.Height);
                VisionDown.DrawScaled(Background.Width, Background.Height);
                VisionLeft.DrawScaled(Background.Width, Background.Height);
                VisionRight.DrawScaled(Background.Width, Background.Height);
                #endregion

                LeftNote.Draw(true);
                UpNote.Draw(true);
                RightNote.Draw(true);
                DownNote.Draw(true);

                if (Paused)
                {
                    ready = false;
                    PauseOverlay.Draw();
                    ResumeButton.Draw(cursor, clicked);
                    ExitButton.Draw(cursor, clicked);
                }

                var totaltime = TimeSpan.FromSeconds((lastnote + 1000) / 500);
                var currenttime = TimeSpan.FromSeconds(Timer / 500);

                TimerTex.Content = $"{currenttime.ToReadableString()} / {totaltime.ToReadableString()}";
                TimerTex.Draw();
                ComboText.Content = $"{CurrentCombo}";
                ComboText.Draw();
                ComboTextSmall.Content = $"{HighestCombo}";
                ComboTextSmall.Draw();

                if (NewState.IsKeyDown(Keys.OemTilde))
                    MapLoaded = false;
            }

            if (!ready && !Paused)
                new Background(spriteBatch, GameData.Instance.CurrentSkin.GetReady, Color.White, Background.Width, Background.Height).Draw();
            if (Keyboard.GetState().IsKeyDown((Keys)GameData.Instance.Options.Up))
                ready = true;

            ModCollection.Content = GameData.Instance.Mods.GetCollectionString();
            ModCollection.X = (ScreenWidth - ModCollection.Width) - 5;
            ModCollection.Draw();
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
            TimerTex = new Text(spriteBatch, GameData.Instance.CurrentSkin.Font, "" + Timer, 0, 0, Color.White);

            ComboText = new Text(spriteBatch, GameData.Instance.CurrentSkin.FontBig, "0", 15, 0, Color.White);
            ComboText.Y = Background.Height - ComboText.Height - 15;
            ComboTextSmall = new Text(spriteBatch, GameData.Instance.CurrentSkin.FontSmall, "0", 15, 0, Color.White);
            ComboTextSmall.Y = ((Background.Height - ComboText.Height - 15) - ComboTextSmall.Height) - 3;
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
                Paused = false;
                Replaying = false;
                GameData.MusicManager.UnPause();
                GameData.MusicManager.Restart();
            };

            ModCollection = new Text(spriteBatch, GameData.Instance.CurrentSkin.FontSmall, "", Graphics.PreferredBackBufferWidth, 5, Color.Azure);
            ScreenWidth = Graphics.PreferredBackBufferWidth;
        }

        public bool IsRestarted = false;
        public bool LetsGoPlayed = false;
        public void Update(GameTime gameTime, Rectangle cursor, bool clicked)
        {
            if (MapLoaded && !Paused && ready)
                Timer = (int)((MediaPlayer.PlayPosition.TotalMilliseconds * 500) / 1000) + GameData.MapManager.Current.Offset;
            if (ready && !IsRestarted)
            {
                GameData.MusicManager.Restart();
                IsRestarted = true;
                LetsGoPlayed = false;
            }
            if (Paused)
            {
                GameData.MusicManager.Pause();
                LetsGoPlayed = false;
                ready = false;
            }
            else if (ready)
            {
                GameData.MusicManager.UnPause();
                if (!LetsGoPlayed)
                {
                    GameData.Instance.CurrentSkin.LetsGo.Play();
                    LetsGoPlayed = true;
                }
            }
            if (Timer > lastnote + 1000 || MediaPlayer.State == MediaState.Stopped && ready)
            {
                string path = Path.Combine("replays/", $"{Playing.Name}-{GameData.Instance.Options.Player}-{new Random().Next(int.MaxValue)}.rizumuplay");
                File.Create(path).Close();
                File.WriteAllText(path, JObject.FromObject(Recording).ToString());
                GameData.Instance.CurrentScreen = "results";
                Replaying = false;
                ((Results)GameData.Instance.Screens.Find(x => x.Name == "results")).ResultsPreloaded = false;
                Timer = 0;
                LetsGoPlayed = false;
                ready = false;
            }
        }

        public int GetLastNote()
        {
            int left = 0;
            int up = 0;
            int right = 0;
            int down = 0;
            foreach (Note n in NotesLeft)
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

        public int GetFirstNote()
        {
            int left = 0;
            int up = 0;
            int right = 0;
            int down = 0;
            left = NotesLeft[0].Time;
            up = NotesUp[0].Time;
            right = NotesRight[0].Time;
            down = NotesDown[0].Time;

            return Math.Min(left, Math.Min(up, Math.Min(right, down)));
        }


    }

    public static class tshelper
    {
        public static string ToReadableString(this TimeSpan span)
        {
            string formatted = string.Format("{0}:{1}",
                string.Format("{0:0}", span.Minutes),
                string.Format("{0:0}", span.Seconds).PadLeft(2, '0'));

            if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);

            if (string.IsNullOrEmpty(formatted)) formatted = "0 seconds";

            return formatted;
        }
    }
}
