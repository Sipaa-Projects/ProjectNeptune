#include <client/Game.h>

using namespace neptune::client;

int main(int argc, char **argv) {
  Game *launcher = new Game(argc, argv);

  return launcher->start();
}