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
        public Rectangle Hitbox;
        public float Rotation;
        public float Scale;

        public Sprite(SpriteBatch spriteBatch, int x, int y, Texture2D texture, Color color, float scale = 1f, float rotation = 0f)
        {
            SpriteBatch = spriteBatch;
            X = x;
            Y = y;
            Texture = texture;
            Color = color;
            Scale = scale;
            Rotation = rotation;
            Hitbox = new Rectangle(x, y, (int)(texture.Width * scale), (int)(texture.Height * scale));
        }

        public void Draw()
        {
            SpriteBatch.Draw(Texture, new Vector2(X, Y), null, Color, Rotation, new Vector2(0, 0), Scale, SpriteEffects.None, 0);
        }

        public void DrawSize(int width, int height)
        {
            SpriteBatch.Draw(Texture, new Rectangle(X, Y, width, height), Color);
        }
    }
}
