using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Core.Engine
{
    internal class MouseValues
    {
        public bool Clicked = false;
        public int X;
        public int Y;
        public Rectangle Hitbox { get { return new Rectangle(X, Y, 1, 1); } }
        private bool _previousclick;

        public void Update(MouseState ms)
        {
            this.X = ms.X;
            this.Y = ms.Y;

            if (!_previousclick && ms.LeftButton == ButtonState.Pressed)
                Clicked = true;
            else
                Clicked = false;

            _previousclick = ms.LeftButton == ButtonState.Pressed;
        }
    }
}
