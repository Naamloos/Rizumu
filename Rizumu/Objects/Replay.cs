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
        public List<int> PressesLeft = new List<int>();

        [JsonProperty("uppresses")]
        public List<int> PressesUp = new List<int>();

        [JsonProperty("rightpresses")]
        public List<int> PressesRight = new List<int>();

        [JsonProperty("downpresses")]
        public List<int> PressesDown = new List<int>();

        [JsonProperty("player")]
        public string Player;
    }
}
