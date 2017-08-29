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

        public Note(SpriteBatch spriteBatch, NoteMode mode, int screenWidth, int screenHeight)
        {
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
            Alpha = -50;
        }

        public void Draw(ref bool KeyPress, bool Paused, bool Ready, float rotation, ref int CurrentCombo, ref float visiondist)
        {
            if (!Paused && Ready)
            {
                NoteSprite.Rotation = rotation;
                Position++;
                Alpha++;
                NoteSprite.Color = new Color(Color.White, 1f);
            }

            if (Mode == NoteMode.left)
            {
                NoteSprite.X = BaseX + Position;
            }
            if (Mode == NoteMode.up)
            {
                NoteSprite.Y = BaseY + Position;
            }
            if (Mode == NoteMode.right)
            {
                NoteSprite.X = BaseX - Position;
            }
            if (Mode == NoteMode.down)
            {
                NoteSprite.Y = BaseY - Position;
            }
            if (traveldistance + (NoteSprite.Texture.Width / 2) > Position && Hit == false)
            {
                float vis = traveldistance - Position;
                visiondist = vis / 1000;
                NoteSprite.Draw(true);
            }
            else if (Miss == false && Hit == false)
            {
                GameData.Instance.CurrentSkin.Miss.Play();
                CurrentCombo = 0;
                Miss = true;
            }

            if (Position > traveldistance - (NoteSprite.Texture.Width * 1.6f) && KeyPress && Miss == false)
            {
                if (Hit == false)
                {
                    KeyPress = false;
                    GameData.Instance.CurrentSkin.Hit.Play();
                    CurrentCombo++;
                }
                Hit = true;
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
