/*
 * Class i use for drawing sprites.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Rizumu
{
    public struct Sprite
    {
        public SpriteBatch spriteBatch;
        public int x;
        public int y;
        public Texture2D texture;
        public Color color;
        public Rectangle hitbox;
        public float rotation;
        public float scale;

        public Sprite(SpriteBatch spriteBatch, int x, int y, Texture2D texture, Color color)
        {
            this.spriteBatch = spriteBatch;
            this.x = x;
            this.y = y;
            this.texture = texture;
            this.color = color;
            this.hitbox = new Rectangle(new Point(x, y), new Point(texture.Width, texture.Height));
            scale = 1;
            rotation = 0;
        }

        public void draw()
        {
            hitbox = new Rectangle(new Point(x, y), new Point(texture.Width, texture.Height));
            spriteBatch.Draw(texture, new Vector2(x + ((texture.Width * scale) / 2), y + ((texture.Height * scale) / 2)), null, color, rotation, new Vector2((texture.Width) / 2, (texture.Height) / 2), scale, SpriteEffects.None, 0);
        }
    }
}
