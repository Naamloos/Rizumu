/*
 * Just the music player logic.
 * The reason i dont use Monogame itself for music playback is mainly loading mp3's
 * 
 * Though I think this should be possible with MonoGame.
 * If you feel like you found a way w/o NAudio, feel free to PR.
 * NAudio breaks cross-platform compatibility (mono, wine, etc)
 */

using NAudio.Wave;
using System;
using System.Threading;

namespace Rizumu
{
    class Music
    {
        public static string oldsong;
        public static WaveOutEvent player;
        public static WaveStream mainOutputStream;
        public static long oldpos = 0;
        public static Thread beatthread;
        public static bool beat = false;

        public static void play(string mp3, long position)
        {
            oldsong = mp3;
            mainOutputStream = new Mp3FileReader(mp3 + "/song.mp3");
            WaveChannel32 volumeStream = new WaveChannel32(mainOutputStream);
            mainOutputStream.Position = position;
            volumeStream.Volume = 0.20f;

            try
            {
                player.Stop();
                player.Dispose();
            }
            catch (Exception)
            {
            }

            player = new WaveOutEvent();

            player.Init(volumeStream);

            player.Play();
        }

        public static void pause()
        {
            player.Pause();
        }
        public static void resume()
        {
            player.Play();
        }
    }
}
