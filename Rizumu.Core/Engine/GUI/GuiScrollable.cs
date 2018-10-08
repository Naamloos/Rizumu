using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Engine.GUI
{
    /// <summary>
    /// a GuiScrollable *should* provide an endless X axis that can be scrolled until the last item is hit.
    /// </summary>
    internal class GuiScrollable
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Rectangle Hitbox { get { return new Rectangle(this.X, this.Y, this.Width, this.Height); } }
        public ScrollDirection Direction { get; private set; }
        private int _scrolled;

        public GuiScrollable(int x, int y, int width, int height, ScrollDirection direction)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.Direction = direction;
        }

        public void Draw(Gui Items, SpriteBatch sb, MouseValues mv)
        {
            var tmv = mv;

            // Translate tmv
            int Scrolled = _scrolled / 5;
            if (Hitbox.Intersects(mv.Hitbox))
            {
                if (Scrolled > 1080 - Items.Height && mv.ScrollDelta < 0 || Scrolled < 0 && mv.ScrollDelta > 0)
                {
                    _scrolled += mv.ScrollDelta;
                }
            }

            Items.Draw(sb, mv, new Vector2(X, Y + Scrolled));
        }
    }

    internal enum ScrollDirection
    {
        Horizontal,
        Vertical
    }
}
