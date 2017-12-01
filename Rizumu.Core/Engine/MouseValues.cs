using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Engine
{
    internal class MouseValues
    {
        public bool Clicked = false;
        public int X = 0;
        public int Y = 0;
        public Rectangle Hitbox { get { return new Rectangle(X, Y, 1, 1); } }
        private bool _previousclick = false;

        public void Update(MouseState ms, int SW, int SH)
        {
            if (ms == null)
                return;

            this.X = (int)((((float)ms.X) / (float)SW) * 1920);
            this.Y = (int)((((float)ms.Y + 5) / (float)SH) * 1080);

            if (!_previousclick && ms.LeftButton == ButtonState.Pressed)
                Clicked = true;
            else
                Clicked = false;

            _previousclick = ms.LeftButton == ButtonState.Pressed;
        }
    }
}
