using Rizumu.Shared;
using System;

namespace Rizumu.CrossPlatform
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new RizumuGame())
                game.Run();
        }
    }
}
