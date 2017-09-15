#define WINDOWS
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using Rizumu.Objects;

namespace Rizumu.Windows
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
            // Check folders on load
            // CreateDirectory should only create when a folder does not exist.
            Directory.CreateDirectory("songs");
            Directory.CreateDirectory("skins");
            Directory.CreateDirectory("replays");
            if (!File.Exists("Options.json"))
            {
                File.Create("Options.json").Close();
                File.WriteAllText("Options.json", JObject.FromObject(new Options()).ToString());
            }

            // useless commit I know
            using (var game = new Game1(1920, 1080, true))
            {
                Game1.RegisterAndroidUri += (sender, e) => { };
                game.Run();
            }
        }
    }
#endif
}
