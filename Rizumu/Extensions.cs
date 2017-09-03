using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu
{
    public static class Extensions
    {
        static List<Keys> Held = new List<Keys>();
        public static bool IsKeyPress(this KeyboardState ks, Keys key)
        {
            if (ks.IsKeyDown(key))
            {
                if (Held.Contains(key))
                    return false;
                else
                {
                    Held.Add(key);
                    return true;
                }
            }
            if (Held.Contains(key))
                Held.Remove(key);
            return false;
        }
    }
}
