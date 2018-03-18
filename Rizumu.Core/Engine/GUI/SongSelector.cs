using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Rizumu.Engine.Entities;
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
        private LoopList<SelectorMap> _maps = new LoopList<SelectorMap>();
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
                    var thumb = new FileStream(files.First(x => x.EndsWith("thumbnail.png")), FileMode.Open);
                    var prev = new FileStream(files.First(x => x.EndsWith("preview.wav")), FileMode.Open);
                    _maps.Add(new SelectorMap()
                    {
                        Metadata = JsonConvert.DeserializeObject<RizumuMetadata>(File.ReadAllText(files.First(x => x.EndsWith("metadata.json")))),
                        Preview = SoundEffect.FromStream(prev),
                        Thumbnail = Texture2D.FromStream(GD, thumb)
                    });
                    thumb.Close();
                    prev.Close();
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
                spriteBatch.Draw(_maps[_index].Thumbnail, new Rectangle(40, 232, 400, 220), Color.White);
                spriteBatch.DrawString(RizumuGame.Font, _maps[_index].Metadata.Name, new Vector2(60, 495), Color.White);
                spriteBatch.DrawString(RizumuGame.Font, _maps[_index].Metadata.Artist, new Vector2(60, 575), Color.White);
                spriteBatch.DrawString(RizumuGame.Font, _maps[_index].Metadata.Author, new Vector2(60, 660), Color.White);

                spriteBatch.Draw(_maps[_index + 1].Thumbnail, new Rectangle(520, 232, 400, 220), Color.White);
                spriteBatch.DrawString(RizumuGame.Font, _maps[_index + 1].Metadata.Name, new Vector2(540, 495), Color.White);
                spriteBatch.DrawString(RizumuGame.Font, _maps[_index + 1].Metadata.Artist, new Vector2(540, 575), Color.White);
                spriteBatch.DrawString(RizumuGame.Font, _maps[_index + 1].Metadata.Author, new Vector2(540, 660), Color.White);

                spriteBatch.Draw(_maps[_index + 2].Thumbnail, new Rectangle(1000, 232, 400, 220), Color.White);
                spriteBatch.DrawString(RizumuGame.Font, _maps[_index + 2].Metadata.Name, new Vector2(1020, 495), Color.White);
                spriteBatch.DrawString(RizumuGame.Font, _maps[_index + 2].Metadata.Artist, new Vector2(1020, 575), Color.White);
                spriteBatch.DrawString(RizumuGame.Font, _maps[_index + 2].Metadata.Author, new Vector2(1020, 660), Color.White);

                spriteBatch.Draw(_maps[_index + 3].Thumbnail, new Rectangle(1480, 232, 400, 220), Color.White);
                spriteBatch.DrawString(RizumuGame.Font, _maps[_index + 3].Metadata.Name, new Vector2(1500, 495), Color.White);
                spriteBatch.DrawString(RizumuGame.Font, _maps[_index + 3].Metadata.Artist, new Vector2(1500, 575), Color.White);
                spriteBatch.DrawString(RizumuGame.Font, _maps[_index + 3].Metadata.Author, new Vector2(1500, 660), Color.White);
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