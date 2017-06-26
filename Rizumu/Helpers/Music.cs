/*
 * Just the music player logic.
 * The reason i dont use Monogame itself for music playback is mainly loading mp3's
 * 
 * Though I think this should be possible with MonoGame.
 * If you feel like you found a way w/o NAudio, feel free to PR.
 * NAudio breaks cross-platform compatibility (mono, wine, etc)
 */

using Microsoft.Xna.Framework.Media;
using System;
using System.IO;
using System.Threading;

namespace Rizumu
{
    class Music
    {
        public static Song song = null;

        public static void play(string mp3, long position)
        {
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Stop();
            }
            if (song != null && !song.IsDisposed)
            {
                song.Dispose();
            }

            song = Song.FromUri(mp3, new Uri(Path.Combine(mp3, GameResources.Maps[mp3].FileName), UriKind.Relative));
            MediaPlayer.IsRepeating = true;
            if (GameResources.Optionss != null)
                MediaPlayer.Volume = GameResources.Optionss.Volume;
            else
                MediaPlayer.Volume = 0.3f;
            MediaPlayer.Play(song);
        }

        public static void pause()
        {
            MediaPlayer.Pause();
        }
        public static void resume()
        {
            MediaPlayer.Resume();
        }
    }
}
