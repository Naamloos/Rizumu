using Rizumu.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace Rizumu.Helpers
{
    class MapManager
    {
        public List<Map> Maps;
        public Map Current;

        public MapManager()
        {
            Maps = new List<Map>();
        }

        // returns true if map list is not empty!
        public bool Preload()
        {
            foreach(string folder in Directory.GetDirectories("songs"))
            {
                if(File.Exists(Path.Combine(folder, "map.json")))
                {
                    Map m = JObject.Parse(File.ReadAllText(Path.Combine(folder, "map.json"))).ToObject<Map>();
                    m.Path = folder;
                    Maps.Add(m);
                }
            }
            if (Maps.Count < 1)
                return false;
            return true;
        }

        public void PreloadBackgrounds(SpriteBatch spriteBatch)
        {
            foreach(Map m in Maps)
            {
                if (File.Exists(Path.Combine(m.Path, m.BackgroundFile)))
                {
                    var fs = new FileStream(Path.Combine(m.Path, m.BackgroundFile), FileMode.Open);
                    m.Background = Texture2D.FromStream(spriteBatch.GraphicsDevice, fs);
                    fs.Close();
                }
                else
                {
                    m.Background = GameData.Instance.CurrentSkin.MenuBackground;
                }
            }
        }
    }
}
