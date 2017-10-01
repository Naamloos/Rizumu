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

        [JsonProperty("leftslides")]
        public List<int> SlidesLeft = new List<int>();

        [JsonProperty("upslides")]
        public List<int> SlidesUp = new List<int>();

        [JsonProperty("rightslides")]
        public List<int> SlidesRight = new List<int>();

        [JsonProperty("downslides")]
        public List<int> SlidesDown = new List<int>();

        [JsonProperty("offset")]
        public int Offset = 0;

        [JsonProperty("events")]
        public List<MapEvent> Events = new List<MapEvent>();

        [JsonIgnore]
        public string Path;

        [JsonIgnore]
        public Texture2D Background;

        [JsonIgnore]
        public string MD5;

        [JsonIgnore]
        public Song Song => SSong();

        public Song SSong()
        {
            var uri = new Uri(System.IO.Path.Combine(Path, FileName), UriKind.Relative);
            var song = Microsoft.Xna.Framework.Media.Song.FromUri(System.IO.Path.Combine(Path, FileName), uri);
            var songType = song.GetType();

#if !WINDOWS
            Game1.RegisterAndroidUri.Invoke(null, new EventArgs.RegisterAndroidUriArgs()
            {
                Path = System.IO.Path.Combine(Path, FileName),
                Song = song,
                SongType = songType
            });
#endif
            return song;
        }
    }

    class MapEvent
    {
        [JsonProperty("filename")]
        public string filename = "file.png";

        [JsonProperty("states")]
        public List<EventState> States;
    }

    class EventState
    {
        [JsonProperty("time")]
        public int Time = 0;

        [JsonProperty("x")]
        public int X = 0;

        [JsonProperty("y")]
        public int Y = 0;

        [JsonProperty("scale")]
        public float Scale = 1f;

        [JsonProperty("rotation")]
        public int Rotation = 0;

        [JsonProperty("movetowards")]
        public bool MoveTowards = false;

        [JsonProperty("rotatetowards")]
        public bool RotateTowards = false;
    }
}
