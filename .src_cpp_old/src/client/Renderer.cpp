#include <client/Renderer.h>
#include <rlgl.h>

using namespace neptune::client::renderer;

// CubeTextures
CubeTextures::CubeTextures(Texture all)
{
    this->back = all;
    this->front = all;
    this->top = all;
    this->bottom = all;
    this->left = all;
    this->right = all;
}

CubeTextures::CubeTextures(Texture side, Texture top, Texture bottom)
{
    this->back = side;
    this->front = side;
    this->left = side;
    this->right = side;
    this->top = top;
    this->bottom = bottom;
}

CubeTextures::CubeTextures(Texture front, Texture back, Texture left, Texture right, Texture top, Texture bottom)
{
    this->back = back;
    this->front = front;
    this->left = left;
    this->right = right;
    this->top = top;
    this->bottom = bottom;
}


// Render functions
void neptune::client::renderer::DrawCubeTexture(Vector3 position, Vector3 size, CubeTextures tex)
{
    float x = position.x;
    float y = position.y;
    float z = position.z;
    
    float width = size.x;
    float height = size.y;
    float length = size.z;

    rlBegin(RL_QUADS);

    // Front Face
    rlNormal3f(0.0f, 0.0f, 1.0f);       // Normal Pointing Towards Viewer
    rlSetTexture(tex.front.id);
    rlTexCoord2f(0.0f, 1.0f); rlVertex3f(x - width/2, y - height/2, z + length/2);  // Bottom Left Of The Texture and Quad
    rlTexCoord2f(1.0f, 1.0f); rlVertex3f(x + width/2, y - height/2, z + length/2);  // Bottom Right Of The Texture and Quad
    rlTexCoord2f(1.0f, 0.0f); rlVertex3f(x + width/2, y + height/2, z + length/2);  // Top Right Of The Texture and Quad
    rlTexCoord2f(0.0f, 0.0f); rlVertex3f(x - width/2, y + height/2, z + length/2);  // Top Left Of The Texture and Quad
            
    // Back Face
    rlNormal3f(0.0f, 0.0f, - 1.0f);     // Normal Pointing Away From Viewer
    rlSetTexture(tex.back.id);
    rlTexCoord2f(1.0f, 1.0f); rlVertex3f(x - width/2, y - height/2, z - length/2);  // Bottom Right Of The Texture and Quad
    rlTexCoord2f(1.0f, 0.0f); rlVertex3f(x - width/2, y + height/2, z - length/2);  // Top Right Of The Texture and Quad
    rlTexCoord2f(0.0f, 0.0f); rlVertex3f(x + width/2, y + height/2, z - length/2);  // Top Left Of The Texture and Quad
    rlTexCoord2f(0.0f, 1.0f); rlVertex3f(x + width/2, y - height/2, z - length/2);  // Bottom Left Of The Texture and Quad
            
    // Top Face
    rlNormal3f(0.0f, 1.0f, 0.0f);       // Normal Pointing Up
    rlSetTexture(tex.top.id);
    rlTexCoord2f(0.0f, 0.0f); rlVertex3f(x - width/2, y + height/2, z - length/2);  // Top Left Of The Texture and Quad
    rlTexCoord2f(0.0f, 1.0f); rlVertex3f(x - width/2, y + height/2, z + length/2);  // Bottom Left Of The Texture and Quad
    rlTexCoord2f(1.0f, 1.0f); rlVertex3f(x + width/2, y + height/2, z + length/2);  // Bottom Right Of The Texture and Quad
    rlTexCoord2f(1.0f, 0.0f); rlVertex3f(x + width/2, y + height/2, z - length/2);  // Top Right Of The Texture and Quad

    // Bottom Face
    rlNormal3f(0.0f, - 1.0f, 0.0f);     // Normal Pointing Down
    rlSetTexture(tex.bottom.id);
    rlTexCoord2f(1.0f, 0.0f); rlVertex3f(x - width/2, y - height/2, z - length/2);  // Top Right Of The Texture and Quad
    rlTexCoord2f(0.0f, 0.0f); rlVertex3f(x + width/2, y - height/2, z - length/2);  // Top Left Of The Texture and Quad
    rlTexCoord2f(0.0f, 1.0f); rlVertex3f(x + width/2, y - height/2, z + length/2);  // Bottom Left Of The Texture and Quad
    rlTexCoord2f(1.0f, 1.0f); rlVertex3f(x - width/2, y - height/2, z + length/2);  // Bottom Right Of The Texture and Quad

    // Right face
    rlNormal3f(1.0f, 0.0f, 0.0f);       // Normal Pointing Right
    rlSetTexture(tex.right.id);
    rlTexCoord2f(1.0f, 1.0f); rlVertex3f(x + width/2, y - height/2, z - length/2);  // Bottom Right Of The Texture and Quad
    rlTexCoord2f(1.0f, 0.0f); rlVertex3f(x + width/2, y + height/2, z - length/2);  // Top Right Of The Texture and Quad
    rlTexCoord2f(0.0f, 0.0f); rlVertex3f(x + width/2, y + height/2, z + length/2);  // Top Left Of The Texture and Quad
    rlTexCoord2f(0.0f, 1.0f); rlVertex3f(x + width/2, y - height/2, z + length/2);  // Bottom Left Of The Texture and Quad
            
    // Left Face
    rlNormal3f( - 1.0f, 0.0f, 0.0f);    // Normal Pointing Left
    rlSetTexture(tex.left.id);
    rlTexCoord2f(0.0f, 1.0f); rlVertex3f(x - width/2, y - height/2, z - length/2);  // Bottom Left Of The Texture and Quad
    rlTexCoord2f(1.0f, 1.0f); rlVertex3f(x - width/2, y - height/2, z + length/2);  // Bottom Right Of The Texture and Quad
    rlTexCoord2f(1.0f, 0.0f); rlVertex3f(x - width/2, y + height/2, z + length/2);  // Top Right Of The Texture and Quad
    rlTexCoord2f(0.0f, 0.0f); rlVertex3f(x - width/2, y + height/2, z - length/2);  // Top Left Of The Texture and Quad
    
    rlEnd();

    rlSetTexture(0);
}