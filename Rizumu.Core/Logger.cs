using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu
{
	public class Logger
	{
		private static FileStream fs = null;
		private static StreamWriter sw = null;

		public static void EnableLoggerDump()
		{
			var now = DateTime.UtcNow;
			fs = File.Create($"logs/{now.DayOfYear}-{now.Year}-{new Random().Next()}.txt");
			sw = new StreamWriter(fs);
		}

		public static void UnloadAndDispose()
		{
			sw.Close();
			fs.Close();
			sw.Dispose();
			fs.Dispose();
		}

		public static void Log(string message)
		{
			Console.Write("[");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(DateTime.UtcNow.ToString());
			Console.ResetColor();
			Console.Write("]");
			Console.WriteLine($" {message}");

			if(sw != null)
			{
				sw.WriteLine($"[{DateTime.UtcNow.ToString()}] {message}");
			}
		}
	}
}
