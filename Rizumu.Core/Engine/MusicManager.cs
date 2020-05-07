using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Engine
{
    // This class handles music playback.
    public class MusicManager
    {
        public static Song Song { get { return _song; } }
        private static Song _song;

        public static void Play(Song song)
        {
            MediaPlayer.Volume = 0.2f;
            if (MediaPlayer.State == MediaState.Playing)
                MediaPlayer.Stop();
            _song = song;
            MediaPlayer.Play(_song);
        }

        public static void CheckMapComplete()
        {
            if(MediaPlayer.State == MediaState.Stopped)
            {
                Play(_song);
            }
        }
    }
}
