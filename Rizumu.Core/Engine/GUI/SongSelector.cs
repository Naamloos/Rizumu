using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Rizumu.GameLogic.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Engine.GUI
{
    public class SongSelector
    {
        private List<SelectorMap> _maps = new List<SelectorMap>();
        private int _index = 0;
        private Gui _overlay;
        private bool _hasmaps = false;

        public SongSelector(GraphicsDevice GD)
        {
            // Load metadatas + thumbnails
            foreach (var d in Directory.GetDirectories("songs"))
            {
                // Check map directory structure
                var files = Directory.GetFiles(d);
                if (files.Any(x => x.EndsWith("thumbnail.png"))
                    && files.Any(x => x.EndsWith("preview.wav"))
                    && files.Any(x => x.EndsWith("metadata.json")))
                {
                    _maps.Add(new SelectorMap()
                    {
                        Metadata = JsonConvert.DeserializeObject<RizumuMetadata>(File.ReadAllText(files.First(x => x.EndsWith("metadata.json")))),
                        Preview = SoundEffect.FromStream(new FileStream(files.First(x => x.EndsWith("preview.wav")), FileMode.Open)),
                        Thumbnail = Texture2D.FromStream(GD, new FileStream(files.First(x => x.EndsWith("thumbnail.png")), FileMode.Open))
                    });
                }
            }
            if (_maps.Count > 0)
            {
                _hasmaps = true;
                this._overlay = new GuiBuilder()
                    .AddSprite(40, 200, "b", "selectorbox")
                    .AddSprite(520, 200, "b", "selectorbox")
                    .AddSprite(1000, 200, "b", "selectorbox")
                    .AddSprite(1480, 200, "b", "selectorbox")
                    .Build();
            }
            else
            {
                _hasmaps = false;
                this._overlay = new GuiBuilder()
                    .AddSprite(615, 300, "", "", text: "Please add some maps to your game!")
                    .Build();
            }
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime, MouseValues mouseValues)
        {
            this._overlay.Draw(spriteBatch, mouseValues);
            if (_hasmaps)
            {
                // do map selector rendering shite here
                spriteBatch.Draw(_maps.First().Thumbnail, new Rectangle(40, 232, 400, 220), Color.White);
                spriteBatch.DrawString(RizumuGame.Font, _maps.First().Metadata.Name, new Vector2(60, 490), Color.White);
                spriteBatch.DrawString(RizumuGame.Font, _maps.First().Metadata.Artist, new Vector2(60, 570), Color.White);
                spriteBatch.DrawString(RizumuGame.Font, _maps.First().Metadata.Author, new Vector2(60, 655), Color.White);
            }
        }

        internal void Update(GameTime gameTime, MouseValues mouseValues)
        {
        }
    }

    public class SelectorMap
    {
        public RizumuMetadata Metadata;

        public Texture2D Thumbnail;

        public SoundEffect Preview;
    }
}