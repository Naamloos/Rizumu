using Microsoft.Xna.Framework.Graphics;
using Rizumu.Core.Engine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Core.Engine.GUI
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

        public GuiItem(string ItemId, string TextureId, string HoverId, GuiItemType Type, int x, int y)
        {
            this.Texture = TextureId;
            this.TextureHover = HoverId;
            this.Texture.X = x;
            this.Texture.Y = y;
            this.TextureHover.X = x;
            this.TextureHover.Y = y;
            this.ItemId = ItemId;
            this.Type = Type;
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

                case GuiItemType.Checkbox:
                    if (mouse.Hitbox.Intersects(this.Texture.Hitbox))
                    {
                        if (mouse.Clicked)
                        {
                            OnClick?.Invoke(this, new GuiEventArgs()
                            {
                                Id = ItemId,
                                Item = this,
                                Type = Type
                            });
                            Value = !Value;
                        }
                        OnHover?.Invoke(this, new GuiEventArgs()
                        {
                            Id = ItemId,
                            Item = this,
                            Type = Type
                        });
                    }
                    break;
                default:
                case GuiItemType.Sprite:
                    break;
            }

            if (Value)
                Texture.Draw(sb);
            else
                TextureHover.Draw(sb);
        }
    }

    internal enum GuiItemType
    {
        Button,
        Checkbox,
        Sprite
    }

    internal class GuiEventArgs : EventArgs
    {
        public GuiItem Item = null;
        public GuiItemType Type = GuiItemType.Button;
        public string Id = "";
    }
}
