#pragma once


namespace neptune::world {
    enum BlockType {
        AIR,
        GRASS,
        DIRT,
        STONE
    };

    class Block {
    public:
        BlockType type;
    };
}