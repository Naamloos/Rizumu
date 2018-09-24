using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Xna.Framework.Input;

namespace Rizumu.GameLogic.Entities
{
    public class Settings
    {
        [JsonProperty("leftkey")]
        public Keys LeftKey = Keys.NumPad4;

        [JsonProperty("rightkey")]
        public Keys RightKey = Keys.NumPad6;

        [JsonProperty("upkey")]
        public Keys UpKey = Keys.NumPad8;

        [JsonProperty("downkey")]
        public Keys DownKey = Keys.NumPad2;
    }
}
