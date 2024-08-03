#include <client/Gui.h>
#include <raylib.h>

using namespace neptune::client;

bool gui::Button(int x, int y, int width, int height, char *text) {
    DrawRectangle(x, y, width, height, { 255, 255, 255, 255 });
    
    int sw = MeasureText(text, 10);
    DrawText(text, x + (width / 2 - sw / 2), y + 5, 10, { });

}