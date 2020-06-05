using System;
using System.Collections.Generic;
using System.Text;

namespace NotDeadSpace
{
    class Alien
    {
        int life;
        int xp;
        List<Item> items = new List<Item>();
        
        public int x;
        public int y;
        public int damage = 1;

        public Alien(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
