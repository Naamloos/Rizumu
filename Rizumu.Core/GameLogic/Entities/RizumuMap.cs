using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.GameLogic.Entities
{
    public class RizumuMap
    {
		[JsonProperty("author")]
		public string Author = "Guest";

		[JsonProperty("difficulties")]
		public List<RizumuDifficulty> Difficulties = new List<RizumuDifficulty>();

		[JsonProperty("filename")]
		public string Filename = "song.mp3";

		[JsonProperty("thumbnail")]
		public string ThumbnailFile = "thumb.png";

		[JsonProperty("background")]
		public string BackgroundFile = "bg.png";

		[JsonProperty("songname")]
		public string SongName = "songo";

		[JsonProperty("artistname")]
		public string ArtistName = "person";

		[JsonIgnore]
		public string Path = "";

		[JsonIgnore]
		public Texture2D Thumbnail;

		[JsonIgnore]
		public Texture2D Background;

		[JsonIgnore]
		public Song MapSong;

		[JsonIgnore]
		public bool Enabled = true;

		public void LoadContent(GraphicsDevice gr)
		{
			if(File.Exists(System.IO.Path.Combine(Path, ThumbnailFile)))
				Thumbnail = Texture2D.FromStream(gr, new FileStream(System.IO.Path.Combine(Path, ThumbnailFile), FileMode.Open));
			if (File.Exists(System.IO.Path.Combine(Path, BackgroundFile)))
				Background = Texture2D.FromStream(gr, new FileStream(System.IO.Path.Combine(Path, BackgroundFile), FileMode.Open));
			Console.WriteLine(System.IO.Path.Combine(Path, Filename));
			if (File.Exists(System.IO.Path.Combine(Path, Filename)))
			{
				MapSong = Song.FromUri(System.IO.Path.Combine(Path, Filename), new Uri(System.IO.Path.Combine(Path, Filename), UriKind.Relative));
			}
			else
			{
				Enabled = false;
			}

			Console.WriteLine($"Loaded map with data:\n{SongName} - {ArtistName}\nPath {Path}\nDiffs: {string.Join(", ", Difficulties.Select(x => x.Name))}" +
				$"\n{(Enabled ? "and it's enabled" : "but it wasn't enabled because no songfile was found!!")}\n");
		}
    }

	public class RizumuDifficulty
	{
		[JsonProperty("name")]
		public string Name = "ez-pz";

		[JsonProperty("leftnotes")]
		public List<int> NotesLeft = new List<int>();

		[JsonProperty("upnotes")]
		public List<int> NotesUp = new List<int>();

		[JsonProperty("rightnotes")]
		public List<int> NotesRight = new List<int>();

		[JsonProperty("downnotes")]
		public List<int> NotesDown = new List<int>();

		[JsonProperty("songoffset")]
		public int Offset = 0;
	}
}
