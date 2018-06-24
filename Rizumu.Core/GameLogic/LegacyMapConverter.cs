using Newtonsoft.Json.Linq;
using Rizumu.GameLogic.Entities;
using System.Linq;

namespace Rizumu.GameLogic
{
	public class LegacyMapConverter
	{
		// Legacy maps only have one baked in difficulty level.
		public static bool CheckMapLegacy(JObject loaded) => loaded["difficulties"].Count() > 0;

		public RizumuMap ConvertLegacy(LegacyRizumuMap legacy)
		{
			var converted = new RizumuMap
			{
				ArtistName = "",
				Author = legacy.Creator,
				BackgroundFile = legacy.BackgroundFile,
				Filename = legacy.FileName,
				SongName = legacy.Name,
				ThumbnailFile = legacy.BackgroundFile,
			};

			var diff = new RizumuDifficulty()
			{
				NotesDown = legacy.NotesDown,
				NotesLeft = legacy.NotesLeft,
				NotesRight = legacy.NotesRight,
				NotesUp = legacy.NotesUp,
				Name = "Normal",
				Offset = legacy.Offset
			};

			converted.Difficulties.Add(diff);

			return converted;
		}
	}
}
