using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Engine
{
    class Sprite
    {
        public SpriteBatch SpriteBatch;
        public Texture2D Texture;
        public int X;
        public int Y;
        public Color Color;
        internal Color WithAlpha { get { return new Color(Color, Alpha); } }
        public float Alpha;
        public Rectangle Hitbox;
        public float Rotation;
        public float Scale;
        public float GlobalScaleX;
        public float GlobalScaleY;

        public Sprite(SpriteBatch spriteBatch, int x, int y, Texture2D texture, Color color, float scale = 1f, float rotation = 0f)
        {
            SpriteBatch = spriteBatch;
            X = x;
            Y = y;
            Texture = texture;
            Color = color;
            Scale = scale;
            Rotation = rotation;

            GlobalScaleX = ((float)GameData.globalwidth / GameData.realwidth) * scale;
            GlobalScaleY = ((float)GameData.globalheight / GameData.realheight) * scale;
            Hitbox = new Rectangle((int)(x * GlobalScaleX), (int)(y *  GlobalScaleY), (int)(texture.Width * GlobalScaleX), (int)(texture.Height * GlobalScaleY));
        }

        public void Draw(bool Note = false)
        {
            GlobalScaleX = ((float)GameData.realwidth / (float)GameData.globalwidth) * Scale;
            GlobalScaleY = ((float)GameData.realheight / (float)GameData.globalheight) * Scale;
            Hitbox = new Rectangle((int)(X * GlobalScaleX), (int)(Y * GlobalScaleY), (int)(Texture.Width * GlobalScaleX), (int)(Texture.Height * GlobalScaleY));

            if (Note)
                SpriteBatch.Draw(Texture, new Vector2(X + (Texture.Width / 2), Y + (Texture.Height / 2)), null, Color, Rotation, new Vector2(Texture.Width / 2, Texture.Height / 2), Scale, SpriteEffects.None, 1f);
            else
                SpriteBatch.Draw(Texture, new Vector2(X, Y), null, Color, Rotation, new Vector2(0,0), Scale, SpriteEffects.None, 1f);
        }

        public void DrawScaled(int width, int height)
        {
            SpriteBatch.Draw(Texture, new Rectangle(X, Y, width, height), WithAlpha);
        }
    }
}
