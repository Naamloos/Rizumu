/*
 * This is where I store most variables that need to be accessible from anywhere.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Rizumu
{
    class GameResources
    {
        // Global Variables
        public static Keys up;
        public static Keys down;
        public static Keys left;
        public static Keys right;
        public static List<string[]> comments = new List<string[]>();


        public static int GameScreen = 0;
        public static Dictionary<string, Objects.RizumuMap> Maps;
        public static string selected = "content/songs/";
        public static int startint = 0;
        public static int fscore = 0;
        public static int gscore = 0;
        public static int hscore = 0;
        public static int jscore = 0;
        public static int fmiss = 0;
        public static int gmiss = 0;
        public static int hmiss = 0;
        public static int jmiss = 0;
        public static int combo = 0;
        public static string skin = "none";
        public static int health = 100;
        public static bool showcursor = true;
        public static bool fullscreen = true;
        public static int offset = 0;
        public static int totalnotes = 0;
        public static MouseState scorebackmouse;
        public static Color basecolor = Color.White;
        public static bool autoplay = false;

        // Textures
        public static Texture2D Button;
        public static Texture2D ButtonSelected;
        public static Texture2D Cursor;
        public static Texture2D Logo;
        public static Texture2D NoteL;
        public static Texture2D NoteU;
        public static Texture2D NoteR;
        public static Texture2D NoteD;
        public static Texture2D NoteHitL;
        public static Texture2D NoteHitU;
        public static Texture2D NoteHitD;
        public static Texture2D NoteHitR;
        public static Texture2D Songbar;
        public static Texture2D Paused;
        public static Texture2D Unchecked;
        public static Texture2D Checked;
        public static Texture2D HealthBar;
        public static Texture2D Mascotte;
        public static Texture2D ScoreBack;
        public static Texture2D ScoreLeft;
        public static Texture2D ScoreUp;
        public static Texture2D ScoreRight;
        public static Texture2D ScoreDown;
        public static Texture2D ScoreMiss;
        public static Texture2D ScoreCombo;
        public static Texture2D SSRank;
        public static Texture2D SRank;
        public static Texture2D ARank;
        public static Texture2D BRank;
        public static Texture2D CRank;
        public static Texture2D DRank;
        public static Texture2D sparkle0;
        public static Texture2D sparkle1;
        public static Texture2D sparkle2;
        public static Texture2D sparkle3;
        public static Texture2D sparkle4;
        public static Texture2D healthoverlay;
        public static Texture2D popup;

        // Animations
        public static Texture2D[] Animation_sparkle;

        // Backgrounds
        public static Texture2D background_menu;
        public static Texture2D songbg;

        // Fonts
        public static SpriteFont font;
        public static SpriteFont debug;

        // Sounds
        public static SoundEffect hit;
        public static SoundEffect combobreak;

        //Making var for content
        public static Microsoft.Xna.Framework.Content.ContentManager globalcontent;
        // Keybinds

        public static void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            globalcontent = Content;
            // Textures
            Button = Content.Load<Texture2D>("button");
            ButtonSelected = Content.Load<Texture2D>("button_selected");
            Cursor = Content.Load<Texture2D>("cursor");
            Logo = Content.Load<Texture2D>("Finallogo");
            NoteL = Content.Load<Texture2D>("note");
            NoteU = Content.Load<Texture2D>("note");
            NoteR = Content.Load<Texture2D>("note");
            NoteD = Content.Load<Texture2D>("note");
            NoteHitL = Content.Load<Texture2D>("note");
            NoteHitU = Content.Load<Texture2D>("note");
            NoteHitR = Content.Load<Texture2D>("note");
            NoteHitD = Content.Load<Texture2D>("note");
            Songbar = Content.Load<Texture2D>("songbar");
            Paused = Content.Load<Texture2D>("paused");
            Unchecked = Content.Load<Texture2D>("unchecked");
            Checked = Content.Load<Texture2D>("checked");
            HealthBar = Content.Load<Texture2D>("healthbar");
            Mascotte = Content.Load<Texture2D>("Mascotte");
            ScoreBack = Content.Load<Texture2D>("scoreback");
            ScoreLeft = Content.Load<Texture2D>("scoreleft");
            ScoreUp = Content.Load<Texture2D>("scoreup");
            ScoreRight = Content.Load<Texture2D>("scoreright");
            ScoreDown = Content.Load<Texture2D>("scoredown");
            ScoreMiss = Content.Load<Texture2D>("scoremiss");
            ScoreCombo = Content.Load<Texture2D>("scorecombo");
            SSRank = Content.Load<Texture2D>("ss");
            SRank = Content.Load<Texture2D>("s");
            ARank = Content.Load<Texture2D>("a");
            BRank = Content.Load<Texture2D>("b");
            CRank = Content.Load<Texture2D>("c");
            DRank = Content.Load<Texture2D>("d");
            sparkle0 = Content.Load<Texture2D>("sparkle-0");
            sparkle1 = Content.Load<Texture2D>("sparkle-1");
            sparkle2 = Content.Load<Texture2D>("sparkle-2");
            sparkle3 = Content.Load<Texture2D>("sparkle-3");
            sparkle4 = Content.Load<Texture2D>("sparkle-4");
            healthoverlay = Content.Load<Texture2D>("healthoverlay");
            popup = Content.Load<Texture2D>("popup");

            // Animations
            Animation_sparkle = new Texture2D[5]
            {
                sparkle0, sparkle1, sparkle2, sparkle3, sparkle4
            };

            // Backgrounds
            background_menu = Content.Load<Texture2D>("menubg");

            // Fonts
            font = Content.Load<SpriteFont>("font");
            debug = Content.Load<SpriteFont>("debug");

            // Sounds
            hit = Content.Load<SoundEffect>("hit");
            combobreak = Content.Load<SoundEffect>("break");

            // Videos


            // Keybinds
            up = Keys.NumPad8;
            down = Keys.NumPad2;
            left = Keys.NumPad4;
            right = Keys.NumPad6;
            string[] keybinds = File.ReadAllLines("settings.ini");
            foreach (string l in keybinds)
            {
                if (l.StartsWith("left:"))
                {
                    string bind = l.Substring(5);
                    left = (Keys)Int32.Parse(bind);
                }
                if (l.StartsWith("up:"))
                {
                    string bind = l.Substring(3);
                    up = (Keys)Int32.Parse(bind);
                }
                if (l.StartsWith("right:"))
                {
                    string bind = l.Substring(6);
                    right = (Keys)Int32.Parse(bind);
                }
                if (l.StartsWith("down:"))
                {
                    string bind = l.Substring(5);
                    down = (Keys)Int32.Parse(bind);
                }
                if (l.StartsWith("fullscreen:"))
                {
                    if (l.Substring(11) == "true")
                    {
                        fullscreen = true;
                    }
                    else
                    {
                        fullscreen = false;
                    }
                }
                if (l.StartsWith("skin:"))
                {
                    skin = l.Substring(5);
                }
            }

            if (skin != "none")
            {
                try
                {
                    background_menu = Content.Load<Texture2D>("..content/skins/" + skin + "/main_background.png");
                }
                catch { }

                try
                {
                    hit = Content.Load<SoundEffect>("..content/skins/" + skin + "/hit.wav");
                }
                catch { }

                try
                {
                    Button = Content.Load<Texture2D>("..content/skins/" + skin + "/button.png");
                }
                catch { }

                try
                {
                    ButtonSelected = Content.Load<Texture2D>("..content/skins/" + skin + "/button_selected.png");
                }
                catch { }

                try
                {
                    Cursor = Content.Load<Texture2D>("..content/skins/" + skin + "/cursor.png");
                }
                catch { }

                try
                {
                    NoteL = Content.Load<Texture2D>("..content/skins/" + skin + "/notel.png");
                }
                catch { }
                try
                {
                    NoteU = Content.Load<Texture2D>("..content/skins/" + skin + "/noteu.png");
                }
                catch { }
                try
                {
                    NoteR = Content.Load<Texture2D>("..content/skins/" + skin + "/noter.png");
                }
                catch { }
                try
                {
                    NoteD = Content.Load<Texture2D>("..content/skins/" + skin + "/noted.png");
                }
                catch { }

                try
                {
                    NoteHitL = Content.Load<Texture2D>("..content/skins/" + skin + "/notehitl.png");
                }
                catch { }
                try
                {
                    NoteHitU = Content.Load<Texture2D>("..content/skins/" + skin + "/notehitu.png");
                }
                catch { }
                try
                {
                    NoteHitD = Content.Load<Texture2D>("..content/skins/" + skin + "/notehitd.png");
                }
                catch { }
                try
                {
                    NoteHitR = Content.Load<Texture2D>("..content/skins/" + skin + "/notehitr.png");
                }
                catch { }

                try
                {
                    Songbar = Content.Load<Texture2D>("..content/skins/" + skin + "/songbar.png");
                }
                catch { }

                try
                {
                    Paused = Content.Load<Texture2D>("..content/skins/" + skin + "/paused.png");
                }
                catch { }

                try
                {
                    HealthBar = Content.Load<Texture2D>("..content/skins/" + skin + "/healthbar.png");
                }
                catch { }
            }
        }

        public static void LoopColor()
        {
            Color newcolor = basecolor;
            if (newcolor.R > byte.MaxValue)
            {
                newcolor.R = 0;
            }
            else
            {
                newcolor.R += 2;
            }
            if (newcolor.G > byte.MaxValue)
            {
                newcolor.G = 0;
            }
            else
            {
                newcolor.G += 4;
            }
            if (newcolor.B > byte.MaxValue)
            {
                newcolor.B = 0;
            }
            else
            {
                newcolor.B += 8;
            }
            basecolor = newcolor;
        }
    }
}
