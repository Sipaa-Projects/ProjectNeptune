#include "client/ResourceManager.h"
#include <client/screen/Screen.h>
#include <raylib.h>
#include <rlgl.h>

using namespace neptune::client::screen;

void __stub_func() {}

Screen::Screen() {
  usesTransparentBackdrop = true;
  update = __stub_func;

  // The resource manager will automatically
  // append the GLSL version to the end of
  // the path, explaining why the shaders
  // paths ends in .fs100, .fs330.
  blurShader = ResourceManager::LoadShader("res/shaders/blur.fs");
  int ba = GetShaderLocation(blurShader, "r");
  int xs = GetShaderLocation(blurShader, "xs");
  int ys = GetShaderLocation(blurShader, "ys");
  int txrV = GetShaderLocation(blurShader, "txr");

  float blurAmount = 12.0f;
  float width = (float)GetRenderWidth();
  float height = (float)GetRenderHeight();
  int txr = 0;

  SetShaderValue(blurShader, ba, (void *)&blurAmount, SHADER_UNIFORM_FLOAT);
  SetShaderValue(blurShader, xs, (void *)&width, SHADER_UNIFORM_FLOAT);
  SetShaderValue(blurShader, ys, (void *)&height, SHADER_UNIFORM_FLOAT);
  SetShaderValue(blurShader, txrV, (void *)&txr, SHADER_UNIFORM_INT);
}

void Screen::InvokeUpdate() {
  if (update) {
    printf("Calling update function...\n");
    update();
  } else {
    printf("Update function is not set.\n");
  }
}

void Screen::RenderBackdrop(RenderTexture target) {
  if (usesTransparentBackdrop == true) {
    BeginShaderMode(blurShader);
    DrawTextureRec(target.texture,
                   (Rectangle){0, 0, (float)target.texture.width,
                               (float)-target.texture.height},
                   (Vector2){0, 0}, WHITE);
    EndShaderMode();
  }
}