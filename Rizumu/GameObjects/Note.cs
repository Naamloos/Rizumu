using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rizumu.Engine;
using Rizumu.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.GameObjects
{
    class Note
    {
        public int Time;
        public int Position;
        public bool Hit;
        public bool Miss;
        public float Accuracy;

        public SpriteBatch spriteBatch;
        public Sprite NoteSprite;
        public NoteMode Mode;
        public int BaseX;
        public int BaseY;
        public int traveldistance;
        public int Alpha;
        public float LocalSpeed;
        public int jumpinoffset;
        int bgw = 0;
        int bgh = 0;

        public Note(SpriteBatch spriteBatch, NoteMode mode, int screenWidth, int screenHeight, float speed)
        {
            bgw = screenWidth;
            bgh = screenHeight;
            LocalSpeed = speed;
            var tex = GameData.Instance.CurrentSkin.Note;
            Mode = mode;
            if (mode == NoteMode.left)
            {
                BaseX = -1 * tex.Width;
                BaseY = (screenHeight / 2) - (tex.Height / 2);
                NoteSprite = new Sprite(spriteBatch, BaseX, BaseY, tex, Color.White);
                traveldistance = screenWidth / 2;
            }
            if (mode == NoteMode.up)
            {
                BaseX = (screenWidth / 2) - (tex.Width / 2);
                BaseY = -1 * tex.Height;
                NoteSprite = new Sprite(spriteBatch, BaseX, BaseY, tex, Color.White);
                traveldistance = screenHeight / 2;
            }
            if (mode == NoteMode.right)
            {
                BaseX = screenWidth + tex.Width;
                BaseY = (screenHeight / 2) - (tex.Height / 2);
                NoteSprite = new Sprite(spriteBatch, BaseX, BaseY, tex, Color.White);
                traveldistance = screenWidth / 2 + NoteSprite.Texture.Width;
            }
            if (mode == NoteMode.down)
            {
                BaseX = (screenWidth / 2) - (tex.Width / 2);
                BaseY = screenHeight + tex.Height;
                NoteSprite = new Sprite(spriteBatch, BaseX, BaseY, tex, Color.White);
                traveldistance = screenHeight / 2 + NoteSprite.Texture.Height;
            }
            jumpinoffset = traveldistance;
            traveldistance = (int)(traveldistance / speed);
            jumpinoffset = traveldistance - jumpinoffset;
            //Time = Time - noteoffset;
            Alpha = -50;
            NoteSprite.Scale = GameData.Instance.Mods.SizeMultiplier;
        }

        public void Draw(ref bool KeyPress, bool Paused, bool Ready, float rotation, ref int CurrentCombo, ref float visiondist, int Timer, bool Auto)
        {
            if (!Paused && Ready)
            {
                NoteSprite.Rotation = rotation;
                Alpha++;
                NoteSprite.Color = new Color(Color.White, 1f);
            }

            if (Mode == NoteMode.left)
            {
                Position = Timer - ((Time - jumpinoffset) - ((bgw / 2) + NoteSprite.Texture.Width));
                Position = (int)(Position * LocalSpeed);
                NoteSprite.X = BaseX + Position;
            }
            if (Mode == NoteMode.up)
            {
                Position = Timer - ((Time - jumpinoffset) - ((bgh / 2) + NoteSprite.Texture.Height));
                Position = (int)(Position * LocalSpeed);
                NoteSprite.Y = BaseY + Position;
            }
            if (Mode == NoteMode.right)
            {
                Position = Timer - ((Time - jumpinoffset) - ((bgw / 2) + NoteSprite.Texture.Width * 2));
                Position = (int)(Position * LocalSpeed);
                NoteSprite.X = BaseX - Position;
            }
            if (Mode == NoteMode.down)
            {
                Position = Timer - ((Time - jumpinoffset) - ((bgh / 2) + NoteSprite.Texture.Height * 2));
                Position = (int)(Position * LocalSpeed);
                NoteSprite.Y = BaseY - Position;
            }
            if (traveldistance * LocalSpeed + (NoteSprite.Texture.Width / 2) > Position && Hit == false)
            {
                NoteSprite.Draw(true);
            }
            else if (!Miss && !Hit)
            {
                GameData.Instance.CurrentSkin.Miss.Play();
                CurrentCombo = 0;
                Miss = true;
            }

            if (Auto)
            {
                if (Position > traveldistance * LocalSpeed - NoteSprite.Texture.Width && !Miss)
                {
                    if (!Hit)
                    {
                        KeyPress = false;
                        if (Game1.Windows)
                        {
                            GameData.Instance.CurrentSkin.Hit.Play();
                        }
                        CurrentCombo++;
                        visiondist = 0.4f;
                    }
                    Hit = true;
                }
            }
            else
            {
                if (Position > traveldistance * LocalSpeed - (NoteSprite.Texture.Width * 1.6f) && KeyPress && Miss == false)
                {
                    if (!Hit)
                    {
                        KeyPress = false;
                        if (Game1.Windows)
                            GameData.Instance.CurrentSkin.Hit.Play();
                        CurrentCombo++;
                        visiondist = 0.4f;
                    }
                    Hit = true;
                }
            }
        }
    }

    enum NoteMode
    {
        left,
        up,
        right,
        down
    }
}
