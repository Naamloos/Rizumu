using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Core.Engine.GUI
{
    internal class GuiBuilder
    {
        private List<GuiItem> Items;

        public GuiBuilder()
        {
            Items = new List<GuiItem>();
        }

        public GuiBuilder AddButton(int x, int y, string id, string texture, string hovertexture)
        {
            Items.Add(new GuiItem(id, texture, hovertexture, GuiItemType.Button, x, y));
            return this;
        }

        public GuiBuilder AddCheckbox(int x, int y ,string id, string texture, string hovertexture)
        {
            Items.Add(new GuiItem(id, texture, hovertexture, GuiItemType.Checkbox, x, y));
            return this;
        }

        public GuiBuilder AddSprite(int x, int y, string id, string texture, string hovertexture = null)
        {
            Items.Add(new GuiItem(id, texture, string.IsNullOrEmpty(hovertexture) ? texture : hovertexture, GuiItemType.Sprite, x, y));
            return this;
        }

        public Gui Build()
        {
            return new Gui(Items);
        }
    }

    internal class Gui
    {
        private List<GuiItem> Items;

        internal Gui(List<GuiItem> items)
        {
            Items = items;
        }

        public void Draw(SpriteBatch sb, MouseValues mv)
        {
            foreach (var i in Items)
            {
                i.Draw(sb, mv);
            }
        }
    }
}
