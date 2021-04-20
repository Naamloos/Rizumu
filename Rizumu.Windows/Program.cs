using Rizumu.Shared;
using System;

namespace Rizumu.Windows
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
