using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rizumu.Engine;
using Rizumu.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.GuiObjects
{
    class MapData
    {
        public int X;
        public int Y;
        public Sprite MapDataHolder;
        public Text MapName;
        public Text MapCreator;
        public bool Selected = false;
        public Sprite MapPreview;

        public MapData(SpriteBatch spriteBatch, int x, int y, string name, string creator, bool selected)
        {
            X = x;
            Y = y;
            MapDataHolder = new Sprite(spriteBatch, x, y, GameData.Instance.CurrentSkin.SongBar, Color.White);
            if (selected)
            {
                MapDataHolder.Scale = 1.1f;
                Selected = true;
            }
            MapName = new Text(spriteBatch, GameData.Instance.CurrentSkin.Font, name, x + 5, y + 5, Color.White);
            MapCreator = new Text(spriteBatch, GameData.Instance.CurrentSkin.FontSmall, creator, MapName.X + MapName.Width + 5, MapName.Y + MapName.Height + 5, Color.White);
            Map mm = GameData.MapManager.Maps.Find(xm => xm.Name == name && xm.Creator == creator);
            var pc = new Color(Color.White, 0.8f);
            MapPreview = new Sprite(spriteBatch, X, Y, mm.Background, pc);
        }

        public void Draw()
        {
            if (Selected)
            {
                this.MapDataHolder.Scale = 1.1f;
                this.MapDataHolder.X = (int)(X - (X * 1.1) + X * 1.02);
                this.MapDataHolder.Y = (int)(Y - (Y * 1.1) + Y * 1.03) + 5;
            }
            else
            {
                this.MapDataHolder.Scale = 1f;
                this.MapDataHolder.X = X;
                this.MapDataHolder.Y = Y;
            }
            MapName.X = MapDataHolder.X + 5;
            MapName.Y = MapDataHolder.Y + 5;
            MapCreator.X = MapName.X + 5;
            MapCreator.Y = MapName.Y + MapName.Height + 5;
            MapPreview.Scale = (float)(MapDataHolder.Texture.Height / (float)MapPreview.Texture.Height) - 0.01f;
            MapPreview.Y = Y;
            MapPreview.X = X + (int)(MapDataHolder.Texture.Width * MapDataHolder.Scale) + 15;

            MapDataHolder.Draw();
            MapName.Draw();
            MapCreator.Draw();
            MapPreview.Draw();
        }
    }
}
