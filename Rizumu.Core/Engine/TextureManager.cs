using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Core.Engine
{
    internal class TextureManager
    {
        static Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();

        public static void LoadTexture(ContentManager Content, string assetname, string id)
        {
            var tx2d = Content.Load<Texture2D>(assetname);
            _textures.Add(id, tx2d);
        }

        public static Texture2D GetTexture(string id)
        {
            if (!_textures.Any(x => x.Key == id))
                throw new Exception($"No texture with ID {id} has been registered!");
            return _textures[id];
        }
    }
}
