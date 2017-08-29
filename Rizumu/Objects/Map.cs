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
        private static Dictionary<string, Song> _songs;
        public static void PreloadSongs()
        {
            _songs = new Dictionary<string, Song>();
            foreach (Map m in GameData.MapManager.Maps)
                _songs.Add(m.MD5, Song.FromUri(m.MD5, new Uri(System.IO.Path.Combine(m.Path, m.FileName), UriKind.Relative)));
        }

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

        [JsonIgnore]
        public string MD5;

        [JsonIgnore]
        public Song Song => _songs[MD5];
    }
}
