#include <client/ResourceManager.h>
#include <cstdio>
#include <raylib.h>
#include <string>

using namespace neptune::client;

Shader ResourceManager::LoadShader(char *path) {
    char buf[2048];
    snprintf(buf, 2048, "%s%d", path, 330);

    return ::LoadShader(NULL, buf);
}