#pragma once

#include <functional>
#include <raylib.h>

namespace neptune::client::screen {
    class Screen {
    public:
        /*
         * This constructor sets render() and update()
         * to a stub function, preventing the program from
         * crashing.
         */
        Screen();
        void RenderBackdrop(RenderTexture threeDSpace);
        void InvokeUpdate();

        bool usesTransparentBackdrop = true;
    private:
        Shader blurShader;

    protected:
        std::function<void()> update;
    };
}