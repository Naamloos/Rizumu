using Microsoft.Xna.Framework.Graphics;
using Rizumu.Engine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

	Following is a little insight on how I plan to place the notes.
	I've decided to use the classic + positioning anyway. This might change later on. idk.

	anyway, the game always renders at a 1920x1080 aspect ratio 
	(and stretched to fit the window. I know, bad. bla bla.)

	Left / Right:
	X POS
	1920 / 2 = distance from border to center
	subtract note width from the current position to render.
	This will start the note just outside screen and end it just in the center
	Y POS
	1080 / 2
	subtract note width / 2 to center it nicely

	Up / Down:
	Same for Left / Right, but swap the X and Y axis.
	Also swap 1920 and 1080

	Welp, now let's move on to code 
	(This is also just a reminder for myself so dw if it makes no sense or w/e)

	*/

namespace Rizumu.GameLogic.Entities
{
    public abstract class BaseRizumuNote
    {
        public int Time = 0;
        public bool Hit = false;
        public bool Miss = false;
        public int Position = 0;
        public Sprite Texture;
        public int TravelTime = 150;
        public int PopupTime => Time - TravelTime;

        public BaseRizumuNote(int time, string texture, int traveltime)
        {
            this.Texture = texture;
            this.Time = time;
        }

        public abstract void Render(SpriteBatch sb);

        public abstract void Update(ref bool keypress, int currenttime);
    }

    public class RizumuLeftNote : BaseRizumuNote
    {
        public RizumuLeftNote(int time, string texture, int traveltime) : base(time, texture, traveltime)
        {
            this.Texture.X = 0;
            this.Texture.Y = (1080 / 2) - (this.Texture.Hitbox.Height / 2);
        }

        public override void Render(SpriteBatch sb)
        {
            if (!(this.Texture.X > ((1920 / 2) - (this.Texture.Hitbox.Width / 2))) && this.Hit == false)
                this.Texture.Draw(sb);
        }

        public override void Update(ref bool keypress, int currenttime)
        {
            int pos = currenttime - (this.Time - ((1920 / 2))) + this.Texture.Hitbox.Width;
            this.Texture.X = pos;

            if (pos > ((1920 / 2) - (this.Texture.Hitbox.Width * 2.5)) && pos < (1920 / 2))
            {
                if (keypress && !this.Hit && !this.Miss)
                {
                    this.Hit = true;
                    RizumuGame.Hit.Play();
                    keypress = false;
                }
            }
            else if (pos > (1920 / 2))
            {
                this.Miss = true;
            }
        }
    }

    public class RizumuUpNote : BaseRizumuNote
    {
        public RizumuUpNote(int time, string texture, int traveltime) : base(time, texture, traveltime)
        {
            this.Texture.X = (1920 / 2) - (this.Texture.Hitbox.Width / 2);
            this.Texture.Y = 0;
        }

        public override void Render(SpriteBatch sb)
        {
            if (!(this.Texture.Y > ((1080 / 2) - (this.Texture.Hitbox.Height / 2))) && this.Hit == false)
                this.Texture.Draw(sb);
        }

        public override void Update(ref bool keypress, int currenttime)
        {
            int pos = currenttime - (this.Time - ((1080 / 2))) + this.Texture.Hitbox.Height;
            this.Texture.Y = pos;

            if (pos > ((1080 / 2) - (this.Texture.Hitbox.Height * 2.5)) && pos < (1080 / 2))
            {
                if (keypress && !this.Hit && !this.Miss)
                {
                    this.Hit = true;
                    RizumuGame.Hit.Play();
                    keypress = false;
                }
            }
            else if (pos > (1080 / 2))
            {
                this.Miss = true;
            }
        }
    }

    public class RizumuRightNote : BaseRizumuNote
    {
        public RizumuRightNote(int time, string texture, int traveltime) : base(time, texture, traveltime)
        {
            this.Texture.X = 1920;
            this.Texture.Y = (1080 / 2) - (this.Texture.Hitbox.Height / 2);
        }

        public override void Render(SpriteBatch sb)
        {
            if (!(this.Texture.X < ((1920 / 2) - (this.Texture.Hitbox.Width / 2))) && this.Hit == false)
                this.Texture.Draw(sb);
        }

        public override void Update(ref bool keypress, int currenttime)
        {
            int pos = currenttime - (this.Time - ((1920 / 2))) + this.Texture.Hitbox.Width;
            this.Texture.X = (1920 - this.Texture.Hitbox.Width) - pos;

            if (pos > ((1920 / 2) - (this.Texture.Hitbox.Width * 2.5)) && pos < (1920 / 2))
            {
                if (keypress && !this.Hit && !this.Miss)
                {
                    this.Hit = true;
                    RizumuGame.Hit.Play();
                    keypress = false;
                }
            }
            else if (pos > (1920 / 2))
            {
                this.Miss = true;
            }
        }
    }

    public class RizumuDownNote : BaseRizumuNote
    {
        public RizumuDownNote(int time, string texture, int traveltime) : base(time, texture, traveltime)
        {
            this.Texture.X = (1920 / 2) - (this.Texture.Hitbox.Width / 2);
            this.Texture.Y = 1080;
        }

        public override void Render(SpriteBatch sb)
        {
            if (!(this.Texture.Y < ((1080 / 2) - (this.Texture.Hitbox.Height / 2))) && this.Hit == false)
                this.Texture.Draw(sb);
        }

        public override void Update(ref bool keypress, int currenttime)
        {
            int pos = currenttime - (this.Time - ((1080 / 2))) + this.Texture.Hitbox.Height;
            this.Texture.Y = (1080 - this.Texture.Hitbox.Height) - pos;

            if (pos > ((1080 / 2) - (this.Texture.Hitbox.Height * 2.5)) && pos < (1080 / 2))
            {
                if (keypress && !this.Hit && !this.Miss)
                {
                    this.Hit = true;
                    RizumuGame.Hit.Play();
                    keypress = false;
                }
            }
            else if (pos > (1080 / 2))
            {
                this.Miss = true;
            }
        }
    }
}
