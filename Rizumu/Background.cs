/*
 * This is the class I use for drawing backrounds. Kind of unneccesary,
 * though this is some of the first code i wrote for the rewrite.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu
{
    struct Background
    {
        public SpriteBatch spriteBatch;
        public Texture2D texture;

        public Background(SpriteBatch spriteBatch, Texture2D texture)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
        }

        public void draw()
        {
            this.spriteBatch.Draw(texture, new Rectangle(0, 0, Game1.graphics.PreferredBackBufferWidth, Game1.graphics.PreferredBackBufferHeight), GameResources.basecolor);
        }
    }
}
