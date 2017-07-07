using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Objects
{
    // Default values == Default options
    class Options
    {
        [JsonProperty("fullscreen")]
        public bool Fullscreen = false;

        [JsonProperty("leftkey")]
        public int Left;

        [JsonProperty("upkey")]
        public int Up;

        [JsonProperty("rightkey")]
        public int Right;

        [JsonProperty("downkey")]
        public int Down;

        [JsonProperty("volume")]
        public float Volume = 0.2f;

        [JsonProperty("skin")]
        public string SkinName = "default";
    }
}
