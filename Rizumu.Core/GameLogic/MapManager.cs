using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
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
					var m = JsonConvert.DeserializeObject<RizumuMap>(File.ReadAllText(Path.Combine(dir, "map.json")));
					m.Path = dir;
					m.LoadContent(gr);
					LoadedMaps.Add(i, m);
				}
				i++;
			}
		}
	}
}
