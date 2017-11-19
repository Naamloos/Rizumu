using Rizumu.Core;
using System;

namespace Rizumu.GL
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new RizumuGame("OpenGL"))
                game.Run();
        }
    }
}
