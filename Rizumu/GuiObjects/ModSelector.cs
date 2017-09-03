using Microsoft.Xna.Framework.Graphics;
using Rizumu.Engine;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

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

        public ModSelector(SpriteBatch sb, int screenwidth, int screenheight)
        {
            var tex = GameData.Instance.CurrentSkin.SelectorBG;
            int x = screenwidth - 400;
            Back = new Sprite(sb, x, 0, tex, Color.White);

            var font = GameData.Instance.CurrentSkin.FontSmall;
            Textx = x + (int)font.MeasureString("+..").X;
            height = (int)(font.MeasureString("A").Y + 3f);
            SpeedMultiplier = new Text(sb, font, $"Speed: {GameData.Instance.Mods.SpeedMultiplier}", Textx, height * 1, Color.White);
            HorizontalMirror = new Text(sb, font, $"Horizontal Mirror Mode: {GameData.Instance.Mods.HorizontalMirror}", Textx, height * 2, Color.White);
            VerticalMirror = new Text(sb, font, $"Vertical Mirror Mode: {GameData.Instance.Mods.VerticalMirror}", Textx, height * 3, Color.White);
            AutoMode = new Text(sb, font, $"AutoMode: {GameData.Instance.Mods.Automode}", Textx, height * 4, Color.White);
            SizeMultiplier = new Text(sb, font, $"Size: {GameData.Instance.Mods.SizeMultiplier}", Textx, height * 5, Color.White);
            FleshLight = new Text(sb, font, $"FlashLight: {GameData.Instance.Mods.FleshLight}", Textx, height * 6, Color.White);
            NoFail = new Text(sb, font, $"No Fail: {GameData.Instance.Mods.NoFail}", Textx, height * 7, Color.White);
            Instafail = new Text(sb, font, $"Insta Fail: {GameData.Instance.Mods.Instafail}", Textx, height * 8, Color.White);
            RotationMode = new Text(sb, font, $"Rotation Mode: {GameData.Instance.Mods.RotationMode}", Textx, height * 9, Color.White);
            Selector = new Text(sb, font, ">", x, height * SelectorIndex, Color.Green);
        }

        public void ChangeBool(ref bool input)
        {
            if (input)
                input = false;
            else
                input = true;
        }

        public void ChangeFloat(ref float input, bool up)
        {
            if (up)
            {
                input += 0.1f;
            }
            else
            {
                if (input > 0.1f)
                    input -= 0.1f;
            }
        }

        public void Draw()
        {
            Back.Draw();
            SpeedMultiplier.Content = $"Speed: {GameData.Instance.Mods.SpeedMultiplier}";
            HorizontalMirror.Content = $"Horizontal Mirror Mode: {GameData.Instance.Mods.HorizontalMirror}";
            VerticalMirror.Content = $"Vertical Mirror Mode: {GameData.Instance.Mods.VerticalMirror}";
            AutoMode.Content = $"AutoMode: {GameData.Instance.Mods.Automode}";
            SizeMultiplier.Content = $"Size: {GameData.Instance.Mods.SizeMultiplier}";
            FleshLight.Content = $"FlashLight: {GameData.Instance.Mods.FleshLight}";
            NoFail.Content = $"No Fail: {GameData.Instance.Mods.NoFail}";
            Instafail.Content = $"Insta Fail: {GameData.Instance.Mods.Instafail}";
            RotationMode.Content = $"Rotation Mode: {GameData.Instance.Mods.RotationMode}";
            SpeedMultiplier.Draw();
            HorizontalMirror.Draw();
            VerticalMirror.Draw();
            AutoMode.Draw();
            SizeMultiplier.Draw();
            FleshLight.Draw();
            NoFail.Draw();
            Instafail.Draw();
            RotationMode.Draw();
            if (Keyboard.GetState().IsKeyPress(Keys.Up))
            {
                if (SelectorIndex > 1)
                    SelectorIndex--;
            }
            if (Keyboard.GetState().IsKeyPress(Keys.Down))
            {
                if (SelectorIndex < 9)
                    SelectorIndex++;
            }
            Selector.Y = height * SelectorIndex;
            Selector.Draw();

            if(SelectorIndex == 1)
            {
                if (Keyboard.GetState().IsKeyPress(Keys.Left))
                    ChangeFloat(ref GameData.Instance.Mods.SpeedMultiplier, false);
                if (Keyboard.GetState().IsKeyPress(Keys.Right))
                    ChangeFloat(ref GameData.Instance.Mods.SpeedMultiplier, true);
            }
            if (SelectorIndex == 2)
            {
                if (Keyboard.GetState().IsKeyPress(Keys.Left) || Keyboard.GetState().IsKeyPress(Keys.Right))
                    ChangeBool(ref GameData.Instance.Mods.HorizontalMirror);
            }
            if (SelectorIndex == 3)
            {
                if (Keyboard.GetState().IsKeyPress(Keys.Left) || Keyboard.GetState().IsKeyPress(Keys.Right))
                    ChangeBool(ref GameData.Instance.Mods.VerticalMirror);
            }
            if (SelectorIndex == 4)
            {
                if (Keyboard.GetState().IsKeyPress(Keys.Left) || Keyboard.GetState().IsKeyPress(Keys.Right))
                    ChangeBool(ref GameData.Instance.Mods.Automode);
            }
            if (SelectorIndex == 5)
            {
                if (Keyboard.GetState().IsKeyPress(Keys.Left))
                    ChangeFloat(ref GameData.Instance.Mods.SizeMultiplier, false);
                if (Keyboard.GetState().IsKeyPress(Keys.Right))
                    ChangeFloat(ref GameData.Instance.Mods.SizeMultiplier, true);
            }
            if (SelectorIndex == 6)
            {
                if (Keyboard.GetState().IsKeyPress(Keys.Left) || Keyboard.GetState().IsKeyPress(Keys.Right))
                    ChangeBool(ref GameData.Instance.Mods.FleshLight);
            }
            if (SelectorIndex == 7)
            {
                if (Keyboard.GetState().IsKeyPress(Keys.Left) || Keyboard.GetState().IsKeyPress(Keys.Right))
                    ChangeBool(ref GameData.Instance.Mods.NoFail);
            }
            if (SelectorIndex == 8)
            {
                if (Keyboard.GetState().IsKeyPress(Keys.Left) || Keyboard.GetState().IsKeyPress(Keys.Right))
                    ChangeBool(ref GameData.Instance.Mods.Instafail);
            }
            if (SelectorIndex == 9)
            {
                if (Keyboard.GetState().IsKeyPress(Keys.Left) || Keyboard.GetState().IsKeyPress(Keys.Right))
                    ChangeBool(ref GameData.Instance.Mods.RotationMode);
            }
        }
    }
}
