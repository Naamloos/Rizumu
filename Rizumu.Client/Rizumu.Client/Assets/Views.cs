using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Client.Assets
{
    public enum Gameview
    {
        Main,
        Songselect,
        Options,
        Ingame,
        Results
    }

    public class Views
    {
        public static View Main;
        public static Button b;

        public static Gameview Current;

        public static void Initialize()
        {
            Main = new View();
            b = new Button(0, 0, new TextureColorCombo(Assets.Textures.Button, Color.White), new TextureColorCombo(Textures.ButtonSelected, Color.White));
            Main.DrawEvent += (sender, e) =>
            {
                b.Draw(e.spriteBatch);
            };
            Main.UpdateEvent += (sender, e) =>
            {
                b.Update();
                b.ClickEvent += (bs, be) =>
                {
                    System.Windows.Forms.MessageBox.Show("Test");
                };
            };
        }
    }
}
