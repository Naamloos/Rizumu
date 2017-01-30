using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Rizumu.Client
{
    public class RizumuMap
    {
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("description")]
        public string Description;

        [JsonProperty("filename")]
        public string Filename;

        [JsonProperty("backgroundname")]
        public string BackgroundName;

        [JsonProperty("leftnotes")]
        public List<int> leftnotes;

        [JsonProperty("upnotes")]
        public List<int> upnotes;

        [JsonProperty("rightnotes")]
        public List<int> rightnotes;

        [JsonProperty("downnotes")]
        public List<int> downnotes;
    }
}
