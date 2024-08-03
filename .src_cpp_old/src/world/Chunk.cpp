#include "world/Block.h"
#include <world/Chunk.h>

using namespace neptune::world;

Chunk::Chunk() {
  // Initialize the chunk with air blocks.
  for (int x = 0; x < 16; x++) {
    for (int y = 0; y < 32; y++) {
      for (int z = 0; z < 16; z++) {
        chunkArr[x][y][z] = new Block();
        chunkArr[x][y][z]->type = AIR;
      }
    }
  }
}