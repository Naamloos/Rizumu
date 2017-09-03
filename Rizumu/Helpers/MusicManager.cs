using Microsoft.Xna.Framework.Media;
using Rizumu.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Helpers
{
    class MusicManager
    {
        public Song Current;
        public Map Playing;
        public bool IsPlaying => MediaPlayer.State == MediaState.Playing;
        public int songindex = 0;
        public MusicManager()
        {
            MediaPlayer.Volume = GameData.Instance.Options.Volume;
            MediaPlayer.IsRepeating = false;
            MediaPlayer.IsShuffled = false;
        }

        public void Pause()
        {
            if (MediaPlayer.State == MediaState.Playing)
                MediaPlayer.Pause();
        }

        public void Stop()
        {
            Current?.Dispose();
            Current = Playing.Song;
            MediaPlayer.Pause();
        }

        public void UnPause()
        {
            if (MediaPlayer.State == MediaState.Paused)
                MediaPlayer.Resume();
        }

        public void Change(string md5)
        {
            Change(GameData.MapManager.Maps.Find(x => x.MD5 == md5));
        }

        public void Change(Map m)
        {
            if (Playing != m)
            {
                MediaPlayer.Stop();
                Current?.Dispose();
                Playing = m;
                Current = Playing.Song;
                MediaPlayer.Play(Current);
            }
        }

        public void Restart()
        {
            UnPause();
            Stop();
            MediaPlayer.Play(Current);
        }

        public void Restart(TimeSpan ts)
        {
            UnPause();
            Stop();
            MediaPlayer.Play(Current, ts);
        }

        public void KeepPlaying()
        {
            if (!IsPlaying)
                Change(Playing);
        }
    }
}
