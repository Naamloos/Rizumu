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

        // Backgrounds
        public Texture2D MenuBackground;
        public Texture2D PauseOverlay;
        public Texture2D GetReady;
        public Texture2D VisionUp;
        public Texture2D VisionDown;
        public Texture2D VisionLeft;
        public Texture2D VisionRight;

        // Sounds
        public SoundEffect Hit;
        public SoundEffect Miss;

        // Fonts
        public SpriteFont FontBig;
        public SpriteFont Font;
        public SpriteFont FontSmall;

        // Effects
        public Effect AlphaShader;

        public static Skin LoadFromPath(GraphicsDevice Graphics, ContentManager content, string path)
        {
            Skin skin = new Skin();

            if (File.Exists(Path.Combine(path, "button.png")))
                skin.Button = Texture2D.FromStream(Graphics, new FileStream(Path.Combine(path, "button.png"), FileMode.Open));
            else
                skin.Button = content.Load<Texture2D>("Gui/Button");

            if (File.Exists(Path.Combine(path, "buttonselected.png")))
                skin.ButtonHover = Texture2D.FromStream(Graphics, new FileStream(Path.Combine(path, "buttonselected.png"), FileMode.Open));
            else
                skin.ButtonHover = content.Load<Texture2D>("Gui/ButtonSelected");

            if (File.Exists(Path.Combine(path, "mainbackground.png")))
                skin.MenuBackground = Texture2D.FromStream(Graphics, new FileStream(Path.Combine(path, "mainbackground.png"), FileMode.Open));
            else
                skin.MenuBackground = content.Load<Texture2D>("Backgrounds/MainBackground");

            if (File.Exists(Path.Combine(path, "songbar.png")))
                skin.SongBar = Texture2D.FromStream(Graphics, new FileStream(Path.Combine(path, "songbar.png"), FileMode.Open));
            else
                skin.SongBar = content.Load<Texture2D>("Gui/MapData");

            if (File.Exists(Path.Combine(path, "note.png")))
                skin.Note = Texture2D.FromStream(Graphics, new FileStream(Path.Combine(path, "note.png"), FileMode.Open));
            else
                skin.Note = content.Load<Texture2D>("Sprite/Note");

            skin.FontBig = content.Load<SpriteFont>("Fonts/MainBig");
            skin.Font = content.Load<SpriteFont>("Fonts/Main");
            skin.FontSmall = content.Load<SpriteFont>("Fonts/MainSmall");
            skin.Hit = content.Load<SoundEffect>("SoundEffects/hit");
            skin.Miss = content.Load<SoundEffect>("SoundEffects/break");
            skin.PauseOverlay = content.Load<Texture2D>("Backgrounds/PauseOverlay");
            skin.AlphaShader = content.Load<Effect>("Fx/AlphaMap");
            skin.Particle = content.Load<Texture2D>("Particles/circle");
            skin.Circle = content.Load<Texture2D>("Sprite/circle");
            skin.GetReady = content.Load<Texture2D>("Backgrounds/getready");

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
            skin.MenuBackground = content.Load<Texture2D>("Backgrounds/MainBackground");
            skin.GetReady = content.Load<Texture2D>("Backgrounds/getready");
            skin.SongBar = content.Load<Texture2D>("Gui/MapData");
            skin.Note = content.Load<Texture2D>("Sprite/Note");
            skin.Hit = content.Load<SoundEffect>("SoundEffects/hit");
            skin.Miss = content.Load<SoundEffect>("SoundEffects/break");
            skin.PauseOverlay = content.Load<Texture2D>("Backgrounds/PauseOverlay");
            skin.AlphaShader = content.Load<Effect>("Fx/AlphaMap");
            skin.Particle = content.Load<Texture2D>("Particles/circle");
            skin.Circle = content.Load<Texture2D>("Sprite/circle");
            skin.VisionUp = content.Load<Texture2D>("Backgrounds/visiontop");
            skin.VisionDown = content.Load<Texture2D>("Backgrounds/visionbottom");
            skin.VisionLeft = content.Load<Texture2D>("Backgrounds/visionleft");
            skin.VisionRight = content.Load<Texture2D>("Backgrounds/visionright");
            return skin;
        }
    }
}
