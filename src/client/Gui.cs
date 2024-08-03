using System.Numerics;
using Raylib_CSharp;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Fonts;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Rendering;
using System;

namespace Neptune.Client.Gui
{
    public static class ColorUtils
    {
        public static Color Lerp(Color colorA, Color colorB, float t)
        {
            byte r = (byte)(colorA.R * (1 - t) + colorB.R * t);
            byte g = (byte)(colorA.G * (1 - t) + colorB.G * t);
            byte b = (byte)(colorA.B * (1 - t) + colorB.B * t);
            byte a = (byte)(colorA.A * (1 - t) + colorB.A * t);
            return new Color(r, g, b, a);
        }
    }

    public static class MathUtils
    {
        public static float Clamp(float value, float min, float max)
        {
            return value < min ? min : value > max ? max : value;
        }
    }

    public class GuiColorAnimation
    {
        public Color StartColor { get; set; }
        public Color EndColor { get; set; }
        public float Duration { get; set; }
        public bool IsPlaying { get; private set; }
        private DateTime startTime;

        public GuiColorAnimation(Color startColor, Color endColor, float duration)
        {
            StartColor = startColor;
            EndColor = endColor;
            Duration = duration;
        }

        public void Start()
        {
            startTime = DateTime.Now;
            IsPlaying = true;
        }

        public void Reset()
        {
            startTime = DateTime.MinValue; // Or any other invalid start time
            IsPlaying = false;
        }

        public Color Update()
        {
            if (!IsPlaying) return StartColor;

            float elapsedTime = (float)(DateTime.Now - startTime).TotalSeconds;
            float t = MathUtils.Clamp(elapsedTime / Duration, 0, 1);
            return ColorUtils.Lerp(StartColor, EndColor, t);
        }
    }

    public class GuiButton
    {
        public int PosX { get; set; } = 0;
        public int PosY { get; set; } = 0;
        public int Width { get; set; } = 150;
        public int Height { get; set; } = 40;
        public string Text { get; set; } = "Button";
        public bool Hovered { get; private set; }

        private Color normalCol = new(32, 32, 32, 147);
        private Color hoverCol = new(46, 46, 46, 147);

        private GuiColorAnimation hoverAnimation;

        public Action OnPress;

        public GuiButton()
        {
            hoverAnimation = new GuiColorAnimation(normalCol, hoverCol, 0.15f);
        }

        public bool Update()
        {
            // Check for hover
            bool newHovered = Input.GetMouseX() >= PosX &&
                              Input.GetMouseY() >= PosY &&
                              Input.GetMouseX() <= PosX + Width &&
                              Input.GetMouseY() <= PosY + Height;

            if (newHovered != Hovered)
            {
                Hovered = newHovered;
                if (Hovered)
                {
                    hoverAnimation.StartColor = normalCol;
                    hoverAnimation.EndColor = hoverCol;
                    hoverAnimation.Start();
                }
                else
                {
                    hoverAnimation.StartColor = hoverCol;
                    hoverAnimation.EndColor = normalCol;
                    hoverAnimation.Start();
                }
            }

            if (Input.IsMouseButtonPressed(MouseButton.Left))
            {

            }

            Color buttonColor = hoverAnimation.Update();
            Graphics.DrawRectangleRounded(new(PosX, PosY, Width, Height), Height, Height, buttonColor);

            Vector2 sz = TextManager.MeasureTextEx(Renderer.Font, Text, 16, 0);
            Renderer.DrawText(Text, (int)(PosX + (Width / 2 - sz.X / 2)), (int)(PosY + (Height / 2 - sz.Y / 2)), 16, Color.White);

            return false;
        }
    }
}
