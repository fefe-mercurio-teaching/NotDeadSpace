using System;
using System.Collections.Generic;
using System.Text;

namespace NotDeadSpace
{
    struct Position
    {
        public int x;
        public int y;

        public bool IsHere(int x, int y)
        {
            return this.x == x && this.y == y;
        }
    }
}
