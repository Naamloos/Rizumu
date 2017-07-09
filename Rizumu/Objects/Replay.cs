using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Objects
{
    class Replay
    {
        [JsonProperty("md5")]
        public string Md5;

        [JsonProperty("leftpresses")]
        public int[] PressesLeft;

        [JsonProperty("uppresses")]
        public int[] PressesUp;

        [JsonProperty("rightpresses")]
        public int[] PressesRight;

        [JsonProperty("downpresses")]
        public int[] PressesDown;

        [JsonProperty("player")]
        public string Player;
    }
}
