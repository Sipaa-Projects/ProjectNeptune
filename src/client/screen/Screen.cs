using Raylib_CSharp;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Shaders;

namespace Neptune.Client.Screen;

public abstract class Screen
{
    protected Shader blurShader;
    public virtual bool UsesTransparentBackdrop { get; }  = true;
    public virtual bool UsesCustomized3DSpaceRendering { get; } = true;

    public Screen()
    {
        blurShader = Shader.Load(null, ResourceManager.GetPhysicalPath("neptune:shaders/blur.fs330"));
    }
    
    public virtual void RenderBackdrop() {
        if (UsesTransparentBackdrop)
        {
            Graphics.BeginShaderMode(blurShader);
                Renderer.Display3DSpace(new(0, 0, 0, 40));
            Graphics.EndShaderMode();
        }
    }

    public abstract void Update();
}