#include <client/InputManager.h>
#include <raylib.h>

using namespace neptune::client;

void InputManager::Update(Game *game)
{
    if (IsWindowResized())
    {
        game->OnResize();
    }

    if (IsKeyPressed(KEY_F11))
    {
        ToggleFullscreen();
        game->OnResize();
    }
}