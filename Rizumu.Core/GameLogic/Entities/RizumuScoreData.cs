using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.GameLogic.Entities
{
    public class RizumuScoreData
    {
        public RizumuMap MapData;

        public int LeftHits = 0;
        public int RightHits = 0;
        public int UpHits = 0;
        public int DownHits = 0;

        public int LeftMisses = 0;
        public int RightMisses = 0;
        public int UpMisses = 0;
        public int DownMisses = 0;

        public UserData Player;
    }
}
