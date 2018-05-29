using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Engine.Entities
{
	internal class Sprite
	{
		internal Texture2D Texture2D { get; set; }
		internal Point Location;
		internal int X { get { return Location.X; } set { Location.X = value; } }
		internal int Y { get { return Location.Y; } set { Location.Y = value; } }
		internal Rectangle Hitbox { get { return new Rectangle(Location.X, Location.Y, Empty ? 1 : Texture2D.Width, Empty ? 1 : Texture2D.Height); } }
		internal bool Empty = false;

		public static Sprite CreateEmpty()
		{
			return new Sprite()
			{
				Empty = true,
			};
		}

		public void Draw(SpriteBatch sb, int? X = null, int? Y = null, int? Width = null, int? Height = null)
		{
			if (!Empty)
			{
				sb.Draw(Texture2D, new Rectangle(X ?? this.X, Y ?? this.Y, Width ?? Texture2D.Width, Height ?? Texture2D.Height), Color.White);
			}
		}

		public static implicit operator Sprite(string texture)
		{
			if (texture == "")
				return Sprite.CreateEmpty();
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
