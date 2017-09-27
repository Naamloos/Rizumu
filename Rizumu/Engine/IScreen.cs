using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rizumu.Enums;

namespace Rizumu.Engine
{
    interface IScreen
    {
        Screen Name { get; }
        void Draw(SpriteBatch spriteBatch, GameTime gameTime, Rectangle cursor, bool clicked, GraphicsDevice g);
        void Update(GameTime gameTime, Rectangle cursor, bool clicked);
        void Preload(SpriteBatch spriteBatch, GraphicsDeviceManager Graphics);
    }
}
