using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Net;
using Newtonsoft.Json.Linq;
using Rizumu.Objects;
using System.IO;

namespace Rizumu.Droid
{
    [Activity(Label = "Rizumu.Android"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , ScreenOrientation = ScreenOrientation.Landscape
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity
    {
        public static Game1 game;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Check folders on load
            // CreateDirectory should only create when a folder does not exist.
            var path = System.IO.Path.Combine(Environment.ExternalStorageDirectory.AbsolutePath, "Rizumu");
            Directory.SetCurrentDirectory(path);
            Directory.CreateDirectory("songs");
            Directory.CreateDirectory("skins");
            Directory.CreateDirectory("replays");
            if (!File.Exists("Options.json"))
            {
                File.Create("Options.json").Close();
                File.WriteAllText("Options.json", JObject.FromObject(new Options()).ToString());
            }

            // useless commit I know
            game = new Game1(1280, 720);
            SetContentView((View)game.Services.GetService(typeof(View)));
            Game1.RegisterAndroidUri += (sender, e) =>
            {
                var fields = e.SongType.GetField("assetUri",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance);
                Uri androidUri = Uri.Parse(e.path);
                fields.SetValue(e.song, androidUri);
            };

            game.Run();
        }
    }
}

