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
        private static string filename = "";

        public static void EnableLoggerDump()
        {
            if (!Directory.Exists("logs"))
                Directory.CreateDirectory("logs");
            var now = DateTime.UtcNow;
            filename = $"{now.DayOfYear}-{now.Year}-{new Random().Next()}.txt";
            fs = File.Create($"logs/{filename}");
            sw = new StreamWriter(fs);
        }

        public static void UnloadAndDispose()
        {
            sw.Close();
            fs.Close();
            sw.Dispose();
            fs.Dispose();
            sw = null;
            fs = null;
            Log($"Dumped log to {filename}");
        }

        public static void Log(string message)
        {
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(DateTime.UtcNow.ToString());
            Console.ResetColor();
            Console.Write("]");
            Console.WriteLine($" {message}");

            if (sw != null)
            {
                sw.WriteLine($"[{DateTime.UtcNow.ToString()}] {message}");
            }
        }
    }
}
