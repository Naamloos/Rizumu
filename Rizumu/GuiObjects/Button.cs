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

        public Button(int x, int y, Texture2D staticTexture, Texture2D hoverTexture, string text = "")
        {
            StaticSprite = new Sprite(x, y, staticTexture, Color.White);
            HoverSprite = new Sprite(x, y, staticTexture, Color.Gray);
            Text = new Text(GameData.Instance.CurrentSkin.Font, text, x + 20,
                y + staticTexture.Height / 2, Color.White);

            Text.Y = y + (staticTexture.Height / 2) - (Text.Height / 2);
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle cursorLocation, bool click)
        {
            if (StaticSprite.Hitbox.Intersects(cursorLocation))
            {
                HoverSprite.Draw(spriteBatch);
                Text.Draw(spriteBatch);
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
                StaticSprite.Draw(spriteBatch);
                Text.Draw(spriteBatch);
            }
        }
    }

    class ButtonEventArgs
    {

    }
}
