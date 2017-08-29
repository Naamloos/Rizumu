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
        public MusicManager()
        {
            int songindex = new Random().Next(0, GameData.MapManager.Maps.Count - 1);
            Current = Song.FromUri(Path.Combine(GameData.MapManager.Maps[songindex].Path, GameData.MapManager.Maps[songindex].FileName),
                new Uri(Path.Combine(GameData.MapManager.Maps[songindex].Path, GameData.MapManager.Maps[songindex].FileName), UriKind.Relative));
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = GameData.Instance.Options.Volume;
            MediaPlayer.Play(Current);
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

        public void Change(Map m)
        {
            if (Playing != m)
            {
                Current = Song.FromUri(m.MD5,
                    new Uri(Path.Combine(m.Path, m.FileName), UriKind.Relative));
                MediaPlayer.Stop();
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
    }
}
