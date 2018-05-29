using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Engine.GUI
{
    internal class GuiBuilder
    {
        private List<GuiItem> Items;

        public GuiBuilder()
        {
            Items = new List<GuiItem>();
        }

        public GuiBuilder AddButton(int x, int y, string id, string texture, string hovertexture, GuiOrigin Origin, string text = "", GuiOrigin TextOrigin = GuiOrigin.TopLeft, Vector2? TextOffset = null)
        {
            Items.Add(new GuiItem(id, texture, hovertexture, GuiItemType.Button, x, y, Origin, text, TextOrigin, textoffset: TextOffset));
            return this;
        }

        public GuiBuilder AddCheckbox(int x, int y ,string id, string texture, string hovertexture, GuiOrigin Origin, string text = "")
        {
            Items.Add(new GuiItem(id, texture, hovertexture, GuiItemType.Checkbox, x, y, Origin, text));
            return this;
        }

        public GuiBuilder AddSprite(int x, int y, string id, string texture, string hovertexture = null, 
            GuiOrigin Origin = GuiOrigin.TopLeft, string text = "", GuiOrigin TextOrigin = GuiOrigin.TopLeft, int widthoverride = -1, int heightoverride = -1)
        {
            Items.Add(new GuiItem(id, texture, string.IsNullOrEmpty(hovertexture) ? texture : hovertexture, GuiItemType.Sprite, x, y, Origin, text, TextOrigin, heightoverride, widthoverride));
            return this;
        }

        public GuiBuilder AddBackground(string texture)
        {
            Items.Add(new GuiItem("", texture, "", GuiItemType.Background, 0, 0, GuiOrigin.TopLeft));
            return this;
        }

        public Gui Build()
        {
            return new Gui(Items);
        }
    }

    internal class Gui
    {
        internal List<GuiItem> Items;
        public event EventHandler<GuiEventArgs> OnClick;
        public event EventHandler<GuiEventArgs> OnHover;
		public int Height => Items.Select(x => x.Texture.Y).Max() + Items.Select(x => x.Texture.Hitbox.Height).Max();
		public int Width => Items.Select(x => x.Texture.X).Max() + Items.Select(x => x.Texture.Hitbox.Width).Max();

		internal Gui(List<GuiItem> items)
        {
            Items = items;
            foreach(var i in Items)
            {
                i.OnClick += (sender, e) => OnClick?.Invoke(sender, e);
                i.OnHover += (sender, e) => OnHover?.Invoke(sender, e);
            }
        }

        public void Draw(SpriteBatch sb, MouseValues mv, Vector2? offset = null)
        {
            foreach (var i in Items)
            {
                i.Draw(sb, mv, offset);
            }
        }
    }
}
