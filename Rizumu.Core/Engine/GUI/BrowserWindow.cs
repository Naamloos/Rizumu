using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp.OffScreen;
using Microsoft.Xna.Framework.Graphics;

namespace Rizumu.Engine.GUI
{
    public class BrowserEventArgs : EventArgs
    {
        public string Name;
        public string Data;
    }

    public class BrowserWindow
    {
        private ChromiumWebBrowser browser;
        private int X;
        private int Y;
        public event EventHandler<BrowserEventArgs> OnEvent;
        private Texture2D tex;
        private GraphicsDevice Gd;
        private RenderTarget2D r2d;

        public BrowserWindow(string htmlfile, int width, int height, int x, int y, GraphicsDevice gd)
        {
            var settings = new CefSharp.BrowserSettings()
            {
                Javascript = CefSharp.CefState.Enabled
            };

            CefSharp.CefSharpSettings.LegacyJavascriptBindingEnabled = true;

            browser = new ChromiumWebBrowser("https://naamloos.dev", settings);
            browser.Size = new System.Drawing.Size(width, height);
            //browser.LoadingStateChanged += Browser_LoadingStateChanged;
            browser.Paint += Browser_Paint;
            browser.RegisterJsObject("game", this);
            this.X = x;
            this.Y = y;
            this.Gd = gd;
            this.r2d = new RenderTarget2D(Gd, width, height);
            tex = new Texture2D(Gd, width, height);
        }

        private void Browser_Paint(object sender, OnPaintEventArgs e)
        {
            var ss = browser.ScreenshotOrNull();

            if (ss != null)
            {
                using (var s = new MemoryStream())
                {
                    tex.Dispose();
                    tex = null;
                    ss.Save(s, ImageFormat.Png);
                    tex = Texture2D.FromStream(Gd, s);
                }
            }
        }

        private void SendDataToGame(string eventname, string data)
        {
            OnEvent.Invoke(this, new BrowserEventArgs() { Name = eventname, Data = data });
        }

        public void SendDataToBrowser(string eventname, string data)
        {
            browser.GetBrowser().MainFrame.ExecuteJavaScriptAsync($"OnBrowserData(\"{eventname}\", \"{data}\");");
        }

        /*private void Browser_LoadingStateChanged(object sender, CefSharp.LoadingStateChangedEventArgs e)
        {
            var ss = browser.ScreenshotOrNull();
            if (ss != null)
            {
                using (var s = new MemoryStream())
                {
                    ss.Save(s, ImageFormat.Png);
                    tex = Texture2D.FromStream(tex.GraphicsDevice, s);
                }
            }
        }*/

        internal void Update(MouseValues mouseValues)
        {
            var x = mouseValues.X - X;
            var y = mouseValues.Y - Y;

            if (browser.IsBrowserInitialized)
            {
                browser.GetBrowser().GetHost().SendMouseMoveEvent(new CefSharp.MouseEvent(x, y, CefSharp.CefEventFlags.None), false);
                browser.GetBrowser().GetHost().SendMouseClickEvent(
                        new CefSharp.MouseEvent(x, y, CefSharp.CefEventFlags.None), CefSharp.MouseButtonType.Left, mouseValues.Clicked, 1);
            }
        }

        public void Draw(SpriteBatch sb)
        {
            if (tex != null)
            {
                var rt = (RenderTarget2D)Gd.GetRenderTargets()[0].RenderTarget;
                Gd.SetRenderTarget(this.r2d);
                sb.Draw(tex, new Microsoft.Xna.Framework.Vector2(0, 0), Microsoft.Xna.Framework.Color.White);
                Gd.SetRenderTarget(rt);
            }
            sb.Draw(r2d, new Microsoft.Xna.Framework.Vector2(X, Y), Microsoft.Xna.Framework.Color.White);
        }
    }
}
