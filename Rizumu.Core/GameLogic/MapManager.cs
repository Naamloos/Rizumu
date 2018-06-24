using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rizumu.GameLogic.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.GameLogic
{
	public class MapManager
	{
		public static Dictionary<int, RizumuMap> LoadedMaps = new Dictionary<int, RizumuMap>();

		public static void LoadMaps(GraphicsDevice gr)
		{
			int i = 1;
			foreach (var dir in Directory.GetDirectories("songs"))
			{
				if (File.Exists(Path.Combine(dir, "map.json")))
				{
					string map = File.ReadAllText(Path.Combine(dir, "map.json"));

					// Convert if legacy!!
					if (LegacyMapConverter.CheckMapLegacy(JObject.Parse(map)))
					{
						var lgc = JsonConvert.DeserializeObject<LegacyRizumuMap>(map);
						var mdr = LegacyMapConverter.ConvertLegacy(lgc);
						File.WriteAllText(Path.Combine(dir, "map.json"), JsonConvert.SerializeObject(mdr));
						map = File.ReadAllText(Path.Combine(dir, "map.json"));
					}

					var m = JsonConvert.DeserializeObject<RizumuMap>(map);
					m.Path = dir;
					m.LoadContent(gr);
					LoadedMaps.Add(i, m);
				}
				i++;
			}
		}
	}
}
