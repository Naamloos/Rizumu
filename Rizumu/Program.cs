/*
 * Main program logic. What i do here is check folders, load config, preload songs, etc
 */

using Newtonsoft.Json.Linq;
using Rizumu.Objects;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Rizumu
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!File.Exists("settings.json"))
            {
                File.Create("settings.json").Close();
                Options o = new Options()
                {
                    Fullscreen = true,
                    Left = 100,
                    Down = 98,
                    Right = 102,
                    Up = 104
                };
                File.WriteAllText("settings.json", JObject.FromObject(o).ToString());
            }

            if (!Directory.Exists("content/songs"))
            {
                Directory.CreateDirectory("content/songs");
            }
            if (!Directory.Exists("content/skins"))
            {
                Directory.CreateDirectory("content/skins");
            }
            if (!Directory.Exists("screenshots"))
            {
                Directory.CreateDirectory("screenshots");
            }
            // Preload songs!
            GameResources.Maps = new System.Collections.Generic.Dictionary<string, Objects.RizumuMap>();
            var songs = System.IO.Directory.GetDirectories("content/songs");
            foreach (string path in songs)
            {
                if (File.Exists(Path.Combine(path, "map.json")))
                {
                    var j = JObject.Parse(File.ReadAllText(Path.Combine(path, "map.json")));
                    var m = j.ToObject<Objects.RizumuMap>();
                    GameResources.Maps.Add(path, m);
                }
            }

            //try
            //{
            Randomsong();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Please add songs to your 'songs' folder!\n\n" + ex.ToString());
            //    Environment.Exit(0);
            //}

            // Check settings
            Properties.Settings.Default.Save();
            using (var game = new Game1())
                game.Run();
        }

        public static void Randomsong()
        {
            int songcount = GameResources.Maps.Count;
            System.Random rndsng = new System.Random();
            int firstsong = rndsng.Next(0, songcount);
            string name = GameResources.Maps.Keys.ToList()[firstsong];
            GameResources.startint = firstsong;
            GameResources.selected = name;
            Music.play(name, 0);
        }
    }
#endif
}
