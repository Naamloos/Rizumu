using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RizumuMapEditor
{
    public enum MusicPlayerAction
    {
        Play,
        Pause,
        SetTime
    }

    public class MusicEventArgs : EventArgs
    {
        public MusicPlayerAction Action;

        public MusicEventArgs(MusicPlayerAction action)
        {
            Action = action;
        }
    }

    public class PlaybarEventArgs : EventArgs
    {
        public int Progress;

        public PlaybarEventArgs(int progress)
        {
            Progress = progress;
        }
    }
}
