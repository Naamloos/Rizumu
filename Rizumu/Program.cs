using Newtonsoft.Json.Linq;
using Rizumu.Objects;
using System;
using System.IO;
using System.Windows.Forms;

namespace Rizumu
{
    public static class Program
    {

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
            using (var game = new Game1())
                game.Run();
        }
    }
}
