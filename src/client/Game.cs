using Serilog;
using Serilog.Core;
using Raylib_CSharp.Windowing;
using Raylib_CSharp;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Textures;
using Raylib_CSharp.Camera.Cam3D;
using Raylib_CSharp.Colors;
using System.Numerics;
using Neptune.Client.Overlay;
using Raylib_CSharp.Fonts;
using Neptune.Client.Screen;

namespace Neptune.Client;

public class Game {
    public static Game thisGame = null;

    public Camera3D Camera;
    public Screen.Screen CurrentScreen = null;
    public bool IsInWorld = false;
    CubeTextures grass;

    const int DefaultWidth = 800;
    const int DefaultHeight = 450;

    public Game() {
        thisGame = this;
    }

    public void OpenScreen(Screen.Screen screen)
    {
        CurrentScreen = screen;

        if (screen == null)
            Input.DisableCursor();
        else
            Input.EnableCursor();
    }

    public void StartIntegratedWorld()
    {
        Log.Information("Initializing the world...");
        Texture2D gbSide = Texture2D.Load(ResourceManager.GetPhysicalPath("neptune:textures/grass_block_side.png"));
        Texture2D gbTop = Texture2D.Load(ResourceManager.GetPhysicalPath("neptune:textures/grass_block_top.png"));
        CubeTextures grass = new(gbSide, gbTop, gbTop);
        
        Camera = new();
        Camera.FovY = 70.0f;
        Camera.Position = new(5.0f);
        Camera.Target = new(0.0f);
        Camera.Up = new(0.0f, 1.0f, 0.0f);
        Camera.Projection = CameraProjection.Perspective;

        IsInWorld = true;
        Input.DisableCursor();
    }

    public int Start(int argc, string[] argv)
    {
        Log.Logger = new LoggerConfiguration()
                    .Enrich.WithProperty("LoggerName", "NeptuneClient/Main")
                    .WriteTo.Console(
                        outputTemplate: "{Timestamp:HH:mm} {LoggerName}) {Level} => {Message}{NewLine}{Exception}"
                    ).CreateLogger();
        
        Log.Information("Initializing window...");

        Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
        Raylib.SetConfigFlags(ConfigFlags.Msaa4XHint);
        Window.Init(DefaultWidth, DefaultHeight, "Neptune");
        Input.SetExitKey(KeyboardKey.Null);
        Time.SetTargetFPS(Window.GetMonitorRefreshRate(Window.GetCurrentMonitor()));
        Renderer.Initialize();

        CurrentScreen = new TitleScreen();

        Log.Information("Now entering the main loop...");

        while (!Window.ShouldClose()) {
            // Update
            if (Window.IsResized())
                Renderer.Resize3DSpace();
            InputManager.Update();

            if ((CurrentScreen == null || CurrentScreen.UsesTransparentBackdrop) && IsInWorld)
            {
                if (CurrentScreen == null)
                    Camera.Update(CameraMode.Free);

                Renderer.Start3DSpaceRendering();
                    Graphics.ClearBackground(new(16, 4, 24, 255));

                    Graphics.BeginMode3D(Camera);
                    Renderer.RenderSkybox();
                    Renderer.DrawCubeTexture(new Vector3(0.0F), new Vector3(1.0f), grass);
                    Renderer.DrawCubeTexture(new Vector3(1.0F), new Vector3(1.0f), grass);
                    Renderer.DrawCubeTexture(new Vector3(2.0F), new Vector3(1.0f), grass);
                    Renderer.DrawCubeTexture(new Vector3(3.0F), new Vector3(1.0f), grass);
                    Graphics.EndMode3D();
                Renderer.End3DSpaceRendering();
            }

            // Render
            Graphics.BeginDrawing();
                Graphics.ClearBackground(Color.White);
                if (CurrentScreen != null)
                {
                    CurrentScreen.RenderBackdrop();
                    CurrentScreen.Update();

                    if (Input.IsKeyPressed(KeyboardKey.Escape))
                    {
                        CurrentScreen = null;
                        Input.DisableCursor();
                    }
                }
                else {
                    Renderer.Display3DSpace(Color.White);
                    DebugOverlay.Update();
                    InputManager.Update_NoMenu();
                }
            Graphics.EndDrawing();
            
        }

        return 0; // The game exited successfully
    }
}