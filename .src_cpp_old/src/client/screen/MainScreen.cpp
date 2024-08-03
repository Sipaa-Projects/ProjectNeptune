#include <client/Gui.h>
#include <client/screen/MainScreen.h>
#include <cstdio>

using namespace neptune::client::screen;

void MainScreen::__update() {
    printf("Hey!");
    gui::Button(10, 10, 100, 100, "Hey!");
}

MainScreen::MainScreen() : scre
{
    update = [this]() { 
        printf("Inside lambda before calling __update\n");
        this->__update(); 
        printf("Inside lambda after calling __update\n");
    };
}