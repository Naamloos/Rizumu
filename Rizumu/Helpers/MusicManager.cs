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
            MediaPlayer.Stop();
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
                Current = m.Song;
                Playing = m;
                MediaPlayer.Play(Current);
            }
        }

        public void Restart()
        {
            UnPause();
            MediaPlayer.Stop();
            MediaPlayer.Play(Current);
        }

        public void KeepPlaying()
        {
            if (!IsPlaying)
                Change(Playing);
        }
    }
}
