using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Rizumu.Objects
{
    // Default values == Default options
    public class Options
    {
        [JsonProperty("fullscreen")]
        public bool Fullscreen = false;

        [JsonProperty("leftkey")]
        public Keys Left = (Keys) 100;

        [JsonProperty("upkey")]
        public Keys Up = (Keys) 104;

        [JsonProperty("rightkey")]
        public Keys Right = (Keys) 102;

        [JsonProperty("downkey")]
        public Keys Down = (Keys) 98;

        [JsonProperty("volume")]
        public float Volume = 0.2f;

        [JsonProperty("skin")]
        public string SkinName = "default";

        [JsonProperty("player")]
        public string Player = "user";
    }
}
