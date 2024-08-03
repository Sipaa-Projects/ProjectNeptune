#pragma once

#include <algorithm>
#include <cerrno>
#include <core/Config.h>
#include <core/Logger.h>
#include <core/ModLoader.h>
#include <cstddef>
#include <dlfcn.h>
#include <filesystem>

using namespace neptune::core;
namespace fs = std::filesystem;

Logger *__ml_logger;

ModLoader::ModLoader() {
  __ml_logger = new Logger("ModLoader");

  fs::path p1(Config::getThisConfig()->instanceDir);
  fs::path p2("mods");
  fs::path modsDir = p1 / p2;

  __ml_logger->Info("Mods directory: %s", modsDir.c_str());

  this->mods_libs = new std::list<void *>();

  for (const auto &entry : fs::directory_iterator(modsDir)) {
    void *mod = dlopen(entry.path().c_str(), RTLD_LAZY);

    if (!mod) {
      __ml_logger->Error("%s", dlerror());
      continue;
    }

    char **name = (char **)dlsym(mod, "MOD_NAME");
    char **id = (char **)dlsym(mod, "MOD_ID");
    char **version = (char **)dlsym(mod, "MOD_VERSION");
    char **author = (char **)dlsym(mod, "MOD_AUTHOR");

    if (name == NULL || version == NULL || author == NULL || id == NULL) {
      __ml_logger->Error("Mod doesn't have one of these constants: MOD_NAME, "
                         "MOD_VERSION, MOD_ID, MOD_AUTHOR.");
      continue;
    }

    __ml_logger->Info("Loaded %s version %s by %s (ID: %s) at address %p",
                      *name, *version, *author, *id);
    mods_libs->push_front(mod);
  }
}

void ModLoader::InvokeFunction(std::string name, void *param) {
  std::for_each(mods_libs->begin(), mods_libs->end(), [&name, &param](void *mod) {
    void (*function)(void *param) = (void (*)(void *))dlsym(mod, name.c_str());
    if (function)
      function(param);
  });
}