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
        private Sprite Texture;
        private Sprite TextureHover;
        private string ItemId;
        public event EventHandler<GuiEventArgs> OnClick;
        public event EventHandler<GuiEventArgs> OnHover;
        public bool Value;
        public GuiItemType Type;
        public GuiItemText Text;

        public GuiItem(string ItemId, string TextureId, string HoverId, GuiItemType Type, int x, int y, string text = "", GuiTextOrigin TextOrigin = GuiTextOrigin.TopLeft)
        {
            this.Texture = TextureId;
            this.TextureHover = string.IsNullOrEmpty(HoverId)? TextureId : HoverId;
            this.Texture.X = x;
            this.Texture.Y = y;
            this.TextureHover.X = x;
            this.TextureHover.Y = y;
            this.ItemId = ItemId;
            this.Type = Type;
            this.Text = new GuiItemText(text, TextOrigin, 2, 2);
        }

        public void Draw(SpriteBatch sb, MouseValues mouse)
        {
            switch (this.Type)
            {
                case GuiItemType.Button:
                    if (mouse.Hitbox.Intersects(this.Texture.Hitbox))
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
                if (Value)
                {
                    Texture.Draw(sb);
                    Text.Draw(sb, this.Texture.X, this.Texture.Y, this.Texture.Hitbox.Height, this.Texture.Hitbox.Width);
                }
                else
                {
                    TextureHover.Draw(sb);
                    Text.Draw(sb, this.TextureHover.X, this.TextureHover.Y, this.TextureHover.Hitbox.Height, this.TextureHover.Hitbox.Width);
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
        Background
    }

    internal class GuiItemText
    {
        public string text = "";
        public bool hastext { get { return text == ""; } }
        public SpriteFont font;
        public Vector2 Offset;
        public GuiTextOrigin Origin;

        public GuiItemText(string text, GuiTextOrigin Origin, int offx = 0, int offy = 0)
        {
            this.text = text;
            this.Origin = Origin;
            this.Offset = new Vector2(offx, offy);
        }

        public void Draw(SpriteBatch sb, int ParentX, int ParentY, int ParentHeight, int ParentWidth)
        {
            var loc = new Vector2(0, 0);
            var str = RizumuGame.Font.MeasureString(text);
            switch (Origin)
            {
                case GuiTextOrigin.TopLeft:
                    loc.Y = ParentY + Offset.Y;
                    loc.X = ParentX + Offset.X;
                    break;
                case GuiTextOrigin.BottomLeft:
                    loc.Y = ((ParentY + ParentHeight) - str.Y) - Offset.Y;
                    loc.X = ParentX + Offset.X;
                    break;
                case GuiTextOrigin.TopRight:
                    loc.Y = ParentY + Offset.Y;
                    loc.X = ((ParentX + ParentWidth) - str.X) - Offset.X;
                    break;
                case GuiTextOrigin.BottomRight:
                    loc.Y = ((ParentY + ParentHeight) - str.Y) - Offset.Y;
                    loc.X = ((ParentX + ParentWidth) - str.X) - Offset.X;
                    break;
            }

            sb.DrawString(RizumuGame.Font, this.text, loc, Color.White);
        }
    }

    internal enum GuiTextOrigin
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
