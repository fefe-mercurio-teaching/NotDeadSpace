using System;
using System.Collections.Generic;
using System.Text;

namespace NotDeadSpace
{
    class Player
    {
        public int x;
        public int y;
        public int life = 10;
        public int maxLife = 10;

        public bool IsHere(int mapX, int mapY)
        {
            return x == mapX && y == mapY;
        }
    }
}
