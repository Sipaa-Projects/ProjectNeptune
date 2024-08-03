using Raylib_CSharp.Colors;
using Raylib_CSharp;
using Raylib_CSharp.Rendering;
using System.Management;
using System.Text;
using Raylib_CSharp.Fonts;
using System.Numerics;

namespace Neptune.Client.Overlay;

public unsafe static class DebugOverlay {
    public static bool Visible = false;
    static Game thisGame = Game.thisGame;
    const int GL_VENDOR = 0x1F00;
    const int GL_RENDERER = 0x1F01;
    const int GL_VERSION = 0x1F02;

    static List<string> GetLeftText() {
        var l = new List<string>();

        l.Add("Project Neptune (0.1.0 vanilla)");
        l.Add($" - {Time.GetFPS()} FPS");
        l.Add($" - CameraPos: " + thisGame.Camera.Position.ToString());
        l.Add($" - CameraTarget: " + thisGame.Camera.Target.ToString());
        l.Add($" - CameraUp: " + thisGame.Camera.Up.ToString());
        l.Add($" - CameraFOV: " + thisGame.Camera.FovY.ToString());
        l.Add($"");
        l.Add($"Hardware information:");
        l.Add($" - Current driver:");

        return l;
    }

    static void RenderLeftText(List<string> text)
    {
        int fontSize = 16;

        int offX = 6;
        int offY = 6;

        int currentY = offY;

        foreach (string t in text)
        {
            if (!String.IsNullOrEmpty(t)) {
                Vector2 sz = TextManager.MeasureTextEx(Renderer.Font, t, fontSize, 0);
                
                Graphics.DrawRectangle(0, currentY - offY, (int)((offX * 2) + sz.X), (int)(offY + sz.Y), new(0, 0, 0, 127));
                Renderer.DrawText(t, offX, currentY, fontSize, Color.White);
            }
            currentY += fontSize + offY;
        }
    }

    public static void Update()
    {
        if (Visible)
        {
            RenderLeftText(GetLeftText());
        }
    }
}