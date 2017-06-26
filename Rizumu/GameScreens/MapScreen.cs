/*
 * Main gameplay logic.
 * Feel free to improve timing etc and ht me with some PRs
 * This GameScreen needs the most improvements.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rizumu.Objects;
using System;
using System.Collections.Generic;
using System.IO;

namespace Rizumu.GameScreens
{
    class MapScreen
    {
        public static List<Note> LeftNotes = new List<Note>();
        public static List<Note> UpNotes = new List<Note>();
        public static List<Note> RightNotes = new List<Note>();
        public static List<Note> DownNotes = new List<Note>();
        public static int timer = 0;
        public static bool loaded = false;
        public static bool paused = false;
        public static Sprite note;
        public static KeyboardState oldState;
        public static int lastnote = 0;
        public static int currentcombo = 0;
        public static float notespeed = 1f;
        public static float noterot = 0.0f;
        public static bool usingvideo = true;

        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!loaded)
            {

                usingvideo = false;

                timer = 0 + GameResources.offset;
                int l = 0;
                int[] ints = GameResources.Maps[GameResources.selected].LeftNotes;
                LeftNotes = new List<Note>();
                while (l < ints.Length)
                {
                    try
                    {
                        LeftNotes.Add(new Note() { hit = false, position = 0, time = ints[l] });
                    }
                    catch (Exception)
                    {
                    }
                    l = l + 1;
                }

                l = 0;
                ints = GameResources.Maps[GameResources.selected].UpNotes;
                UpNotes = new List<Note>();
                while (l < ints.Length)
                {
                    try
                    {
                        UpNotes.Add(new Note() { hit = false, position = 0, time = ints[l] });
                    }
                    catch (Exception)
                    {
                    }
                    l = l + 1;
                }

                l = 0;
                ints = GameResources.Maps[GameResources.selected].RightNotes;
                RightNotes = new List<Note>();
                while (l < ints.Length)
                {
                    try
                    {
                        RightNotes.Add(new Note() { hit = false, position = 0, time = ints[l] });
                    }
                    catch (Exception)
                    {
                    }
                    l = l + 1;
                }

                l = 0;
                ints = GameResources.Maps[GameResources.selected].DownNotes;
                DownNotes = new List<Note>();
                while (l < ints.Length)
                {
                    try
                    {
                        DownNotes.Add(new Note() { hit = false, position = 0, time = ints[l] });
                    }
                    catch (Exception)
                    {
                    }
                    l = l + 1;
                }

                if (File.Exists(Path.Combine(GameResources.selected, GameResources.Maps[GameResources.selected].BackgroundFile)))
                {
                    System.IO.Stream stream4 = TitleContainer.OpenStream(Path.Combine(GameResources.selected, GameResources.Maps[GameResources.selected].BackgroundFile));
                    GameResources.songbg = Texture2D.FromStream(Game1.graphics.GraphicsDevice, stream4);
                }
                else
                {
                    GameResources.songbg = GameResources.background_menu;
                }
                int lastf = 0;
                int lastg = 0;
                int lasth = 0;
                int lastj = 0;
                foreach (Note note in LeftNotes)
                {
                    lastf = note.time;
                }
                foreach (Note note in UpNotes)
                {
                    lastg = note.time;
                }
                foreach (Note note in RightNotes)
                {
                    lasth = note.time;
                }
                foreach (Note note in DownNotes)
                {
                    lastj = note.time;
                }

                lastnote = Math.Max(lastf, Math.Max(lastg, Math.Max(lasth, lastj)));
                GameResources.showcursor = false;
                GameResources.fmiss = 0;
                GameResources.gmiss = 0;
                GameResources.hmiss = 0;
                GameResources.jmiss = 0;
                GameResources.combo = 0;
                GameResources.health = 100;
                GameResources.totalnotes = LeftNotes.Count + UpNotes.Count + RightNotes.Count + DownNotes.Count;
                loaded = true;
                Music.play(GameResources.selected, 0);
                timer = timer + 50;
            }
            KeyboardState newState = Keyboard.GetState();

            if (paused)
            {
                if (newState.IsKeyDown(Keys.Escape) && !oldState.IsKeyDown(Keys.Escape))
                {
                    Music.resume();
                    paused = false;
                }
            }
            else
            {
                if (newState.IsKeyDown(Keys.Escape) && !oldState.IsKeyDown(Keys.Escape))
                {
                    Music.pause();
                    paused = true;
                }
            }

            bool fpress = false;
            bool gpress = false;
            bool hpress = false;
            bool jpress = false;

            if (newState.IsKeyDown(GameResources.left))
            {
                if (!oldState.IsKeyDown(GameResources.left))
                {
                    fpress = true;
                }
            }
            else if (oldState.IsKeyDown(GameResources.left))
            {
                fpress = false;
            }

            if (newState.IsKeyDown(GameResources.up))
            {
                if (!oldState.IsKeyDown(GameResources.up))
                {
                    gpress = true;
                }
            }
            else if (oldState.IsKeyDown(GameResources.up))
            {
                gpress = false;
            }

            if (newState.IsKeyDown(GameResources.right))
            {
                if (!oldState.IsKeyDown(GameResources.right))
                {
                    hpress = true;
                }
            }
            else if (oldState.IsKeyDown(GameResources.right))
            {
                hpress = false;
            }

            if (newState.IsKeyDown(GameResources.down))
            {
                if (!oldState.IsKeyDown(GameResources.down))
                {
                    jpress = true;
                }
            }
            else if (oldState.IsKeyDown(GameResources.down))
            {
                jpress = false;
            }

            oldState = newState;

            Background bg = new Background(spriteBatch, GameResources.songbg);

            bg.draw();

            if (currentcombo > GameResources.combo)
            {
                GameResources.combo = currentcombo;
            }

            Text.draw(GameResources.debug, (timer / 500) + "/" + ((lastnote / 500) + 1), 0, 25, spriteBatch);
            int fi = 0;
            int notew = GameResources.NoteL.Width;
            int noteh = GameResources.NoteL.Height;
            int centerx = Game1.graphics.PreferredBackBufferWidth / 2 - GameResources.NoteL.Width / 2;
            int centery = Game1.graphics.PreferredBackBufferHeight / 2 - GameResources.NoteL.Height / 2;
            if (Keyboard.GetState().IsKeyDown(GameResources.left))
                note = new Sprite(spriteBatch, centerx - notew, centery, GameResources.NoteL, GameResources.basecolor);
            else
                note = new Sprite(spriteBatch, centerx - notew, centery, GameResources.NoteL, new Color(GameResources.basecolor, 0.5f));
            note.draw();

            if (Keyboard.GetState().IsKeyDown(GameResources.up))
                note = new Sprite(spriteBatch, centerx, centery - noteh, GameResources.NoteU, GameResources.basecolor);
            else
                note = new Sprite(spriteBatch, centerx, centery - noteh, GameResources.NoteU, new Color(GameResources.basecolor, 0.5f));
            note.draw();

            if (Keyboard.GetState().IsKeyDown(GameResources.right))
                note = new Sprite(spriteBatch, centerx + notew, centery, GameResources.NoteR, GameResources.basecolor);
            else
                note = new Sprite(spriteBatch, centerx + notew, centery, GameResources.NoteR, new Color(GameResources.basecolor, 0.5f));
            note.draw();

            if (Keyboard.GetState().IsKeyDown(GameResources.down))
                note = new Sprite(spriteBatch, centerx, centery + noteh, GameResources.NoteD, GameResources.basecolor);
            else
                note = new Sprite(spriteBatch, centerx, centery + noteh, GameResources.NoteD, new Color(GameResources.basecolor, 0.5f));
            note.draw();

            if (!paused)
            {
                noterot += 0.05f;
                int framert = (int)(1.0 / gameTime.ElapsedGameTime.TotalSeconds);
                int fr = 500 / framert;

                while (fi < LeftNotes.Count)
                {
                    if (LeftNotes[fi].time < timer - (notew * 2) + (centerx / notespeed) - notew * 2 && LeftNotes[fi].position < centerx + notew + 5 && LeftNotes[fi].hit == false)
                    {
                        note = new Sprite(spriteBatch, LeftNotes[fi].position, centery, GameResources.NoteL, GameResources.basecolor);
                        LeftNotes[fi] = new Note() { hit = LeftNotes[fi].hit, position = LeftNotes[fi].position + (int)(10 * notespeed), time = LeftNotes[fi].time };
                        note.rotation = noterot;
                        note.draw();
                        if (GameResources.autoplay)
                        {
                            fpress = true;
                        }
                        if (fpress && LeftNotes[fi].position > centerx - notew * 1.5)
                        {
                            LeftNotes[fi] = new Note() { hit = true, position = LeftNotes[fi].position, time = LeftNotes[fi].time };
                            GameResources.hit.Play();
                            currentcombo++;
                            GameResources.fscore++;
                            if (GameResources.health < 100)
                            {
                                GameResources.health++;
                            }
                        }
                        if (LeftNotes[fi].position > centerx + notew && LeftNotes[fi].hit == false)
                        {
                            GameResources.health -= 5;
                            GameResources.fmiss++;
                            GameResources.combobreak.Play();
                            currentcombo = 0;
                        }
                    }
                    fi++;
                }
                fi = 0;


                while (fi < UpNotes.Count)
                {
                    if (UpNotes[fi].time < timer - (noteh * 2) + (centery / notespeed) - noteh && UpNotes[fi].position < centery + noteh + 5 && UpNotes[fi].hit == false)
                    {
                        note = new Sprite(spriteBatch, centerx, UpNotes[fi].position, GameResources.NoteU, GameResources.basecolor);
                        UpNotes[fi] = new Note() { hit = UpNotes[fi].hit, position = UpNotes[fi].position + (int)(10 * notespeed), time = UpNotes[fi].time };
                        note.rotation = noterot;
                        note.draw();
                        if (GameResources.autoplay)
                        {
                            gpress = true;
                        }
                        if (gpress && UpNotes[fi].position > centery - noteh * 1.5)
                        {
                            UpNotes[fi] = new Note() { hit = true, position = UpNotes[fi].position, time = UpNotes[fi].time };
                            GameResources.hit.Play();
                            currentcombo++;
                            GameResources.gscore++;
                            if (GameResources.health < 100)
                            {
                                GameResources.health++;
                            }
                        }
                        if (UpNotes[fi].position > centery + noteh && UpNotes[fi].hit == false)
                        {
                            GameResources.health -= 5;
                            GameResources.gmiss++;
                            GameResources.combobreak.Play();
                            currentcombo = 0;
                        }
                    }
                    fi++;
                }
                fi = 0;



                while (fi < RightNotes.Count)
                {
                    if (RightNotes[fi].time < timer - (notew * 2) + (centerx / notespeed) - notew * 2 && RightNotes[fi].position < centerx + notew + 5 && RightNotes[fi].hit == false)
                    {
                        note = new Sprite(spriteBatch, Game1.graphics.PreferredBackBufferWidth - notew - RightNotes[fi].position, centery, GameResources.NoteR, GameResources.basecolor);
                        RightNotes[fi] = new Note() { hit = RightNotes[fi].hit, position = RightNotes[fi].position + (int)(10 * notespeed), time = RightNotes[fi].time };
                        note.rotation = noterot;
                        note.draw();
                        if (GameResources.autoplay)
                        {
                            hpress = true;
                        }
                        if (hpress && RightNotes[fi].position > centerx - notew * 1.5)
                        {
                            RightNotes[fi] = new Note() { hit = true, position = RightNotes[fi].position, time = RightNotes[fi].time };
                            GameResources.hit.Play();
                            currentcombo++;
                            GameResources.hscore++;
                            if (GameResources.health < 100)
                            {
                                GameResources.health++;
                            }
                        }
                        if (RightNotes[fi].position > centerx + notew && RightNotes[fi].hit == false)
                        {
                            GameResources.health -= 5;
                            GameResources.hmiss++;
                            GameResources.combobreak.Play();
                            currentcombo = 0;
                        }
                    }
                    fi++;
                }
                fi = 0;


                while (fi < DownNotes.Count)
                {
                    if (DownNotes[fi].time < timer - (noteh * 2) + (centery / notespeed) - noteh && DownNotes[fi].position < centery + noteh + 5 && DownNotes[fi].hit == false)
                    {
                        note = new Sprite(spriteBatch, centerx, Game1.graphics.PreferredBackBufferHeight - DownNotes[fi].position - noteh, GameResources.NoteD, GameResources.basecolor);
                        DownNotes[fi] = new Note() { hit = DownNotes[fi].hit, position = DownNotes[fi].position + (int)(10 * notespeed), time = DownNotes[fi].time };
                        note.rotation = noterot;
                        note.draw();
                        if (GameResources.autoplay)
                        {
                            jpress = true;
                        }
                        if (jpress && DownNotes[fi].position > centery - noteh * 1.5)
                        {
                            DownNotes[fi] = new Note() { hit = true, position = DownNotes[fi].position, time = DownNotes[fi].time };
                            GameResources.hit.Play();
                            currentcombo++;
                            GameResources.jscore++;
                            if (GameResources.health < 100)
                            {
                                GameResources.health++;
                            }
                        }
                        if (DownNotes[fi].position > centery + noteh && DownNotes[fi].hit == false)
                        {
                            GameResources.health -= 5;
                            GameResources.jmiss++;
                            GameResources.combobreak.Play();
                            currentcombo = 0;
                        }
                    }
                    fi++;
                }
                fi = 0;

                if (timer > lastnote + 500)
                {

                    if (File.Exists(Path.Combine(GameResources.selected, GameResources.Maps[GameResources.selected].BackgroundFile)))
                    {
                        System.IO.Stream stream4 = TitleContainer.OpenStream(Path.Combine(GameResources.selected, GameResources.Maps[GameResources.selected].BackgroundFile));
                        GameResources.songbg = Texture2D.FromStream(Game1.graphics.GraphicsDevice, stream4);
                    }
                    else
                    {
                        GameResources.songbg = GameResources.background_menu;
                    }
                    GameResources.GameScreen = 3;
                    usingvideo = true;
                    GameResources.showcursor = true;
                }
                Sprite health = new Sprite();
                int i = 0;
                while (i < GameResources.health)
                {
                    health = new Sprite(spriteBatch, i * 5, 0, GameResources.HealthBar, GameResources.basecolor);
                    health.draw();
                    i++;
                }
                Sprite healthoverlay = new Sprite(spriteBatch, 0, 0, GameResources.healthoverlay, GameResources.basecolor);
                healthoverlay.draw();
            }
            if (paused)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    if (File.Exists(Path.Combine(GameResources.selected, GameResources.Maps[GameResources.selected].BackgroundFile)))
                    {
                        System.IO.Stream stream4 = TitleContainer.OpenStream(Path.Combine(GameResources.selected, GameResources.Maps[GameResources.selected].BackgroundFile));
                        GameResources.songbg = Texture2D.FromStream(Game1.graphics.GraphicsDevice, stream4);
                    }
                    else
                    {
                        GameResources.songbg = GameResources.background_menu;
                    }
                    loaded = false;
                    timer = 0;
                    paused = false;
                    GameResources.GameScreen = 1;
                    usingvideo = true;
                    GameResources.showcursor = true;
                }
                int pausex = Game1.graphics.PreferredBackBufferWidth / 2 - GameResources.Paused.Width / 2;
                int pausey = Game1.graphics.PreferredBackBufferHeight / 2 - GameResources.Paused.Height / 2;
                Sprite pause = new Sprite(spriteBatch, pausex, pausey, GameResources.Paused, GameResources.basecolor);
                pause.draw();
                Text.draw(GameResources.font, "Press Space to exit map\nPress Escape to return\nPress ` to retry.\nButtons will be added soon.", pausex,
                    pausey + pause.hitbox.Height, spriteBatch);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.OemTilde))
            {
                loaded = false;
                timer = 0;
                paused = false;
            }
        }

        public static void Update(Microsoft.Xna.Framework.Content.ContentManager Content, GraphicsDevice graph)
        {
            // nah fam
            if (!paused)
            {
                timer++;
            }
        }
    }
}
