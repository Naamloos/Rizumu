using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Client
{
    public class GameView
    {
        public Texture2D Background;
        public event EventHandler Update;
        public event EventHandler Draw;
    }
}
