// Manage input

#pragma once

#include <client/Game.h>

namespace neptune::client {
    class InputManager {
    public:
        static void Update(Game *game);
        static void RegisterEvent(int key, void(*callback)());
    };
}