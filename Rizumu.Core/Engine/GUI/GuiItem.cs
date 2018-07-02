using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rizumu;
using Rizumu.Engine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Engine.GUI
{
	internal class GuiItem
	{
		internal Sprite Texture;
		internal Sprite TextureHover;
		internal string ItemId;
		internal GuiOrigin Origin;
		public event EventHandler<GuiEventArgs> OnClick;
		public event EventHandler<GuiEventArgs> OnHover;
		public bool Value;
		public GuiItemType Type;
		public GuiItemText Text;
		public int CustomHeight = -1;
		public int CustomWidth = -1;
		public GuiItemText SongSelectArtist;

		public GuiItem(string ItemId, string TextureId, string HoverId, GuiItemType Type, int x, int y, GuiOrigin Origin,
			string text = "", GuiOrigin TextOrigin = GuiOrigin.TopLeft, int heightoverride = -1, int widthoverride = -1, Vector2? textoffset = null)
		{
			this.Texture = TextureId;
			this.TextureHover = string.IsNullOrEmpty(HoverId) ? TextureId : HoverId;
			this.ItemId = ItemId;
			this.Type = Type;
			this.Origin = Origin;
			this.Text = new GuiItemText(text, TextOrigin, 2, 2);
			if (textoffset != null)
				this.Text.Offset = (Vector2)textoffset;
			this.CustomHeight = heightoverride;
			this.CustomWidth = widthoverride;

			int locx = 0;
			int locy = 0;

			switch (Origin)
			{
				case GuiOrigin.TopLeft:
					locx = x;
					locy = y;
					break;
				case GuiOrigin.TopRight:
					locx = (1920 - this.Texture.Hitbox.Width) - x;
					locy = y;
					break;
				case GuiOrigin.BottomLeft:
					locx = x;
					locy = (1080 - this.Texture.Hitbox.Height) - y;
					break;
				case GuiOrigin.BottomRight:
					locx = (1920 - this.Texture.Hitbox.Width) - x;
					locy = (1080 - this.Texture.Hitbox.Height) - y;
					break;
			}

			this.Texture.X = locx;
			this.Texture.Y = locy;
			this.TextureHover.X = locx;
			this.TextureHover.Y = locy;
		}

		public void Draw(SpriteBatch sb, MouseValues mouse, Vector2? offset = null)
		{
			var hitbox = Texture.Hitbox;
			var loc = Texture.Location;

			if (offset != null)
			{
				hitbox.X += (int)offset.Value.X;
				hitbox.Y += (int)offset.Value.Y;
				loc.X += (int)offset.Value.X;
				loc.Y += (int)offset.Value.Y;
			}

			switch (this.Type)
			{
				case GuiItemType.Button:
					if (mouse.Hitbox.Intersects(hitbox))
					{
						Value = true;
						if (mouse.Clicked)
							OnClick?.Invoke(this, new GuiEventArgs()
							{
								Id = ItemId,
								Item = this,
								Type = Type
							});
						OnHover?.Invoke(this, new GuiEventArgs()
						{
							Id = ItemId,
							Item = this,
							Type = Type
						});
					}
					else
						Value = false;
					break;
				default:
				case GuiItemType.Sprite:
					break;
			}

			if (Type == GuiItemType.Background)
			{
				Texture.Draw(sb, Width: 1920, Height: 1080);
			}
			else
			{
				if (!Value)
				{
					Texture.Draw(sb, loc.X, loc.Y, CustomWidth == -1 ? (int?)null : CustomWidth, CustomHeight == -1 ? (int?)null : CustomHeight);
					Text.Draw(sb, loc.X, loc.Y, this.Texture.Hitbox.Height, this.Texture.Hitbox.Width);
					if (this.SongSelectArtist != null)
						SongSelectArtist.Draw(sb, loc.X, loc.Y, this.Texture.Hitbox.Height, this.Texture.Hitbox.Width);
				}
				else
				{
					TextureHover.Draw(sb, loc.X, loc.Y, CustomWidth == -1 ? (int?)null : CustomWidth, CustomHeight == -1 ? (int?)null : CustomHeight);
					Text.Draw(sb, loc.X, loc.Y, this.TextureHover.Hitbox.Height, this.TextureHover.Hitbox.Width);
					if (this.SongSelectArtist != null)
						SongSelectArtist.Draw(sb, loc.X, loc.Y, this.Texture.Hitbox.Height, this.Texture.Hitbox.Width);
				}
			}
		}
	}

	internal enum GuiItemType
	{
		Button,
		Checkbox,
		Sprite,
		Text,
		Background,
		SongButton,
	}

	internal class GuiItemText
	{
		public string text = "";
		public bool hastext { get { return text == ""; } }
		public SpriteFont font;
		public Vector2 Offset;
		public GuiOrigin Origin;
		public Color Color = Color.White;

		public GuiItemText(string text, GuiOrigin Origin, int offx = 0, int offy = 0)
		{
			this.text = text;
			this.Origin = Origin;
			this.Offset = new Vector2(offx, offy);
			this.font = RizumuGame.Font;
		}

		public void Draw(SpriteBatch sb, int ParentX, int ParentY, int ParentHeight, int ParentWidth)
		{
			var loc = new Vector2(0, 0);
			var str = font.MeasureString(text);
			switch (Origin)
			{
				case GuiOrigin.TopLeft:
					loc.Y = ParentY + Offset.Y;
					loc.X = ParentX + Offset.X;
					break;
				case GuiOrigin.BottomLeft:
					loc.Y = ((ParentY + ParentHeight) - str.Y) - Offset.Y;
					loc.X = ParentX + Offset.X;
					break;
				case GuiOrigin.TopRight:
					loc.Y = ParentY + Offset.Y;
					loc.X = ((ParentX + ParentWidth) - str.X) - Offset.X;
					break;
				case GuiOrigin.BottomRight:
					loc.Y = ((ParentY + ParentHeight) - str.Y) - Offset.Y;
					loc.X = ((ParentX + ParentWidth) - str.X) - Offset.X;
					break;
			}

			sb.DrawString(font, this.text, loc, Color);
		}
	}

	internal enum GuiOrigin
	{
		TopLeft,
		TopRight,
		BottomLeft,
		BottomRight
	}

	internal class GuiEventArgs : EventArgs
	{
		public GuiItem Item = null;
		public GuiItemType Type = GuiItemType.Button;
		public string Id = "";
	}
}
