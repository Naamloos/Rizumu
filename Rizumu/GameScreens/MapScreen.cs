/*
 * Main gameplay logic.
 * Feel free to improve timing etc and ht me with some PRs
 * This GameScreen needs the most improvements.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;

namespace Rizumu.GameScreens
{
    class MapScreen
    {
        public static int[][] fnotes;
        public static int[][] gnotes;
        public static int[][] hnotes;
        public static int[][] jnotes;
        public static int timer = 0;
        public static bool loaded = false;
        public static bool paused = false;
        public static Sprite note;
        public static KeyboardState oldState;
        public static int lastnote = 0;
        public static bool vidplaying = false;
        public static int currentcombo = 0;
        public static float notespeed = 1f;
        public static float noterot = 0.0f;
        public static List<Animation> sparkles = new List<Animation>();
        public static VideoPlayer vp;
        public static bool usingvideo = true;

        public static void draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!loaded)
            {
                try
                {
                    vp.Stop();
                    vp.Dispose();
                    vp = null;
                }
                catch (Exception)
                { }
                vp = new VideoPlayer();
                vp.IsMuted = true;
                GameResources.mapvid = null;
                try
                {
                    GameResources.mapvid = GameResources.globalcontent.Load<Video>(GameResources.selected.Substring(8) + "\\video");
                }
                catch
                {
                    usingvideo = false;
                }
                timer = 0 + GameResources.offset;
                int l = 0;
                string[] lf = File.ReadAllLines(GameResources.selected + "\\fnotes.rizum");
                fnotes = new int[lf.Length][];
                while (l < lf.Length)
                {
                    try
                    {
                        fnotes[l] = new int[4] { Int32.Parse(lf[l]), 0, 0, 0 };
                    }
                    catch (Exception)
                    {
                        fnotes[l] = new int[4] { 0, 0, 0, 0 };
                    }
                    l = l + 1;
                }

                l = 0;
                lf = File.ReadAllLines(GameResources.selected + "\\gnotes.rizum");
                gnotes = new int[lf.Length][];
                while (l < lf.Length)
                {
                    try
                    {
                        gnotes[l] = new int[4] { Int32.Parse(lf[l]), 0, 0, 0 };
                    }
                    catch (Exception)
                    {
                        gnotes[l] = new int[4] { 0, 0, 0, 0 };
                    }
                    l = l + 1;
                }

                l = 0;
                lf = File.ReadAllLines(GameResources.selected + "\\hnotes.rizum");
                hnotes = new int[lf.Length][];
                while (l < lf.Length)
                {
                    try
                    {
                        hnotes[l] = new int[4] { Int32.Parse(lf[l]), 0, 0, 0 };
                    }
                    catch (Exception)
                    {
                        hnotes[l] = new int[4] { 0, 0, 0, 0 };
                    }
                    l = l + 1;
                }

                l = 0;
                lf = File.ReadAllLines(GameResources.selected + "\\jnotes.rizum");
                jnotes = new int[lf.Length][];
                while (l < lf.Length)
                {
                    try
                    {
                        jnotes[l] = new int[4] { Int32.Parse(lf[l]), 0, 0, 0 };
                    }
                    catch (Exception)
                    {
                        jnotes[l] = new int[4] { 0, 0, 0, 0 };
                    }
                    l = l + 1;
                }

                if (File.Exists(GameResources.selected + "/back.png"))
                {
                    System.IO.Stream stream4 = TitleContainer.OpenStream(GameResources.selected + "/back.png");
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
                foreach (int[] note in fnotes)
                {
                    lastf = note[0];
                }
                foreach (int[] note in gnotes)
                {
                    lastg = note[0];
                }
                foreach (int[] note in hnotes)
                {
                    lasth = note[0];
                }
                foreach (int[] note in jnotes)
                {
                    lastj = note[0];
                }

                lastnote = Math.Max(lastf, Math.Max(lastg, Math.Max(lasth, lastj)));
                GameResources.showcursor = false;
                GameResources.fmiss = 0;
                GameResources.gmiss = 0;
                GameResources.hmiss = 0;
                GameResources.jmiss = 0;
                GameResources.combo = 0;
                GameResources.health = 100;
                GameResources.totalnotes = fnotes.Length + gnotes.Length + hnotes.Length + jnotes.Length;
                loaded = true;
                Music.play(GameResources.selected, 0);
                if (usingvideo)
                {
                    try
                    {
                        vp.Play(GameResources.mapvid);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            if (usingvideo)
            {
                if (vp.State != MediaState.Stopped)
                    GameResources.songbg = vp.GetTexture();
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
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad4))
                note = new Sprite(spriteBatch, centerx - notew, centery, GameResources.NoteL, GameResources.basecolor);
            else
                note = new Sprite(spriteBatch, centerx - notew, centery, GameResources.NoteL, new Color(GameResources.basecolor, 0.5f));
            note.draw();

            if (Keyboard.GetState().IsKeyDown(Keys.NumPad8))
                note = new Sprite(spriteBatch, centerx, centery - noteh, GameResources.NoteU, GameResources.basecolor);
            else
                note = new Sprite(spriteBatch, centerx, centery - noteh, GameResources.NoteU, new Color(GameResources.basecolor, 0.5f));
            note.draw();

            if (Keyboard.GetState().IsKeyDown(Keys.NumPad6))
                note = new Sprite(spriteBatch, centerx + notew, centery, GameResources.NoteR, GameResources.basecolor);
            else
                note = new Sprite(spriteBatch, centerx + notew, centery, GameResources.NoteR, new Color(GameResources.basecolor, 0.5f));
            note.draw();

            if (Keyboard.GetState().IsKeyDown(Keys.NumPad2))
                note = new Sprite(spriteBatch, centerx, centery + noteh, GameResources.NoteD, GameResources.basecolor);
            else
                note = new Sprite(spriteBatch, centerx, centery + noteh, GameResources.NoteD, new Color(GameResources.basecolor, 0.5f));
            note.draw();

            if (!paused)
            {
                noterot += 0.05f;
                int framert = (int)(1.0 / gameTime.ElapsedGameTime.TotalSeconds);
                int fr = 500 / framert;

                while (fi < fnotes.Length)
                {
                    if (fnotes[fi][0] < timer - (notew * 2) + (centerx / notespeed) - notew * 2 && fnotes[fi][1] < centerx + notew && fnotes[fi][2] == 0)
                    {
                        note = new Sprite(spriteBatch, fnotes[fi][1], centery, GameResources.NoteL, GameResources.basecolor);
                        fnotes[fi][1] = fnotes[fi][1] + (int)(10 * notespeed);
                        note.rotation = noterot;
                        note.draw();
                        if (GameResources.autoplay)
                        {
                            fpress = true;
                        }
                        if (fpress && fnotes[fi][1] > centerx - notew * 1.5)
                        {
                            fnotes[fi][2] = 1;
                            GameResources.hit.Play();
                            sparkles.Add(new Animation(GameResources.Animation_sparkle, false, fnotes[fi][0], centerx - notew, centery, 25));
                            currentcombo++;
                            GameResources.fscore++;
                            if (GameResources.health < 100)
                            {
                                GameResources.health++;
                            }
                        }
                        if (fnotes[fi][1] > centerx + notew && fnotes[fi][2] == 0)
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


                while (fi < gnotes.Length)
                {
                    if (gnotes[fi][0] < timer - (noteh * 2) + (centery / notespeed) - noteh && gnotes[fi][1] < centery + noteh && gnotes[fi][2] == 0)
                    {
                        note = new Sprite(spriteBatch, centerx, gnotes[fi][1], GameResources.NoteU, GameResources.basecolor);
                        gnotes[fi][1] = gnotes[fi][1] + (int)(10 * notespeed);
                        note.rotation = noterot;
                        note.draw();
                        if (GameResources.autoplay)
                        {
                            gpress = true;
                        }
                        if (gpress && gnotes[fi][1] > centery - noteh * 1.5)
                        {
                            gnotes[fi][2] = 1;
                            GameResources.hit.Play();
                            sparkles.Add(new Animation(GameResources.Animation_sparkle, false, gnotes[fi][0], centerx, centery - notew, 25));
                            currentcombo++;
                            GameResources.gscore++;
                            if (GameResources.health < 100)
                            {
                                GameResources.health++;
                            }
                        }
                        if (gnotes[fi][1] > centery + noteh && gnotes[fi][2] == 0)
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



                while (fi < hnotes.Length)
                {
                    if (hnotes[fi][0] < timer - (notew * 2) + (centerx / notespeed) - notew * 2 && hnotes[fi][1] < centerx + notew && hnotes[fi][2] == 0)
                    {
                        note = new Sprite(spriteBatch, Game1.graphics.PreferredBackBufferWidth - notew - hnotes[fi][1], centery, GameResources.NoteR, GameResources.basecolor);
                        hnotes[fi][1] = hnotes[fi][1] + (int)(10 * notespeed);
                        note.rotation = noterot;
                        note.draw();
                        if (GameResources.autoplay)
                        {
                            hpress = true;
                        }
                        if (hpress && hnotes[fi][1] > centerx - notew * 1.5)
                        {
                            hnotes[fi][2] = 1;
                            GameResources.hit.Play();
                            sparkles.Add(new Animation(GameResources.Animation_sparkle, false, hnotes[fi][0], centerx + notew, centery, 25));
                            currentcombo++;
                            GameResources.hscore++;
                            if (GameResources.health < 100)
                            {
                                GameResources.health++;
                            }
                        }
                        if (hnotes[fi][1] > centerx + notew && hnotes[fi][2] == 0)
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


                while (fi < jnotes.Length)
                {
                    if (jnotes[fi][0] < timer - (noteh * 2) + (centery / notespeed) - noteh && jnotes[fi][1] < centery + noteh && jnotes[fi][2] == 0)
                    {
                        note = new Sprite(spriteBatch, centerx, Game1.graphics.PreferredBackBufferHeight - jnotes[fi][1] - noteh, GameResources.NoteD, GameResources.basecolor);
                        jnotes[fi][1] = jnotes[fi][1] + (int)(10 * notespeed);
                        note.rotation = noterot;
                        note.draw();
                        if (GameResources.autoplay)
                        {
                            jpress = true;
                        }
                        if (jpress && jnotes[fi][1] > centery - noteh * 1.5)
                        {
                            jnotes[fi][2] = 1;
                            GameResources.hit.Play();
                            sparkles.Add(new Animation(GameResources.Animation_sparkle, false, jnotes[fi][0], centerx, centery + noteh, 25));
                            currentcombo++;
                            GameResources.jscore++;
                            if (GameResources.health < 100)
                            {
                                GameResources.health++;
                            }
                        }
                        if (jnotes[fi][1] > centery + noteh && jnotes[fi][2] == 0)
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

                // TODO: Maybe add back later, but the code sucks atm

                /*int thefuckingnumber = 0;
                while(thefuckingnumber < sparkles.Count - 1)
                {
                    if (!sparkles[thefuckingnumber].finished)
                    {
                        sparkles[thefuckingnumber] = sparkles[thefuckingnumber].draw(spriteBatch);
                    }
                    thefuckingnumber++;
                }*/


                if (timer > lastnote + 500)
                {
                    try
                    {
                        vp.Stop();
                        vp.Dispose();
                        vp = null;
                    }
                    catch (Exception)
                    {

                    }
                    if (File.Exists(GameResources.selected + "/back.png"))
                    {
                        System.IO.Stream stream4 = TitleContainer.OpenStream(GameResources.selected + "/back.png");
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
            }
            if (paused)
            {
                vp.Pause();
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    try
                    {
                        vp.Stop();
                        vp.Dispose();
                        vp = null;
                    }
                    catch (Exception)
                    {

                    }
                    if (File.Exists(GameResources.selected + "/back.png"))
                    {
                        System.IO.Stream stream4 = TitleContainer.OpenStream(GameResources.selected + "/back.png");
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
                vp.Stop();
                loaded = false;
                timer = 0;
                paused = false;
            }
        }

        public static void update(Microsoft.Xna.Framework.Content.ContentManager Content, GraphicsDevice graph)
        {
            // nah fam
            if (!paused)
            {
                timer++;
            }
        }
    }
}
