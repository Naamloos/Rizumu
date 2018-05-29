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
		public int ScrollDelta { get; private set; }

		private static int OldScroll = 0;

        public void Update(MouseState ms, int SW, int SH, bool focused)
        {
            if (ms == null)
                return;
            float xscale = 1920f / SW;
            float yscale = 1080f / SH;
            this.X = (int)(ms.X * xscale);
            this.Y = (int)(ms.Y * yscale);

            if (!_previousclick && ms.LeftButton == ButtonState.Pressed && focused)
                Clicked = true;
            else
                Clicked = false;

            _previousclick = ms.LeftButton == ButtonState.Pressed;
			ScrollDelta = ms.ScrollWheelValue - OldScroll;
			OldScroll = ms.ScrollWheelValue;
        }
    }
}
