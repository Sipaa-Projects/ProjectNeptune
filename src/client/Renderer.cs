using System.Numerics;
using Raylib_CSharp;
using Raylib_CSharp.Textures;
using Raylib_CSharp.Rendering.Gl;
using Raylib_CSharp.Windowing;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Camera.Cam3D;
using Raylib_CSharp.Transformations;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Fonts;
using Raylib_CSharp.Geometry;
using Raylib_CSharp.Shaders;
using Raylib_CSharp.Materials;
using Raylib_CSharp.Images;

namespace Neptune.Client;

public class CubeTextures {
    public Texture2D back;
    public Texture2D front;
    public Texture2D left;
    public Texture2D right;
    public Texture2D top;
    public Texture2D bottom;

    public CubeTextures(Texture2D all)
    {
        back = all;
        front = all;
        top = all;
        bottom = all;
        left = all;
        right = all;
    }

    public CubeTextures(Texture2D side, Texture2D top, Texture2D bottom)
    {
        back = side;
        front = side;
        left = side;
        right = side;
        this.top = top;
        this.bottom = bottom;
    }

    public CubeTextures(Texture2D front, Texture2D back, Texture2D left, Texture2D right, Texture2D top, Texture2D bottom)
    {
        this.back = back;
        this.front = front;
        this.left = left;
        this.right = right;
        this.top = top;
        this.bottom = bottom;
    }
}

public class Renderer {
    /// <summary>
    /// The 3D space is a RenderTexture which contains the rendered 3D world.
    /// </summary>
    static RenderTexture2D TDSpace;
    static Font font;
    static Mesh skyboxCube;
    static Model skybox;
    static Shader shdrCubemap;

    public static Font Font { get => font; }

    public static void Initialize()
    {
        LoadFont(ResourceManager.GetPhysicalPath("neptune:fonts/notosans-regular.ttf"));
        font.Texture.GenMipmaps();

        Initialize3DSpace();
        InitializeSkybox();
    }

#region Skybox
    public static void InitializeSkybox()
    {
        skyboxCube = Mesh.GenCube(1.0f, 1.0f, 1.0f);
        skybox = Model.LoadFromMesh(skyboxCube);

        skybox.Materials[0].Shader = Shader.Load(ResourceManager.GetPhysicalPath("neptune:shaders/skybox.vs330"),
                                                 ResourceManager.GetPhysicalPath("neptune:shaders/skybox.fs330"));

        skybox.Materials[0].Shader.SetValue(skybox.Materials[0].Shader.GetLocation("environmentMap"), MaterialMapIndex.Cubemap, ShaderUniformDataType.Int);
        skybox.Materials[0].Shader.SetValue(skybox.Materials[0].Shader.GetLocation("doGamma"),  0, ShaderUniformDataType.Int);
        skybox.Materials[0].Shader.SetValue(skybox.Materials[0].Shader.GetLocation("vflipped"), 0, ShaderUniformDataType.Int);

        shdrCubemap = Shader.Load(ResourceManager.GetPhysicalPath("neptune:shaders/cubemap.vs330"),
                                  ResourceManager.GetPhysicalPath("neptune:shaders/cubemap.fs330"));

        shdrCubemap.SetValue(shdrCubemap.GetLocation("equirectangularMap"), 0, ShaderUniformDataType.Int);
    
        Image img = Image.Load(ResourceManager.GetPhysicalPath("neptune:textures/skybox.png"));
        skybox.Materials[0].Maps[(int)MaterialMapIndex.Cubemap].Texture = Texture2D.LoadCubemap(img, CubemapLayout.CrossFourByThree);    // CUBEMAP_LAYOUT_PANORAMA
        img.Unload();
    }
#endregion

#region Font management
    public static void LoadFont(string name)
    {
        if (font.Texture.Id != 0)
            font.Unload();
        
        font = Font.LoadEx(name, 96, null);

        // Add some bilinear filter so the font isn't ugly when small
        font.Texture.SetFilter(TextureFilter.Bilinear);
    }
#endregion

#region 3D Space
    public static Vector2 Get3DSpaceDims()
    {
        return new(TDSpace.Texture.Width, TDSpace.Texture.Height);
    }
    
