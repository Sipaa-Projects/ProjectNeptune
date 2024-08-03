using Neptune.Client.Gui;
using Raylib_CSharp.Camera.Cam3D;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Windowing;

namespace Neptune.Client.Screen;

public class TitleScreen : Screen
{
    public override bool UsesTransparentBackdrop => false;
    Camera3D cam;
    const string licenseText = "Copyright (C) 2024-present Sipaa Projects. Licensed under the MIT License.";
    
    public TitleScreen()
    {
        cam = new Camera3D();
        cam.FovY = 70.0f;
        cam.Position = new(1.0f);
        cam.Target = new(0.0f, 3.0f, 0.0f);
        cam.Up = new(0.0f, 1.0f, 0.0f);
        cam.Projection = CameraProjection.Perspective;
    }

    public override void RenderBackdrop()
    {
        base.RenderBackdrop();

        cam.Update(CameraMode.Orbital);
        Graphics.BeginMode3D(cam);
            Renderer.RenderSkybox();
        Graphics.EndMode3D();

        Graphics.DrawRectangle(0, 0, Window.GetRenderWidth(), Window.GetRenderHeight(), new(0, 0, 0, 40));
    }

    public override void Update()
    {
        Renderer.DrawText("Project Neptune 0.1.0 (Vanilla)", 4, Window.GetRenderHeight() - 16 - 4, 16, new(255, 255, 255, 255));
    }
}