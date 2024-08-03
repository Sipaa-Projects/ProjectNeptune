# Compiler and Linker
CC = gcc
CXX = g++
CFLAGS = -I/usr/local/include -Iinclude -g
LDFLAGS = -L/usr/local/lib -lraylib

# Directories
SRC_DIR = src
OBJ_DIR = obj
BIN_DIR = bin
RES_DIR = res

# Files
SOURCES_C = $(shell find $(SRC_DIR) -name '*.c')
OBJECTS_C = $(patsubst $(SRC_DIR)/%.c, $(OBJ_DIR)/%.c.o, $(SOURCES_C))

SOURCES_CPP = $(shell find $(SRC_DIR) -name '*.cpp')
OBJECTS_CPP = $(patsubst $(SRC_DIR)/%.cpp, $(OBJ_DIR)/%.o, $(SOURCES_CPP))

TARGET = $(BIN_DIR)/neptune

# Default target
all: $(TARGET) copy-resources

# Build target
$(TARGET): $(OBJECTS_CPP) $(OBJECTS_C)
	@mkdir -p $(BIN_DIR)
	$(CXX) $(OBJECTS_CPP) $(OBJECTS_C) -o $@ $(LDFLAGS)

# Compile C++ source files
$(OBJ_DIR)/%.o: $(SRC_DIR)/%.cpp
	@mkdir -p $(@D)
	$(CXX) $(CFLAGS) -c $< -o $@

# Compile C source files
$(OBJ_DIR)/%.c.o: $(SRC_DIR)/%.c
	@mkdir -p $(@D)
	$(CC) $(CFLAGS) -c $< -o $@

# Copy resources
copy-resources:
	@mkdir -p $(BIN_DIR)
	@cp -r $(RES_DIR) $(BIN_DIR)

# Clean up
clean:
	rm -rf $(OBJ_DIR) $(BIN_DIR)

.PHONY: all copy-resources clean print-vars
