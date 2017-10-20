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
using System.Security.Cryptography;
using Microsoft.Xna.Framework.Media;

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
            ConvertOsuMaps();
            foreach (string folder in Directory.GetDirectories("songs"))
            {
                if (File.Exists(Path.Combine(folder, "map.json")))
                {
                    Map m = JObject.Parse(File.ReadAllText(Path.Combine(folder, "map.json"))).ToObject<Map>();
                    using (var md5 = MD5.Create())
                    {
                        using (var stream = File.OpenRead(Path.Combine(folder, "map.json")))
                        {
                            m.MD5 = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                        }
                    }
                    m.Path = folder;
                    Maps.Add(m);
                }
            }
            return Maps.Any();
        }

        public void PreloadSongs()
        {

        }

        public void PreloadBackgrounds(SpriteBatch spriteBatch)
        {
            foreach (Map m in Maps)
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

        public void ConvertOsuMaps()
        {
            foreach (string folder in Directory.GetDirectories("songs"))
            {
                bool HasOsu = false;
                if (Directory.GetFiles(folder).Any(x => x.EndsWith(".osu")))
                {
                    int heck = 0;
                    foreach (string file in Directory.GetFiles(folder))
                    {
                        if (file.EndsWith(".osu"))
                        {
                            Map m = new Map();

                            m = ShittyOsuConverter.FromBeatmap(file);
                            m.Name = file.Substring(file.LastIndexOf('\\') + 1).Replace(".osu", "");
                            m.Creator = "OsuGame";

                            var images = Directory.GetFiles(folder).Where(x => x.EndsWith(".png") || x.EndsWith(".jpg") || x.EndsWith(".jpeg"));
                            if (images.Any())
                            {
                                m.BackgroundFile = images.First().Substring(images.First().LastIndexOf('\\') + 1);
                            }

                            var mp3s = Directory.GetFiles(folder).Where(x => x.EndsWith(".mp3"));
                            if (mp3s.Any())
                            {
                                m.FileName = mp3s.First().Substring(mp3s.First().LastIndexOf('\\') + 1);
                            }
                            string fn = file.Substring(file.LastIndexOf('\\') + 1).Replace(".osu", "");
                            string newpath = Path.Combine("songs", fn);
                            Directory.CreateDirectory(newpath);
                            if (m.FileName != "empty")
                                File.Copy(Path.Combine(folder, m.FileName), Path.Combine(newpath, m.FileName));
                            if (m.BackgroundFile != "empty")
                                File.Copy(Path.Combine(folder, m.BackgroundFile), Path.Combine(newpath, m.BackgroundFile));
                            File.Create(Path.Combine(newpath, "map.json")).Close();
                            File.WriteAllText(Path.Combine(newpath, "map.json"), JsonConvert.SerializeObject(m));
                            HasOsu = true;
                        }
                    }

                    Directory.Delete(folder, true);
                }
            }
        }
    }
}
