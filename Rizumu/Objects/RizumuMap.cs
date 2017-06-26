using Newtonsoft.Json;

namespace Rizumu.Objects
{
    public class RizumuMap
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
        public int[] LeftNotes;

        [JsonProperty("upnotes")]
        public int[] UpNotes;

        [JsonProperty("rightnotes")]
        public int[] RightNotes;

        [JsonProperty("downnotes")]
        public int[] DownNotes;
    }
}
