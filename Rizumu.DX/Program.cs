using Newtonsoft.Json;
using Rizumu.GameLogic.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Rizumu.DX
{
    public class Entry
    {
        [STAThread]
        static void Main()
        {
            Logger.EnableLoggerDump();
            Logger.Log("Checking folder/file prerequisites");
            // Check folder prerequisites
            if (!Directory.Exists("songs"))
                Directory.CreateDirectory("songs");
            if (!File.Exists("settings.json"))
            {
                var fs = File.Create("settings.json");
                byte[] emptysettings = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Settings()));
                fs.Write(emptysettings, 0, emptysettings.Length);
                fs.Close();
            }
#if DEBUG
            if (!Directory.Exists("songs/mock"))
            {
                Directory.CreateDirectory("songs/mock");
                File.Create("songs/mock/map.json").Close();
                var m = new RizumuMap();
                m.Difficulties.Add(new RizumuDifficulty()
                {
                    Name = "pritty-hard",
                    NotesDown = new List<int>() { 20 },
                    NotesLeft = new List<int>() { 20 },
                    NotesRight = new List<int>() { 20 },
                    NotesUp = new List<int>() { 20 },
                    Offset = 0
                });
                m.Author = "Debug";
                File.WriteAllText("songs/mock/map.json", JsonConvert.SerializeObject(m, Formatting.Indented));
            }
#endif
            Logger.Log("Starting game");
            using (var game = new RizumuGame(""))
            {
                game.Run();
            }

#if DEBUG
            Console.ReadKey();
#endif
        }
    }
}
