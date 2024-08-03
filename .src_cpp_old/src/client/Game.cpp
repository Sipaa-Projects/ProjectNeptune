#include <client/InputManager.h>
#include <client/screen/Screen.h>
#include <client/screen/MainScreen.h>
#include <client/Game.h>
#include <client/Renderer.h>
#include <core/Logger.h>
#include <cstddef>
#include <cstdio>
#include <cstring>
#include <libgen.h>
#include <raylib.h>
#include <rlgl.h>
#include <unistd.h>

using namespace neptune::client;

Game::Game(int argc, char **argv) {
  this->argc = argc;
  this->argv = argv;
  this->logger = new core::Logger("Client/Main thread");

  const int bufsize = 2048;
  char buf[bufsize];
  readlink("/proc/self/exe", buf, bufsize); // Get the app's directory
  this->execDir = dirname(buf);
}

void __init3DSpace(RenderTexture *tex)
{
  *tex = LoadRenderTexture(GetRenderWidth(), GetRenderHeight());
}

void Game::OnResize()
{
  if (tdSpace.id != 0)
    UnloadRenderTexture(tdSpace);
  
  __init3DSpace(&tdSpace);
}

int Game::start() {
  // Initialize raylib window
  SetConfigFlags(FLAG_WINDOW_RESIZABLE);
  InitWindow(800, 600, "Project Neptune");

  SetExitKey(KEY_NULL);
  SetTargetFPS(GetMonitorRefreshRate(0));

  __init3DSpace(&tdSpace);

  Texture2D gbSide = LoadTexture("res/textures/grass_block_side.png");
  Texture2D gbTop = LoadTexture("res/textures/grass_block_top.png");
  renderer::CubeTextures tex((Texture)gbSide, gbTop, gbTop);

  Camera3D cam = { };
  cam.fovy = 70.0f;
  cam.position = { 5.0f, 5.0f, 5.0f };
  cam.target = { 0.0f, 0.0f, 0.0f };
  cam.up = { 0.0f, 1.0f, 0.0f };
  cam.projection = CAMERA_PERSPECTIVE;

  char fpsbuf[6 + 4];

  screen::MainScreen *s = new screen::MainScreen();
  currentScreen = (screen::Screen *)s;

  while (!WindowShouldClose()) {
    InputManager::Update(this);

    if (currentScreen == NULL || currentScreen->usesTransparentBackdrop == true) {
      if (currentScreen == NULL)
        UpdateCamera(&cam, CAMERA_FREE);

      BeginTextureMode(tdSpace);
        ClearBackground(BLACK);
        BeginMode3D(cam);
          renderer::DrawCubeTexture({ 0.0f, 0.0f, 0.0f }, { 1.0f, 1.0f, 1.0f }, tex);
        EndMode3D();
      EndTextureMode();
    }

    BeginDrawing();

      ClearBackground(WHITE);

      if (currentScreen != NULL) {
        s->RenderBackdrop(tdSpace);
        s->InvokeUpdate();

        if (IsKeyPressed(KEY_ESCAPE)) {
          currentScreen = NULL;
          DisableCursor();
        }
      } else {
        DrawTextureRec(tdSpace.texture, (Rectangle){ 0, 0, (float)tdSpace.texture.width, (float)-tdSpace.texture.height }, (Vector2){ 0, 0 }, WHITE);
      }

    EndDrawing();
  }

  return 0;
}