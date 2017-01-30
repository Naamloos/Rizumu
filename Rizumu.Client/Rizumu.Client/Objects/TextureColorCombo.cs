using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Client
{
    public class TextureColorCombo
    {
        public Texture2D Texture;
        public Color Color;

        public TextureColorCombo(Texture2D texture, Color color)
        {
            Texture = texture;
            Color = color;
        }
    }
}
