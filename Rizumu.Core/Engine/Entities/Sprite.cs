using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Core.Engine.Entities
{
    internal class Sprite
    {
        internal Texture2D Texture2D { get; set; }
        internal Point Location;
        internal int X { get { return Location.X; } set { Location.X = value; } }
        internal int Y { get { return Location.Y; } set { Location.Y = value; } }
        internal Rectangle Hitbox { get { return new Rectangle(Location, new Point(Texture2D.Width, Texture2D.Height)); } }

        public void Draw(SpriteBatch sb, int? X = null, int? Y = null)
        {
            if (!(X == null))
                this.X = (int)X;
            if (!(Y == null))
                this.Y = (int)Y;

            sb.Draw(Texture2D, Hitbox, Color.White);
        }

        public static implicit operator Sprite(string texture)
        {
            if (texture == null)
                return null;
            return new Sprite()
            {
                Texture2D = TextureManager.GetTexture(texture),
                X = 0,
                Y = 0
            };
        }
    }
}
