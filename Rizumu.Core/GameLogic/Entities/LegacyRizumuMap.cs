using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.GameLogic.Entities
{
	public class LegacyRizumuMap
	{
		[JsonProperty("name")]
		public string Name = "empty";

		[JsonProperty("desc")]
		public string Description = "empty";

		[JsonProperty("creator")]
		public string Creator = "empty";

		[JsonProperty("songfile")]
		public string FileName = "empty";

		[JsonProperty("backgroundfile")]
		public string BackgroundFile = "empty";

		[JsonProperty("leftnotes")]
		public List<int> NotesLeft = new List<int>();

		[JsonProperty("upnotes")]
		public List<int> NotesUp = new List<int>();

		[JsonProperty("rightnotes")]
		public List<int> NotesRight = new List<int>();

		[JsonProperty("downnotes")]
		public List<int> NotesDown = new List<int>();

		[JsonProperty("leftslides")]
		public List<int> SlidesLeft = new List<int>();

		[JsonProperty("upslides")]
		public List<int> SlidesUp = new List<int>();

		[JsonProperty("rightslides")]
		public List<int> SlidesRight = new List<int>();

		[JsonProperty("downslides")]
		public List<int> SlidesDown = new List<int>();

		[JsonProperty("offset")]
		public int Offset = 0;
	}
}
