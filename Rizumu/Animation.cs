/*
 * Yet unused animation class.
 * The animation should update every time draw() is called.
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
    struct Animation
    {
        Texture2D[] frames;
        public static int frameno;
        public bool loops;
        public bool finished;
        public int identifier;
        public int x;
        public int y;
        public int framedelay;
        int delayedframes;


        public Animation(Texture2D[] frames, bool loops, int identifier, int x, int y, int framedelay)
        {
            this.frames = frames;
            this.loops = loops;
            frameno = 0;
            this.finished = false;
            this.identifier = identifier;
            this.x = x;
            this.y = y;
            this.framedelay = framedelay;
            delayedframes = 0;
        }

        public Animation draw(SpriteBatch spriteBatch)
        {
            if (!loops)
            {
                if (frameno == frames.Length - 1)
                {
                    finished = true;
                }
            }
            if (!finished)
            {
                Sprite current = new Sprite(spriteBatch, x, y, frames[frameno], GameResources.basecolor);
                current.draw();
            }
            delayedframes++;
            if (delayedframes == framedelay)
            {
                delayedframes = 0;
                frameno++;
            }
            return this;
        }
    }
}
