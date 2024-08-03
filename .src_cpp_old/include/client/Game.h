#pragma once

#include <client/screen/Screen.h>

#include <core/ModLoader.h>
#include <core/Logger.h>
#include <core/Config.h>
#include <raylib.h>

namespace neptune::client
{
    const int GAME_VERSION_MAJOR = 1;
    const int GAME_VERSION_MINOR = 0;
    const int GAME_VERSION_IS_ALPHA = 1;
    const int GAME_VERSION_IS_BETA = 0;
    const int GAME_VERSION_IS_PRERELEASE = 0;

    class Game {
    public:
        static Game *thisGame;
        Game(int argc, char **argv);
        int start();

        void OnResize();

        screen::Screen *currentScreen;
    private:
        int __width = 800;
        int __height = 600;

        int argc;
        char **argv;
        char *execDir;
        RenderTexture tdSpace;
        core::Config *conf;
        core::Logger *logger;
        core::ModLoader *modloader;
        void __window_init();
    };
}