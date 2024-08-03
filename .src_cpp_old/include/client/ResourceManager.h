#pragma once

#include <raylib.h>
#include <string>

namespace neptune::client {
    class ResourceManager {
    public:
        static Shader LoadShader  (char *path);
        static Texture LoadTexture(char *path);
        static Model LoadModel    (char *path);
        static Music LoadAudio    (char *path);
        static std::string LoadString(char *path);
    private:
        static bool __initialized;
        static void __init();
    };
}