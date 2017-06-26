using Newtonsoft.Json;

namespace Rizumu.Objects
{
    public class Options
    {
        [JsonProperty("fullscreen")]
        public bool Fullscreen;

        [JsonProperty("leftkey")]
        public int Left;

        [JsonProperty("upkey")]
        public int Up;

        [JsonProperty("rightkey")]
        public int Right;

        [JsonProperty("downkey")]
        public int Down;

        [JsonProperty("volume")]
        public float Volume;
    }
}
