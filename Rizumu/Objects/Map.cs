using Microsoft.Xna.Framework.Graphics;
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
        public string Name;

        [JsonProperty("desc")]
        public string Description;

        [JsonProperty("creator")]
        public string Creator;

        [JsonProperty("songfile")]
        public string FileName;

        [JsonProperty("backgroundfile")]
        public string BackgroundFile;

        [JsonProperty("leftnotes")]
        public int[] NotesLeft;

        [JsonProperty("upnotes")]
        public int[] NotesUp;

        [JsonProperty("rightnotes")]
        public int[] NotesRight;

        [JsonProperty("downnotes")]
        public int[] NotesDown;

        [JsonProperty("offset")]
        public int Offset = 0;

        [JsonIgnore]
        public string Path;

        [JsonIgnore]
        public Texture2D Background;
    }
}
