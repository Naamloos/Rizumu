using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Core.Engine
{
    internal class AnimationManager
    {
        private static Dictionary<string, float> _values = new Dictionary<string, float>();
        public static float GetValue(string id)
        {
            if (!_values.Keys.Contains(id))
                return 0;
            return _values[id];
        }

        public
    }
}
