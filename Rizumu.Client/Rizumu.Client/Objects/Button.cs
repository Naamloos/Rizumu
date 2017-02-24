using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Client
{
    public class Button
    {
        public event EventHandler<string> ClickEvent;
        public event EventHandler<string> MouseOverEvent;
        public event EventHandler<string> MouseOffEvent;

        public float x;
        public float y;
        public float Width;
        public float Height;
        public TextureColorCombo Idle;
        public TextureColorCombo MouseOver;
        public SoundEffect Click;
        TextureColorCombo Current;
        public bool Visible = false;
        bool mouseOver = false;
        bool click = false;

        public Button(float x, float y, TextureColorCombo Idle, TextureColorCombo MouseOver, bool Visible = true, SoundEffect Click = null, float width = 0, float height = 0)
        {
            this.x = x;
            this.y = y;
            this.Idle = Idle;
            this.MouseOver = MouseOver;
            this.Click = Click;
            Current = this.Idle;
            this.Visible = Visible;

            if (width == 0)
                Width = Idle.Texture.Width;
            else
                Width = width;

            if (height == 0)
                Height = Idle.Texture.Height;
            else
                Height = height;

            ClickEvent += (sender, e) =>
            {
                if (this.Click != null)
                    Click.Play(StaticStuff.Volume, 1f, 1f);
            };

            MouseOverEvent += (sender, e) =>
            {
                Current = this.MouseOver;
                return;
            };

            MouseOffEvent += (sender, e) =>
            {
                Current = this.Idle;
                return;
            };
        }

        public void Update()
        {
            MouseState ms = StaticStuff.mouseState;
            Rectangle location = new Rectangle((int)x, (int)y, (int)Width, (int)Height);
            Rectangle mouse = new Rectangle(ms.X, ms.Y, 1, 1);
            if (mouse.Intersects(location))
            {
                if (!mouseOver)
                    MouseOverEvent(this, null);
                if (StaticStuff.oldMouseState.LeftButton != ButtonState.Pressed && StaticStuff.mouseState.LeftButton == ButtonState.Pressed && click == false)
                {
                    ClickEvent(this, null);
                    StaticStuff.mouseState = new MouseState(0, 0, 0, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released);
                }
                else
                    click = false;
            }
            else if (mouseOver == true)
                MouseOffEvent(this, null);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
                spriteBatch.Draw(Current.Texture, new Rectangle((int)x, (int)y, (int)Width, (int)Height), Current.Color);
        }
    }
}
