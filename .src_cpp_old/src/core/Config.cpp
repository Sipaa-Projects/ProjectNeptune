#include <algorithm>
#include <core/Config.h>
#include <iostream>
#include <string>
#include <cstring>

using namespace neptune::core;

Config *gThisConfig;

bool __strtobool(char *str)
{
    std::string lcstr = str;
    std::transform(lcstr.begin(), lcstr.end(), lcstr.begin(), ::tolower);

    if (!strcmp(lcstr.c_str(), "true"))
        return true;

    return false;
}

void Config::__config_parse_value(char *name, char *val)
{
    if (!strcmp(name, "fov")) {
        fov = strtod(val, NULL); 
    }
    else if (!strcmp(name, "vsync")) {
        vsync = __strtobool(val);
    }
    else if (!strcmp(name, "gameDir")) {
        instanceDir = val;
    }
}

Config::Config(int argc, char **argv) {
    gThisConfig = this;
    
    if (argc == 1)
        return;

    int i;
    while (i < argc) {
        std::string *carg = new std::string(argv[i]);

        if (carg->rfind("--", 0) == 0) {
            carg->erase(0, 2);

            char *token = strtok((char*)carg->c_str(), "=");

            bool name_passed = false;
            std::string name;

            // Keep printing tokens while one of the
            // delimiters present in str[].
            while (token != NULL)
            {
                printf("%s\n", token);
                token = strtok(NULL, "-");

                if (!name_passed)
                {
                    name = token;
                    name_passed = true;
                }
                else {
                    __config_parse_value((char*)name.c_str(), token);
                }
            }
 
        }

        i++;
    }
}

Config *Config::getThisConfig() {
    return gThisConfig;
}