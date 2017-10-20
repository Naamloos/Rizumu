using Microsoft.Xna.Framework.Graphics;
using Rizumu.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Rizumu.GuiObjects
{
    class Button
    {
        public Sprite StaticSprite;
        public Sprite HoverSprite;
        public Text Text;
        public bool MouseIsOver;
        public event EventHandler<ButtonEventArgs> OnClick;
        public event EventHandler<ButtonEventArgs> OnMouseEnter;

        public Button(SpriteBatch spriteBatch, int x, int y, Texture2D staticTexture, Texture2D hoverTexture, string text = "")
        {
            StaticSprite = new Sprite(spriteBatch, x, y, staticTexture, Color.White);
            HoverSprite = new Sprite(spriteBatch, x, y, hoverTexture, Color.White);
            Text = new Text(spriteBatch, GameData.Instance.CurrentSkin.Font, text, x + staticTexture.Width - 25,
                y + staticTexture.Height / 2, Color.White);

            Text.X = x + staticTexture.Width - (Text.Width + 75);
            Text.Y = y + staticTexture.Height - (Text.Height + 5);
        }

        public void Draw(Rectangle cursorLocation, bool click)
        {
            if (StaticSprite.Hitbox.Intersects(cursorLocation))
            {
                HoverSprite.Draw();
                Text.Draw();
                if (click)
                {
                    OnClick?.Invoke(null, new ButtonEventArgs());
                    GameData.Instance.CurrentSkin.Click.Play();
                }
                if (!MouseIsOver)
                {
                    MouseIsOver = true;
                    OnMouseEnter?.Invoke(null, new ButtonEventArgs());
                    GameData.Instance.CurrentSkin.MouseOver.Play();
                }
            }
            else
            {
                MouseIsOver = false;
                StaticSprite.Draw();
                Text.Draw();
            }
        }
    }

    class ButtonEventArgs
    {

    }
}
