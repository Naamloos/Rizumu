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
        public static Song Song { get; private set; }

        public static void Play(Song song)
        {
            MediaPlayer.Volume = 0.2f;
            if (MediaPlayer.State == MediaState.Playing)
                MediaPlayer.Stop();
            Song = song;
            MediaPlayer.Play(Song);
        }

        public static void CheckMapComplete()
        {
            if(MediaPlayer.State == MediaState.Stopped)
            {
                Play(Song);
            }
        }
    }
}
