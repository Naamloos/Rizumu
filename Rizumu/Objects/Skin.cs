using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Objects
{
    class Skin
    {
        // Textures
        public Texture2D Button;
        public Texture2D ButtonHover;
        public Texture2D Note;
        public Texture2D SongBar;
        public Texture2D Paused;
        public Texture2D HealthBar;
        public Texture2D HealthBarOverlay;
        public Texture2D RankSS;
        public Texture2D RankS;
        public Texture2D RandA;
        public Texture2D RankB;
        public Texture2D RankC;
        public Texture2D RankD;
        public Texture2D Popup;
        public Texture2D Particle;
        public Texture2D Circle;
        public Texture2D SelectorBG;
        public Texture2D SlideLeft;
        public Texture2D SlideUp;
        public Texture2D SlideRight;
        public Texture2D SlideDown;

        // Backgrounds
        public Texture2D MenuBackground;
        public Texture2D PauseOverlay;
        public Texture2D GetReady;
        public Texture2D VisionUp;
        public Texture2D VisionDown;
        public Texture2D VisionLeft;
        public Texture2D VisionRight;
        public Texture2D FunctionOverlay;

        // Sounds
        public SoundEffect Hit;
        public SoundEffectInstance HitIns;
        public SoundEffect Miss;
        public SoundEffect Hello;
        public SoundEffect Click;
        public SoundEffect MouseOver;
        public SoundEffect LetsGo;

        // Fonts
        public SpriteFont FontBig;
        public SpriteFont Font;
        public SpriteFont FontSmall;

        // Effects
        //public Effect AlphaShader;

        public static Skin LoadFromPath(GraphicsDevice Graphics, ContentManager content, string path)
        {
            Skin skin = new Skin();

            skin.Button = content.Load<Texture2D>("Gui/Button");
            skin.ButtonHover = content.Load<Texture2D>("Gui/ButtonSelected");
            skin.FontBig = content.Load<SpriteFont>("Fonts/MainBig");
            skin.Font = content.Load<SpriteFont>("Fonts/Main");
            skin.FontSmall = content.Load<SpriteFont>("Fonts/MainSmall");
            skin.MenuBackground = content.Load<Texture2D>("Backgrounds/tempwp1");
            skin.GetReady = content.Load<Texture2D>("Backgrounds/getready");
            skin.SongBar = content.Load<Texture2D>("Gui/MapData");
            skin.Note = content.Load<Texture2D>("Sprite/Note");
            skin.Hit = content.Load<SoundEffect>("SoundEffects/hit");
            skin.Miss = content.Load<SoundEffect>("SoundEffects/break");
            skin.PauseOverlay = content.Load<Texture2D>("Backgrounds/PauseOverlay");
            //skin.AlphaShader = content.Load<Effect>("Fx/AlphaMap");
            skin.Particle = content.Load<Texture2D>("Particles/circle");
            skin.Circle = content.Load<Texture2D>("Sprite/circle");
            skin.VisionUp = content.Load<Texture2D>("Backgrounds/visiontop");
            skin.VisionDown = content.Load<Texture2D>("Backgrounds/visionbottom");
            skin.VisionLeft = content.Load<Texture2D>("Backgrounds/visionleft");
            skin.VisionRight = content.Load<Texture2D>("Backgrounds/visionright");
            skin.FunctionOverlay = content.Load<Texture2D>("Backgrounds/functionoverlay");
            skin.Hello = content.Load<SoundEffect>("SoundEffects/hello");
            skin.Click = content.Load<SoundEffect>("SoundEffects/click");
            skin.MouseOver = content.Load<SoundEffect>("SoundEffects/mover");
            skin.LetsGo = content.Load<SoundEffect>("SoundEffects/letsgo");
            skin.SelectorBG = content.Load<Texture2D>("Sprite/ModSelectorBG");
            skin.HitIns = skin.Hit.CreateInstance();

            if (File.Exists(Path.Combine(path, "button.png")))
                skin.Button = Texture2D.FromStream(Graphics, new FileStream(Path.Combine(path, "button.png"), FileMode.Open));

            if (File.Exists(Path.Combine(path, "buttonselected.png")))
                skin.ButtonHover = Texture2D.FromStream(Graphics, new FileStream(Path.Combine(path, "buttonselected.png"), FileMode.Open));

            if (File.Exists(Path.Combine(path, "mainbackground.png")))
                skin.MenuBackground = Texture2D.FromStream(Graphics, new FileStream(Path.Combine(path, "mainbackground.png"), FileMode.Open));

            if (File.Exists(Path.Combine(path, "songbar.png")))
                skin.SongBar = Texture2D.FromStream(Graphics, new FileStream(Path.Combine(path, "songbar.png"), FileMode.Open));

            if (File.Exists(Path.Combine(path, "note.png")))
                skin.Note = Texture2D.FromStream(Graphics, new FileStream(Path.Combine(path, "note.png"), FileMode.Open));

            return skin;
        }

        public static Skin LoadDefault(ContentManager content)
        {
            Skin skin = new Skin();
            skin.Button = content.Load<Texture2D>("Gui/Button");
            skin.ButtonHover = content.Load<Texture2D>("Gui/ButtonSelected");
            skin.FontBig = content.Load<SpriteFont>("Fonts/MainBig");
            skin.Font = content.Load<SpriteFont>("Fonts/Main");
            skin.FontSmall = content.Load<SpriteFont>("Fonts/MainSmall");
            skin.MenuBackground = content.Load<Texture2D>("Backgrounds/tempwp1");
            skin.GetReady = content.Load<Texture2D>("Backgrounds/getready");
            skin.SongBar = content.Load<Texture2D>("Gui/MapData");
            skin.Note = content.Load<Texture2D>("Sprite/note");
            skin.Hit = content.Load<SoundEffect>("SoundEffects/hit");
            skin.Miss = content.Load<SoundEffect>("SoundEffects/break");
            skin.PauseOverlay = content.Load<Texture2D>("Backgrounds/PauseOverlay");
            //skin.AlphaShader = content.Load<Effect>("Fx/AlphaMap");
            skin.Particle = content.Load<Texture2D>("Particles/circle");
            skin.Circle = content.Load<Texture2D>("Sprite/circle");
            skin.VisionUp = content.Load<Texture2D>("Backgrounds/visiontop");
            skin.VisionDown = content.Load<Texture2D>("Backgrounds/visionbottom");
            skin.VisionLeft = content.Load<Texture2D>("Backgrounds/visionleft");
            skin.VisionRight = content.Load<Texture2D>("Backgrounds/visionright");
            skin.FunctionOverlay = content.Load<Texture2D>("Backgrounds/functionoverlay");
            skin.Hello = content.Load<SoundEffect>("SoundEffects/hello");
            skin.Click = content.Load<SoundEffect>("SoundEffects/click");
            skin.MouseOver = content.Load<SoundEffect>("SoundEffects/mover");
            skin.LetsGo = content.Load<SoundEffect>("SoundEffects/letsgo");
            skin.SelectorBG = content.Load<Texture2D>("Sprite/ModSelectorBG");
            skin.HitIns = skin.Hit.CreateInstance();
            skin.SlideLeft = content.Load<Texture2D>("Sprite/slideleft");
            skin.SlideUp = content.Load<Texture2D>("Sprite/slideup");
            skin.SlideRight = content.Load<Texture2D>("Sprite/slideright");
            skin.SlideDown = content.Load<Texture2D>("Sprite/slidedown");
            return skin;
        }
    }
}
