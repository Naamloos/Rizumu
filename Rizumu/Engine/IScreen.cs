using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Engine
{
    interface IScreen
    {
        string Name { get; }
        void Draw(SpriteBatch spriteBatch, GameTime gameTime, Rectangle cursor, bool clicked);
        void Update(GameTime gameTime, Rectangle cursor, bool clicked);
        void Preload(SpriteBatch spriteBatch, GraphicsDeviceManager Graphics);
    }
}
