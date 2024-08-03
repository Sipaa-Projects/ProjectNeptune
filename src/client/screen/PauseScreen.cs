using Neptune.Client.Gui;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Rendering;

namespace Neptune.Client.Screen;

public class PauseScreen : Screen
{
    GuiButton resumeBtn;
    const int menuWidth = 300;
    
    public PauseScreen()
    {
        int btnWidth = menuWidth - (10 * 2);

        resumeBtn = new() {
            Text = "Resume",
            PosX = 10,
            PosY = 60,
            Width = btnWidth,
            Height = 40
        };
    }
    public override void RenderBackdrop()
    {
        Renderer.Display3DSpace(Color.White);

        if (UsesTransparentBackdrop)
        {
            Graphics.BeginShaderMode(blurShader);
                Renderer.Display3DSpacePart(new(0, 0, 300, -Renderer.Get3DSpaceDims().Y), new(0, 0, 0, 40));
            Graphics.EndShaderMode();

            Graphics.DrawRectangle(0, 0, 300, (int)Renderer.Get3DSpaceDims().Y, new(0, 0, 0, 143));
            Graphics.DrawRectangleGradientH(300, 0, 10, (int)Renderer.Get3DSpaceDims().Y, new(0, 0, 0, 147), new(0, 0, 0, 0));
        }
    }
    public override void Update()
    {
        Renderer.DrawText("Paused", 10, 10, 26, Color.White);
        Renderer.DrawText("Press ESC to resume", 10, 26 + 8, 16, Color.White);

        resumeBtn.Update();
        
    }
}