#pragma once

#include <string>

namespace neptune::core
{
    class Config {
    public:
        Config(char* config_file);
        Config(int argc, char **argv);
    
        static Config *getThisConfig();
        double fov = 70.0f;
        std::string instanceDir = ".neptune"; // Directory to the game's instance.
        bool vsync = true;
    private:
        const char *component;
        void __config_parse_value(char *name, char *val);
    };
}