    public static void Initialize3DSpace()
    {
        TDSpace = RenderTexture2D.Load(
            Window.GetRenderWidth(),
            Window.GetRenderHeight()
        );
    }

    public static void Resize3DSpace()
    {
        if (TDSpace.Id != 0)
            TDSpace.Unload();
        
        Initialize3DSpace();
    }

    public static void Start3DSpaceRendering()
    {
        Graphics.BeginTextureMode(TDSpace);
    }

    public static void End3DSpaceRendering()
    {
        Graphics.EndTextureMode();
    }
#endregion

#region 3D Element rendering
    public static void RenderSkybox()
    {
        RlGl.DisableBackfaceCulling();
        RlGl.DisableDepthMask();
            Graphics.DrawModel(skybox, new(0.0f), 1.0f, Color.White);
        RlGl.EnableBackfaceCulling();
        RlGl.EnableDepthMask();
    }

    public static void DrawCubeTexture(Vector3 position, Vector3 size, CubeTextures tex)
    {
        float x = position.X;
        float y = position.Y;
        float z = position.Z;
        
        float width = size.X;
        float height = size.Y;
        float length = size.Z;

        RlGl.Begin(DrawMode.Quads);

        // Front Face
        RlGl.Normal3F(0.0f, 0.0f, 1.0f);       // Normal Pointing Towards Viewer
        RlGl.SetTexture(tex.front.Id);
        RlGl.TexCoord2F(0.0f, 1.0f); RlGl.Vertex3F(x - width/2, y - height/2, z + length/2);  // Bottom Left Of The Texture and Quad
        RlGl.TexCoord2F(1.0f, 1.0f); RlGl.Vertex3F(x + width/2, y - height/2, z + length/2);  // Bottom Right Of The Texture and Quad
        RlGl.TexCoord2F(1.0f, 0.0f); RlGl.Vertex3F(x + width/2, y + height/2, z + length/2);  // Top Right Of The Texture and Quad
        RlGl.TexCoord2F(0.0f, 0.0f); RlGl.Vertex3F(x - width/2, y + height/2, z + length/2);  // Top Left Of The Texture and Quad
                
        // Back Face
        RlGl.Normal3F(0.0f, 0.0f, - 1.0f);     // Normal Pointing Away From Viewer
        RlGl.SetTexture(tex.back.Id);
        RlGl.TexCoord2F(1.0f, 1.0f); RlGl.Vertex3F(x - width/2, y - height/2, z - length/2);  // Bottom Right Of The Texture and Quad
        RlGl.TexCoord2F(1.0f, 0.0f); RlGl.Vertex3F(x - width/2, y + height/2, z - length/2);  // Top Right Of The Texture and Quad
        RlGl.TexCoord2F(0.0f, 0.0f); RlGl.Vertex3F(x + width/2, y + height/2, z - length/2);  // Top Left Of The Texture and Quad
        RlGl.TexCoord2F(0.0f, 1.0f); RlGl.Vertex3F(x + width/2, y - height/2, z - length/2);  // Bottom Left Of The Texture and Quad
                
        // Top Face
        RlGl.Normal3F(0.0f, 1.0f, 0.0f);       // Normal Pointing Up
        RlGl.SetTexture(tex.top.Id);
        RlGl.TexCoord2F(0.0f, 0.0f); RlGl.Vertex3F(x - width/2, y + height/2, z - length/2);  // Top Left Of The Texture and Quad
        RlGl.TexCoord2F(0.0f, 1.0f); RlGl.Vertex3F(x - width/2, y + height/2, z + length/2);  // Bottom Left Of The Texture and Quad
        RlGl.TexCoord2F(1.0f, 1.0f); RlGl.Vertex3F(x + width/2, y + height/2, z + length/2);  // Bottom Right Of The Texture and Quad
        RlGl.TexCoord2F(1.0f, 0.0f); RlGl.Vertex3F(x + width/2, y + height/2, z - length/2);  // Top Right Of The Texture and Quad

        // Bottom Face
        RlGl.Normal3F(0.0f, - 1.0f, 0.0f);     // Normal Pointing Down
        RlGl.SetTexture(tex.bottom.Id);
        RlGl.TexCoord2F(1.0f, 0.0f); RlGl.Vertex3F(x - width/2, y - height/2, z - length/2);  // Top Right Of The Texture and Quad
        RlGl.TexCoord2F(0.0f, 0.0f); RlGl.Vertex3F(x + width/2, y - height/2, z - length/2);  // Top Left Of The Texture and Quad
        RlGl.TexCoord2F(0.0f, 1.0f); RlGl.Vertex3F(x + width/2, y - height/2, z + length/2);  // Bottom Left Of The Texture and Quad
        RlGl.TexCoord2F(1.0f, 1.0f); RlGl.Vertex3F(x - width/2, y - height/2, z + length/2);  // Bottom Right Of The Texture and Quad

        // Right face
        RlGl.Normal3F(1.0f, 0.0f, 0.0f);       // Normal Pointing Right
        RlGl.SetTexture(tex.right.Id);
        RlGl.TexCoord2F(1.0f, 1.0f); RlGl.Vertex3F(x + width/2, y - height/2, z - length/2);  // Bottom Right Of The Texture and Quad
        RlGl.TexCoord2F(1.0f, 0.0f); RlGl.Vertex3F(x + width/2, y + height/2, z - length/2);  // Top Right Of The Texture and Quad
        RlGl.TexCoord2F(0.0f, 0.0f); RlGl.Vertex3F(x + width/2, y + height/2, z + length/2);  // Top Left Of The Texture and Quad
        RlGl.TexCoord2F(0.0f, 1.0f); RlGl.Vertex3F(x + width/2, y - height/2, z + length/2);  // Bottom Left Of The Texture and Quad
                
        // Left Face
        RlGl.Normal3F( - 1.0f, 0.0f, 0.0f);    // Normal Pointing Left
        RlGl.SetTexture(tex.left.Id);
        RlGl.TexCoord2F(0.0f, 1.0f); RlGl.Vertex3F(x - width/2, y - height/2, z - length/2);  // Bottom Left Of The Texture and Quad
        RlGl.TexCoord2F(1.0f, 1.0f); RlGl.Vertex3F(x - width/2, y - height/2, z + length/2);  // Bottom Right Of The Texture and Quad
        RlGl.TexCoord2F(1.0f, 0.0f); RlGl.Vertex3F(x - width/2, y + height/2, z + length/2);  // Top Right Of The Texture and Quad
        RlGl.TexCoord2F(0.0f, 0.0f); RlGl.Vertex3F(x - width/2, y + height/2, z - length/2);  // Top Left Of The Texture and Quad
        
        RlGl.End();

        RlGl.SetTexture(0);
    }
#endregion

#region 2D Element rendering
    /// <summary>
    /// Displays the 3D space to the screen. 
    /// Putting the rendering code in a function makes it easy for screens to render it with a custom shader
    /// </summary>
    public static void Display3DSpace(Color color)
    {
        Graphics.DrawTextureRec(TDSpace.Texture, new(0, 0, (float)TDSpace.Texture.Width, (float)-TDSpace.Texture.Height ), new(0, 0), color);
    }

    /// <summary>
    /// Displays the 3D space to the screen. 
    /// Putting the rendering code in a function makes it easy for screens to render it with a custom shader
    /// </summary>
    public static void Display3DSpacePart(Rectangle part, Color color)
    {
        if (part.Width == -1)
            part.Width = TDSpace.Texture.Width;

        if (part.Height == 1)
            part.Height = -TDSpace.Texture.Height;

        Graphics.DrawTextureRec(TDSpace.Texture, part, new(0, 0), color);
    }

    public static void DrawText(string text, int posX, int posY, int fontSize, Color color)
    {
        Graphics.DrawTextEx(font, text, new(posX, posY), fontSize, 0, color);
    }
#endregion
}