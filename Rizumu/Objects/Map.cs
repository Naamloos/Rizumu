using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Objects
{
    class Map
    {
        [JsonProperty("name")]
        public string Name = "empty";

        [JsonProperty("desc")]
        public string Description = "empty";

        [JsonProperty("creator")]
        public string Creator = "empty";

        [JsonProperty("songfile")]
        public string FileName = "empty";

        [JsonProperty("backgroundfile")]
        public string BackgroundFile = "empty";

        [JsonProperty("leftnotes")]
        public List<int> NotesLeft = new List<int>();

        [JsonProperty("upnotes")]
        public List<int> NotesUp = new List<int>();

        [JsonProperty("rightnotes")]
        public List<int> NotesRight = new List<int>();

        [JsonProperty("downnotes")]
        public List<int> NotesDown = new List<int>();

        [JsonProperty("offset")]
        public int Offset = 0;

        [JsonIgnore]
        public string Path;

        [JsonIgnore]
        public Texture2D Background;

        [JsonIgnore]
        public string MD5;

        [JsonIgnore]
        public Song Song => Song.FromUri(MD5,
                    new Uri(System.IO.Path.Combine(Path, FileName), UriKind.Relative));
    }
}
