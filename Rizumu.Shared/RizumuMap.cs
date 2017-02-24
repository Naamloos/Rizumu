using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Rizumu.Shared
{
    public class RizumuMap
    {
        [JsonProperty("artist")]
        public string Artist;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("creator")]
        public string Creator;

        [JsonProperty("song_path")]
        public string SongPath;

        [JsonProperty("background_path")]
        public string BackgroundPath;

        [JsonProperty("leftnotes")]
        public List<int> LeftNotes;

        [JsonProperty("upnotes")]
        public List<int> UpNotes;

        [JsonProperty("rightnotes")]
        public List<int> RightNotes;

        [JsonProperty("downnotes")]
        public List<int> DownNotes;
    }
}
