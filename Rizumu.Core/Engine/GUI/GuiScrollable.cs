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
	/// a GuiScrollable *should* provide an endless X or Y axis that can be scrolled until the last item is hit.
	/// </summary>
	internal class GuiScrollable
	{
		public Gui Items;
		public int X { get; private set; }
		public int Y { get; private set; }
		public int Width { get; private set; }
		public int Height { get; private set; }
		public Rectangle Hitbox { get { return new Rectangle(this.X, this.Y, this.Width, this.Height); } }
		public ScrollDirection Direction { get; private set; }
		private RenderTarget2D RenderTarget;

		public GuiScrollable(Gui items, int x, int y, int width, int height, ScrollDirection direction, GraphicsDevice graphics)
		{
			this.Items = items;
			this.X = x;
			this.Y = y;
			this.Width = width;
			this.Height = height;
			this.Direction = direction;
			this.RenderTarget = new RenderTarget2D(graphics, width, height);
		}

		public void Draw(SpriteBatch sb, MouseValues mv)
		{
			var tmv = mv;
			// TODO: Translate tmv
			Items.Draw(sb, tmv, RenderTarget);
			sb.Draw(RenderTarget, Hitbox, Color.White);
		}
	}

	internal enum ScrollDirection
	{
		Horizontal,
		Vertical
	}
}
