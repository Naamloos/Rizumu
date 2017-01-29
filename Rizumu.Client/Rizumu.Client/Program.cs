using System;

namespace Rizumu.Client
{
    public static class Program
    {
        /*
         * Here we start the Gamu thanks
         */
        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
                game.Run();
        }
    }
}
