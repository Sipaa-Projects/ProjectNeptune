#pragma once

#include <client/screen/Screen.h>

namespace neptune::client::screen {
    class MainScreen : public Screen {
    public:
        MainScreen();
    private:
        void __update();
    };
}