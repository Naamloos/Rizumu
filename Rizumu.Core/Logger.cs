using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu
{
	public class Logger
	{
		public static void Log(string message)
		{
			Console.Write("[");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(DateTime.UtcNow.ToString());
			Console.ResetColor();
			Console.Write("]");
			Console.WriteLine($" {message}");
		}
	}
}
