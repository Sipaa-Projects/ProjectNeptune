using Neptune.Client.Overlay;
using Neptune.Client.Screen;
using Raylib_CSharp;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Windowing;

namespace Neptune.Client;

public class InputManager {
    static Game thisGame = Game.thisGame;

    static string GetScreenshotPath() {
        string fileName = "screenshot_" + DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss") + ".png";

        return Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            fileName
        );
    }

    public static void Update()
    {
        if (Input.IsKeyPressed(KeyboardKey.F3))
            DebugOverlay.Visible = !DebugOverlay.Visible;

        if (Input.IsKeyPressed(KeyboardKey.F2))
            Raylib.TakeScreenshot(GetScreenshotPath());
    }    
    
    public static void Update_NoMenu()
    {
        if (thisGame.CurrentScreen == null && Input.IsKeyPressed(KeyboardKey.Escape))
            thisGame.OpenScreen(new PauseScreen());
    }
}