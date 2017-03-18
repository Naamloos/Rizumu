/*
 * Main program logic. What i do here is check folders, load config, preload songs, etc
 */

using System;
using System.IO;
using System.Windows.Forms;

namespace Rizumu
{
#if WINDOWS || LINUX
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
            if (!File.Exists("settings.ini"))
            {
                File.Create("settings.ini").Close();
                string keybinds =
                    "fullscreen:true" +
                    "\nleft:100" +
                    "\nup:104" +
                    "\nright:102" +
                    "\ndown:98";
                File.WriteAllText("settings.ini", keybinds);
            }

            if (!Directory.Exists("content/songs"))
            {
                Directory.CreateDirectory("content/songs");
            }
            if (!Directory.Exists("content/skins"))
            {
                Directory.CreateDirectory("content/skins");
            }
            if (!Directory.Exists("screenshots"))
            {
                Directory.CreateDirectory("screenshots");
            }
            try
            {
                randomsong();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please add songs to your 'songs' folder!\n\n" + ex.ToString());
                Environment.Exit(0);
            }
            // Preload songs!
            Rizumu.GameResources.songs = Directory.GetDirectories("content/songs");

            // Check settings
            Properties.Settings.Default.Save();
            using (var game = new Game1())
                game.Run();
        }

        public static void randomsong()
        {
            GameResources.songs = System.IO.Directory.GetDirectories("content/songs");
            int songcount = GameResources.songs.Length;
            System.Random rndsng = new System.Random();
            int firstsong = rndsng.Next(0, songcount);
            string name = GameResources.songs[firstsong];
            GameResources.startint = firstsong;
            GameResources.selected = name;
            Music.play(name, 0);
        }
    }
#endif
}
