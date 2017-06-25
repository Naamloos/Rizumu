using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Xna.Framework.Input;

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
    }
}
