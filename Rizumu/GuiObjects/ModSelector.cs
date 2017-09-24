using Microsoft.Xna.Framework.Graphics;
using Rizumu.Engine;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Rizumu.GameObjects;

namespace Rizumu.GuiObjects
{
    public class ModSelector
    {
        Sprite Back;
        int Textx;

        Text SpeedMultiplier;
        Text HorizontalMirror;
        Text VerticalMirror;
        Text AutoMode;
        Text SizeMultiplier;
        Text FleshLight;
        Text NoFail;
        Text Instafail;
        Text RotationMode;
        Text Selector;
        int SelectorIndex = 1;
        int height = 0;

        ModCollection mods = GameData.Instance.Mods;

        public ModSelector(SpriteBatch sb, int screenwidth, int screenheight)
        {
            var tex = GameData.Instance.CurrentSkin.SelectorBG;
            int x = screenwidth - 400;
            Back = new Sprite(sb, x, 0, tex, Color.White);

            var font = GameData.Instance.CurrentSkin.FontSmall;
            Textx = x + (int)font.MeasureString("+..").X;
            height = (int)(font.MeasureString("A").Y + 3f);
            SpeedMultiplier = new Text(sb, font, $"Speed: {mods.SpeedMultiplier}", Textx, height * 1, Color.White);
            HorizontalMirror = new Text(sb, font, $"Horizontal Mirror Mode: {mods.HorizontalMirror}", Textx, height * 2, Color.White);
            VerticalMirror = new Text(sb, font, $"Vertical Mirror Mode: {mods.VerticalMirror}", Textx, height * 3, Color.White);
            AutoMode = new Text(sb, font, $"AutoMode: {mods.Automode}", Textx, height * 4, Color.White);
            SizeMultiplier = new Text(sb, font, $"Size: {mods.SizeMultiplier}", Textx, height * 5, Color.White);
            FleshLight = new Text(sb, font, $"FlashLight: {mods.FleshLight}", Textx, height * 6, Color.White);
            NoFail = new Text(sb, font, $"No Fail: {mods.NoFail}", Textx, height * 7, Color.White);
            Instafail = new Text(sb, font, $"Insta Fail: {mods.Instafail}", Textx, height * 8, Color.White);
            RotationMode = new Text(sb, font, $"Rotation Mode: {mods.RotationMode}", Textx, height * 9, Color.White);
            Selector = new Text(sb, font, ">", x, height * SelectorIndex, Color.Green);
        }

        public void Draw()
        {
            var keyboard = Keyboard.GetState();

            Back.Draw();
            SpeedMultiplier.Content = $"Speed: {mods.SpeedMultiplier}";
            HorizontalMirror.Content = $"Horizontal Mirror Mode: {mods.HorizontalMirror}";
            VerticalMirror.Content = $"Vertical Mirror Mode: {mods.VerticalMirror}";
            AutoMode.Content = $"AutoMode: {mods.Automode}";
            SizeMultiplier.Content = $"Size: {mods.SizeMultiplier}";
            FleshLight.Content = $"FlashLight: {mods.FleshLight}";
            NoFail.Content = $"No Fail: {mods.NoFail}";
            Instafail.Content = $"Insta Fail: {mods.Instafail}";
            RotationMode.Content = $"Rotation Mode: {mods.RotationMode}";
            SpeedMultiplier.Draw();
            HorizontalMirror.Draw();
            VerticalMirror.Draw();
            AutoMode.Draw();
            SizeMultiplier.Draw();
            FleshLight.Draw();
            NoFail.Draw();
            Instafail.Draw();
            RotationMode.Draw();
            if (keyboard.IsKeyPress(Keys.Up) && SelectorIndex > 1)
            {
                SelectorIndex--;
            }
            if (keyboard.IsKeyPress(Keys.Down) && SelectorIndex < 9)
            {
                SelectorIndex++;
            }
            Selector.Y = height * SelectorIndex;
            Selector.Draw();

            switch (SelectorIndex)
            {
                case 1 when keyboard.IsKeyPress(Keys.Left):
                    if (mods.SpeedMultiplier > 0.1F)
                        mods.SpeedMultiplier -= 0.1f;
                    break;
                case 1 when keyboard.IsKeyPress(Keys.Right):
                    mods.SpeedMultiplier += 0.1f;
                    break;
                case 2 when keyboard.IsKeyPress(Keys.Left) || keyboard.IsKeyPress(Keys.Right):
                    mods.HorizontalMirror = !mods.HorizontalMirror;
                    break;
                case 3 when keyboard.IsKeyPress(Keys.Left) || keyboard.IsKeyPress(Keys.Right):
                    mods.VerticalMirror = !mods.VerticalMirror;
                    break;
                case 4 when keyboard.IsKeyPress(Keys.Left) || keyboard.IsKeyPress(Keys.Right):
                    mods.Automode = !mods.Automode;
                    break;
                case 5 when keyboard.IsKeyPress(Keys.Left):
                    if (mods.SizeMultiplier > 0.1f)
                        mods.SizeMultiplier -= 0.1f;
                    break;
                case 5 when keyboard.IsKeyPress(Keys.Right):
                    mods.SizeMultiplier += 0.1f;
                    break;
                case 6 when keyboard.IsKeyPress(Keys.Left) || keyboard.IsKeyPress(Keys.Right):
                    mods.FleshLight = !mods.FleshLight;
                    break;
                case 7 when keyboard.IsKeyPress(Keys.Left) || keyboard.IsKeyPress(Keys.Right):
                    mods.NoFail = !mods.NoFail;
                    break;
                case 8 when keyboard.IsKeyPress(Keys.Left) || keyboard.IsKeyPress(Keys.Right):
                    mods.Instafail = !mods.Instafail;
                    break;
                case 9 when keyboard.IsKeyPress(Keys.Left) || keyboard.IsKeyPress(Keys.Right):
                    mods.RotationMode = !mods.RotationMode;
                    break;
            }
            GameData.Instance.Mods = mods;
        }
    }
}
