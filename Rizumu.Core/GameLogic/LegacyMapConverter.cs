using Newtonsoft.Json.Linq;
using Rizumu.GameLogic.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Rizumu.GameLogic
{
	public class LegacyMapConverter
	{
		// Legacy maps only have one baked in difficulty level.
		public static bool CheckMapLegacy(JObject loaded) => loaded["difficulties"] == null;

		public static RizumuMap ConvertLegacy(LegacyRizumuMap legacy)
		{
			string artist = "";
			string songname = "";
			if (legacy.Name.Contains('-'))
			{
				var s = new List<string>();
				s.AddRange(legacy.Name.Split('-'));
				artist = s[0];
				s.RemoveAt(0);
				songname = string.Join("-", s);
				if (songname.StartsWith(" "))
					songname = songname.Substring(1);
				if (artist.EndsWith(" "))
					artist = artist.Remove(artist.Length - 1);
			}
			else
			{
				songname = legacy.Name;
			}

			var converted = new RizumuMap
			{
				ArtistName = artist,
				Author = legacy.Creator,
				BackgroundFile = legacy.BackgroundFile,
				Filename = legacy.FileName,
				SongName = songname,
				ThumbnailFile = legacy.BackgroundFile,
			};

			var diff = new RizumuDifficulty()
			{
				NotesDown = legacy.NotesDown,
				NotesLeft = legacy.NotesLeft,
				NotesRight = legacy.NotesRight,
				NotesUp = legacy.NotesUp,
				Name = "Legacy",
				Offset = legacy.Offset + 50
			};

			converted.Difficulties.Add(diff);

			Logger.Log($"Converted legacy map: {songname} by {artist}");
			return converted;
		}
	}
}
