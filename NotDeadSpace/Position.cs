using System;
using System.Collections.Generic;
using System.Text;

namespace NotDeadSpace
{
    struct Position
    {
        public static Position operator +(Position a, Position b)
        {
            Position result;
            result.x = a.x + b.x;
            result.y = a.y + b.y;

            return result;
        }

        public static Position operator -(Position a, Position b)
        {
            Position result;
            result.x = a.x - b.x;
            result.y = a.y - b.y;

            return result;
        }

        public int x;
        public int y;

        public bool IsHere(int x, int y)
        {
            return this.x == x && this.y == y;
        }

        public bool IsHere(Position otherPosition)
        {
            return x == otherPosition.x && y == otherPosition.y;
        }
    }
}
