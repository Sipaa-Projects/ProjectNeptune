#pragma once

#include <raylib.h>

// The renderer is an abstraction above RLGL, used to do certain things that Raylib can't by default.
namespace neptune::client::renderer {
    class CubeTextures {
    public:
        CubeTextures(Texture all);
        CubeTextures(Texture sides, Texture top, Texture bottom);
        CubeTextures(Texture front, Texture back, Texture left, Texture right, Texture top, Texture bottom);

        Texture left;
        Texture right;
        Texture front;
        Texture back;
        Texture top;
        Texture bottom;
    };
    
    void DrawCubeTexture(Vector3 position, Vector3 size, CubeTextures tex);
}