#include <core/Logger.h>
#include <cstdarg>
#include <cstdio>

using namespace neptune::core;

Logger::Logger(const char *component) {
    this->component = component;
}

void Logger::Info(char *format, ...) {
    printf("%s: INFO: ", this->component);

    va_list lst;
    va_start(lst, format);
    vprintf(format, lst);
    va_end(lst);

    printf("\n");
}

void Logger::Success(char *format, ...) {
    printf("%s: SUCCESS: ", this->component);

    va_list lst;
    va_start(lst, format);
    vprintf(format, lst);
    va_end(lst);

    printf("\n");
}

void Logger::Debug(char *format, ...) {
    printf("%s: DEBUG: ", this->component);

    va_list lst;
    va_start(lst, format);
    vprintf(format, lst);
    va_end(lst);

    printf("\n");
}

void Logger::Warn(char *format, ...) {
    printf("%s: WARN: ", this->component);

    va_list lst;
    va_start(lst, format);
    vprintf(format, lst);
    va_end(lst);

    printf("\n");
}

void Logger::Error(char *format, ...) {
    printf("%s: ERROR: ", this->component);

    va_list lst;
    va_start(lst, format);
    vprintf(format, lst);
    va_end(lst);

    printf("\n");
}

void Logger::Fatal(char *format, ...) {
    printf("%s: FATAL: ", this->component);

    va_list lst;
    va_start(lst, format);
    vprintf(format, lst);
    va_end(lst);

    printf("\n");
}