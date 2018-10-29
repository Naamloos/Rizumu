using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Engine
{
    internal class AnimationManager
    {
        private static List<Animation> _values = new List<Animation>();
        public static float GetValue(string id)
        {
            if (!_values.Any(x => x.Id == id))
                return 0;
            return _values.First(x => x.Id == id).Y;
        }

        public static void UpdateValues()
        {
            for (int i = 0; i < _values.Count; i++)
            {
                _values[i].X += _values[i].Increment;
                _values[i].Y = 0;
            }
        }
    }

    internal class Animation
    {
        internal string Id = "";
        internal float X = 0;
        internal float Y = 0;
        internal float Increment = 1;
    }
}