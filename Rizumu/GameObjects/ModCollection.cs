using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.GameObjects
{
    class ModCollection
    {
        public float SpeedMultiplier = 1f; // Current note speed
        public bool HorizontalMirror = false; // Mirror input horizontally
        public bool VerticalMirror = false; // Mirror input vertically
        public bool Automode = true; // Auto mode
        public float SizeMultiplier = 0.5f; // Current note size. Just for fun.
        public bool FleshLight = false; // :^) Shine a flashligt to the middle of the screen
        public bool NoFail = false; // You can't fail
        public bool Instafail = false; // One miss makes you fail
        public bool RotationMode = false; // Screen keeps rotating, adds direction text to center

        public string GetCollectionString()
        {
            return
                $"{(SpeedMultiplier != 1f ? $"Speed: {SpeedMultiplier}x\n" : "")}" +
                $"{(HorizontalMirror ? "Horizontal Mirror Enabled\n" : "")}" +
                $"{(VerticalMirror ? "Vertical Mirror Enabled\n" : "")}" +
                $"{(Automode ? "Automode Enabled\n" : "")}" +
                $"{(SizeMultiplier != 1f ? $"Size: {SizeMultiplier}x\n" : "")}" +
                $"{(FleshLight ? "Flashlight Enabled\n" : "")}" +
                $"{(NoFail ? "No Fail Enabled\n" : "")}" +
                $"{(Instafail ? "Insta Fail Enabled\n" : "")}" +
                $"{(RotationMode ? "Rotation Mode Enabled\n" : "")}";
        }
    }
}
