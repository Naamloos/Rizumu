using Rizumu.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Helpers
{
    class ShittyOsuConverter
    {
        public static Map FromBeatmap(string path)
        {
            Map m = new Map();
            m.Offset = -100;
            string Beatmap = File.ReadAllText(path);

            string notes = Beatmap.Substring(Beatmap.IndexOf("[HitObjects]") + 12);
            string[] ns = notes.Replace("\r", "").Split('\n');
            foreach (string n in ns)
            {
                if (!string.IsNullOrEmpty(n))
                {
                    var ON = new OsuNote(n);
                    if (ON.X > 256)
                    {
                        if (ON.Y > 172)
                            m.NotesRight.Add(ON.RizumuTime);
                        else
                            m.NotesDown.Add(ON.RizumuTime);
                    }
                    else
                    {
                        if (ON.Y > 172)
                            m.NotesLeft.Add(ON.RizumuTime);
                        else
                            m.NotesUp.Add(ON.RizumuTime);
                    }
                }
            }
            return m;
        }
    }

    public class OsuNote
    {
        public readonly int X;
        public readonly int Y;
        private int Time;
        public int RizumuTime => Time / 2;

        public OsuNote(string Input)
        {
            string[] N = Input.Replace('\r', ' ').Split(',');
            X = int.Parse(N[0]);
            Y = int.Parse(N[1]);
            Time = int.Parse(N[2]);
        }
    }
}
