#pragma once

#include <world/Block.h>

namespace neptune::world {
    class Chunk {
    public:
        Chunk();
        Block *chunkArr[16][32][16]; // X Y Z (width height depth)
    };
}