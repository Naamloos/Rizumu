using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.GameLogic.Entities
{
    public class RizumuMap
    {
        [JsonProperty("difficulty")]
        public int Difficulty = 1;

        [JsonProperty("leftnotes")]
        public List<int> NotesLeft = new List<int>();

        [JsonProperty("upnotes")]
        public List<int> NotesUp = new List<int>();

        [JsonProperty("rightnotes")]
        public List<int> NotesRight = new List<int>();

        [JsonProperty("downnotes")]
        public List<int> NotesDown = new List<int>();

        [JsonProperty("songoffset")]
        public int Offset = 0;
    }

    public class RizumuMetadata
    {
        [JsonProperty("name")]
        public string Name = "";

        [JsonProperty("artist")]
        public string Artist = "";

        [JsonProperty("author")]
        public string Author = "";

        [JsonProperty("songpath")]
        public string SongPath = "";

        [JsonProperty("previewpath")]
        public string PreviewPath = "";

        [JsonProperty("backgroundpath")]
        public string BackgroundPath = "";

        [JsonProperty("thumbnailpath")]
        public string ThumbnailPath = "";
    }
}
