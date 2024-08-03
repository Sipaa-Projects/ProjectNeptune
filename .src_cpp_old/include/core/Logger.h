#pragma once

#include <cstdarg>

namespace neptune::core
{
    class Logger {
    public:
        Logger(const char*);
        void Success(char *format, ...);
        void Debug(char *format, ...);
        void Warn(char  *format, ...);
        void Info(char  *format, ...);
        void Fatal(char *format, ...);
        void Error(char *format, ...);
    private:
        const char *component;
    };
}