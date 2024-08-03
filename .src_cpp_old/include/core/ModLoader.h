#pragma once

#include <list>
#include <string>

namespace neptune::core {


    /*
        The mod loader only works with Linux and macOS for now. 
        Porting the mod loader to Windows' dynamic libraries is a TODO.
     */
    class ModLoader {
    public:
        ModLoader(); // ModLoader loads all the mods in a specific directory
        void InvokeFunction(std::string functionName, void *param);
    private:
        std::list<void*> *mods_libs; // HINSTANCE on Windows, void* on Linux, macOS
    };
}