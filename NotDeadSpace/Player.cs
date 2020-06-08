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
        public int damage = 1;

        int xp = 0;
        int level = 1;

        public int Level
        {
            get
            {
                return level;
            }
        }

        public int XP
        {
            get
            {
                return xp;
            }
        }

        public bool IsHere(int mapX, int mapY)
        {
            return x == mapX && y == mapY;
        }

        public void AddXP(int amount)
        {
            xp += amount;

            if (xp >= 100)
            {
                level++;
                xp -= 100;

                maxLife++;
                life = maxLife;

                if (level % 3 == 0)
                {
                    damage++;
                }
            }
        }
    }
}
